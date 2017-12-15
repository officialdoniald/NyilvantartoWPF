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
    public partial class UgyfelRendelesModositas : Page
    {

        #region attr

        ObservableCollection<Megrendelések> megrendelesek = new ObservableCollection<Megrendelések>();

        Megrendelések eredeti = new Megrendelések();

        Megrendelések modositottott = new Megrendelések();

        NyilvantartoDAODBImpl dbImplementation = new NyilvantartoDAODBImpl();

        #endregion

        #region init

        public UgyfelRendelesModositas()
        {
            InitializeComponent();

            megrendelesek = dbImplementation.getMegrendelések(0, 0);

            rendelesek_listazasa_listbox.ItemsSource = megrendelesek;
        }

        #endregion

        private void rendelesek_listazasa_listbox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int index = rendelesek_listazasa_listbox.SelectedIndex;
            int szamlalo = 0;

            foreach (var item in megrendelesek)
            {
                if (szamlalo == index)
                {
                    eredeti = item;

                    ugyfelnev_textbox.Text = item.ügyfélnév;
                    anyagnév_textbox.Text = item.anyagnév;
                    mennyiség_textbox.Text = item.eredeti_mennyiség;
                    dátum_textbox.Text = item.dátum;
                    break;
                }

                szamlalo++;
            }
        }

        private void modosit_button_Click(object sender, RoutedEventArgs e)
        {
            int osszeg = Convert.ToInt32(mennyiség_textbox.Text);

            if (osszeg < Convert.ToInt32(eredeti.eredeti_mennyiség))
            {
                
                osszeg -=
                    Convert.ToInt32(eredeti.eredeti_mennyiség)
                    -
                    Convert.ToInt32(eredeti.mennyiség)
                    ;
            }
            else
            {
                osszeg -=
                    Convert.ToInt32(eredeti.eredeti_mennyiség)
                    -
                    Convert.ToInt32(eredeti.mennyiség)
                    ;
            }

            modositottott = new Megrendelések()
            {
                ügyfélnév = ugyfelnev_textbox.Text,
                anyagnév = anyagnév_textbox.Text,
                mennyiség = osszeg.ToString(),
                dátum = dátum_textbox.Text,
                egységár = eredeti.egységár,
                eredeti_mennyiség = mennyiség_textbox.Text,
                megrendelés_id = eredeti.megrendelés_id,
                áfatartalom = eredeti.áfatartalom,
                ügyfélcím = eredeti.ügyfélcím
            };

            var success = dbImplementation.updateMegrendelések(eredeti, modositottott);

            if (success)
            {
                MessageBox.Show("Sikeres!");

                rendelesek_listazasa_listbox.ItemsSource = null;

                megrendelesek = dbImplementation.getMegrendelések(0, 0);

                rendelesek_listazasa_listbox.ItemsSource = megrendelesek;
            }
            else
            {
                MessageBox.Show("Nem sikerült, hivj fel!");
            }
        }
    }
}
