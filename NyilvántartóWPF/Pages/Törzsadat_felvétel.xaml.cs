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
     public partial class Törzsadat_felvétel : Page
     {
          string kat = "";
          string focsop = "";
          string kisz = "";

          string[] kiszerelesek = new string[14] { "db", "csomag", "1l", "3l", "5l", "10l", "20l", "25l", "t", "zsák", "BB/500", "BB/600", "BB/700", "BB/1000", };

          string[] focsoportok = new string[4] { "lombtrágya","műtrágya", "növényvédőszer", "vetőmag" };

          string[] kategoriak = new string[4] { " ","I. Kategória", "II. Kategória", "III. Kategória" };

          ObservableCollection<Törzsadatlista> törzsadatok = new ObservableCollection<Törzsadatlista>();

          NyilvantartoDAODBImpl db_implementation = new NyilvantartoDAODBImpl();
          public Törzsadat_felvétel()
          {
               InitializeComponent();
          }
          
          private void kiszereles_combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
          {
               int index = kiszereles_combobox.SelectedIndex;
               kisz = kiszerelesek[index];
          }

          private void focsoport_combobox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
          {
               int index = focsoport_combobox.SelectedIndex;
               focsop = focsoportok[index];
          }

          private void kat_combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
          {
               int index = kat_combobox.SelectedIndex;
               kat = kategoriak[index];
          }
          private void törzsadat_felvetel_button_Click(object sender, RoutedEventArgs e)
          {
               bool sikerult_e = db_implementation.insertTörzsadat(anyagnev_textbox.Text, kisz, focsop, alcsoport_textbox.Text, gyarto_textbox.Text, kat, un_szam_textbox.Text);
               if (sikerult_e == true)
               {
                    anyagnev_textbox.Text = "";
                    alcsoport_textbox.Text = "";
                    gyarto_textbox.Text = "";
                    un_szam_textbox.Text = "";
                    UpdateLayout();
                    MessageBox.Show("Sikeres Felvétel!");
               }
               else
               {
                    MessageBox.Show("Sikertelen felvétel, hívj fel!");
               }
          }
          
     }
}
