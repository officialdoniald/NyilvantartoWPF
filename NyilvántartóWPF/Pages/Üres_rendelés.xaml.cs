using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

    public partial class Üres_rendelés : Page
    {

        #region attribútumok

        string beszállító = "";

        string anyagnév = "";

        string beszallito_email = "";

        string beszállító_kapcsolattartó_neve = "";

        Szamok szamok = new Szamok();

        mind_itt mind = new mind_itt();

        ObservableCollection<Törzsadatlista> törzsadatok = new ObservableCollection<Törzsadatlista>();

        NyilvantartoDAODBImpl db_implementation = new NyilvantartoDAODBImpl();

        ObservableCollection<Raktár> raktártartalma = new ObservableCollection<Raktár>();

        ObservableCollection<Beszállítók> beszállítók = new ObservableCollection<Beszállítók>();

        ObservableCollection<Megrendelések> megrendelések = new ObservableCollection<Megrendelések>();

        ObservableCollection<Szamok> leadott_mennyiseg = new ObservableCollection<Szamok>();

        ObservableCollection<mind_itt> minden_itt = new ObservableCollection<mind_itt>();

        #endregion

        public Üres_rendelés()
        {
            InitializeComponent();

            raktártartalma = db_implementation.getRaktár();

            var valami3 = raktártartalma.OrderBy(a => a.anyagnév);

            törzsadatok = db_implementation.getTörzsadlista();
            
            var valami4 = törzsadatok.OrderBy(a => a.anyagnév);

            törzsadatok_combobox.ItemsSource = valami4;

            beszállítók = db_implementation.getBeszállítók();
            var valami = beszállítók.OrderBy(a => a.név);
            beszállítók_vegleg_combobox.ItemsSource = valami;

            megrendelések = db_implementation.getMegrendelések(0,0);
            var valami1 = megrendelések.OrderBy(a => a.anyagnév);
            
            foreach (var rakt_item in valami3)
            {
                szamok = new Szamok();
                foreach (var megrend_item in valami1)
                {
                    if (rakt_item.anyagnév == megrend_item.anyagnév)
                    {
                        szamok.szamok += Convert.ToInt32(megrend_item.mennyiség);
                    }
                }
                leadott_mennyiseg.Add(szamok);
            }

            int index = 0;
            foreach (var item in valami3)
            {
                mind = new mind_itt();
                mind.anyagnév = item.anyagnév;
                mind.szamok = leadott_mennyiseg[index].szamok;
                mind.mennyiség = item.mennyiség;
                minden_itt.Add(mind);
                index++;
            }

            raktár_anyagneve_listview.ItemsSource = minden_itt;
        }

        private void rendelés_button_Click(object sender, RoutedEventArgs e)
        {
            //Server is webmail.yourwebsite.com
            //string server = "mail.rackhost.hu";
            //string to = "bence960206@gmail.com"/*beszallito_email*/;
            //string from = "doniald@kovipatika.hu";
            //string subject = "Rendelés";
            //string body = @"Using this new feature, you can send an e-mail message from an application very easily.";
            //MailMessage message = new MailMessage(from, to, subject, body);
            //SmtpClient client = new SmtpClient(server);
            //Console.WriteLine("Changing time out from {0} to 100.", client.Timeout);
            //client.Timeout = 100;
            //// Credentials are necessary if the server requires the client 
            //// to authenticate before it will send e-mail on the client's behalf.
            //client.Credentials = CredentialCache.DefaultNetworkCredentials;
            ////Process.Start(string.Format("mailto:{0}?subject={1}&body={2}", urlTextBox.Text, subjectTextBox.Text, ... ));
            //try
            //{
            //     client.Send(message);
            //}
            //catch (Exception ex)
            //{
            //     Console.WriteLine("Exception caught in CreateTimeoutTestMessage(): {0}",
            //           ex.ToString());
            //}

            bool sikerult_e = db_implementation.insertUres_rendeles(beszállító, anyagnév, mennyi_textbox.Text, mennyi_textbox.Text, rendelés_dátuma_datepicker.DisplayDate.ToString("yyyy.MM.dd"));

            ObservableCollection<Beszallito_Összesített_ki_mit_rendelt> beszösszesített_ki_mit_rendelt = new ObservableCollection<Beszallito_Összesített_ki_mit_rendelt>();

            beszösszesített_ki_mit_rendelt = db_implementation.getBeszállító_Összesített_ki_mit_rendelt();

            Beszallito_Összesített_ki_mit_rendelt eredeti = new Beszallito_Összesített_ki_mit_rendelt();

            Beszallito_Összesített_ki_mit_rendelt változtatott = new Beszallito_Összesített_ki_mit_rendelt();

            bool bement_e = false;

            bool sikerulteamasik = false;

            foreach (var item in beszösszesített_ki_mit_rendelt)
            {
                if (beszállító == item.név && anyagnév == item.mit)
                {
                    eredeti.név = item.név;
                    eredeti.mennyit = item.mennyit;
                    eredeti.mit = item.mit;
                    eredeti.kiszerelés = item.kiszerelés;
                    bement_e = true;
                    break;
                }
            }

            int menny = 0;

            if (bement_e == true)
            {
                változtatott.név = eredeti.név;
                változtatott.mit = eredeti.mit;
                változtatott.kiszerelés = eredeti.kiszerelés;
                menny = Convert.ToInt32(eredeti.mennyit) + Convert.ToInt32(mennyi_textbox.Text);
                változtatott.mennyit = Convert.ToString(menny);
                sikerulteamasik = db_implementation.updateBeszallito_Összesített_ki_mit_rendelt(eredeti,változtatott);
            }
            else
            {
                string kiszereles = "";
                foreach (var item in db_implementation.getTörzsadlista())
                {
                    if (anyagnév == item.anyagnév)
                    {
                        kiszereles = item.kiszerelés;
                        break;
                    }
                }
                sikerulteamasik = db_implementation.insertBeszallito_összesített_ki_mit_rendelt(beszállító,anyagnév,mennyi_textbox.Text,kiszereles);
            }

            if (sikerult_e == true && sikerulteamasik == true)
            {
                mennyi_textbox.Text = "";
                MessageBox.Show("Sikeres Felvétel!");
            }
            else
            {
                MessageBox.Show("Sikertelen felvétel, hívj fel!");
            }
        }

        #region selectionChanged

        private void törzsadatok_combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = törzsadatok_combobox.SelectedIndex;
            törzsadatok = db_implementation.getTörzsadlista();
            var valami3 = raktártartalma.OrderBy(a => a.anyagnév);
            int szamlalo = 0;
            foreach (var item in valami3)
            {
                if (index == szamlalo)
                {
                    anyagnév = item.anyagnév;
                    break;
                }
                szamlalo++;
            }
        }

        private void beszállítók_vegleg_combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = beszállítók_vegleg_combobox.SelectedIndex;
            beszállítók = db_implementation.getBeszállítók();
            var valami = beszállítók.OrderBy(a => a.név);
            int szamlalo = 0;
            foreach (var item in valami)
            {
                if (index == szamlalo)
                {
                    beszállító = item.név;
                    beszallito_email = item.kapcsolattartó_email_címe;
                    beszállító_kapcsolattartó_neve = item.kapcsolattartó_neve;
                    break;
                }
                szamlalo++;
            }
        }
    }

    #endregion

    class mind_itt
    {
        public string anyagnév { get; set; }
        public int szamok { get; set; }
        public string mennyiség { get; set; }
    }
}
