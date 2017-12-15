using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace NyilvántartóWPF
{
    public class PDF_Raktarkonyv
    {
        #region attribútumok

        NyilvantartoDAODBImpl db_implements = new NyilvantartoDAODBImpl();

        ObservableCollection<Behoz_beszallito> behoz_szallitok = new ObservableCollection<Behoz_beszallito>();

        ObservableCollection<Elvisz_ugyfelek> elvisz_ugyfelek = new ObservableCollection<Elvisz_ugyfelek>();

        ObservableCollection<Törzsadatlista> törzsadatok_amikbol_az_anyagnev_kell = new ObservableCollection<Törzsadatlista>();

        ObservableCollection<Raktartkonyv> raktarkonyvek = new ObservableCollection<Raktartkonyv>();

        Raktartkonyv raktarkonyv = new Raktartkonyv();

        #endregion

        public PDF_Raktarkonyv(string miszerint_rendezze, string ésmit)
        {
            DateTime dt = DateTime.Now;

            string megrendelés_id = dt.ToString("yyyy.MM.dd");

            törzsadatok_amikbol_az_anyagnev_kell = db_implements.getTörzsadlista();

            behoz_szallitok = db_implements.getBehoz_beszallito(0,0);

            elvisz_ugyfelek = db_implements.getElvisz_ugyfelek(0,0);

            var valami = törzsadatok_amikbol_az_anyagnev_kell.OrderBy(a => a.anyagnév);

            foreach (var item in behoz_szallitok)
            {
                if (Convert.ToDateTime(item.mikor).Year >= dt.Year)
                {
                    raktarkonyv = new Raktartkonyv();
                    raktarkonyv.nev = item.név;
                    raktarkonyv.beszallitott = item.mennyit;
                    raktarkonyv.kiszallitott = "";
                    raktarkonyv.keszlet = "";
                    raktarkonyv.mit = item.mit;
                    raktarkonyv.datum = megrendelés_id;

                    raktarkonyvek.Add(raktarkonyv);
                }
            }

            foreach (var item in elvisz_ugyfelek)
            {
                if (Convert.ToDateTime(item.mikor).Year >= dt.Year)
                {
                    raktarkonyv = new Raktartkonyv();
                    raktarkonyv.nev = item.név;
                    raktarkonyv.beszallitott = "";
                    raktarkonyv.kiszallitott = item.mennyit;
                    raktarkonyv.keszlet = "";
                    raktarkonyv.mit = item.mit;
                    raktarkonyv.datum = megrendelés_id;

                    raktarkonyvek.Add(raktarkonyv);
                }
            }

            var valami2 = raktarkonyvek.OrderBy(a => a.datum);

            BaseFont MySpecialBaseFont = iTextSharp.text.pdf.BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.EMBEDDED);

            iTextSharp.text.Font MySpecialCharfont = new iTextSharp.text.Font(MySpecialBaseFont);

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10f, 10f, 20f, 0);

            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("test.pdf", System.IO.FileMode.Create));

            doc.Open();

            PdfPTable table = new PdfPTable(5);

            var header_font = FontFactory.GetFont("Times New Roman", 30, BaseColor.BLACK);
            var alap_font = FontFactory.GetFont("Times New Roman", 12, BaseColor.BLACK);

            iTextSharp.text.Paragraph header = new iTextSharp.text.Paragraph(new Phrase("Raktárkönyv", new iTextSharp.text.Font(header_font)));

            header.Alignment = 1;

            doc.Add(header);
            doc.Add(new iTextSharp.text.Paragraph(" "));

            Phrase datum_phrase = new Phrase("Dátum", MySpecialCharfont);
            PdfPCell datum_cella = new PdfPCell(datum_phrase);
            datum_cella.HorizontalAlignment = 1;

            Phrase nev_phrase = new Phrase("Név", MySpecialCharfont);
            PdfPCell nev_cella = new PdfPCell(nev_phrase);
            nev_cella.HorizontalAlignment = 1;

            Phrase beszall_phrase = new Phrase("Beszáll.", MySpecialCharfont);
            PdfPCell beszall_cella = new PdfPCell(beszall_phrase);
            beszall_cella.HorizontalAlignment = 1;

            Phrase kiszall_phrase = new Phrase("Kiszáll.", MySpecialCharfont);
            PdfPCell kiszall_cella = new PdfPCell(kiszall_phrase);
            kiszall_cella.HorizontalAlignment = 1;

            Phrase keszlet_phrase = new Phrase("Készlet", MySpecialCharfont);
            PdfPCell keszlet_cella = new PdfPCell(keszlet_phrase);
            keszlet_cella.HorizontalAlignment = 1;

            table.AddCell(datum_cella);
            table.AddCell(nev_cella);
            table.AddCell(beszall_cella);
            table.AddCell(kiszall_cella);
            table.AddCell(keszlet_cella);

            int keszlet = 0;

            if (miszerint_rendezze == "anyag")
            {
                foreach (var item in valami)
                {
                    if (item.anyagnév == ésmit)
                    {
                        keszlet = 0;
                        Phrase anyagnev_phrase = new Phrase(item.anyagnév, MySpecialCharfont);
                        PdfPCell anyagnev_cella = new PdfPCell(anyagnev_phrase);
                        anyagnev_cella.HorizontalAlignment = 1;

                        table.AddCell(anyagnev_cella);
                        table.AddCell("");
                        table.AddCell("");
                        table.AddCell("");
                        table.AddCell("");

                        foreach (var item2 in valami2)
                        {
                            if (item.anyagnév == item2.mit)
                            {
                                table.AddCell(item2.datum);
                                table.AddCell(item2.nev);

                                if (item2.beszallitott != "")
                                {
                                    keszlet += Convert.ToInt32(item2.beszallitott);

                                    table.AddCell(item2.beszallitott);
                                    table.AddCell("");
                                    table.AddCell(Convert.ToString(keszlet));
                                }
                                else
                                {
                                    keszlet -= Convert.ToInt32(item2.kiszallitott);

                                    table.AddCell("");
                                    table.AddCell(item2.kiszallitott);
                                    table.AddCell(Convert.ToString(keszlet));
                                }
                            }

                            table.AddCell("");
                            table.AddCell("");
                            table.AddCell("");
                            table.AddCell("");
                            table.AddCell("");
                        }
                    }

                }
            }
            else if (miszerint_rendezze == "főcsoport")
            {
                var valami3 = valami.OrderBy(a => a.főcsoport);

                string voltmar = "";

                foreach (var item in valami3)
                {
                    keszlet = 0;
                    if (item.főcsoport == ésmit)
                    {
                        if (voltmar != item.főcsoport)
                        {
                            Phrase anyagnev_phrase = new Phrase(item.főcsoport, MySpecialCharfont);
                            PdfPCell anyagnev_cella = new PdfPCell(anyagnev_phrase);
                            anyagnev_cella.HorizontalAlignment = 1;

                            table.AddCell(anyagnev_cella);
                            keszlet = 0;
                        }
                        else
                        {
                            table.AddCell("");
                        }

                        table.AddCell("");
                        table.AddCell("");
                        table.AddCell("");
                        table.AddCell("");

                        foreach (var item2 in valami2)
                        {
                            if (item.anyagnév == item2.mit)
                            {
                                table.AddCell(item2.datum);
                                table.AddCell(item2.nev);

                                if (item2.beszallitott != "")
                                {
                                    keszlet += Convert.ToInt32(item2.beszallitott);

                                    table.AddCell(item2.beszallitott);
                                    table.AddCell("");
                                    table.AddCell(Convert.ToString(keszlet));
                                }
                                else
                                {
                                    keszlet -= Convert.ToInt32(item2.kiszallitott);

                                    table.AddCell("");
                                    table.AddCell(item2.kiszallitott);
                                    table.AddCell(Convert.ToString(keszlet));
                                }
                            }

                            table.AddCell("");
                            table.AddCell("");
                            table.AddCell("");
                            table.AddCell("");
                            table.AddCell("");

                            voltmar = item.főcsoport;
                        }
                    }
                }
            }
            else if (miszerint_rendezze == "gyártó")
            {
                var valami3 = valami.OrderBy(a => a.gyártó);

                string voltmar = "";

                foreach (var item in valami3)
                {
                    if (item.gyártó == ésmit)
                    {
                        if (voltmar != item.gyártó)
                        {
                            Phrase anyagnev_phrase = new Phrase(item.gyártó, MySpecialCharfont);
                            PdfPCell anyagnev_cella = new PdfPCell(anyagnev_phrase);
                            anyagnev_cella.HorizontalAlignment = 1;

                            table.AddCell(anyagnev_cella);
                            keszlet = 0;
                        }
                        else
                        {
                            table.AddCell("");
                        }

                        table.AddCell("");
                        table.AddCell("");
                        table.AddCell("");
                        table.AddCell("");

                        foreach (var item2 in valami2)
                        {
                            if (item.anyagnév == item2.mit)
                            {
                                table.AddCell(item2.datum);
                                table.AddCell(item2.nev);

                                if (item2.beszallitott != "")
                                {
                                    keszlet += Convert.ToInt32(item2.beszallitott);

                                    table.AddCell(item2.beszallitott);
                                    table.AddCell("");
                                    table.AddCell(Convert.ToString(keszlet));
                                }
                                else
                                {
                                    keszlet -= Convert.ToInt32(item2.kiszallitott);

                                    table.AddCell("");
                                    table.AddCell(item2.kiszallitott);
                                    table.AddCell(Convert.ToString(keszlet));
                                }
                            }

                            table.AddCell("");
                            table.AddCell("");
                            table.AddCell("");
                            table.AddCell("");
                            table.AddCell("");

                            voltmar = item.gyártó;
                        }
                    }

                }
            }

            doc.Add(table);

            doc.Close();

            Process.Start("test.pdf");
        }
    }
    class Raktartkonyv
    {
        public string datum { get; set; }
        public string nev { get; set; }
        public string mit { get; set; }
        public string beszallitott { get; set; }
        public string kiszallitott { get; set; }
        public string keszlet { get; set; }
    }
}
