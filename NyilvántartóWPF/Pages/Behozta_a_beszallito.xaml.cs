using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace NyilvántartóWPF.Pages
{
    public partial class Behozta_a_beszallito : Page
    {
        #region attribútumok

        ObservableCollection<Behoz_beszallito> behoz_beszallitok = new ObservableCollection<Behoz_beszallito>();

        ObservableCollection<Beszállítók> beszallitok = new ObservableCollection<Beszállítók>();

        ObservableCollection<Törzsadatlista> törzsadatok = new ObservableCollection<Törzsadatlista>();

        ObservableCollection<Ures_rendeles> ures_rendelesek = new ObservableCollection<Ures_rendeles>();

        ObservableCollection<Szállítólevél> szállítólevelek = new ObservableCollection<Szállítólevél>();

        NyilvantartoDAODBImpl db_implements = new NyilvantartoDAODBImpl();

        Ures_rendeles ures_rendeles = new Ures_rendeles();

        Behoz_beszallito behozta_beszallito = new Behoz_beszallito();

        Törzsadatlista törzsadat = new Törzsadatlista();

        Beszállítók beszallito = new Beszállítók();

        Szállítólevél szállítólevvél = new Szállítólevél();

        DateTime dt = DateTime.Now;

        string megrendelés_id;

        string kivalasztott_beszallito = "";

        string kivalasztott_torzsadat = "";

        #endregion

        #region init
        public Behozta_a_beszallito()
        {
            InitializeComponent();

            megrendelés_id = dt.ToString("MM-dd-yyyy");
            
            beszallitok = db_implements.getBeszállítók();
            var rendezett_beszallito = beszallitok.OrderBy(a => a.név);

            törzsadatok = db_implements.getTörzsadlista();
            var rendezett_törzsadatlista = törzsadatok.OrderBy(a => a.anyagnév);

            beszallitok_listazasa_listbox.ItemsSource = rendezett_beszallito;

            törzsadatok_listbox.ItemsSource = rendezett_törzsadatlista;
            foreach (var item in db_implements.getCég_Szállítólevél())
            {
                szállítólevelek.Add(item);
            }
        }
        #endregion

        #region selection_changed
        private void beszallitok_listazasa_listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = beszallitok_listazasa_listbox.SelectedIndex;

            int szamlalo = 0;

            var rendezett_beszallito = beszallitok.OrderBy(a => a.név);

            foreach (var item in rendezett_beszallito)
            {
                if (szamlalo == index)
                {
                    kivalasztott_beszallito = item.név;
                    break;
                }
                szamlalo++;
            }
        }

        private void törzsadatok_listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = törzsadatok_listbox.SelectedIndex;

            int szamlalo = 0;

            var rendezett_törzsadatlista = törzsadatok.OrderBy(a => a.anyagnév);

            foreach (var item in rendezett_törzsadatlista)
            {
                if (szamlalo == index)
                {
                    kivalasztott_torzsadat = item.anyagnév;
                    break;
                }
                szamlalo++;
            }
        }

        #endregion

        #region clicks
        private void hozzaad_button_Click(object sender, RoutedEventArgs e)
        {
            behozta_beszallito = new Behoz_beszallito();
            behozta_beszallito.név = kivalasztott_beszallito;
            behozta_beszallito.mit = kivalasztott_torzsadat;
            behozta_beszallito.mennyit = mennyi_textbox.Text;
            behozta_beszallito.mikor = rendelés_dátuma_datepicker.DisplayDate.ToString("MM-dd-yyyy");

            szállítólevvél = new Szállítólevél();
            szállítólevvél.miből = behozta_beszallito.mit;
            szállítólevvél.mennyit = behozta_beszallito.mennyit;
            szállítólevvél.egységár = Convert.ToString(Convert.ToInt32(egysegar_textbox.Text) * Convert.ToInt32(szállítólevvél.mennyit));
            szállítólevvél.áfatartalom = afatartalom_textbox.Text;

            szállítólevelek.Add(szállítólevvél);

            db_implements.insertCég_Szállítólevél(szállítólevvél.miből, szállítólevvél.mennyit, szállítólevvél.egységár, szállítólevvél.áfatartalom);

            behoz_beszallitok.Add(behozta_beszallito);

            int mennyiteddig = 0;

            ures_rendelesek = db_implements.getUres_rendeles(0,0);

            var valami3 = ures_rendelesek.OrderBy(a => a.anyagnév);

            DateTime dt2 = Convert.ToDateTime(megrendelés_id);

            foreach (var item in valami3)
            {
                DateTime dt3 = Convert.ToDateTime(item.dátum);

                if (dt3 <= dt2 && item.beszállító == kivalasztott_beszallito && item.anyagnév == kivalasztott_torzsadat)
                {
                    mennyiteddig += Convert.ToInt32(item.eredeti_mennyiség);
                }
            }

            bool val = false;

            foreach (var item in behoz_beszallitok)
            {
                Behoz_beszallito behoz = new Behoz_beszallito();
                behoz.név = item.név;
                behoz.mit = item.mit;

                foreach (var item2 in db_implements.getBehoz_beszallito(0,0))
                {
                    if (item2.név == item.név && item2.mit == item.mit)
                    {
                        behoz.mennyit = item2.mennyit;
                        val = true;
                        db_implements.insertBehoz_beszallito(item.név, item.mit, Convert.ToString(Convert.ToInt32(item.mennyit) + Convert.ToInt32(item2.mennyit)), item.mikor, Convert.ToString(mennyiteddig));
                    }
                }
                db_implements.deleteBehoz_beszállító(behoz);
            }

            if (val == false)
            {
                foreach (var item in behoz_beszallitok)
                {
                    db_implements.insertBehoz_beszallito(item.név, item.mit, item.mennyit, item.mikor, Convert.ToString(mennyiteddig));
                }
            }
            MessageBox.Show("Hozzáadva!");
        }

        private void vegleg_button_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Beszallito_Összesített_ki_mit_rendelt> besz_ossz_megrend = new ObservableCollection<Beszallito_Összesített_ki_mit_rendelt>();

            ObservableCollection<Beszallito_Összesített_ki_mit_rendelt> ebbe_kell_besz_ossz_megrend = new ObservableCollection<Beszallito_Összesített_ki_mit_rendelt>();

            Beszallito_Összesített_ki_mit_rendelt eredeti = new Beszallito_Összesített_ki_mit_rendelt();

            Beszallito_Összesített_ki_mit_rendelt változtatott = new Beszallito_Összesített_ki_mit_rendelt();

            besz_ossz_megrend = db_implements.getBeszállító_Összesített_ki_mit_rendelt();

            foreach (var item in behoz_beszallitok)
            {
                foreach (var item2 in besz_ossz_megrend)
                {
                    if (item.név == item2.név)
                    {
                        eredeti.név = item.név;
                        eredeti.mit = item.mit;

                        változtatott.név = item.név;
                        változtatott.mit = item.mit;

                        foreach (var kisz in db_implements.getTörzsadlista())
                        {
                            if (item.mit == kisz.anyagnév)
                            {
                                eredeti.kiszerelés = változtatott.kiszerelés = kisz.kiszerelés;
                            }
                        }

                        eredeti.mennyit = item2.mennyit;

                        változtatott.mennyit = Convert.ToString(Convert.ToInt32(item2.mennyit) - Convert.ToInt32(item.mennyit));

                        db_implements.updateBeszallito_Összesített_ki_mit_rendelt(eredeti, változtatott);
                    }
                }
            }

            new PDFBehozBeszallito(szállítólevelek, behoz_beszallitok[0].név);

            behoz_beszallitok = new ObservableCollection<Behoz_beszallito>();
            szállítólevelek = new ObservableCollection<Szállítólevél>();

            db_implements.deleteCég_Szállítólevél();
        }

        #endregion
    }
}
