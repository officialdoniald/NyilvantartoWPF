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
     
     public partial class Beszállító_felvétele : Page
     {
          public Beszállító_felvétele()
          {
               InitializeComponent();
          }

          private void felv_button_Click(object sender, RoutedEventArgs e)
          {
               NyilvantartoDAODBImpl db_impl = new NyilvantartoDAODBImpl();
               bool sikerult_e = db_impl.insertBeszállitok(ceg_nev_textbox.Text, ceg_cime_textbox.Text,ceg_adoszama_textbox.Text,ceg_bankszámlaszama_textbox.Text,kapcsolattarto_nev_textbox.Text,kapcsolattarto_telszam_textbox.Text,kapcsolattarto_email_textbox.Text);
               if (sikerult_e == true)
               {
                    MessageBox.Show("Sikeres Felvétel!");
               }
               else
               {
                    MessageBox.Show("Sikertelen felvétel, hívj fel!");
               }
          }
     }
}
