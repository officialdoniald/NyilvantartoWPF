using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace NyilvántartóWPF
{
     public class PrintPDFDoc
     {
          private static BaseFont MySpecialBaseFont = iTextSharp.text.pdf.BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.EMBEDDED);

          private static iTextSharp.text.Font MySpecialCharfont = new iTextSharp.text.Font(MySpecialBaseFont);

          NyilvantartoDAODBImpl db_implementation = new NyilvantartoDAODBImpl();

          int mennyi = 0;

          public PrintPDFDoc(ObservableCollection<Szállítólevél> elvitt_termékek, string ügyfél_név, string ügyfél_cím,string mennyiség_egység,string adószam, string zöldkönyvszám)
          {
               Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10f, 10f, 20f, 0);
               
               PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("test.pdf", System.IO.FileMode.Create));

               doc.Open();

               PdfPTable table = new PdfPTable(7);
               
               var header_font = FontFactory.GetFont("Times New Roman", 30, BaseColor.BLACK);
               var alap_font = FontFactory.GetFont("Times New Roman", 12, BaseColor.BLACK);

               iTextSharp.text.Paragraph header = new iTextSharp.text.Paragraph(new Phrase("Szállítólevél", new iTextSharp.text.Font(header_font)));
               header.Alignment = 1;

               doc.Add(header);
               doc.Add(new iTextSharp.text.Paragraph(" "));

               iTextSharp.text.Image kovi_logo = iTextSharp.text.Image.GetInstance("kovi_logo.png");
               kovi_logo.ScaleToFit(150f, 250f);
               PdfPCell image_cell = new PdfPCell(kovi_logo);
               image_cell.HorizontalAlignment = 1;
               image_cell.VerticalAlignment = 0;
               image_cell.Colspan = 4;
               table.AddCell(image_cell);

               DateTime dt = DateTime.Now;
               int year = dt.Year;

               mennyi = db_implementation.getMennyiszallitoleveleddig();
               db_implementation.updateMennyiszallitoleveleddig(mennyi, mennyi+1);

               string formazottstring = "";
               if (mennyi < 10)
               {
                    formazottstring = "000" + mennyi;
               }
               else if (mennyi < 100)
               {
                    formazottstring = "00" + mennyi;
               }
               else if (mennyi < 1000)
               {
                    formazottstring = "0" + mennyi;
               }
               else
               {
                    formazottstring = "" + mennyi;
               }

               PdfPCell header_cell = new PdfPCell(new Phrase("Szállítólevél száma:\nKOVI-"+ formazottstring + "\\"+ year, new iTextSharp.text.Font(alap_font)));
               header_cell.Colspan = 3;
               header_cell.VerticalAlignment = 1;//0 = Left 1=Center 2=Right
               header_cell.HorizontalAlignment = 2;
               table.AddCell(header_cell);
               
               Phrase szallito_phrase = new Phrase("A szállító (név, cím, adószám):\n"+" KOVI-NÖVÉNYPATIKA"  + "\n6640 Csongrád, Zrínyi M. u. 6.\nAdószám: 24252672-2-06" , MySpecialCharfont);
               PdfPCell szallito_cella = new PdfPCell(szallito_phrase);
               szallito_cella.Colspan = 3;
               
               Phrase vevo_phrase = new Phrase("A vevő (név, cím, adószám, zöldkönyvszám.):\n" + ügyfél_név + "\n" + ügyfél_cím + "\n" + adószam + "\n" + zöldkönyvszám, MySpecialCharfont);
               PdfPCell vevo_cella = new PdfPCell(vevo_phrase);
               vevo_cella.Colspan = 4;

               table.AddCell(szallito_cella);
               
               table.AddCell(vevo_cella);

               doc.Add(table);

               PdfPTable table2 = new PdfPTable(7);
               float[] width = new float[] { 30f, 200f, 30f, 50f, 50f, 54f, 50f };
               table2.SetWidths(width);

               Phrase sorszam_phrase = new Phrase("Sorszám", MySpecialCharfont);
               PdfPCell sorszam_cella = new PdfPCell(sorszam_phrase);
               sorszam_cella.Rotation = 90;
               sorszam_cella.HorizontalAlignment = 1;

               Phrase cikkszam_phrase = new Phrase("\nCikkszám; besorolási szám; Az áru szabványos megnevezése, kódja, minősége és egyéb ismertetőjele.", MySpecialCharfont);
               PdfPCell cikkszam_cella = new PdfPCell(cikkszam_phrase);
               cikkszam_cella.HorizontalAlignment = 1;
               cikkszam_cella.FixedHeight = 60;

               Phrase afa_phrase = new Phrase("ÁFA-\n kulcs", MySpecialCharfont);
               PdfPCell afa_cella = new PdfPCell(afa_phrase);
               afa_cella.Rotation = 90;
               afa_cella.HorizontalAlignment = 1;

               Phrase mennyegysegseg_phrase = new Phrase("\n Menny.\n egység", MySpecialCharfont);
               PdfPCell mennyegysegseg_cella = new PdfPCell(mennyegysegseg_phrase);
               mennyegysegseg_cella.Rotation = 90;
               mennyegysegseg_cella.HorizontalAlignment = 1;

               Phrase mennyiseg_phrase = new Phrase("\nMennyi-\nség", MySpecialCharfont);
               PdfPCell mennyiseg_cella = new PdfPCell(mennyiseg_phrase);
               mennyiseg_cella.HorizontalAlignment = 1;

               Phrase egysegar_phrase = new Phrase("\nEgységár", MySpecialCharfont);
               PdfPCell egysegar_cella = new PdfPCell(egysegar_phrase);
               egysegar_cella.HorizontalAlignment = 1;

               Phrase ertek_phrase = new Phrase("\nÉrték", MySpecialCharfont);
               PdfPCell ertek_cella = new PdfPCell(ertek_phrase);
               ertek_cella.HorizontalAlignment = 1;

               table2.AddCell(sorszam_cella);
               table2.AddCell(cikkszam_cella);
               table2.AddCell(afa_cella);
               table2.AddCell(mennyegysegseg_cella);
               table2.AddCell(mennyiseg_cella);
               table2.AddCell(egysegar_cella);
               table2.AddCell(ertek_cella);

               int index = 1;

               foreach (var item in elvitt_termékek)
               {
                    Phrase szamlalo_phrase = new Phrase(index + ".", MySpecialCharfont);
                    PdfPCell szamlalo_cella = new PdfPCell(szamlalo_phrase);
                    szamlalo_cella.HorizontalAlignment = 1;
                    szamlalo_cella.FixedHeight = 23;

                    table2.AddCell(szamlalo_cella);
                    table2.AddCell(item.miből);
                    table2.AddCell(item.áfatartalom);
                    table2.AddCell(mennyiség_egység);
                    table2.AddCell(item.mennyit);
                    table2.AddCell(item.egységár);
                    double véglegár = 0;
                    if (item.áfatartalom == "0")
                    {
                         véglegár = Convert.ToDouble(item.egységár)*Convert.ToInt32(item.mennyit);
                    }
                    else
                    {
                         véglegár = Math.Round((Convert.ToDouble(item.egységár) * Convert.ToInt32(item.mennyit) )* 1.27);
                    }

                    table2.AddCell(véglegár+"");
                    index++;
               }
               
               for (int i = index; i < 19; i++)
               {

                    Phrase szamlalo_phrase = new Phrase(i + ".", MySpecialCharfont);
                    PdfPCell szamlalo_cella = new PdfPCell(szamlalo_phrase);
                    szamlalo_cella.HorizontalAlignment = 1;
                    szamlalo_cella.FixedHeight = 23;

                    table2.AddCell(szamlalo_cella);

                    /*ide fog jönni me*/
                    table2.AddCell("");
                    table2.AddCell("");
                    table2.AddCell("");
                    table2.AddCell("");
                    table2.AddCell("");
                    table2.AddCell("");
               }

               doc.Add(table2);

               PdfPTable table3 = new PdfPTable(5);
               float[] width2 = new float[] { 40f, 80f, 600f, 40f, 80f };
               table2.SetWidths(width);

               Phrase kiallito_phrase = new Phrase("Kiállító", new iTextSharp.text.Font(alap_font));
               PdfPCell kiallito_cella = new PdfPCell(kiallito_phrase);
               kiallito_cella.Colspan = 2;
               kiallito_cella.HorizontalAlignment = 1;

               Phrase atv_phrase = new Phrase("Átvételi feljegyzések:", new iTextSharp.text.Font(alap_font));
               PdfPCell atv_cella = new PdfPCell(atv_phrase);
               atv_cella.HorizontalAlignment = 1;

               Phrase atvet_phrase = new Phrase("Átvevö", new iTextSharp.text.Font(alap_font));
               PdfPCell atvet_cella = new PdfPCell(atvet_phrase);
               atvet_cella.Colspan = 2;
               atvet_cella.HorizontalAlignment = 1;

               DateTime thisDay = DateTime.Today;

               Phrase kelt_phrase = new Phrase("Kelet\n\n\n" + thisDay.ToString("D"), new iTextSharp.text.Font(alap_font));
               PdfPCell kelt_cella = new PdfPCell(kelt_phrase);
               kelt_cella.FixedHeight = 60;

               Phrase alairas_phrase = new Phrase("Aláírása", new iTextSharp.text.Font(alap_font));
               PdfPCell alairas_cella = new PdfPCell(alairas_phrase);

               Phrase ph_phrase = new Phrase("PH.", new iTextSharp.text.Font(alap_font));
               PdfPCell ph_cella = new PdfPCell(ph_phrase);

               table3.AddCell(kiallito_cella);
               table3.AddCell(atv_cella);
               table3.AddCell(atvet_cella);

               table3.AddCell(kelt_cella);
               table3.AddCell(alairas_cella);
               table3.AddCell(ph_cella);
               table3.AddCell(kelt_cella);
               table3.AddCell(alairas_cella);

               doc.Add(table3);
               
               doc.Close();

               Process.Start("test.pdf");
               
          }
     }
}
