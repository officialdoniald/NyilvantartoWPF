using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyilvántartóWPF
{
    public class NyilvantartoDAODBImpl : INyilvantartoDAOInterface
    {
        #region attribútumok

        ObservableCollection<Törzsadatlista> törzsadatlista;

        ObservableCollection<Ügyfelek> ügyfelek;

        ObservableCollection<Megrendelések> megrendelések;

        ObservableCollection<Beszállítók> beszállítók;

        ObservableCollection<Raktár> raktár;

        ObservableCollection<Ures_rendeles> ures_rendelések;

        ObservableCollection<Elvisz_ugyfelek> elvisz_ugyfelek;

        ObservableCollection<Behoz_beszallito> behoz_beszallito;

        ObservableCollection<Összesített_ki_mit_rendelt> összesített_rendelések = new ObservableCollection<Összesített_ki_mit_rendelt>();

        ObservableCollection<Beszallito_Összesített_ki_mit_rendelt> besz_össze_rend = new ObservableCollection<Beszallito_Összesített_ki_mit_rendelt>();

        ObservableCollection<Szállítólevél> szállítólevél = new ObservableCollection<Szállítólevél>();

        private SqlConnection connection;

        private string connectionString = "Server=tcp:doniald.database.windows.net,1433;Database=kovipatika;User ID=doniald@doniald;Password=Qwer01234;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        #endregion

        #region get_from_db
        public ObservableCollection<Törzsadatlista> getTörzsadlista()
        {
            try
            {
                connection = new SqlConnection(connectionString);

                connection.Open();

                string query = "SELECT * FROM törzsadat";

                törzsadatlista = new ObservableCollection<Törzsadatlista>();

                Törzsadatlista törzsadat;

                SqlCommand cmd = new SqlCommand(query, connection);

                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    törzsadat = new Törzsadatlista();

                    törzsadat.anyagnév = dataReader.GetString(0);
                    törzsadat.kiszerelés = dataReader.GetString(1);
                    törzsadat.főcsoport = dataReader.GetString(2);
                    törzsadat.alcsoport = dataReader.GetString(3);
                    törzsadat.gyártó = dataReader.GetString(4);
                    törzsadat.kategória = dataReader.GetString(5);
                    törzsadat.un_szám = dataReader.GetString(6);

                    törzsadatlista.Add(törzsadat);
                }

                dataReader.Close();
                connection.Close();

                return törzsadatlista;
            }
            catch (SqlException)
            {
                return törzsadatlista;
            }
        }

        public ObservableCollection<Ügyfelek> getÜgyfelek()
        {
            try
            {
                connection = new SqlConnection(connectionString);

                connection.Open();

                string query = "SELECT * FROM ügyfelek";

                ügyfelek = new ObservableCollection<Ügyfelek>();

                Ügyfelek ügyfél;

                SqlCommand cmd = new SqlCommand(query, connection);

                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    ügyfél = new Ügyfelek();

                    ügyfél.teljes_név = dataReader.GetString(0);
                    ügyfél.leánykori_név = dataReader.GetString(1);
                    ügyfél.lakcím = dataReader.GetString(2);
                    ügyfél.anyja_neve = dataReader.GetString(3);
                    ügyfél.születési_helye = dataReader.GetString(4);
                    ügyfél.születési_ideje = dataReader.GetString(5);
                    ügyfél.telefonszám = dataReader.GetString(6);
                    ügyfél.email = dataReader.GetString(7);
                    ügyfél.adószám = dataReader.GetString(8);
                    ügyfél.adóazonosító_jel = dataReader.GetString(9);
                    ügyfél.mvh = dataReader.GetString(10);
                    ügyfél.zöldkártyaszám = dataReader.GetString(11);

                    ügyfelek.Add(ügyfél);
                }

                dataReader.Close();
                connection.Close();

                return ügyfelek;
            }
            catch (SqlException)
            {
                return ügyfelek;
            }
        }

        public ObservableCollection<Megrendelések> getMegrendelések(int tol, int ig)
        {
            try
            {
                connection = new SqlConnection(connectionString);

                connection.Open();

                string query = "SELECT * FROM megrendelések";

                megrendelések = new ObservableCollection<Megrendelések>();

                Megrendelések megrendelés;

                SqlCommand cmd = new SqlCommand(query, connection);

                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    if (tol == 0)
                    {
                        megrendelés = new Megrendelések();
                        megrendelés.ügyfélnév = dataReader.GetString(0);
                        megrendelés.ügyfélcím = dataReader.GetString(1);
                        megrendelés.anyagnév = dataReader.GetString(2);
                        megrendelés.mennyiség = dataReader.GetString(3);
                        megrendelés.egységár = dataReader.GetString(4);
                        megrendelés.megrendelés_id = dataReader.GetString(5);
                        megrendelés.áfatartalom = dataReader.GetString(6);
                        megrendelés.eredeti_mennyiség = dataReader.GetString(7);
                        megrendelés.dátum = dataReader.GetString(8);
                        megrendelések.Add(megrendelés);
                    }
                    else
                    {
                        int year = Convert.ToInt32(dataReader.GetString(3).Substring(6));
                        if (year >= tol && year <= ig)
                        {
                            megrendelés = new Megrendelések();
                            megrendelés.ügyfélnév = dataReader.GetString(0);
                            megrendelés.ügyfélcím = dataReader.GetString(1);
                            megrendelés.anyagnév = dataReader.GetString(2);
                            megrendelés.mennyiség = dataReader.GetString(3);
                            megrendelés.egységár = dataReader.GetString(4);
                            megrendelés.megrendelés_id = dataReader.GetString(5);
                            megrendelés.áfatartalom = dataReader.GetString(6);
                            megrendelés.eredeti_mennyiség = dataReader.GetString(7);
                            megrendelés.dátum = dataReader.GetString(8);
                            megrendelések.Add(megrendelés);
                        }
                    }
                }

                dataReader.Close();
                connection.Close();

                return megrendelések;
            }
            catch (SqlException)
            {
                return megrendelések;
            }
        }

        public ObservableCollection<Beszállítók> getBeszállítók()
        {
            try
            {
                connection = new SqlConnection(connectionString);

                connection.Open();

                string query = "SELECT * FROM beszallítók";

                beszállítók = new ObservableCollection<Beszállítók>();

                Beszállítók beszállító;

                SqlCommand cmd = new SqlCommand(query, connection);

                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    beszállító = new Beszállítók();

                    beszállító.név = dataReader.GetString(0);
                    beszállító.cím = dataReader.GetString(1);
                    beszállító.adószám = dataReader.GetString(2);
                    beszállító.bankszámlaszám = dataReader.GetString(3);
                    beszállító.kapcsolattartó_neve = dataReader.GetString(4);
                    beszállító.kapcsolattartó_telefonszáma = dataReader.GetString(5);
                    beszállító.kapcsolattartó_email_címe = dataReader.GetString(6);

                    beszállítók.Add(beszállító);
                }

                dataReader.Close();
                connection.Close();

                return beszállítók;
            }
            catch (SqlException)
            {
                return null;
            }
        }

        public ObservableCollection<Raktár> getRaktár()
        {
            try
            {
                connection = new SqlConnection(connectionString);

                connection.Open();

                string query = "SELECT * FROM raktár";

                raktár = new ObservableCollection<Raktár>();

                Raktár rakt;

                SqlCommand cmd = new SqlCommand(query, connection);

                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    rakt = new Raktár();

                    rakt.anyagnév = dataReader.GetString(0);
                    rakt.mennyiség = dataReader.GetString(1);

                    raktár.Add(rakt);
                }

                dataReader.Close();
                connection.Close();

                return raktár;
            }
            catch (SqlException)
            {
                return raktár;
            }
        }

        public int getMennyiszallitoleveleddig()
        {
            try
            {
                connection = new SqlConnection(connectionString);

                connection.Open();

                string query = "SELECT * FROM mennyiszallitoleveleddig";

                int mennyi = 0;

                SqlCommand cmd = new SqlCommand(query, connection);

                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    mennyi = dataReader.GetInt32(0);
                }

                dataReader.Close();
                connection.Close();

                return mennyi;
            }
            catch (SqlException)
            {
                return 0;
            }
        }

        public ObservableCollection<Ures_rendeles> getUres_rendeles(int tol, int ig)
        {
            try
            {
                connection = new SqlConnection(connectionString);

                connection.Open();

                string query = "SELECT * FROM ures_rendeles";
                
                SqlCommand cmd = new SqlCommand(query, connection);

                SqlDataReader dataReader = cmd.ExecuteReader();

                ures_rendelések = new ObservableCollection<Ures_rendeles>();

                Ures_rendeles ures_rendeles;

                while (dataReader.Read())
                {
                    if (tol == 0)
                    {
                        ures_rendeles = new Ures_rendeles();
                        ures_rendeles.beszállító = dataReader.GetString(0);
                        ures_rendeles.anyagnév = dataReader.GetString(1);
                        ures_rendeles.mennyiség = dataReader.GetString(2);
                        ures_rendeles.eredeti_mennyiség = dataReader.GetString(3);
                        ures_rendeles.dátum = dataReader.GetString(4);
                        ures_rendelések.Add(ures_rendeles);
                    }
                    else
                    {
                        int year = Convert.ToInt32(dataReader.GetString(3).Substring(6));
                        if (year >= tol && year <= ig)
                        {
                            ures_rendeles = new Ures_rendeles();
                            ures_rendeles.beszállító = dataReader.GetString(0);
                            ures_rendeles.anyagnév = dataReader.GetString(1);
                            ures_rendeles.mennyiség = dataReader.GetString(2);
                            ures_rendeles.eredeti_mennyiség = dataReader.GetString(3);
                            ures_rendeles.dátum = dataReader.GetString(4);
                            ures_rendelések.Add(ures_rendeles);
                        }
                    }
                }

                dataReader.Close();
                connection.Close();

                return ures_rendelések;
            }
            catch (SqlException)
            {
                return ures_rendelések;
            }
        }

        public ObservableCollection<Elvisz_ugyfelek> getElvisz_ugyfelek(int tol, int ig)
        {
            try
            {
                connection = new SqlConnection(connectionString);

                connection.Open();

                string query = "SELECT * FROM elvisz_ugyfelek";

                SqlCommand cmd = new SqlCommand(query, connection);

                SqlDataReader dataReader = cmd.ExecuteReader();

                elvisz_ugyfelek = new ObservableCollection<Elvisz_ugyfelek>();

                Elvisz_ugyfelek elvisz_ugyfel;

                while (dataReader.Read())
                {
                    if (tol == 0)
                    {
                        elvisz_ugyfel = new Elvisz_ugyfelek();
                        elvisz_ugyfel.név = dataReader.GetString(0);
                        elvisz_ugyfel.mit = dataReader.GetString(1);
                        elvisz_ugyfel.mennyit = dataReader.GetString(2);
                        elvisz_ugyfel.mikor = dataReader.GetString(3);
                        elvisz_ugyfel.mennyiteddig = dataReader.GetString(4);
                        elvisz_ugyfelek.Add(elvisz_ugyfel);
                    }
                    else
                    {
                        int year = Convert.ToInt32(dataReader.GetString(3).Substring(6));
                        if (year >= tol && year <= ig)
                        {
                            elvisz_ugyfel = new Elvisz_ugyfelek();
                            elvisz_ugyfel.név = dataReader.GetString(0);
                            elvisz_ugyfel.mit = dataReader.GetString(1);
                            elvisz_ugyfel.mennyit = dataReader.GetString(2);
                            elvisz_ugyfel.mikor = dataReader.GetString(3);
                            elvisz_ugyfel.mennyiteddig = dataReader.GetString(4);
                            elvisz_ugyfelek.Add(elvisz_ugyfel);
                        }
                    }
                }

                dataReader.Close();
                connection.Close();

                return elvisz_ugyfelek;
            }
            catch (SqlException)
            {
                return elvisz_ugyfelek;
            }
        }
        
        public ObservableCollection<Behoz_beszallito> getBehoz_beszallito(int tol, int ig)
        {
            try
            {
                connection = new SqlConnection(connectionString);

                connection.Open();

                string query = "SELECT * FROM behoz_beszallito";

                SqlCommand cmd = new SqlCommand(query, connection);

                SqlDataReader dataReader = cmd.ExecuteReader();

                behoz_beszallito = new ObservableCollection<Behoz_beszallito>();

                Behoz_beszallito behoz_beszall;

                while (dataReader.Read())
                {
                    if (tol == 0)
                    {
                        behoz_beszall = new Behoz_beszallito();
                        behoz_beszall.név = dataReader.GetString(0);
                        behoz_beszall.mit = dataReader.GetString(1);
                        behoz_beszall.mennyit = dataReader.GetString(2);
                        behoz_beszall.mikor = dataReader.GetString(3);
                        behoz_beszall.mennyiteddig = dataReader.GetString(4);
                        behoz_beszallito.Add(behoz_beszall);
                    }
                    else
                    {
                        int year = Convert.ToInt32(dataReader.GetString(3).Substring(6));
                        if (year >= tol && year <= ig)
                        {
                            behoz_beszall = new Behoz_beszallito();
                            behoz_beszall.név = dataReader.GetString(0);
                            behoz_beszall.mit = dataReader.GetString(1);
                            behoz_beszall.mennyit = dataReader.GetString(2);
                            behoz_beszall.mikor = dataReader.GetString(3);
                            behoz_beszall.mennyiteddig = dataReader.GetString(4);
                            behoz_beszallito.Add(behoz_beszall);
                        }
                    }
                }

                dataReader.Close();
                connection.Close();

                return behoz_beszallito;
            }
            catch (SqlException)
            {
                return behoz_beszallito;
            }
        }

        public ObservableCollection<Összesített_ki_mit_rendelt> getÖsszesített_ki_mit_rendelt()
        {
            try
            {
                connection = new SqlConnection(connectionString);

                connection.Open();

                string query = "SELECT * FROM összesített_ki_mit_rendelt";

                SqlCommand cmd = new SqlCommand(query, connection);

                SqlDataReader dataReader = cmd.ExecuteReader();

                összesített_rendelések = new ObservableCollection<Összesített_ki_mit_rendelt>();

                Összesített_ki_mit_rendelt össz;

                while (dataReader.Read())
                {
                    össz = new Összesített_ki_mit_rendelt();
                    össz.név = dataReader.GetString(0);
                    össz.mit = dataReader.GetString(1);
                    össz.mennyit = dataReader.GetString(2);
                    össz.kiszerelés = dataReader.GetString(3);
                    összesített_rendelések.Add(össz);
                }

                dataReader.Close();
                connection.Close();

                return összesített_rendelések;
            }
            catch (SqlException)
            {
                return összesített_rendelések;
            }
        }

        public ObservableCollection<Beszallito_Összesített_ki_mit_rendelt> getBeszállító_Összesített_ki_mit_rendelt()
        {
            try
            {
                connection = new SqlConnection(connectionString);

                connection.Open();

                string query = "SELECT * FROM beszallito_összesített_ki_mit_rendelt";

                SqlCommand cmd = new SqlCommand(query, connection);

                SqlDataReader dataReader = cmd.ExecuteReader();

                besz_össze_rend = new ObservableCollection<Beszallito_Összesített_ki_mit_rendelt>();

                Beszallito_Összesített_ki_mit_rendelt össz;

                while (dataReader.Read())
                {
                    össz = new Beszallito_Összesített_ki_mit_rendelt();
                    össz.név = dataReader.GetString(0);
                    össz.mit = dataReader.GetString(1);
                    össz.mennyit = dataReader.GetString(2);
                    össz.kiszerelés = dataReader.GetString(3);
                    besz_össze_rend.Add(össz);
                }

                dataReader.Close();
                connection.Close();

                return besz_össze_rend;
            }
            catch (SqlException)
            {
                return besz_össze_rend;
            }
        }

        public ObservableCollection<Szállítólevél> getÜgyfél_Szállítólevél()
        {
            try
            {
                connection = new SqlConnection(connectionString);

                connection.Open();

                string query = "SELECT * FROM szallitolevel_ugyfel";

                SqlCommand cmd = new SqlCommand(query, connection);

                SqlDataReader dataReader = cmd.ExecuteReader();

                szállítólevél = new ObservableCollection<Szállítólevél>();

                Szállítólevél össz;

                while (dataReader.Read())
                {
                    össz = new Szállítólevél();
                    össz.miből = dataReader.GetString(0);
                    össz.mennyit = dataReader.GetString(1);
                    össz.egységár = dataReader.GetString(2);
                    össz.áfatartalom = dataReader.GetString(3);
                    szállítólevél.Add(össz);
                }

                dataReader.Close();
                connection.Close();

                return szállítólevél;
            }
            catch (SqlException)
            {
                return szállítólevél;
            }
        }

        public ObservableCollection<Szállítólevél> getCég_Szállítólevél()
        {
            try
            {
                connection = new SqlConnection(connectionString);

                connection.Open();

                string query = "SELECT * FROM szallitolevel_ceg";

                SqlCommand cmd = new SqlCommand(query, connection);

                SqlDataReader dataReader = cmd.ExecuteReader();

                szállítólevél = new ObservableCollection<Szállítólevél>();

                Szállítólevél össz;

                while (dataReader.Read())
                {
                    össz = new Szállítólevél();
                    össz.miből = dataReader.GetString(0);
                    össz.mennyit = dataReader.GetString(1);
                    össz.egységár = dataReader.GetString(2);
                    össz.áfatartalom = dataReader.GetString(3);
                    szállítólevél.Add(össz);
                }

                dataReader.Close();
                connection.Close();

                return szállítólevél;
            }
            catch (SqlException)
            {
                return szállítólevél;
            }
        }

        #endregion

        #region insert_fvk
        public bool insertTörzsadat(string anyagnév, string kiszerelés, string főcsoport, string alcsoport, string gyártó, string kategória, string un_szám)
        {
            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = "INSERT INTO törzsadat(anyagnév, kiszerelés,főcsoport, alcsoport, gyártó, kategória, un_szám)"
                     + " VALUES(N'"
                     + anyagnév
                     + "',N'"
                     + kiszerelés
                     + "',N'"
                     + főcsoport
                     + "',N'"
                     + alcsoport
                     + "',N'"
                     + gyártó
                     + "',N'"
                     + kategória
                     + "',N'"
                     + un_szám
                     + "');";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool insertÜgyfél(string teljes_név, string leánykori_név, string lakcím, string anyja_neve, string születési_helye, string szul_ido, string telefonszám, string email, string adószám, string adóazonosító_jel, string mvh, string zöldkártyaszám)
        {
            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string query = "INSERT INTO ügyfelek(név, leánykori_név,lakcím, anyja_neve, szul_hely, szul_ideje, telefonszám, email, adoszam, adóazonosítójel, mvh_regszam, zöldkönyvszám)"
                     + " VALUES(N'"
                     + teljes_név
                     + "',N'"
                     + leánykori_név
                     + "',N'"
                     + lakcím
                     + "',N'"
                     + anyja_neve
                     + "',N'"
                     + születési_helye
                     + "',N'"
                     + szul_ido
                     + "',N'"
                     + telefonszám
                     + "',N'"
                     + email
                     + "',N'"
                     + adószám
                     + "',N'"
                     + adóazonosító_jel
                     + "',N'"
                     + mvh
                     + "',N'"
                     + zöldkártyaszám
                     + "');";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (SqlException ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        public bool insertMegrendelések(string ügyfélnév, string ügyfélcím, string anyagnév, string mennyiség, string egységár, string megrendelés_id, string áfatartalom, string eredeti_mennyiség, string dátum)
        {
            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string query = "INSERT INTO megrendelések(ügyfélnév, ügyfélcím, anyagnév, mennyiség, egységár, megrendelés_id, áfatartalom, eredeti_mennyiség, dátum)"
                     + " VALUES(N'"
                     + ügyfélnév
                     + "',N'"
                     + ügyfélcím
                     + "',N'"
                     + anyagnév
                     + "',N'"
                     + mennyiség
                     + "',N'"
                     + egységár
                     + "',N'"
                     + megrendelés_id
                     + "',N'"
                     + áfatartalom
                     + "',N'"
                     + eredeti_mennyiség
                     + "',N'"
                     + dátum
                     + "');";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (SqlException ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        public bool insertBeszállitok(string név, string cím, string adószám, string bankszámlaszám, string kapcsolattartó_neve, string kapcsolattartó_telefonszáma, string kapcsolattartó_email_címe)
        {
            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string query = "INSERT INTO beszallítók(név, cím, adószám, bankszámlaszám, kapcsolattartó_neve, kapcsolattartó_telefonszáma, kapcsolattartó_email_címe)"
                     + " VALUES(N'"
                     + név
                     + "',N'"
                     + cím
                     + "',N'"
                     + adószám
                     + "',N'"
                     + bankszámlaszám
                     + "',N'"
                     + kapcsolattartó_neve
                     + "',N'"
                     + kapcsolattartó_telefonszáma
                     + "',N'"
                     + kapcsolattartó_email_címe
                     + "');";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (SqlException ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        public bool insertUres_rendeles(string beszállító, string anyagnév, string mennyit, string eredeti_mennyiség, string dátum)
        {
            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string query = "INSERT INTO ures_rendeles(beszállító_név, anyagnév, mennyiség, eredeti_mennyiség, dátum)"
                     + " VALUES(N'"
                     + beszállító
                     + "',N'"
                     + anyagnév
                     + "',N'"
                     + mennyit
                     + "',N'"
                     + eredeti_mennyiség
                     + "',N'"
                     + dátum
                     + "');";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (SqlException ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        public bool insertElvisz_ugyfelek(string név, string mit, string mennyit, string mikor, string mennyiteddig)
        {
            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string query = "INSERT INTO elvisz_ugyfelek(név, mit, mennyit, mikor,mennyiteddig)"
                     + " VALUES(N'"
                     + név
                     + "',N'"
                     + mit
                     + "',N'"
                     + mennyit
                     + "',N'"
                     + mikor
                     + "',N'"
                     + mennyiteddig
                     + "');";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (SqlException ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        public bool insertBehoz_beszallito(string név, string mit, string mennyit, string mikor, string mennyiteddig)
        {
            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string query = "INSERT INTO behoz_beszallito(név, mit, mennyit, mikor,mennyiteddig)"
                     + " VALUES(N'"
                     + név
                     + "',N'"
                     + mit
                     + "',N'"
                     + mennyit
                     + "',N'"
                     + mikor
                     + "',N'"
                     + mennyiteddig
                     + "');";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (SqlException ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        public bool insertÖsszesített_ki_mit_rendelt(string név, string mit, string mennyit, string kiszerelés)
        {
            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string query = "INSERT INTO összesített_ki_mit_rendelt(név, mit, mennyit,kiszerelés)"
                     + " VALUES(N'"
                     + név
                     + "',N'"
                     + mit
                     + "',N'"
                     + mennyit
                     + "',N'"
                     + kiszerelés
                     + "');";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (SqlException ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        public bool insertBeszallito_összesített_ki_mit_rendelt(string név, string mit, string mennyit, string kiszerelés)
        {
            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string query = "INSERT INTO beszallito_összesített_ki_mit_rendelt(név, mit, mennyit,kiszerelés)"
                     + " VALUES(N'"
                     + név
                     + "',N'"
                     + mit
                     + "',N'"
                     + mennyit
                     + "',N'"
                     + kiszerelés
                     + "');";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (SqlException ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        public bool insertÜgyfél_Szállítólevél(string miből, string mennyit, string egységár, string áfatartalom)
        {
            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string query = "INSERT INTO szallitolevel_ugyfel(miből, mennyit, egységár, áfatartalom)"
                     + " VALUES(N'"
                     + miből
                     + "',N'"
                     + mennyit
                     + "',N'"
                     + egységár
                     + "',N'"
                     + áfatartalom
                     + "');";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (SqlException ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        public bool insertCég_Szállítólevél(string miből, string mennyit, string egységár, string áfatartalom)
        {
            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string query = "INSERT INTO szallitolevel_ceg(miből, mennyit, egységár, áfatartalom)"
                     + " VALUES(N'"
                     + miből
                     + "',N'"
                     + mennyit
                     + "',N'"
                     + egységár
                     + "',N'"
                     + áfatartalom
                     + "');";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (SqlException ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        #endregion

        #region updat_fvk

        public bool updateÜgyfél(Ügyfelek eredeti, Ügyfelek változtatott)
        {
            try
            {
                connection = new SqlConnection(connectionString);

                connection.Open();

                string query = "UPDATE ügyfelek SET név=N'" + változtatott.teljes_név + "',leánykori_név=N'" + változtatott.leánykori_név + "',lakcím=N'" + változtatott.lakcím + "',anyja_neve=N'" + változtatott.anyja_neve + "',szul_hely=N'" + változtatott.születési_helye + "',szul_ideje=N'" + változtatott.születési_ideje + "',telefonszám=N'" + változtatott.telefonszám + "',email=N'" + változtatott.email + "',adoszam=N'" + változtatott.adószám + "',adóazonosítójel=N'" + változtatott.adóazonosító_jel + "',mvh_regszam=N'" + változtatott.mvh + "',zöldkönyvszám=N'" + változtatott.zöldkártyaszám
                     + "' WHERE " + "név=N'" + eredeti.teljes_név + "' AND leánykori_név=N'" + eredeti.leánykori_név + "' AND lakcím=N'" + eredeti.lakcím + "' AND anyja_neve=N'" + eredeti.anyja_neve + "' AND szul_hely=N'" + eredeti.születési_helye + "' AND szul_ideje=N'" + eredeti.születési_ideje + "' AND telefonszám=N'" + eredeti.telefonszám + "' AND email=N'" + eredeti.email + "' AND adoszam=N'" + eredeti.adószám + "' AND adóazonosítójel=N'" + eredeti.adóazonosító_jel + "' AND mvh_regszam=N'" + eredeti.mvh + "' AND zöldkönyvszám=N'" + eredeti.zöldkártyaszám + "'";

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = query;

                cmd.Connection = connection;

                cmd.ExecuteNonQuery();

                connection.Close();

                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool updateTörzsadat(Törzsadatlista eredeti, Törzsadatlista változtatott)
        {
            try
            {
                connection = new SqlConnection(connectionString);

                connection.Open();

                string query = "UPDATE törzsadat SET anyagnév=N'" + változtatott.anyagnév + "',kiszerelés=N'" + változtatott.kiszerelés + "',főcsoport=N'" + változtatott.főcsoport + "',alcsoport=N'" + változtatott.alcsoport + "',gyártó=N'" + változtatott.gyártó + "',kategória=N'" + változtatott.kategória + "',un_szám=N'" + változtatott.un_szám
                     + "' WHERE " + "anyagnév=N'" + eredeti.anyagnév + "' AND kiszerelés=N'" + eredeti.kiszerelés + "' AND főcsoport=N'" + eredeti.főcsoport + "' AND alcsoport=N'" + eredeti.alcsoport + "' AND gyártó=N'" + eredeti.gyártó + "' AND kategória=N'" + eredeti.kategória + "' AND un_szám=N'" + eredeti.un_szám + "'";

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = query;

                cmd.Connection = connection;

                cmd.ExecuteNonQuery();

                connection.Close();

                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool updateMegrendelések(Megrendelések eredeti, Megrendelések változtatott)
        {
            try
            {
                connection = new SqlConnection(connectionString);

                connection.Open();

                string query = "UPDATE megrendelések SET ügyfélnév=N'" + változtatott.ügyfélnév + "',ügyfélcím=N'" + változtatott.ügyfélcím + "',anyagnév=N'" + változtatott.anyagnév + "',mennyiség=N'" + változtatott.mennyiség + "',egységár=N'" + változtatott.egységár + "',megrendelés_id=N'" + változtatott.megrendelés_id + "',áfatartalom=N'" + változtatott.áfatartalom + "',eredeti_mennyiség=N'" + eredeti.eredeti_mennyiség
                     + "' WHERE " + "ügyfélnév=N'" + eredeti.ügyfélnév + "' AND ügyfélcím=N'" + eredeti.ügyfélcím + "' AND anyagnév=N'" + eredeti.anyagnév + "' AND mennyiség=N'" + eredeti.mennyiség + "' AND egységár=N'" + eredeti.egységár + "' AND megrendelés_id=N'" + eredeti.megrendelés_id + "' AND áfatartalom=N'" + eredeti.áfatartalom + "' AND eredeti_mennyiség=N'" + eredeti.eredeti_mennyiség + "'";

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = query;

                cmd.Connection = connection;

                cmd.ExecuteNonQuery();

                connection.Close();

                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool updateBeszállító(Beszállítók eredeti, Beszállítók változtatott)
        {
            try
            {
                connection = new SqlConnection(connectionString);

                connection.Open();

                string query = "UPDATE beszallítók SET név=N'" + változtatott.név + "',cím=N'" + változtatott.cím + "',adószám=N'" + változtatott.adószám + "',bankszámlaszám=N'" + változtatott.bankszámlaszám + "',kapcsolattartó_neve=N'" + változtatott.kapcsolattartó_neve + "',kapcsolattartó_telefonszáma=N'" + változtatott.kapcsolattartó_telefonszáma + "',kapcsolattartó_email_címe=N'" + változtatott.kapcsolattartó_email_címe
                     + "' WHERE " + "név=N'" + eredeti.név + "' AND cím=N'" + eredeti.cím + "' AND adószám=N'" + eredeti.adószám + "' AND bankszámlaszám=N'" + eredeti.bankszámlaszám + "' AND kapcsolattartó_neve=N'" + eredeti.kapcsolattartó_neve + "' AND kapcsolattartó_telefonszáma=N'" + eredeti.kapcsolattartó_telefonszáma + "' AND kapcsolattartó_email_címe=N'" + eredeti.kapcsolattartó_email_címe + "'";

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = query;

                cmd.Connection = connection;

                cmd.ExecuteNonQuery();

                connection.Close();

                return true;
            }
            catch (SqlException ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        public bool updateRaktár(Raktár eredeti, Raktár változtatott)
        {
            try
            {
                connection = new SqlConnection(connectionString);

                connection.Open();

                string query = "UPDATE raktár SET anyagnév=N'" + változtatott.anyagnév + "',mennyiség=N'" + változtatott.mennyiség
                     + "' WHERE " + "anyagnév=N'" + eredeti.anyagnév + "' AND mennyiség=N'" + eredeti.mennyiség + "'";

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = query;

                cmd.Connection = connection;

                cmd.ExecuteNonQuery();

                connection.Close();

                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool updateMennyiszallitoleveleddig(int eredetimennyi, int változtatottmennyi)
        {
            try
            {
                connection = new SqlConnection(connectionString);

                connection.Open();

                string query = "UPDATE mennyiszallitoleveleddig SET mennyi=N'" + változtatottmennyi
                     + "' WHERE " + "mennyi=N'" + eredetimennyi + "'";

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = query;

                cmd.Connection = connection;

                cmd.ExecuteNonQuery();

                connection.Close();

                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool updateUres_rendeles(Ures_rendeles eredeti, Ures_rendeles változtatott)
        {
            try
            {
                connection = new SqlConnection(connectionString);

                connection.Open();

                string query = "UPDATE ures_rendeles SET beszállító_név=N'" + változtatott.beszállító + "',anyagnév = N'" + változtatott.anyagnév + "',mennyiség = N'" + változtatott.mennyiség + "',eredeti_mennyiség = N'" + változtatott.eredeti_mennyiség
                       + "' WHERE " + "beszállító_név=N'" + eredeti.beszállító + "' AND anyagnév = N'" + eredeti.anyagnév + "' AND mennyiség = N'" + eredeti.mennyiség + "' AND eredeti_mennyiség = N'" + eredeti.eredeti_mennyiség + "'";

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = query;

                cmd.Connection = connection;

                cmd.ExecuteNonQuery();

                connection.Close();

                return true;
            }
            catch (SqlException ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        public bool updateÖsszesített_ki_mit_rendelt(Összesített_ki_mit_rendelt eredeti, Összesített_ki_mit_rendelt változtatott)
        {
            try
            {
                connection = new SqlConnection(connectionString);

                connection.Open();

                string query = "UPDATE összesített_ki_mit_rendelt SET név=N'" + változtatott.név + "',mit = N'" + változtatott.mit + "',mennyit = N'" + változtatott.mennyit + "',kiszerelés = N'" + változtatott.kiszerelés
                       + "' WHERE " + "név=N'" + eredeti.név + "' AND mit = N'" + eredeti.mit + "' AND mennyit = N'" + eredeti.mennyit + "' AND kiszerelés = N'" + eredeti.kiszerelés + "'";

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = query;

                cmd.Connection = connection;

                cmd.ExecuteNonQuery();

                connection.Close();

                return true;
            }
            catch (SqlException ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        public bool updateBeszallito_Összesített_ki_mit_rendelt(Beszallito_Összesített_ki_mit_rendelt eredeti, Beszallito_Összesített_ki_mit_rendelt változtatott)
        {
            try
            {
                connection = new SqlConnection(connectionString);

                connection.Open();

                string query = "UPDATE beszallito_összesített_ki_mit_rendelt SET név=N'" + változtatott.név + "',mit = N'" + változtatott.mit + "',mennyit = N'" + változtatott.mennyit + "',kiszerelés = N'" + változtatott.kiszerelés
                       + "' WHERE " + "név=N'" + eredeti.név + "' AND mit = N'" + eredeti.mit + "' AND mennyit = N'" + eredeti.mennyit + "' AND kiszerelés = N'" + eredeti.kiszerelés + "'";

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = query;

                cmd.Connection = connection;

                cmd.ExecuteNonQuery();

                connection.Close();

                return true;
            }
            catch (SqlException ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        #endregion

        #region delete_fvk

        public bool deleteÜgyfél(Ügyfelek eredeti)
        {
            try
            {
                connection = new SqlConnection(connectionString);

                connection.Open();

                string query = "DELETE FROM ügyfelek  WHERE " + "név= N'" + eredeti.teljes_név + "' AND leánykori_név= N'" + eredeti.leánykori_név + "' AND lakcím= N'" + eredeti.lakcím + "' AND anyja_neve= N'" + eredeti.anyja_neve + "' AND szul_hely= N'" + eredeti.születési_helye + "' AND szul_ideje= N'" + eredeti.születési_ideje + "' AND telefonszám= N'" + eredeti.telefonszám + "' AND email= N'" + eredeti.email + "' AND adoszam= N'" + eredeti.adószám + "' AND adóazonosítójel= N'" + eredeti.adóazonosító_jel + "' AND mvh_regszam= N'" + eredeti.mvh + "' AND zöldkönyvszám= N'" + eredeti.zöldkártyaszám + "'";

                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.ExecuteNonQuery();

                connection.Close();

                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool deleteTörzsadat(Törzsadatlista eredeti)
        {
            try
            {
                connection = new SqlConnection(connectionString);

                connection.Open();

                string query = "DELETE FROM törzsadat WHERE " + "anyagnév=N'" + eredeti.anyagnév + "' AND kiszerelés=N'" + eredeti.kiszerelés + "' AND főcsoport=N'" + eredeti.főcsoport + "' AND alcsoport=N'" + eredeti.alcsoport + "' AND gyártó=N'" + eredeti.gyártó + "' AND kategória=N'" + eredeti.kategória + "' AND un_szám=N'" + eredeti.un_szám + "'";

                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.ExecuteNonQuery();

                connection.Close();

                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool deleteBeszállító(Beszállítók eredeti)
        {
            try
            {
                connection = new SqlConnection(connectionString);

                connection.Open();

                string query = "DELETE FROM beszallítók WHERE " + "név=N'" + eredeti.név + "' AND cím=N'" + eredeti.cím + "' AND adószám=N'" + eredeti.adószám + "' AND bankszámlaszám=N'" + eredeti.bankszámlaszám + "' AND kapcsolattartó_neve=N'" + eredeti.kapcsolattartó_neve + "' AND kapcsolattartó_telefonszáma=N'" + eredeti.kapcsolattartó_telefonszáma + "' AND kapcsolattartó_email_címe=N'" + eredeti.kapcsolattartó_email_címe + "'";

                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.ExecuteNonQuery();

                connection.Close();

                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool deleteUres_rendeles(Ures_rendeles eredeti)
        {
            try
            {
                connection = new SqlConnection(connectionString);

                connection.Open();

                string query = "DELETE FROM ures_rendeles WHERE " + "beszállító_név=N'" + eredeti.beszállító + "' AND anyagnév = N'" + eredeti.anyagnév + "' AND mennyiség = N'" + eredeti.mennyiség + "' AND eredeti_mennyiség = N'" + eredeti.eredeti_mennyiség + "'";

                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.ExecuteNonQuery();

                connection.Close();

                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool deleteÖsszesített_ki_mit_rendelt(Összesített_ki_mit_rendelt osszesitett)
        {
            try
            {
                connection = new SqlConnection(connectionString);

                connection.Open();

                string query = "DELETE FROM összesített_ki_mit_rendelt WHERE " + "név=N'" + osszesitett.név + "' AND mit = N'" + osszesitett.mit + "' AND mennyit = N'" + osszesitett.mennyit + "' AND kiszerelés = N'" + osszesitett.kiszerelés + "'";

                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.ExecuteNonQuery();

                connection.Close();

                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool deleteBeszállító_Összesített_ki_mit_rendelt(Beszallito_Összesített_ki_mit_rendelt beszall)
        {
            try
            {
                connection = new SqlConnection(connectionString);

                connection.Open();

                string query = "DELETE FROM beszallito_összesített_ki_mit_rendelt WHERE " + "név=N'" + beszall.név + "' AND mit = N'" + beszall.mit + "' AND mennyit = N'" + beszall.mennyit + "' AND kiszerelés = N'" + beszall.kiszerelés + "'";

                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.ExecuteNonQuery();

                connection.Close();

                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool deleteBehoz_beszállító(Behoz_beszallito behoz)
        {
            try
            {
                connection = new SqlConnection(connectionString);

                connection.Open();

                string query = "DELETE FROM behoz_beszallito WHERE " + "név=N'" + behoz.név + "' AND mit = N'" + behoz.mit + "' AND mennyit = N'" + behoz.mennyit + "'";

                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.ExecuteNonQuery();

                connection.Close();

                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool deleteElvisz_ugyfelek(Elvisz_ugyfelek elvisz)
        {
            try
            {
                connection = new SqlConnection(connectionString);

                connection.Open();

                string query = "DELETE FROM elvisz_ugyfelek WHERE " + "név=N'" + elvisz.név + "' AND mit = N'" + elvisz.mit + "' AND mennyit = N'" + elvisz.mennyit + "'";

                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.ExecuteNonQuery();

                connection.Close();

                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool deleteÜgyfél_Szállítólevél()
        {
            try
            {
                connection = new SqlConnection(connectionString);

                connection.Open();

                string query = "DELETE FROM szallitolevel_ugyfel ";

                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.ExecuteNonQuery();

                connection.Close();

                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool deleteCég_Szállítólevél()
        {
            try
            {
                connection = new SqlConnection(connectionString);

                connection.Open();

                string query = "DELETE FROM szallitolevel_ceg ";

                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.ExecuteNonQuery();

                connection.Close();

                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }
        
        #endregion
    }
}
