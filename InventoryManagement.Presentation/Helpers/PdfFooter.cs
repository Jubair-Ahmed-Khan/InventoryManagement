using iTextSharp.text.pdf;
using iTextSharp.text;

namespace InventoryManagement.Presentation.Helpers;

public class PdfFooter : PdfPageEventHelper
{
    public override void OnEndPage(PdfWriter writer, Document document)
    {
        Font footerFont = FontFactory.GetFont("Arial", 10, Font.ITALIC);

        PdfPTable footerTable = new PdfPTable(1)
        {
            TotalWidth = 300,
            HorizontalAlignment = Element.ALIGN_CENTER
        };

        PdfPCell cell = new PdfPCell(new Phrase("Page " + writer.PageNumber, footerFont))
        {
            Border = Rectangle.NO_BORDER,
            HorizontalAlignment = Element.ALIGN_CENTER
        };

        footerTable.AddCell(cell);

        float xPosition = (document.PageSize.Width - footerTable.TotalWidth) / 2;

        footerTable.WriteSelectedRows(0, -1, xPosition, 30, writer.DirectContent);
    }
}
