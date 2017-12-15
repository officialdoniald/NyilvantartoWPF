using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace NyilvántartóWPF.Pages
{
    public partial class Listák : Page
    {
        #region attribútumok

        ObservableCollection<Ügyfelek> ügyfelek = new ObservableCollection<Ügyfelek>();

        ObservableCollection<Törzsadatlista> törzsadatlista = new ObservableCollection<Törzsadatlista>();

        ObservableCollection<Beszállítók> beszállítók = new ObservableCollection<Beszállítók>();

        ObservableCollection<Megrendelések> megrendelések = new ObservableCollection<Megrendelések>();

        ObservableCollection<Behoz_beszallito> behozott_termékek_a_beszállítóktól = new ObservableCollection<Behoz_beszallito>();

        ObservableCollection<Elvisz_ugyfelek> elvitt_termékek_ügyfelek = new ObservableCollection<Elvisz_ugyfelek>();

        ObservableCollection<mik> mik_anyagok = new ObservableCollection<mik>();
        ObservableCollection<mik> mik_főcsoport = new ObservableCollection<mik>();
        ObservableCollection<mik> mik_gyártó = new ObservableCollection<mik>();

        NyilvantartoDAODBImpl db_implements = new NyilvantartoDAODBImpl();

        string ügyfélnév = "";

        string beszállítónév = "";

        string[] szuro_combobox_tomb = new string[4] { "", "anyag", "főcsoport", "gyártó" };

        string selected_szuro_combobox = "";

        string ésmitt = "";

        #endregion

        #region init
        public Listák()
        {
            InitializeComponent();

            ügyfelek = db_implements.getÜgyfelek();

            var valami = ügyfelek.OrderBy(a => a.teljes_név);

            ugyfelek_listazasa_listbox.ItemsSource = valami;

            beszállítók = db_implements.getBeszállítók();

            var valami2 = beszállítók.OrderBy(a => a.név);

            beszallitok_listazasa_listbox.ItemsSource = valami2;

            foreach (var item in db_implements.getTörzsadlista().OrderBy(a => a.anyagnév))
            {
                mik mi = new mik();
                mi.mi = item.anyagnév;
                mik_anyagok.Add(mi);
            }
            string volte = "";
            foreach (var item in db_implements.getTörzsadlista().OrderBy(a => a.főcsoport))
            {
                if (volte != item.főcsoport)
                {
                    volte = item.főcsoport;
                    mik mi = new mik();
                    mi.mi = volte;
                    mik_főcsoport.Add(mi);
                }
            }
            volte = "";
            foreach (var item in db_implements.getTörzsadlista().OrderBy(a => a.gyártó))
            {
                if (volte != item.gyártó)
                {
                    volte = item.gyártó;
                    mik mi = new mik();
                    mi.mi = volte;
                    mik_gyártó.Add(mi);
                }
            }
        }
        #endregion

        #region clicks

        /// <summary>
        /// Ki miből mennyit rendelt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void elso_button_Click(object sender, RoutedEventArgs e)
        {
            BaseFont MySpecialBaseFont = iTextSharp.text.pdf.BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.EMBEDDED);

            iTextSharp.text.Font MySpecialCharfont = new iTextSharp.text.Font(MySpecialBaseFont);

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10f, 10f, 20f, 0);

            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("test.pdf", System.IO.FileMode.Create));

            doc.Open();

            PdfPTable table = new PdfPTable(3);

            var header_font = FontFactory.GetFont("Times New Roman", 30, BaseColor.BLACK);
            var alap_font = FontFactory.GetFont("Times New Roman", 12, BaseColor.BLACK);

            iTextSharp.text.Paragraph header = new iTextSharp.text.Paragraph(new Phrase("Ki miből mennyit rendelt", new iTextSharp.text.Font(header_font)));

            header.Alignment = 1;

            doc.Add(header);
            doc.Add(new iTextSharp.text.Paragraph(" "));

            Phrase datum_phrase = new Phrase("Ki", MySpecialCharfont);
            PdfPCell datum_cella = new PdfPCell(datum_phrase);
            datum_cella.HorizontalAlignment = 1;

            table.AddCell(datum_cella);

            Phrase mit_phrase = new Phrase("Mit", MySpecialCharfont);
            PdfPCell mit_cella = new PdfPCell(mit_phrase);
            mit_cella.HorizontalAlignment = 1;

            table.AddCell(mit_cella);

            Phrase mennyit_phrase = new Phrase("Mennyit", MySpecialCharfont);
            PdfPCell mennyit_cella = new PdfPCell(mennyit_phrase);
            mit_cella.HorizontalAlignment = 1;

            table.AddCell(mennyit_cella);

            ObservableCollection<Összesített_ki_mit_rendelt> öszzsesített_mi_mennyit = new ObservableCollection<Összesített_ki_mit_rendelt>();

            öszzsesített_mi_mennyit = db_implements.getÖsszesített_ki_mit_rendelt();

            var valami = öszzsesített_mi_mennyit.OrderBy(a => a.név);

            string név = "";

            foreach (var item in valami)
            {
                if (név == item.név)
                {
                    if (ésmitt != "")
                    {
                        foreach (var item2 in db_implements.getTörzsadlista())
                        {
                            if (item.mit == item2.anyagnév && (item2.anyagnév == ésmitt || item2.gyártó == ésmitt || item2.főcsoport == ésmitt))
                            {
                                table.AddCell("");
                                table.AddCell(item.mit);
                                string kiszereléssel = "";
                                kiszereléssel += item.mennyit + " " + item.kiszerelés;
                                table.AddCell(kiszereléssel);
                                break;
                            }
                        }
                    }
                    else
                    {
                        table.AddCell("");
                        table.AddCell(item.mit);
                        string kiszereléssel = "";
                        kiszereléssel += item.mennyit + " " + item.kiszerelés;
                        table.AddCell(kiszereléssel);
                    }

                }
                else
                {
                    if (ésmitt != "")
                    {
                        foreach (var item2 in db_implements.getTörzsadlista())
                        {
                            if (item.mit == item2.anyagnév && (item2.anyagnév == ésmitt || item2.gyártó == ésmitt || item2.főcsoport == ésmitt) || (ésmitt == ""))
                            {
                                név = item.név;
                                table.AddCell(név);
                                table.AddCell(item.mit);
                                string kiszereléssel = "";
                                kiszereléssel += item.mennyit + " " + item.kiszerelés;
                                table.AddCell(kiszereléssel);
                                break;
                            }
                        }
                    }
                    else
                    {
                        név = item.név;
                        table.AddCell(név);
                        table.AddCell(item.mit);
                        string kiszereléssel = "";
                        kiszereléssel += item.mennyit + " " + item.kiszerelés;
                        table.AddCell(kiszereléssel);
                    }
                }
            }

            doc.Add(table);

            doc.Close();

            Process.Start("test.pdf");
        }

        /// <summary>
        /// Miből ki mennyit rendelt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void masodik_button_Click(object sender, RoutedEventArgs e)
        {
            BaseFont MySpecialBaseFont = iTextSharp.text.pdf.BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.EMBEDDED);

            iTextSharp.text.Font MySpecialCharfont = new iTextSharp.text.Font(MySpecialBaseFont);

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10f, 10f, 20f, 0);

            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("test.pdf", System.IO.FileMode.Create));

            doc.Open();

            PdfPTable table = new PdfPTable(3);

            var header_font = FontFactory.GetFont("Times New Roman", 30, BaseColor.BLACK);
            var alap_font = FontFactory.GetFont("Times New Roman", 12, BaseColor.BLACK);

            iTextSharp.text.Paragraph header = new iTextSharp.text.Paragraph(new Phrase("Miből ki mennyit rendelt", new iTextSharp.text.Font(header_font)));

            header.Alignment = 1;

            doc.Add(header);
            doc.Add(new iTextSharp.text.Paragraph(" "));

            Phrase datum_phrase = new Phrase("Mit", MySpecialCharfont);
            PdfPCell datum_cella = new PdfPCell(datum_phrase);
            datum_cella.HorizontalAlignment = 1;

            table.AddCell(datum_cella);

            Phrase mit_phrase = new Phrase("Ki", MySpecialCharfont);
            PdfPCell mit_cella = new PdfPCell(mit_phrase);
            mit_cella.HorizontalAlignment = 1;

            table.AddCell(mit_cella);

            Phrase mennyit_phrase = new Phrase("Mennyit", MySpecialCharfont);
            PdfPCell mennyit_cella = new PdfPCell(mennyit_phrase);
            mit_cella.HorizontalAlignment = 1;

            table.AddCell(mennyit_cella);

            ObservableCollection<Összesített_ki_mit_rendelt> öszzsesített_mi_mennyit = new ObservableCollection<Összesített_ki_mit_rendelt>();

            öszzsesített_mi_mennyit = db_implements.getÖsszesített_ki_mit_rendelt();

            var valami = öszzsesített_mi_mennyit.OrderBy(a => a.mit);

            string anyagnév = "";

            double ossz = 0;
            int index = 1;
            foreach (var item in valami)
            {
                if (anyagnév == item.mit)
                {
                    if (ésmitt != "")
                    {
                        foreach (var item2 in db_implements.getTörzsadlista())
                        {
                            if (item.mit == item2.anyagnév && (item2.anyagnév == ésmitt || item2.gyártó == ésmitt || item2.főcsoport == ésmitt) || (ésmitt == ""))
                            {
                                table.AddCell("");
                                table.AddCell(item.név);
                                string kiszereléssel = "";
                                kiszereléssel += item.mennyit + " " + item.kiszerelés;
                                ossz += Convert.ToDouble(item.mennyit);
                                table.AddCell(kiszereléssel);
                                break;
                            }
                        }
                    }
                    else
                    {
                        table.AddCell("");
                        table.AddCell(item.név);
                        string kiszereléssel = "";
                        kiszereléssel += item.mennyit + " " + item.kiszerelés;
                        ossz += Convert.ToDouble(item.mennyit);
                        table.AddCell(kiszereléssel);
                    }
                }
                else
                {
                    if (ossz != 0)
                    {
                        table.AddCell("");
                        table.AddCell("Összesen:");
                        table.AddCell(Convert.ToString(ossz));
                        ossz = 0;
                    }
                    if (ésmitt != "")
                    {
                        foreach (var item2 in db_implements.getTörzsadlista())
                        {
                            if (item.mit == item2.anyagnév && (item2.anyagnév == ésmitt || item2.gyártó == ésmitt || item2.főcsoport == ésmitt) || (ésmitt == ""))
                            {
                                ossz += Convert.ToDouble(item.mennyit);
                                anyagnév = item.mit;
                                table.AddCell(anyagnév);
                                table.AddCell(item.név);
                                string kiszereléssel = "";
                                kiszereléssel += item.mennyit + " " + item.kiszerelés;
                                table.AddCell(kiszereléssel);
                                break;
                            }
                        }
                    }
                    else
                    {
                        ossz += Convert.ToDouble(item.mennyit);
                        anyagnév = item.mit;
                        table.AddCell(anyagnév);
                        table.AddCell(item.név);
                        string kiszereléssel = "";
                        kiszereléssel += item.mennyit + " " + item.kiszerelés;
                        table.AddCell(kiszereléssel);

                    }
                }
                if (index == valami.Count() && ossz != 0)
                {
                    table.AddCell("");
                    table.AddCell("Összesen:");
                    table.AddCell(Convert.ToString(ossz));
                }
                else
                {
                    index++;
                }
            }

            doc.Add(table);

            doc.Close();

            Process.Start("test.pdf");
        }

        /// <summary>
        /// Beszállító miből mennyit rendeltetek
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void harmadik_button_Click(object sender, RoutedEventArgs e)
        {
            BaseFont MySpecialBaseFont = iTextSharp.text.pdf.BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.EMBEDDED);

            iTextSharp.text.Font MySpecialCharfont = new iTextSharp.text.Font(MySpecialBaseFont);

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10f, 10f, 20f, 0);

            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("test.pdf", System.IO.FileMode.Create));

            doc.Open();

            PdfPTable table = new PdfPTable(3);

            var header_font = FontFactory.GetFont("Times New Roman", 30, BaseColor.BLACK);
            var alap_font = FontFactory.GetFont("Times New Roman", 12, BaseColor.BLACK);

            iTextSharp.text.Paragraph header = new iTextSharp.text.Paragraph(new Phrase("Beszállítótól miből mennyit rendeltetek", new iTextSharp.text.Font(header_font)));

            header.Alignment = 1;

            doc.Add(header);
            doc.Add(new iTextSharp.text.Paragraph(" "));

            Phrase datum_phrase = new Phrase("Beszállító", MySpecialCharfont);
            PdfPCell datum_cella = new PdfPCell(datum_phrase);
            datum_cella.HorizontalAlignment = 1;

            table.AddCell(datum_cella);

            Phrase mit_phrase = new Phrase("Mit", MySpecialCharfont);
            PdfPCell mit_cella = new PdfPCell(mit_phrase);
            mit_cella.HorizontalAlignment = 1;

            table.AddCell(mit_cella);

            Phrase mennyit_phrase = new Phrase("Mennyit", MySpecialCharfont);
            PdfPCell mennyit_cella = new PdfPCell(mennyit_phrase);
            mit_cella.HorizontalAlignment = 1;

            table.AddCell(mennyit_cella);

            ObservableCollection<Beszallito_Összesített_ki_mit_rendelt> öszzsesített_mi_mennyit = new ObservableCollection<Beszallito_Összesített_ki_mit_rendelt>();

            öszzsesített_mi_mennyit = db_implements.getBeszállító_Összesített_ki_mit_rendelt();

            var valami = öszzsesített_mi_mennyit.OrderBy(a => a.név);

            string név = "";

            foreach (var item in valami)
            {
                if (név == item.név)
                {
                    if (ésmitt != "")
                    {
                        foreach (var item2 in db_implements.getTörzsadlista())
                        {
                            if (item.mit == item2.anyagnév && (item2.anyagnév == ésmitt || item2.gyártó == ésmitt || item2.főcsoport == ésmitt) || (ésmitt == ""))
                            {
                                table.AddCell("");
                                table.AddCell(item.mit);
                                string kiszereléssel = "";
                                kiszereléssel += item.mennyit + " " + item.kiszerelés;
                                table.AddCell(kiszereléssel);
                                break;
                            }
                        }
                    }
                    else
                    {
                        table.AddCell("");
                        table.AddCell(item.mit);
                        string kiszereléssel = "";
                        kiszereléssel += item.mennyit + " " + item.kiszerelés;
                        table.AddCell(kiszereléssel);
                    }
                }
                else
                {
                    if (ésmitt != "")
                    {
                        foreach (var item2 in db_implements.getTörzsadlista())
                        {
                            if (item.mit == item2.anyagnév && (item2.anyagnév == ésmitt || item2.gyártó == ésmitt || item2.főcsoport == ésmitt) || (ésmitt == ""))
                            {
                                név = item.név;
                                table.AddCell(név);
                                table.AddCell(item.mit);
                                string kiszereléssel = "";
                                kiszereléssel += item.mennyit + " " + item.kiszerelés;
                                table.AddCell(kiszereléssel);
                                break;
                            }
                        }
                    }
                    else
                    {
                        név = item.név;
                        table.AddCell(név);
                        table.AddCell(item.mit);
                        string kiszereléssel = "";
                        kiszereléssel += item.mennyit + " " + item.kiszerelés;
                        table.AddCell(kiszereléssel);
                    }
                }
            }

            doc.Add(table);

            doc.Close();

            Process.Start("test.pdf");
        }

        /// <summary>
        /// Miből mennyit hoztak be a beszállítók
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hatodik_button_Click(object sender, RoutedEventArgs e)
        {
            BaseFont MySpecialBaseFont = iTextSharp.text.pdf.BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.EMBEDDED);

            iTextSharp.text.Font MySpecialCharfont = new iTextSharp.text.Font(MySpecialBaseFont);

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10f, 10f, 20f, 0);

            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("test.pdf", System.IO.FileMode.Create));

            doc.Open();

            PdfPTable table = new PdfPTable(3);

            var header_font = FontFactory.GetFont("Times New Roman", 30, BaseColor.BLACK);
            var alap_font = FontFactory.GetFont("Times New Roman", 12, BaseColor.BLACK);

            iTextSharp.text.Paragraph header = new iTextSharp.text.Paragraph(new Phrase("Miből mennyit hoztak be a beszállítók", new iTextSharp.text.Font(header_font)));

            header.Alignment = 1;

            doc.Add(header);
            doc.Add(new iTextSharp.text.Paragraph(" "));

            Phrase datum_phrase = new Phrase("Mit", MySpecialCharfont);
            PdfPCell datum_cella = new PdfPCell(datum_phrase);
            datum_cella.HorizontalAlignment = 1;

            table.AddCell(datum_cella);

            Phrase mit_phrase = new Phrase("Beszállító", MySpecialCharfont);
            PdfPCell mit_cella = new PdfPCell(mit_phrase);
            mit_cella.HorizontalAlignment = 1;

            table.AddCell(mit_cella);

            Phrase mennyit_phrase = new Phrase("Mennyit", MySpecialCharfont);
            PdfPCell mennyit_cella = new PdfPCell(mennyit_phrase);
            mit_cella.HorizontalAlignment = 1;

            table.AddCell(mennyit_cella);

            ObservableCollection<Beszallito_Összesített_ki_mit_rendelt> öszzsesített_mi_mennyit = new ObservableCollection<Beszallito_Összesített_ki_mit_rendelt>();

            öszzsesített_mi_mennyit = db_implements.getBeszállító_Összesített_ki_mit_rendelt();

            var valami = öszzsesített_mi_mennyit.OrderBy(a => a.mit);

            string anyagnév = "";

            int index = 1;

            double ossz = 0;

            foreach (var item in valami)
            {
                if (anyagnév == item.mit)
                {
                    if (ésmitt != "")
                    {
                        foreach (var item2 in db_implements.getTörzsadlista())
                        {
                            if (item.mit == item2.anyagnév && (item2.anyagnév == ésmitt || item2.gyártó == ésmitt || item2.főcsoport == ésmitt) || (ésmitt == ""))
                            {
                                table.AddCell("");
                                table.AddCell(item.név);
                                string kiszereléssel = "";
                                kiszereléssel += item.mennyit + " " + item.kiszerelés;
                                ossz += Convert.ToDouble(item.mennyit);
                                table.AddCell(kiszereléssel);
                            }
                        }
                    }
                    else
                    {
                        table.AddCell("");
                        table.AddCell(item.név);
                        string kiszereléssel = "";
                        kiszereléssel += item.mennyit + " " + item.kiszerelés;
                        ossz += Convert.ToDouble(item.mennyit);
                        table.AddCell(kiszereléssel);
                    }
                }
                else
                {
                    if (ossz != 0)
                    {
                        table.AddCell("");
                        table.AddCell("Összesen:");
                        table.AddCell(Convert.ToString(ossz));
                        ossz = 0;
                    }
                    if (ésmitt != "")
                    {
                        foreach (var item2 in db_implements.getTörzsadlista())
                    {
                        if (item.mit == item2.anyagnév && (item2.anyagnév == ésmitt || item2.gyártó == ésmitt || item2.főcsoport == ésmitt) || (ésmitt == ""))
                        {
                            ossz += Convert.ToDouble(item.mennyit);
                            anyagnév = item.mit;
                            table.AddCell(anyagnév);
                            table.AddCell(item.név);
                            string kiszereléssel = "";
                            kiszereléssel += item.mennyit + " " + item.kiszerelés;
                            table.AddCell(kiszereléssel);
                            }
                        }
                    }
                    else
                    {
                        ossz += Convert.ToDouble(item.mennyit);
                        anyagnév = item.mit;
                        table.AddCell(anyagnév);
                        table.AddCell(item.név);
                        string kiszereléssel = "";
                        kiszereléssel += item.mennyit + " " + item.kiszerelés;
                        table.AddCell(kiszereléssel);

                    }
                }
                if (index == valami.Count() && ossz != 0)
                {
                    table.AddCell("");
                    table.AddCell("Összesen:");
                    table.AddCell(Convert.ToString(ossz));
                }
                else
                {
                    index++;
                }
            }

            doc.Add(table);

            doc.Close();

            Process.Start("test.pdf");
        }

        /// <summary>
        /// Ugyfelkarton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void negyedik_button_Click(object sender, RoutedEventArgs e)
        {
            BaseFont MySpecialBaseFont = iTextSharp.text.pdf.BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.EMBEDDED);

            iTextSharp.text.Font MySpecialCharfont = new iTextSharp.text.Font(MySpecialBaseFont);

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10f, 10f, 20f, 0);

            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("test.pdf", System.IO.FileMode.Create));

            doc.Open();

            PdfPTable table = new PdfPTable(4);

            var header_font = FontFactory.GetFont("Times New Roman", 30, BaseColor.BLACK);
            var alap_font = FontFactory.GetFont("Times New Roman", 12, BaseColor.BLACK);

            iTextSharp.text.Paragraph header = new iTextSharp.text.Paragraph(new Phrase("Ügyfélkarton", new iTextSharp.text.Font(header_font)));

            header.Alignment = 1;

            doc.Add(header);
            doc.Add(new iTextSharp.text.Paragraph(" "));

            Phrase anyag_phrase = new Phrase("Anyag", MySpecialCharfont);
            PdfPCell anyag_cella = new PdfPCell(anyag_phrase);
            anyag_cella.HorizontalAlignment = 1;

            table.AddCell(anyag_cella);

            Phrase datum_phrase = new Phrase("Megrendelés", MySpecialCharfont);
            PdfPCell datum_cella = new PdfPCell(datum_phrase);
            datum_cella.HorizontalAlignment = 1;

            table.AddCell(datum_cella);

            Phrase mit_phrase = new Phrase("Kiszállítás", MySpecialCharfont);
            PdfPCell mit_cella = new PdfPCell(mit_phrase);
            mit_cella.HorizontalAlignment = 1;

            table.AddCell(mit_cella);

            Phrase mennyit_phrase = new Phrase("Készlet", MySpecialCharfont);
            PdfPCell mennyit_cella = new PdfPCell(mennyit_phrase);
            mit_cella.HorizontalAlignment = 1;

            table.AddCell(mennyit_cella);

            ObservableCollection<Elvisz_ugyfelek> elvisz = new ObservableCollection<Elvisz_ugyfelek>();

            List<string> amiket_elvitt = new List<string>();

            if (string.IsNullOrEmpty(tol_textbox.Text))
            {
                elvisz = db_implements.getElvisz_ugyfelek(0,0);
            }
            else
            {
                elvisz = db_implements.getElvisz_ugyfelek(Convert.ToInt32(tol_textbox.Text), Convert.ToInt32(ig_textbox.Text));
            }

            var valami = elvisz.OrderBy(a => a.név);
            
            table.AddCell(ügyfélnév);
            table.AddCell("");
            table.AddCell("");
            table.AddCell("");

            foreach (var item in valami)
            {
                if (ügyfélnév == item.név)
                {
                    amiket_elvitt.Add(item.mit);
                    string kiszerelés = "";
                    foreach (var item2 in db_implements.getTörzsadlista())
                    {
                        if (item.mit == item2.anyagnév)
                        {
                            kiszerelés = item2.kiszerelés;
                        }
                    }
                    table.AddCell(item.mit);
                    string mit = "";
                    mit = item.mennyiteddig + " " + kiszerelés;
                    table.AddCell(mit);
                    mit = "";
                    mit = item.mennyit + " " + kiszerelés;
                    table.AddCell(mit);
                    mit = "";
                    mit = Convert.ToString(Convert.ToInt32(item.mennyiteddig) - Convert.ToInt32(item.mennyit)) + " " + kiszerelés;
                    table.AddCell(mit);
                }
            }
            
            foreach (var item in db_implements.getÖsszesített_ki_mit_rendelt().OrderBy(a => a.név))
            {
                foreach (var item2 in amiket_elvitt)
                {
                    if (item.mit != item2 && item.név == ügyfélnév)
                    {
                        table.AddCell(item.mit);
                        string kiszerelés = "";
                        foreach (var item3 in db_implements.getTörzsadlista())
                        {
                            if (item.mit == item3.anyagnév)
                            {
                                kiszerelés = item3.kiszerelés;
                            }
                        }
                        string mit = "";
                        mit = item.mennyit + " " + kiszerelés;
                        table.AddCell(mit);
                        table.AddCell("0");
                        table.AddCell(mit);
                        break;
                    }
                }
            }

            doc.Add(table);

            doc.Close();

            Process.Start("test.pdf");
        }

        /// <summary>
        /// Szallitokarton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void otodik_button_Click(object sender, RoutedEventArgs e)
        {
            BaseFont MySpecialBaseFont = iTextSharp.text.pdf.BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.EMBEDDED);

            iTextSharp.text.Font MySpecialCharfont = new iTextSharp.text.Font(MySpecialBaseFont);

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10f, 10f, 20f, 0);

            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("test.pdf", System.IO.FileMode.Create));

            doc.Open();

            PdfPTable table = new PdfPTable(4);

            var header_font = FontFactory.GetFont("Times New Roman", 30, BaseColor.BLACK);
            var alap_font = FontFactory.GetFont("Times New Roman", 12, BaseColor.BLACK);

            iTextSharp.text.Paragraph header = new iTextSharp.text.Paragraph(new Phrase("Ügyfélkarton csak ez cégnél", new iTextSharp.text.Font(header_font)));

            header.Alignment = 1;

            doc.Add(header);
            doc.Add(new iTextSharp.text.Paragraph(" "));

            Phrase anyag_phrase = new Phrase("Anyag", MySpecialCharfont);
            PdfPCell anyag_cella = new PdfPCell(anyag_phrase);
            anyag_cella.HorizontalAlignment = 1;

            table.AddCell(anyag_cella);

            Phrase datum_phrase = new Phrase("Megrendelés", MySpecialCharfont);
            PdfPCell datum_cella = new PdfPCell(datum_phrase);
            datum_cella.HorizontalAlignment = 1;

            table.AddCell(datum_cella);

            Phrase mit_phrase = new Phrase("Kiszállítás", MySpecialCharfont);
            PdfPCell mit_cella = new PdfPCell(mit_phrase);
            mit_cella.HorizontalAlignment = 1;

            table.AddCell(mit_cella);

            Phrase mennyit_phrase = new Phrase("Készlet", MySpecialCharfont);
            PdfPCell mennyit_cella = new PdfPCell(mennyit_phrase);
            mit_cella.HorizontalAlignment = 1;

            table.AddCell(mennyit_cella);

            ObservableCollection<Behoz_beszallito> elvisz = new ObservableCollection<Behoz_beszallito>();

            List<string> amiket_elvitt = new List<string>();

            if (string.IsNullOrEmpty(tol_textbox.Text))
            {
                elvisz = db_implements.getBehoz_beszallito(0,0);
            }
            else
            {
                elvisz = db_implements.getBehoz_beszallito(Convert.ToInt32(tol_textbox.Text), Convert.ToInt32(ig_textbox.Text));
            }

            var valami = elvisz.OrderBy(a => a.név);

            table.AddCell(beszállítónév);
            table.AddCell("");
            table.AddCell("");
            table.AddCell("");

            foreach (var item in valami)
            {
                if (beszállítónév == item.név)
                {
                    amiket_elvitt.Add(item.név);
                    string kiszerelés = "";
                    foreach (var item2 in db_implements.getTörzsadlista())
                    {
                        if (item.mit == item2.anyagnév)
                        {
                            kiszerelés = item2.kiszerelés;
                        }
                    }
                    table.AddCell(item.mit);
                    string mit = "";
                    mit = item.mennyiteddig + " " + kiszerelés;
                    table.AddCell(mit);
                    mit = "";
                    mit = item.mennyit + " " + kiszerelés;
                    table.AddCell(mit);
                    mit = "";
                    mit = Convert.ToString(Convert.ToInt32(item.mennyiteddig) - Convert.ToInt32(item.mennyit)) + " " + kiszerelés;
                    table.AddCell(mit);
                }
            }

            foreach (var item in db_implements.getBeszállító_Összesített_ki_mit_rendelt().OrderBy(a => a.név))
            {
                foreach (var item2 in amiket_elvitt)
                {
                    if (item.mit != item2 && item.név == ügyfélnév)
                    {
                        table.AddCell(item.mit);
                        string kiszerelés = "";
                        foreach (var item3 in db_implements.getTörzsadlista())
                        {
                            if (item.mit == item3.anyagnév)
                            {
                                kiszerelés = item3.kiszerelés;
                            }
                        }
                        string mit = "";
                        mit = item.mennyit + " " + kiszerelés;
                        table.AddCell(mit);
                        table.AddCell("0");
                        table.AddCell(mit);
                        break;
                    }
                }
            }

            doc.Add(table);

            doc.Close();

            Process.Start("test.pdf");
        }

        /// <summary>
        /// Felhasznalo miből mennyit rendeltetek levonasok nelkul
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hetedik_button_Click(object sender, RoutedEventArgs e)
        {
            BaseFont MySpecialBaseFont = iTextSharp.text.pdf.BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.EMBEDDED);

            iTextSharp.text.Font MySpecialCharfont = new iTextSharp.text.Font(MySpecialBaseFont);

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10f, 10f, 20f, 0);

            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("test.pdf", System.IO.FileMode.Create));

            doc.Open();

            PdfPTable table = new PdfPTable(4);

            var header_font = FontFactory.GetFont("Times New Roman", 30, BaseColor.BLACK);
            var alap_font = FontFactory.GetFont("Times New Roman", 12, BaseColor.BLACK);

            iTextSharp.text.Paragraph header = new iTextSharp.text.Paragraph(new Phrase("Ki miből mennyit rendelt", new iTextSharp.text.Font(header_font)));

            header.Alignment = 1;

            doc.Add(header);
            doc.Add(new iTextSharp.text.Paragraph(" "));

            Phrase ki_phrase = new Phrase("Ki", MySpecialCharfont);
            PdfPCell ki_cella = new PdfPCell(ki_phrase);
            ki_cella.HorizontalAlignment = 1;

            table.AddCell(ki_cella);

            Phrase mit_phrase = new Phrase("Mit", MySpecialCharfont);
            PdfPCell mit_cella = new PdfPCell(mit_phrase);
            mit_cella.HorizontalAlignment = 1;

            table.AddCell(mit_cella);

            Phrase mennyit_phrase = new Phrase("Mennyit", MySpecialCharfont);
            PdfPCell mennyit_cella = new PdfPCell(mennyit_phrase);
            mit_cella.HorizontalAlignment = 1;

            table.AddCell(mennyit_cella);

            Phrase datum_phrase = new Phrase("Datum", MySpecialCharfont);
            PdfPCell datum_cella = new PdfPCell(datum_phrase);
            mit_cella.HorizontalAlignment = 1;

            table.AddCell(datum_cella);

            ObservableCollection<Megrendelések> öszzsesített_mi_mennyit = new ObservableCollection<Megrendelések>();

            if (string.IsNullOrEmpty(tol_textbox.Text))
            {
                öszzsesített_mi_mennyit = db_implements.getMegrendelések(0, 0);
            }
            else
            {
                öszzsesített_mi_mennyit = db_implements.getMegrendelések(Convert.ToInt32(tol_textbox.Text), Convert.ToInt32(ig_textbox.Text));
            }

            var valami = öszzsesített_mi_mennyit.OrderBy(a => a.ügyfélnév);

            var anyagok = db_implements.getTörzsadlista();

            string név = "";

            foreach (var item in valami)
            {
                if (név == item.ügyfélnév)
                {
                    if (ésmitt != "")
                    {
                        foreach (var item2 in anyagok)
                        {
                            if (item.anyagnév == item2.anyagnév && 
                                (item2.anyagnév == ésmitt || item2.gyártó == ésmitt 
                                || item2.főcsoport == ésmitt))
                            {
                                table.AddCell("");
                                table.AddCell(item.anyagnév);
                                string kiszereléssel = "";
                                kiszereléssel += item.eredeti_mennyiség + " " + item2.kiszerelés;
                                table.AddCell(kiszereléssel);
                                table.AddCell(item.dátum);
                                break;
                            }
                        }
                    }
                    else
                    {
                        table.AddCell("");
                        table.AddCell(item.anyagnév);
                        string kiszereléssel = "";
                        foreach (var item2 in anyagok)
                        {
                            if (item2.anyagnév == item.anyagnév)
                            {
                                kiszereléssel += item.eredeti_mennyiség + " " + item2.kiszerelés;
                                break;
                            }
                        }
                        table.AddCell(kiszereléssel);
                        table.AddCell(item.dátum);
                    }

                }
                else
                {
                    if (ésmitt != "")
                    {
                        foreach (var item2 in anyagok)
                        {
                            if (item.anyagnév == item2.anyagnév && (item2.anyagnév == ésmitt || item2.gyártó == ésmitt || item2.főcsoport == ésmitt) || (ésmitt == ""))
                            {
                                név = item.ügyfélnév;
                                table.AddCell(név);
                                table.AddCell(item.anyagnév);
                                string kiszereléssel = "";
                                kiszereléssel += item.eredeti_mennyiség + " " + item2.kiszerelés;
                                table.AddCell(kiszereléssel);
                                table.AddCell(item.dátum);
                                break;
                            }
                        }
                    }
                    else
                    {
                        név = item.ügyfélnév;
                        table.AddCell(név);
                        table.AddCell(item.anyagnév);
                        string kiszereléssel = "";
                        foreach (var item2 in anyagok)
                        {
                            if (item2.anyagnév == item.anyagnév)
                            {
                                kiszereléssel += item.eredeti_mennyiség + " " + item2.kiszerelés;
                                break;
                            }
                        }
                        table.AddCell(kiszereléssel);
                        table.AddCell(item.dátum);
                    }
                }
            }

            doc.Add(table);

            doc.Close();

            Process.Start("test.pdf");
        }

        /// <summary>
        /// Beszállító miből mennyit rendeltetek levonasok nelkul
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nyolcadik_button_Click(object sender, RoutedEventArgs e)
        {
            BaseFont MySpecialBaseFont = iTextSharp.text.pdf.BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.EMBEDDED);

            iTextSharp.text.Font MySpecialCharfont = new iTextSharp.text.Font(MySpecialBaseFont);

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10f, 10f, 20f, 0);

            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("test.pdf", System.IO.FileMode.Create));

            doc.Open();

            PdfPTable table = new PdfPTable(4);

            var header_font = FontFactory.GetFont("Times New Roman", 30, BaseColor.BLACK);
            var alap_font = FontFactory.GetFont("Times New Roman", 12, BaseColor.BLACK);

            iTextSharp.text.Paragraph header = new iTextSharp.text.Paragraph(new Phrase("Beszállítótól mennyit rendeltetek", new iTextSharp.text.Font(header_font)));

            header.Alignment = 1;

            doc.Add(header);
            doc.Add(new iTextSharp.text.Paragraph(" "));

            Phrase ki_phrase = new Phrase("Beszállító", MySpecialCharfont);
            PdfPCell ki_cella = new PdfPCell(ki_phrase);
            ki_cella.HorizontalAlignment = 1;

            table.AddCell(ki_cella);

            Phrase mit_phrase = new Phrase("Mit", MySpecialCharfont);
            PdfPCell mit_cella = new PdfPCell(mit_phrase);
            mit_cella.HorizontalAlignment = 1;

            table.AddCell(mit_cella);

            Phrase mennyit_phrase = new Phrase("Mennyit", MySpecialCharfont);
            PdfPCell mennyit_cella = new PdfPCell(mennyit_phrase);
            mit_cella.HorizontalAlignment = 1;

            table.AddCell(mennyit_cella);

            Phrase datum_phrase = new Phrase("Datum", MySpecialCharfont);
            PdfPCell datum_cella = new PdfPCell(datum_phrase);
            mit_cella.HorizontalAlignment = 1;

            table.AddCell(datum_cella);

            ObservableCollection<Ures_rendeles> öszzsesített_mi_mennyit = new ObservableCollection<Ures_rendeles>();

            if (string.IsNullOrEmpty(tol_textbox.Text))
            {
                öszzsesített_mi_mennyit = db_implements.getUres_rendeles(0, 0);
            }
            else
            {
                öszzsesített_mi_mennyit = db_implements.getUres_rendeles(Convert.ToInt32(tol_textbox.Text), Convert.ToInt32(ig_textbox.Text));
            }

            var valami = öszzsesített_mi_mennyit.OrderBy(a => a.anyagnév);

            var anyagok = db_implements.getTörzsadlista();

            string név = "";

            foreach (var item in valami)
            {
                if (név == item.beszállító)
                {
                    if (ésmitt != "")
                    {
                        foreach (var item2 in anyagok)
                        {
                            if (item.anyagnév == item2.anyagnév &&
                                (item2.anyagnév == ésmitt || item2.gyártó == ésmitt
                                || item2.főcsoport == ésmitt))
                            {
                                table.AddCell("");
                                table.AddCell(item.anyagnév);
                                string kiszereléssel = "";
                                kiszereléssel += item.eredeti_mennyiség + " " + item2.kiszerelés;
                                table.AddCell(kiszereléssel);
                                table.AddCell(item.dátum);
                                break;
                            }
                        }
                    }
                    else
                    {
                        table.AddCell("");
                        table.AddCell(item.anyagnév);
                        string kiszereléssel = "";
                        foreach (var item2 in anyagok)
                        {
                            if (item2.anyagnév == item.anyagnév)
                            {
                                kiszereléssel += item.eredeti_mennyiség + " " + item2.kiszerelés;
                                break;
                            }
                        }
                        table.AddCell(kiszereléssel);
                        table.AddCell(item.dátum);
                    }

                }
                else
                {
                    if (ésmitt != "")
                    {
                        foreach (var item2 in anyagok)
                        {
                            if (item.anyagnév == item2.anyagnév && (item2.anyagnév == ésmitt || item2.gyártó == ésmitt || item2.főcsoport == ésmitt) || (ésmitt == ""))
                            {
                                név = item.beszállító;
                                table.AddCell(név);
                                table.AddCell(item.anyagnév);
                                string kiszereléssel = "";
                                kiszereléssel += item.eredeti_mennyiség + " " + item2.kiszerelés;
                                table.AddCell(kiszereléssel);
                                table.AddCell(item.dátum);
                                break;
                            }
                        }
                    }
                    else
                    {
                        név = item.beszállító;
                        table.AddCell(név);
                        table.AddCell(item.anyagnév);
                        string kiszereléssel = "";
                        foreach (var item2 in anyagok)
                        {
                            if (item2.anyagnév == item.anyagnév)
                            {
                                kiszereléssel += item.eredeti_mennyiség + " " + item2.kiszerelés;
                                break;
                            }
                        }
                        table.AddCell(kiszereléssel);
                        table.AddCell(item.dátum);
                    }
                }
            }

            doc.Add(table);

            doc.Close();

            Process.Start("test.pdf");
        }

        /// <summary>
        /// Ugyfelek listazasa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ugyfelek_button_Click(object sender, RoutedEventArgs e)
        {
            BaseFont MySpecialBaseFont = iTextSharp.text.pdf.BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.EMBEDDED);

            iTextSharp.text.Font MySpecialCharfont = new iTextSharp.text.Font(MySpecialBaseFont);

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10f, 10f, 20f, 0);

            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("test.pdf", System.IO.FileMode.Create));

            doc.Open();

            PdfPTable table = new PdfPTable(12);

            var header_font = FontFactory.GetFont("Times New Roman", 30, BaseColor.BLACK);
            var alap_font = FontFactory.GetFont("Times New Roman", 12, BaseColor.BLACK);

            iTextSharp.text.Paragraph header = new iTextSharp.text.Paragraph(new Phrase("Beszállítótól mennyit rendeltetek", new iTextSharp.text.Font(header_font)));

            header.Alignment = 1;

            doc.Add(header);
            doc.Add(new iTextSharp.text.Paragraph(" "));

            Phrase ki_phrase = new Phrase("Név", MySpecialCharfont);
            PdfPCell ki_cella = new PdfPCell(ki_phrase);
            ki_cella.HorizontalAlignment = 1;

            table.AddCell(ki_cella);

            Phrase mit_phrase = new Phrase("Leánykori név", MySpecialCharfont);
            PdfPCell mit_cella = new PdfPCell(mit_phrase);
            mit_cella.HorizontalAlignment = 1;

            table.AddCell(mit_cella);

            Phrase mennyit_phrase = new Phrase("Anyja neve", MySpecialCharfont);
            PdfPCell mennyit_cella = new PdfPCell(mennyit_phrase);
            mit_cella.HorizontalAlignment = 1;

            table.AddCell(mennyit_cella);

            Phrase datum_phrase = new Phrase("Lakcim", MySpecialCharfont);
            PdfPCell datum_cella = new PdfPCell(datum_phrase);
            mit_cella.HorizontalAlignment = 1;

            table.AddCell(datum_cella);

            Phrase szul_phrase = new Phrase("Szül. hely", MySpecialCharfont);
            PdfPCell szul_cella = new PdfPCell(szul_phrase);
            mit_cella.HorizontalAlignment = 1;

            table.AddCell(szul_cella);

            Phrase szulh_phrase = new Phrase("Szül. idő", MySpecialCharfont);
            PdfPCell szulh_cella = new PdfPCell(szulh_phrase);
            mit_cella.HorizontalAlignment = 1;

            table.AddCell(szulh_cella);

            Phrase tel_phrase = new Phrase("Telefonszám", MySpecialCharfont);
            PdfPCell tel_cella = new PdfPCell(tel_phrase);
            mit_cella.HorizontalAlignment = 1;

            table.AddCell(tel_cella);

            Phrase email_phrase = new Phrase("Email", MySpecialCharfont);
            PdfPCell email_cella = new PdfPCell(email_phrase);
            mit_cella.HorizontalAlignment = 1;

            table.AddCell(email_cella);

            Phrase ado_phrase = new Phrase("Adószám", MySpecialCharfont);
            PdfPCell ado_cella = new PdfPCell(ado_phrase);
            mit_cella.HorizontalAlignment = 1;

            table.AddCell(ado_cella);

            Phrase adoaz_phrase = new Phrase("Adóazonositó jel", MySpecialCharfont);
            PdfPCell adoaz_cella = new PdfPCell(adoaz_phrase);
            mit_cella.HorizontalAlignment = 1;

            table.AddCell(adoaz_cella);

            Phrase mvh_phrase = new Phrase("MVH", MySpecialCharfont);
            PdfPCell mvh_cella = new PdfPCell(mvh_phrase);
            mit_cella.HorizontalAlignment = 1;

            table.AddCell(mvh_cella);

            Phrase zold_phrase = new Phrase("Zöldkártyaszám", MySpecialCharfont);
            PdfPCell zold_cella = new PdfPCell(zold_phrase);
            mit_cella.HorizontalAlignment = 1;

            table.AddCell(zold_cella);

            ObservableCollection<Ügyfelek> ugyfelek = new ObservableCollection<Ügyfelek>();

            ugyfelek = db_implements.getÜgyfelek();

            var valami = ugyfelek.OrderBy(a => a.teljes_név);
            
            foreach (var item in valami)
            {
                table.AddCell(item.teljes_név);
                table.AddCell(item.leánykori_név);
                table.AddCell(item.anyja_neve);
                table.AddCell(item.lakcím);
                table.AddCell(item.születési_helye);
                table.AddCell(item.születési_ideje);
                table.AddCell(item.telefonszám);
                table.AddCell(item.email);
                table.AddCell(item.adószám);
                table.AddCell(item.adóazonosító_jel);
                table.AddCell(item.mvh);
                table.AddCell(item.zöldkártyaszám);
            }

            doc.Add(table);

            doc.Close();

            Process.Start("test.pdf");
        }
        #endregion

        #region selection_changed
        private void ugyfelek_listazasa_listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = 0;

            int keresett = ugyfelek_listazasa_listbox.SelectedIndex;
            
            ügyfelek = db_implements.getÜgyfelek();

            var valami = ügyfelek.OrderBy(a => a.teljes_név);

            foreach (var item in valami)
            {
                if (index == keresett)
                {
                    ügyfélnév = item.teljes_név;
                }
                index++;
            }
        }

        private void beszallitok_listazasa_listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = 0;

            int keresett = beszallitok_listazasa_listbox.SelectedIndex;

            beszállítók = db_implements.getBeszállítók();

            var valami = beszállítók.OrderBy(a => a.név);

            foreach (var item in valami)
            {
                if (index == keresett)
                {
                    beszállítónév = item.név;
                }
                index++;
            }
        }

        private void ésmit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = 0;
            if (selected_szuro_combobox == "anyag")
            {
                foreach (var item in mik_anyagok)
                {
                    if (index == ésmit.SelectedIndex)
                    {
                        ésmitt = item.mi;
                        break;
                    }
                    else
                    {
                        index++;
                    }
                }
            }
            else if (selected_szuro_combobox == "főcsoport")
            {
                foreach (var item in mik_főcsoport)
                {
                    if (index == ésmit.SelectedIndex)
                    {
                        ésmitt = item.mi;
                        break;
                    }
                    else
                    {
                        index++;
                    }
                }
            }
            else if (selected_szuro_combobox == "gyártó")
            {
                foreach (var item in mik_gyártó)
                {
                    if (index == ésmit.SelectedIndex)
                    {
                        ésmitt = item.mi;
                        break;
                    }
                    else
                    {
                        index++;
                    }
                }
            }
            else
            {
                ésmitt = string.Empty;
            }
        }
        
        private void szuro_combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = 0;
            selectedIndex = szuro_combobox.SelectedIndex;
            selected_szuro_combobox = szuro_combobox_tomb[selectedIndex];
            ésmit.ItemsSource = null;
            if (selected_szuro_combobox == "anyag")
            {
                ésmit.ItemsSource = mik_anyagok;
            }
            else if (selected_szuro_combobox == "főcsoport")
            {
                ésmit.ItemsSource = mik_főcsoport;
            }
            else if (selected_szuro_combobox == "gyártó")
            {
                ésmit.ItemsSource = mik_gyártó;
            }
        }


        #endregion

    }
}
