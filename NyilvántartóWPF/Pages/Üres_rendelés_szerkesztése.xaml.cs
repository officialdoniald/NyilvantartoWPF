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

     public partial class Üres_rendelés_szerkesztése : Page
     {
          #region attribútumok

          string anyagnév = "";

          string beszállító = "";

          string mennyiség = "";
          
          NyilvantartoDAODBImpl db_implementation = new NyilvantartoDAODBImpl();

          Ures_rendeles szerk_ures_rendeles = new Ures_rendeles();

          Törzsadatlista szerk_törzsadat = new Törzsadatlista();

          Beszállítók szerk_beszállító = new Beszállítók();

          Ures_rendeles eredeti_ures_rendeles = new Ures_rendeles();

          Törzsadatlista eredeti_törzsadat = new Törzsadatlista();

          Beszállítók eredeti_beszállító = new Beszállítók();

          ObservableCollection<Ures_rendeles> ures_rendelesek = new ObservableCollection<Ures_rendeles>();

          ObservableCollection<Törzsadatlista> törzsadatok = new ObservableCollection<Törzsadatlista>();

          ObservableCollection<Beszállítók> beszállítók = new ObservableCollection<Beszállítók>();

          #endregion

          public Üres_rendelés_szerkesztése()
          {
               InitializeComponent();
               ures_rendelesek = db_implementation.getUres_rendeles(0,0);
               var rendelesek = ures_rendelesek.OrderBy(a => a.beszállító);
               szerk_ures_rendelesek_listazasa_listbox.ItemsSource = rendelesek;
          }

          private void szerk_törzsadatok_combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
          {
               törzsadatok = db_implementation.getTörzsadlista();
               var törzsadatok2 = törzsadatok.OrderBy(a => a.anyagnév);
               int index = 0;
               foreach (var item in törzsadatok2)
               {
                    if (index == szerk_törzsadatok_combobox.SelectedIndex)
                    {
                         anyagnév = item.anyagnév;
                    }
                    index++;
               }
          }

          private void szerk_beszállítók_vegleg_combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
          {
               beszállítók = db_implementation.getBeszállítók();
               var beszállítók2 = beszállítók.OrderBy(a => a.név);
               int index = 0;
               foreach (var item in beszállítók2)
               {
                    if (index == szerk_beszállítók_vegleg_combobox.SelectedIndex)
                    {
                         beszállító = item.név;
                    }
                    index++;
               }
          }
          
          private void szerk_ures_rendelesek_listazasa_listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
          {
               ures_rendelesek = db_implementation.getUres_rendeles(0,0);
               var rendelesek = ures_rendelesek.OrderBy(a => a.beszállító);
               int index = 0;
               foreach (var item in rendelesek)
               {
                    if (index == szerk_ures_rendelesek_listazasa_listbox.SelectedIndex)
                    {
                         eredeti_ures_rendeles.anyagnév = item.anyagnév;
                         eredeti_ures_rendeles.beszállító = item.beszállító;
                         beszállító = item.beszállító;
                         anyagnév = item.anyagnév;
                         eredeti_ures_rendeles.dátum = item.dátum;
                         eredeti_ures_rendeles.eredeti_mennyiség = item.eredeti_mennyiség;
                         eredeti_ures_rendeles.mennyiség = item.mennyiség;
                    }
                    index++;
               }
          }

          private void szerk_ures_rendelesek_listazasa_listbox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
          {
               törzsadatok = db_implementation.getTörzsadlista();
               var törzsadatok2 = törzsadatok.OrderBy(a => a.anyagnév);
               int index = 0;
               foreach (var item in törzsadatok2)
               {
                    if (anyagnév == item.anyagnév)
                    {
                         szerk_törzsadatok_combobox.ItemsSource = törzsadatok2;
                         szerk_törzsadatok_combobox.SelectedIndex = index;
                         break;
                    }
                    index++;
               }
               beszállítók = db_implementation.getBeszállítók();
               var beszállítók2 = beszállítók.OrderBy(a => a.név);
               index = 0;
               foreach (var item in beszállítók2)
               {
                    if (beszállító == item.név)
                    {
                         szerk_beszállítók_vegleg_combobox.ItemsSource = beszállítók2;
                         szerk_beszállítók_vegleg_combobox.SelectedIndex = index;
                         break;
                    }
                    index++;
               }
               szerk_mennyi_textbox.Text = eredeti_ures_rendeles.eredeti_mennyiség;
          }

          private void del_rendelés_button_Click(object sender, RoutedEventArgs e)
          {
               bool sikerult_e = db_implementation.deleteUres_rendeles(eredeti_ures_rendeles);
               if (sikerult_e == true)
               {
                    ures_rendelesek = db_implementation.getUres_rendeles(0,0);
                    var rendelesek = ures_rendelesek.OrderBy(a => a.beszállító);
                    szerk_ures_rendelesek_listazasa_listbox.ItemsSource = rendelesek;
                    MessageBox.Show("Sikerült a módosítás");
               }
               else
               {
                    MessageBox.Show("Nem sikerült a módoítás, hívj fel!");
               }
          }

          private void szerk_rendelés_button_Click(object sender, RoutedEventArgs e)
          {
               szerk_ures_rendeles = new Ures_rendeles();
               szerk_ures_rendeles.anyagnév = anyagnév;
               szerk_ures_rendeles.beszállító = beszállító;
               mennyiség = szerk_mennyi_textbox.Text;
               szerk_ures_rendeles.mennyiség = eredeti_ures_rendeles.mennyiség;
               szerk_ures_rendeles.dátum = eredeti_ures_rendeles.dátum;
               szerk_ures_rendeles.eredeti_mennyiség = mennyiség;

               bool sikerult_e = db_implementation.updateUres_rendeles(eredeti_ures_rendeles,szerk_ures_rendeles);
               if (sikerult_e == true)
               {
                    ures_rendelesek = db_implementation.getUres_rendeles(0,0);
                    var rendelesek = ures_rendelesek.OrderBy(a => a.beszállító);
                    szerk_ures_rendelesek_listazasa_listbox.ItemsSource = rendelesek;
                    MessageBox.Show("Sikerült a módosítás");
               }
               else
               {
                    MessageBox.Show("Nem sikerült a módoítás, hívj fel!");
               }
          }

     }
}
