using NyilvántartóWPF.Pages;
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
     public partial class Ügyfél_Listázása : Page
     {

          NyilvantartoDAODBImpl db_implementation = new NyilvantartoDAODBImpl();

          ObservableCollection<Ügyfelek> ügyfelek = new ObservableCollection<Ügyfelek>();

          Ügyfelek szerkeszteni_kívant_ügyfel = new Ügyfelek();

          public Ügyfél_Listázása()
          {
               InitializeComponent();
               ügyfelek = db_implementation.getÜgyfelek();
               var valami = ügyfelek.OrderBy(a => a.teljes_név);
               ügyfelek_listazasa_listbox.ItemsSource = valami;
          }

          private void ügyfelek_listazasa_listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
          {
               int index = 0;
               int selecteditem = ügyfelek_listazasa_listbox.SelectedIndex;
               var valami = ügyfelek.OrderBy(a => a.teljes_név);
               foreach (var item in valami)
               {
                    if (index == selecteditem)
                    {
                         szerkeszteni_kívant_ügyfel = item;
                         break;
                    }
                    index++;
               }
          }

          private void ügyfelek_listazasa_listbox_DoubleTapped(object sender, MouseButtonEventArgs e)
          {
               Ügyfél_szerkesztése(szerkeszteni_kívant_ügyfel);
          }

          public void Ügyfél_szerkesztése(Ügyfelek szerkeszteni_kívant_ügyfel)
          {
               this.szerkeszteni_kívant_ügyfel = szerkeszteni_kívant_ügyfel;
               szerk_teljes_nev_textbox.Text = szerkeszteni_kívant_ügyfel.teljes_név;
               szerk_leanykori_nev_textbox.Text = szerkeszteni_kívant_ügyfel.leánykori_név;
               szerk_lakcím_textbox.Text = szerkeszteni_kívant_ügyfel.lakcím;
               szerk_anyja_nev_textbox.Text = szerkeszteni_kívant_ügyfel.anyja_neve;
               szerk_szuletesi_helye_textbox.Text = szerkeszteni_kívant_ügyfel.születési_helye;
               szerk_szul_datum_datepicker.DataContext = szerkeszteni_kívant_ügyfel.születési_ideje;
               szerk_telefonszám_textbox.Text = szerkeszteni_kívant_ügyfel.telefonszám;
               szerk_email_textbox.Text = szerkeszteni_kívant_ügyfel.email;
               szerk_adoszam_textbox.Text = szerkeszteni_kívant_ügyfel.adószám;
               szerk_adoazonosito_jel_textbox.Text = szerkeszteni_kívant_ügyfel.adóazonosító_jel;
               szerk_mvh_reg_szam_textbox.Text = szerkeszteni_kívant_ügyfel.mvh;
               szerk_zöldkönyv_szam_textbox.Text = szerkeszteni_kívant_ügyfel.zöldkártyaszám;
          }

          private void szerk_ugyfel_button_Click(object sender, RoutedEventArgs e)
          {
               Ügyfelek szerkesztett_ügyfél = new Ügyfelek();
               szerkesztett_ügyfél.adóazonosító_jel = szerk_adoazonosito_jel_textbox.Text;
               szerkesztett_ügyfél.adószám = szerk_adoszam_textbox.Text;
               szerkesztett_ügyfél.anyja_neve = szerk_anyja_nev_textbox.Text;
               szerkesztett_ügyfél.email = szerk_email_textbox.Text;
               szerkesztett_ügyfél.lakcím = szerk_lakcím_textbox.Text;
               szerkesztett_ügyfél.leánykori_név = szerk_leanykori_nev_textbox.Text;
               szerkesztett_ügyfél.mvh = szerk_mvh_reg_szam_textbox.Text;
               szerkesztett_ügyfél.születési_helye = szerk_szuletesi_helye_textbox.Text;
               DateTime dt = new DateTime();
               dt = szerk_szul_datum_datepicker.DisplayDate;
               string datetime = dt.ToString("MM-dd-yyyy");
               szerkesztett_ügyfél.születési_ideje = datetime;
               szerkesztett_ügyfél.telefonszám = szerk_telefonszám_textbox.Text;
               szerkesztett_ügyfél.teljes_név = szerk_teljes_nev_textbox.Text;
               szerkesztett_ügyfél.zöldkártyaszám = szerk_zöldkönyv_szam_textbox.Text;
               bool sikerult_e = db_implementation.updateÜgyfél(szerkeszteni_kívant_ügyfel, szerkesztett_ügyfél);
               if (sikerult_e == true)
               {
                    ügyfelek_listazasa_listbox.ItemsSource = null;
                    ügyfelek = db_implementation.getÜgyfelek();
                    var valami = ügyfelek.OrderBy(a => a.teljes_név);
                    ügyfelek_listazasa_listbox.ItemsSource = valami;
                    this.UpdateLayout();
                    MessageBox.Show("Sikeres módosítás!");
               }
               else
               {
                    MessageBox.Show("Nem sikerült a módosítás, hívj fel!");
               }
          }

          private void del_ugyfel_button_Click(object sender, RoutedEventArgs e)
          {
               bool sikerult_e = db_implementation.deleteÜgyfél(szerkeszteni_kívant_ügyfel);
               if (sikerult_e == true)
               {
                    ügyfelek_listazasa_listbox.ItemsSource = null;
                    ügyfelek = db_implementation.getÜgyfelek();
                    var valami = ügyfelek.OrderBy(a => a.teljes_név);
                    ügyfelek_listazasa_listbox.ItemsSource = valami;
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
