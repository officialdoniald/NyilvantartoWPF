using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace NyilvántartóWPF.Pages
{
    public partial class Rendelés_véglegesítése : Page
    {
        #region attribútumok

        string[] tomb = new string[2];

        string ügyfélnév = "";

        string ügyfélcím = "";

        string adoszam = "";

        string zoldkonyvszam = "";

        string anyagnév = "";

        string mennyiség_egysége = "";

        int mennyiség = 0;

        int maxmenny = 0;

        Szamok szam = new Szamok();

        Raktár raktár = new Raktár();

        Ügyfelek ügyfelek = new Ügyfelek();

        Megrendelések megrend = new Megrendelések();

        Szállítólevél szállítólevél = new Szállítólevél();

        Törzsadatlista törzsadat = new Törzsadatlista();

        Elvisz_ugyfelek elvisz_ugyfel = new Elvisz_ugyfelek();

        Behoz_beszallito behoz_beszallit = new Behoz_beszallito();

        ObservableCollection<Szamok> csakszamok = new ObservableCollection<Szamok>();

        ObservableCollection<Megrendelések> megrendelések = new ObservableCollection<Megrendelések>();

        ObservableCollection<Megrendelések> vegleg_megrendeles = new ObservableCollection<Megrendelések>();

        ObservableCollection<Ügyfelek> ügyfelek_név_cím = new ObservableCollection<Ügyfelek>();

        ObservableCollection<Ügyfelek> szerk_ügyfelek_név_cím = new ObservableCollection<Ügyfelek>();

        ObservableCollection<Raktár> raktár_tartalma = new ObservableCollection<Raktár>();

        ObservableCollection<Szállítólevél> szállítólevelek = new ObservableCollection<Szállítólevél>();

        ObservableCollection<Törzsadatlista> törzsadatok = new ObservableCollection<Törzsadatlista>();

        ObservableCollection<Behoz_beszallito> behoz_beszallito = new ObservableCollection<Behoz_beszallito>();

        ObservableCollection<Elvisz_ugyfelek> elvisz_ugyfelek = new ObservableCollection<Elvisz_ugyfelek>();

        NyilvantartoDAODBImpl db_implementation = new NyilvantartoDAODBImpl();

        #endregion

        #region init

        public Rendelés_véglegesítése()
        {
            InitializeComponent();
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
            ügyfélkiválasztós_vegleg_combobox.ItemsSource = valami;
            foreach (var item in db_implementation.getÜgyfél_Szállítólevél())
            {
                szállítólevelek.Add(item);
            }
        }

        #endregion

        #region selectionChanged
        private void ügyfélkiválasztós_vegleg_combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = ügyfélkiválasztós_vegleg_combobox.SelectedIndex;
            var valami = szerk_ügyfelek_név_cím.OrderBy(a => a.teljes_név);
            int index = 0;
            foreach (var item in valami)
            {
                if (selectedIndex == index)
                {
                    tomb = new string[2];
                    tomb[0] = item.teljes_név.Split(':')[0];
                    ügyfélnév = tomb[0];
                    tomb[1] = item.teljes_név.Split(':')[1];
                    ügyfélcím = tomb[1];
                    zoldkonyvszam = item.zöldkártyaszám;
                    adoszam = item.adószám;
                    break;
                }
                index++;
            }
            string anyag = "";
            megrendelések = db_implementation.getMegrendelések(0,0);
            var valami2 = megrendelések.OrderBy(a => a.anyagnév);
            vegleg_megrendeles = new ObservableCollection<Megrendelések>();
            foreach (var item in valami2)
            {
                if (tomb[0] == item.ügyfélnév && tomb[1] == item.ügyfélcím && item.mennyiség != "0")
                {
                    megrend = new Megrendelések();
                    megrend.anyagnév += item.anyagnév + ":" + item.mennyiség;
                    anyag = item.anyagnév;
                    vegleg_megrendeles.Add(megrend);
                }
            }
            megrendeles_kivalaszt_combobox.ItemsSource = vegleg_megrendeles;
            törzsadatok = db_implementation.getTörzsadlista();
            foreach (var item in törzsadatok)
            {
                if (anyag == item.anyagnév)
                {
                    mennyiség_egysége = item.kiszerelés;
                }
            }
        }

        private void megrendeles_kivalaszt_combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            rendellista();
        }

        private void szamok_combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mennyiség = szamok_combobox.SelectedIndex + 1;
        }

        #endregion

        #region button_click
        private void szallitolevelhez_ad_Click(object sender, RoutedEventArgs e)
        {
            szállítólevél = new Szállítólevél();
            szállítólevél.miből = anyagnév;
            szállítólevél.mennyit = mennyiség + "";
            megrendelések = new ObservableCollection<Megrendelések>();
            megrendelések = db_implementation.getMegrendelések(0,0);
            var valami2 = megrendelések.OrderBy(a => a.anyagnév);
            Megrendelések ered = new Megrendelések();
            Megrendelések szerk_megrend = new Megrendelések();
            string nezzuk_mennyi_mennyiseg = "";
            foreach (var item in valami2)
            {
                if (anyagnév == item.anyagnév && (maxmenny + "") == item.mennyiség)
                {
                    ered = item;
                    szerk_megrend.megrendelés_id = item.megrendelés_id;
                    szerk_megrend.anyagnév = item.anyagnév;
                    szerk_megrend.egységár = item.egységár;
                    szerk_megrend.áfatartalom = item.áfatartalom;
                    szerk_megrend.ügyfélcím = ügyfélcím;
                    szerk_megrend.ügyfélnév = ügyfélnév;
                    nezzuk_mennyi_mennyiseg = (Convert.ToInt32(maxmenny) - Convert.ToInt32(mennyiség)) + "";
                    szerk_megrend.mennyiség = nezzuk_mennyi_mennyiseg;
                    szállítólevél.egységár = item.egységár;
                    szállítólevél.áfatartalom = item.áfatartalom;
                }
            }
            szállítólevelek.Add(szállítólevél);
            db_implementation.insertÜgyfél_Szállítólevél(szállítólevél.miből,szállítólevél.mennyit,szállítólevél.egységár,szállítólevél.áfatartalom);
            db_implementation.updateMegrendelések(ered, szerk_megrend);
            int váltmenny = mennyiség;
            megrendelések = db_implementation.getMegrendelések(0,0);
            var valami3 = megrendelések.OrderBy(a => a.anyagnév);
            vegleg_megrendeles = new ObservableCollection<Megrendelések>();
            foreach (var item in valami3)
            {
                if (tomb[0] == item.ügyfélnév && tomb[1] == item.ügyfélcím && item.mennyiség != "0")
                {
                    megrend = new Megrendelések();
                    megrend.anyagnév += item.anyagnév + ":" + item.mennyiség;
                    vegleg_megrendeles.Add(megrend);
                }
            }
            megrendeles_kivalaszt_combobox.ItemsSource = vegleg_megrendeles;
            raktár_tartalma = db_implementation.getRaktár();
            var valami4 = raktár_tartalma.OrderBy(a => a.anyagnév);
            raktár = new Raktár();
            Raktár eredraktar = new Raktár();
            foreach (var item in valami4)
            {
                if (anyagnév == item.anyagnév)
                {
                    eredraktar = item;
                    raktár.anyagnév = anyagnév;
                    raktár.mennyiség = (Convert.ToInt32(item.mennyiség) - Convert.ToInt32(váltmenny)) + "";
                }
            }
            db_implementation.updateRaktár(eredraktar, raktár);

            string megrendelés_id = kabbe.DisplayDate.ToString("MM-dd-yyyy");

            int mennyiteddig = 0;

            var valami33 = megrendelések.OrderBy(a => a.anyagnév);

            DateTime dt2 = Convert.ToDateTime(megrendelés_id);

            foreach (var item in valami33)
            {
                DateTime dt3 = Convert.ToDateTime(item.dátum);

                if (dt3 <= dt2 && item.ügyfélnév == ügyfélnév && item.anyagnév == anyagnév)
                {
                    mennyiteddig += Convert.ToInt32(item.eredeti_mennyiség);
                }
            }

            bool val = false;

            foreach (var item in szállítólevelek)
            {
                Elvisz_ugyfelek behoz = new Elvisz_ugyfelek();
                behoz.név = ügyfélnév;
                behoz.mit = item.miből;
                
                foreach (var item2 in db_implementation.getElvisz_ugyfelek(0,0))
                {
                    if (item2.név == ügyfélnév && item2.mit == item.miből)
                    {
                        behoz.mennyit = item2.mennyit;
                        val = true;
                        db_implementation.insertElvisz_ugyfelek(ügyfélnév, item.miből, Convert.ToString(Convert.ToInt32(item.mennyit) + Convert.ToInt32(item2.mennyit)), megrendelés_id, Convert.ToString(mennyiteddig));
                    }
                }
                db_implementation.deleteElvisz_ugyfelek(behoz);
            }

            if (val == false)
            {
                foreach (var item in szállítólevelek)
                {
                    db_implementation.insertElvisz_ugyfelek(ügyfélnév, item.miből, item.mennyit, megrendelés_id, Convert.ToString(mennyiteddig));
                }
            }
            rendellista();
            UpdateLayout();
            MessageBox.Show("Sikeres Felvétel!");
        }

        private void vegleges_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Összesített_ki_mit_rendelt> besz_ossz_megrend = new ObservableCollection<Összesített_ki_mit_rendelt>();

            ObservableCollection<Összesített_ki_mit_rendelt> ebbe_kell_besz_ossz_megrend = new ObservableCollection<Összesített_ki_mit_rendelt>();

            Összesített_ki_mit_rendelt eredeti = new Összesített_ki_mit_rendelt();

            Összesített_ki_mit_rendelt változtatott = new Összesített_ki_mit_rendelt();

            besz_ossz_megrend = db_implementation.getÖsszesített_ki_mit_rendelt();

            foreach (var item in szállítólevelek)
            {
                foreach (var item2 in besz_ossz_megrend)
                {
                    if (ügyfélnév == item2.név)
                    {
                        eredeti.név = ügyfélnév;
                        eredeti.mit = item.miből;

                        változtatott.név = ügyfélnév;
                        változtatott.mit = item.miből;

                        foreach (var kisz in db_implementation.getTörzsadlista())
                        {
                            if (item.miből == kisz.anyagnév)
                            {
                                eredeti.kiszerelés = változtatott.kiszerelés = kisz.kiszerelés;
                            }
                        }

                        eredeti.mennyit = item2.mennyit;

                        változtatott.mennyit = Convert.ToString(Convert.ToInt32(item2.mennyit) - Convert.ToInt32(item.mennyit));

                        db_implementation.updateÖsszesített_ki_mit_rendelt(eredeti, változtatott);
                    }
                }
            }
            db_implementation.deleteÜgyfél_Szállítólevél();
            PrintPDFDoc pdf = new PrintPDFDoc(szállítólevelek, ügyfélnév, ügyfélcím, mennyiség_egysége, adoszam, zoldkonyvszam);
            szállítólevelek = new ObservableCollection<Szállítólevél>();
        }

        #endregion

        public void rendellista()
        {
            int index = 0;
            int selectedIndex = megrendeles_kivalaszt_combobox.SelectedIndex;
            var valami2 = vegleg_megrendeles.OrderBy(a => a.anyagnév);
            foreach (var item in valami2)
            {
                if (selectedIndex == index && item.mennyiség != "0")
                {
                    csakszamok = new ObservableCollection<Szamok>();
                    szam = new Szamok();
                    anyagnév = item.anyagnév.Split(':')[0];
                    szam.szamok = Convert.ToInt32(item.anyagnév.Split(':')[1]);
                    csakszamok.Add(szam);
                    break;
                }
                index++;
            }
            int[] tomb = new int[csakszamok[0].szamok];
            maxmenny = tomb.Length;
            for (int i = 0; i < csakszamok[0].szamok; i++)
            {
                tomb[i] = i + 1;
            }
            csakszamok = new ObservableCollection<Szamok>();
            for (int i = 0; i < tomb.Length; i++)
            {
                szam = new Szamok();
                szam.szamok = i + 1;
                csakszamok.Add(szam);
            }
            szamok_combobox.ItemsSource = csakszamok;
        }
    }

    public class Szamok
    {
        public int szamok { get; set; }
    }

}
