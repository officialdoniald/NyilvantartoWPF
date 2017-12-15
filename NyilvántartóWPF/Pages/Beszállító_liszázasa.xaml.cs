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
     public partial class Beszállító_liszázasa : Page
     {
          NyilvantartoDAODBImpl db_implementation = new NyilvantartoDAODBImpl();

          ObservableCollection<Beszállítók> beszallitok = new ObservableCollection<Beszállítók>();

          Beszállítók szerkeszteni_kívant_beszallito = new Beszállítók();

          public Beszállító_liszázasa()
          {
               InitializeComponent();
               beszallitok = db_implementation.getBeszállítók();
               var valami = beszallitok.OrderBy(a => a.név);
               beszallitok_listazasa_listbox.ItemsSource = valami;
          }

          private void beszallitok_listazasa_listbox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
          {
               Beszállítók_szerkesztése(szerkeszteni_kívant_beszallito);
          }

          private void beszallitok_listazasa_listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
          {
               int index = 0;
               int selecteditem = beszallitok_listazasa_listbox.SelectedIndex;
               var valami = beszallitok.OrderBy(a => a.név);
               foreach (var item in valami)
               {
                    if (index == selecteditem)
                    {
                         szerkeszteni_kívant_beszallito = item;
                         break;
                    }
                    index++;
               }
          }

          private void update_button_Click(object sender, RoutedEventArgs e)
          {
               Beszállítók szerkesztett_beszállító = new Beszállítók();
               szerkesztett_beszállító.név = szerk_ceg_nev_textbox.Text;
               szerkesztett_beszállító.cím = szerk_ceg_cime_textbox.Text;
               szerkesztett_beszállító.adószám = szerk_ceg_adoszama_textbox.Text;
               szerkesztett_beszállító.bankszámlaszám = szerk_ceg_bankszámlaszama_textbox.Text;
               szerkesztett_beszállító.kapcsolattartó_neve = szerk_kapcsolattarto_nev_textbox.Text;
               szerkesztett_beszállító.kapcsolattartó_telefonszáma = szerk_kapcsolattarto_telszam_textbox.Text;
               szerkesztett_beszállító.kapcsolattartó_email_címe = szerk_kapcsolattarto_email_textbox.Text;
               bool sikerult_e = db_implementation.updateBeszállító(szerkeszteni_kívant_beszallito, szerkesztett_beszállító);
               if (sikerult_e == true)
               {
                    beszallitok_listazasa_listbox.ItemsSource = null;
                    beszallitok = db_implementation.getBeszállítók();
                    var valami = beszallitok.OrderBy(a => a.név);
                    beszallitok_listazasa_listbox.ItemsSource = valami;
                    this.UpdateLayout();
                    MessageBox.Show("Sikeres Felvétel!");
               }
               else
               {
                    MessageBox.Show("Sikertelen felvétel, hívj fel!");
               }
          }

          private void delete_button_Click(object sender, RoutedEventArgs e)
          {
               bool sikerult_e = db_implementation.deleteBeszállító(szerkeszteni_kívant_beszallito);
               if (sikerult_e == true)
               {
                    beszallitok_listazasa_listbox.ItemsSource = null;
                    beszallitok = db_implementation.getBeszállítók();
                    var valami = beszallitok.OrderBy(a => a.név);
                    beszallitok_listazasa_listbox.ItemsSource = valami;
                    this.UpdateLayout();
                    MessageBox.Show("Sikeres törlés!");
               }
               else
               {
                    MessageBox.Show("Nem sikerült a törlés, hívj fel!");
               }
          }

          public void Beszállítók_szerkesztése(Beszállítók szerkeszteni_kívant_beszallito)
          {
               this.szerkeszteni_kívant_beszallito = szerkeszteni_kívant_beszallito;
               szerk_ceg_nev_textbox.Text = szerkeszteni_kívant_beszallito.név;
               szerk_ceg_cime_textbox.Text = szerkeszteni_kívant_beszallito.cím;
               szerk_ceg_adoszama_textbox.Text = szerkeszteni_kívant_beszallito.adószám;
               szerk_ceg_bankszámlaszama_textbox.Text = szerkeszteni_kívant_beszallito.bankszámlaszám;
               szerk_kapcsolattarto_nev_textbox.Text = szerkeszteni_kívant_beszallito.kapcsolattartó_neve;
               szerk_kapcsolattarto_telszam_textbox.Text = szerkeszteni_kívant_beszallito.kapcsolattartó_telefonszáma;
               szerk_kapcsolattarto_email_textbox.Text = szerkeszteni_kívant_beszallito.kapcsolattartó_email_címe;
          }
     }
}
