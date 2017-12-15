using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyilvántartóWPF
{
    public interface INyilvantartoDAOInterface
    {
        bool insertÜgyfél(string teljes_név, string leánykori_név, string lakcím, string anyja_neve, string születési_helye, string szul_ido, string telefonszám, string email, string adószám, string adóazonosító_jel, string mvh, string zöldkártyaszám);

        bool insertTörzsadat(string ügyfélnév, string ügyfélcím, string anyagnév, string mennyiség, string egységár, string megrendelés_id, string áfatartalom);

        bool insertMegrendelések(string anyagnév, string kiszerelés, string főcsoport, string alcsoport, string gyártó, string kategória, string un_szám, string eredeti_mennyiség, string dátum);

        bool insertBeszállitok(string név, string cím, string adószám, string bankszámlaszám, string kapcsolattartó_neve, string kapcsolattartó_telefonszáma, string kapcsolattartó_email_címe);

        bool insertUres_rendeles(string beszállító, string anyagnév, string mennyit, string eredeti_mennyiség, string dátum);

        bool insertElvisz_ugyfelek(string név, string mit, string mennyit, string mikor, string mennyiteddig);

        bool insertBehoz_beszallito(string név, string mit, string mennyit, string mikor, string mennyiteddig);

        bool insertÖsszesített_ki_mit_rendelt(string név, string mit, string mennyit, string kiszerelés);

        bool insertÜgyfél_Szállítólevél(string miből, string mennyit, string egységár, string áfatartalom);

        bool insertCég_Szállítólevél(string miből, string mennyit, string egységár, string áfatartalom);

        bool insertBeszallito_összesített_ki_mit_rendelt(string név, string mit, string mennyit, string kiszerelés);

        bool updateMennyiszallitoleveleddig(int eredetimennyi, int változtatottmennyi);

        bool updateMegrendelések(Megrendelések eredeti, Megrendelések változtatott);

        bool updateUres_rendeles(Ures_rendeles eredeti, Ures_rendeles változtatott);

        bool updateÜgyfél(Ügyfelek eredeti, Ügyfelek változtatott);

        bool updateTörzsadat(Törzsadatlista eredeti, Törzsadatlista változtatott);

        bool updateBeszállító(Beszállítók eredeti, Beszállítók változtatott);

        bool updateRaktár(Raktár eredeti, Raktár változtatott);

        bool updateÖsszesített_ki_mit_rendelt(Összesített_ki_mit_rendelt eredeti, Összesített_ki_mit_rendelt változtatott);

        bool updateBeszallito_Összesített_ki_mit_rendelt(Beszallito_Összesített_ki_mit_rendelt eredeti, Beszallito_Összesített_ki_mit_rendelt változtatott);

        bool deleteÜgyfél(Ügyfelek eredeti);

        bool deleteTörzsadat(Törzsadatlista eredeti);

        bool deleteBeszállító(Beszállítók eredeti);

        bool deleteUres_rendeles(Ures_rendeles ures_rendeles);

        bool deleteÖsszesített_ki_mit_rendelt(Összesített_ki_mit_rendelt osszesitett);

        bool deleteBeszállító_Összesített_ki_mit_rendelt(Beszallito_Összesített_ki_mit_rendelt beszall);

        bool deleteBehoz_beszállító(Behoz_beszallito behoz);

        bool deleteElvisz_ugyfelek(Elvisz_ugyfelek elvisz);

        bool deleteÜgyfél_Szállítólevél();

        bool deleteCég_Szállítólevél();

        ObservableCollection<Ügyfelek> getÜgyfelek();

        ObservableCollection<Törzsadatlista> getTörzsadlista();

        ObservableCollection<Megrendelések> getMegrendelések(int ol, int ig);

        ObservableCollection<Beszállítók> getBeszállítók();

        ObservableCollection<Raktár> getRaktár();

        ObservableCollection<Elvisz_ugyfelek> getElvisz_ugyfelek(int ol, int ig);

        ObservableCollection<Behoz_beszallito> getBehoz_beszallito(int ol, int ig);

        ObservableCollection<Ures_rendeles> getUres_rendeles(int ol, int ig);

        ObservableCollection<Összesített_ki_mit_rendelt> getÖsszesített_ki_mit_rendelt();

        ObservableCollection<Beszallito_Összesített_ki_mit_rendelt> getBeszállító_Összesített_ki_mit_rendelt();

        ObservableCollection<Szállítólevél> getÜgyfél_Szállítólevél();

        ObservableCollection<Szállítólevél> getCég_Szállítólevél();

        int getMennyiszallitoleveleddig();
    }
}
