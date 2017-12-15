using System;
using System.Collections.Generic;
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
     /// <summary>
     /// Interaction logic for Ügyfél_felvétel.xaml
     /// </summary>
     public partial class Ügyfél_felvétel : Page
     {
          NyilvantartoDAODBImpl db_implementation = new NyilvantartoDAODBImpl();

          public Ügyfél_felvétel()
          {
               InitializeComponent();
          }

          private void reg_button_Click(object sender, RoutedEventArgs e)
          {
               DateTime dt = new DateTime();
               dt = szul_datum_datepicker.DisplayDate;
               string datetime = dt.ToString("MM-dd-yyyy");
               bool sikerult_e = db_implementation.insertÜgyfél(teljes_nev_textbox.Text, leanykori_nev_textbox.Text, lakcím_textbox.Text, anyja_nev_textbox.Text, szuletesi_helye_textbox.Text, datetime, telefonszám_textbox.Text, email_textbox.Text, adoszam_textbox.Text, adoazonosito_jel_textbox.Text, mvh_reg_szam_textbox.Text, zöldkönyv_szam_textbox.Text);
               if (sikerult_e == true)
               {
                    teljes_nev_textbox.Text = "";
                    leanykori_nev_textbox.Text = "";
                    lakcím_textbox.Text = "";
                    anyja_nev_textbox.Text = "";
                    szuletesi_helye_textbox.Text = "";
                    telefonszám_textbox.Text = "";
                    email_textbox.Text = "";
                    adoszam_textbox.Text = "";
                    adoazonosito_jel_textbox.Text = "";
                    mvh_reg_szam_textbox.Text = "";
                    zöldkönyv_szam_textbox.Text = "";
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
