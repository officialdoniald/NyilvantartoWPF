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

namespace NyilvántartóWPF
{
    public class PDFBehozBeszallito
    {
        public PDFBehozBeszallito(ObservableCollection<Szállítólevél> szallitolevel, string beszallito)
        {
            BaseFont MySpecialBaseFont = iTextSharp.text.pdf.BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.EMBEDDED);

            iTextSharp.text.Font MySpecialCharfont = new iTextSharp.text.Font(MySpecialBaseFont);

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10f, 10f, 20f, 0);

            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("test.pdf", System.IO.FileMode.Create));

            doc.Open();

            PdfPTable table = new PdfPTable(4);

            var header_font = FontFactory.GetFont("Times New Roman", 30, BaseColor.BLACK);
            var alap_font = FontFactory.GetFont("Times New Roman", 12, BaseColor.BLACK);

            iTextSharp.text.Paragraph header = new iTextSharp.text.Paragraph(new Phrase("Szallitólevél", new iTextSharp.text.Font(header_font)));

            header.Alignment = 1;

            doc.Add(header);
            doc.Add(new iTextSharp.text.Paragraph(" "));

            Phrase datum_phrase = new Phrase("Mit", MySpecialCharfont);
            PdfPCell datum_cella = new PdfPCell(datum_phrase);
            datum_cella.HorizontalAlignment = 1;

            Phrase nev_phrase = new Phrase("Mennyit", MySpecialCharfont);
            PdfPCell nev_cella = new PdfPCell(nev_phrase);
            nev_cella.HorizontalAlignment = 1;

            Phrase beszall_phrase = new Phrase("Egységár", MySpecialCharfont);
            PdfPCell beszall_cella = new PdfPCell(beszall_phrase);
            beszall_cella.HorizontalAlignment = 1;

            Phrase beszall2_phrase = new Phrase("Áfatartalom", MySpecialCharfont);
            PdfPCell beszall2_cella = new PdfPCell(beszall2_phrase);
            beszall2_cella.HorizontalAlignment = 1;

            table.AddCell(datum_cella);
            table.AddCell(nev_cella);
            table.AddCell(beszall_cella);
            table.AddCell(beszall2_cella);

            int osszeg = 0;

            foreach (var item in szallitolevel)
            {
                table.AddCell(item.miből);
                table.AddCell(item.mennyit);
                table.AddCell(item.egységár);
                table.AddCell(item.áfatartalom);

                osszeg += Convert.ToInt32(item.egységár);
            }

            table.AddCell("");
            table.AddCell("");
            table.AddCell("Összesen:");
            table.AddCell(osszeg + " Ft");

            doc.Add(table);

            doc.Close();

            Process.Start("test.pdf");
        }
    }
}
