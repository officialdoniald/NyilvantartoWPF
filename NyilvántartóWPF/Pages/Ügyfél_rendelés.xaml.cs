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

namespace NyilvántartóWPF
{
    public partial class Ügyfél_rendelés : Page
    {
        #region attribútumok

        string választott_ügyfél = "";

        string választott_anyagnév = "";

        string megrendelés_id = "";

        string választott_ügyfél_cím = "";

        ObservableCollection<Ügyfelek> ügyfelek_név_cím = new ObservableCollection<Ügyfelek>();

        ObservableCollection<Ügyfelek> szerk_ügyfelek_név_cím = new ObservableCollection<Ügyfelek>();

        NyilvantartoDAODBImpl db_implementation = new NyilvantartoDAODBImpl();

        ObservableCollection<Törzsadatlista> törzsadatok = new ObservableCollection<Törzsadatlista>();

        Ügyfelek ügyfelek = new Ügyfelek();

        #endregion

        #region init
        public Ügyfél_rendelés()
        {
            InitializeComponent();
            DateTime dt = new DateTime();
            megrendelés_id = dt.ToString("MM-dd-yyyy");
            ügyfelek_név_cím = db_implementation.getÜgyfelek();
            var valami2 = ügyfelek_név_cím.OrderBy(a => a.teljes_név);
            int index = 0;
            foreach (var item in valami2)
            {
                ügyfelek = new Ügyfelek();
                ügyfelek.teljes_név += item.teljes_név + ":" + item.lakcím;
                szerk_ügyfelek_név_cím.Add(ügyfelek);
                index++;
            }
            var valami = szerk_ügyfelek_név_cím.OrderBy(a => a.teljes_név);
            ügyfélkiválasztós_combobox.ItemsSource = valami;
            index = 0;
            törzsadatok = db_implementation.getTörzsadlista();
            var valami3 = törzsadatok.OrderBy(a => a.anyagnév);
            anyagnévválasztós_combobox.ItemsSource = valami3;
        }
        #endregion

        #region clicks
        private void ügyfél_rendelés_leadása_button_Click(object sender, RoutedEventArgs e)
        {
            bool sikerult_e = db_implementation.insertMegrendelések(választott_ügyfél, választott_ügyfél_cím, választott_anyagnév, mennyiség_textbox.Text, egységár_textbox.Text, megrendelés_id, áfatartalom_textbox.Text, mennyiség_textbox.Text, rendelés_dátuma_datepicker.DisplayDate.ToString("yyyy.MM.dd"));

            ObservableCollection<Összesített_ki_mit_rendelt> beszösszesített_ki_mit_rendelt = new ObservableCollection<Összesített_ki_mit_rendelt>();

            beszösszesített_ki_mit_rendelt = db_implementation.getÖsszesített_ki_mit_rendelt();

            Összesített_ki_mit_rendelt eredeti = new Összesített_ki_mit_rendelt();

            Összesített_ki_mit_rendelt változtatott = new Összesített_ki_mit_rendelt();

            bool bement_e = false;

            bool sikerulteamasik = false;

            foreach (var item in beszösszesített_ki_mit_rendelt)
            {
                if (választott_ügyfél == item.név && választott_anyagnév == item.mit)
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
                menny = Convert.ToInt32(eredeti.mennyit) + Convert.ToInt32(mennyiség_textbox.Text);
                változtatott.mennyit = Convert.ToString(menny);
                sikerulteamasik = db_implementation.updateÖsszesített_ki_mit_rendelt(eredeti, változtatott);
            }
            else
            {
                string kiszereles = "";
                foreach (var item in db_implementation.getTörzsadlista())
                {
                    if (választott_anyagnév == item.anyagnév)
                    {
                        kiszereles = item.kiszerelés;
                    }
                }
                sikerulteamasik = db_implementation.insertÖsszesített_ki_mit_rendelt(választott_ügyfél, választott_anyagnév, mennyiség_textbox.Text, kiszereles);
            }

            if (sikerult_e == true && sikerulteamasik == true)
            {
                mennyiség_textbox.Text = "";
                egységár_textbox.Text = "";
                áfatartalom_textbox.Text = "";
                MessageBox.Show("Sikeres felvétel!");
            }
            else
            {
                MessageBox.Show("Nem sikerült a felvétel, hívj fel!");
            }
        }

        #endregion

        #region selection_changed
        private void ügyfélkiválasztós_combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = ügyfélkiválasztós_combobox.SelectedIndex;
            int index = 0;
            var valami = szerk_ügyfelek_név_cím.OrderBy(a => a.teljes_név);
            foreach (var item in valami)
            {
                if (selectedIndex == index)
                {
                    string[] tomb = new string[2];
                    tomb = item.teljes_név.Split(':');
                    választott_ügyfél = tomb[0];
                    választott_ügyfél_cím = tomb[1];
                    megrendelés_id += "_" + választott_ügyfél;
                }
                index++;
            }
        }

        private void anyagnévválasztós_combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedItem = anyagnévválasztós_combobox.SelectedIndex;
            int index = 0;
            törzsadatok = db_implementation.getTörzsadlista();
            var valami3 = törzsadatok.OrderBy(a => a.anyagnév);
            foreach (var item in valami3)
            {
                if (selectedItem == index)
                {
                    választott_anyagnév = item.anyagnév;
                }
                index++;
            }
        }
        #endregion

    }
}
