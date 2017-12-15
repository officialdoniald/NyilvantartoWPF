using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NyilvántartóWPF.Pages
{
     public partial class Törzsadat_listázása : Page
     {
          string kat = "";
          string focsop = "";
          string kisz = "";
          
          string[] kiszerelesek = new string[14] { "db", "csomag", "1l", "3l", "5l", "10l", "20l", "25l", "t", "zsák", "BB/500", "BB/600", "BB/700", "BB/1000", };

          string[] focsoportok = new string[4] { "lombtrágya","műtrágya", "növényvédőszer", "vetőmag" };

          string[] kategoriak = new string[4] { " ","I. Kategória", "II. Kategória", "III. Kategória" };

          NyilvantartoDAODBImpl db_implementation = new NyilvantartoDAODBImpl();

          ObservableCollection<Törzsadatlista> törzsadatok = new ObservableCollection<Törzsadatlista>();

          Törzsadatlista szerkeszteni_kívánt_törzsadat = new Törzsadatlista();

          public Törzsadat_listázása()
          {
               InitializeComponent();
               törzsadatok = db_implementation.getTörzsadlista();
               var valami = törzsadatok.OrderBy(a => a.anyagnév);
               törzsadatok_listazasa_listbox.ItemsSource = valami;
          }

          private void szerk_kat_combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
          {
               int index = szerk_kat_combobox.SelectedIndex;
               kat = kategoriak[index];
          }

          private void szerk_focsoport_combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
          {
               int index = szerk_focsoport_combobox.SelectedIndex;
               focsop = focsoportok[index];
          }

          private void szerk_kiszereles_combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
          {
               int index = szerk_kiszereles_combobox.SelectedIndex;
               kisz = kiszerelesek[index];
          }
          private void törzsadatok_listazasa_listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
          {
               int index = 0;
               int selecteditem = törzsadatok_listazasa_listbox.SelectedIndex;
               var valami = törzsadatok.OrderBy(a => a.anyagnév);
               foreach (var item in valami)
               {
                    if (index == selecteditem)
                    {
                         szerkeszteni_kívánt_törzsadat = item;
                    }
                    index++;
               }
          }

          private void törzsadatok_listazasa_listbox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
          {
               szerk_anyagnev_textbox.Text = szerkeszteni_kívánt_törzsadat.anyagnév;
               int selectedindex = 0;
               for (int i = 0; i < kiszerelesek.Length; i++)
               {
                    if (szerkeszteni_kívánt_törzsadat.kiszerelés == kiszerelesek[i])
                    {
                         selectedindex = i;
                         break;
                    }
               }
               szerk_kiszereles_combobox.SelectedIndex = selectedindex;
               selectedindex = 0;
               for (int i = 0; i < focsoportok.Length; i++)
               {
                    if (szerkeszteni_kívánt_törzsadat.főcsoport == focsoportok[i])
                    {
                         selectedindex = i;
                         break;
                    }
               }
               szerk_focsoport_combobox.SelectedIndex = selectedindex;
               selectedindex = 0;
               szerk_alcsoport_textbox.Text = szerkeszteni_kívánt_törzsadat.alcsoport;
               szerk_gyarto_textbox.Text = szerkeszteni_kívánt_törzsadat.gyártó;
               for (int i = 0; i < kategoriak.Length; i++)
               {
                    if (szerkeszteni_kívánt_törzsadat.kategória == kategoriak[i])
                    {
                         selectedindex = i;
                         break;
                    }
               }
               szerk_kat_combobox.SelectedIndex = selectedindex;
               szerk_un_szam_textbox.Text = szerkeszteni_kívánt_törzsadat.un_szám;
          }

          private void szerk_törzsadat_felvetel_button_Click(object sender, RoutedEventArgs e)
          {
               Törzsadatlista szerkesztett_törzsadat = new Törzsadatlista();
               szerkesztett_törzsadat.alcsoport = szerk_alcsoport_textbox.Text;
               szerkesztett_törzsadat.anyagnév = szerk_anyagnev_textbox.Text;
               szerkesztett_törzsadat.főcsoport = focsop;
               szerkesztett_törzsadat.gyártó = szerk_gyarto_textbox.Text;
               szerkesztett_törzsadat.kategória = kat;
               szerkesztett_törzsadat.kiszerelés = kisz;
               szerkesztett_törzsadat.un_szám = szerk_un_szam_textbox.Text;
               bool sikerult_e = db_implementation.updateTörzsadat(szerkeszteni_kívánt_törzsadat, szerkesztett_törzsadat);
               if (sikerult_e == true)
               {
                    törzsadatok_listazasa_listbox.ItemsSource = null;
                    törzsadatok = db_implementation.getTörzsadlista();
                    var valami = törzsadatok.OrderBy(a => a.anyagnév);
                    törzsadatok_listazasa_listbox.ItemsSource = valami;
                    this.UpdateLayout();
                    MessageBox.Show("Sikeres módosítás!");
               }
               else
               {
                    MessageBox.Show("Nem sikerült a módosítás, hívj fel!");
               }
          }

          private void del_törzsadat_felvetel_button_Click(object sender, RoutedEventArgs e)
          {
               bool sikerult_e = db_implementation.deleteTörzsadat(szerkeszteni_kívánt_törzsadat);
               if (sikerult_e == true)
               {
                    törzsadatok_listazasa_listbox.ItemsSource = null;
                    törzsadatok = db_implementation.getTörzsadlista();
                    var valami = törzsadatok.OrderBy(a => a.anyagnév);
                    törzsadatok_listazasa_listbox.ItemsSource = valami;
                    this.UpdateLayout();
                    MessageBox.Show("Sikeres törlés!");
               }
               else
               {
                    MessageBox.Show("Nem sikerült a törlés, hívj fel!");
               }
          }

     }
}
