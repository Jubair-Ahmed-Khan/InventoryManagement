using InventoryManagement.Presentation.Helpers;
using InventoryManagement.Presentation.Models;
using InventoryManagement.DataAccess.Models;
using InventoryManagement.Services.Contacts;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace InventoryManagement.Presentation.Controllers;

public class PurchaseController : Controller
{
    private readonly IPurchaseService _purchaseService;
    private readonly IProductService _productService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public PurchaseController(IPurchaseService purchaseService, IProductService productService, IWebHostEnvironment webHostEnvironment)
    {
        _purchaseService = purchaseService;
        _productService = productService;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 30)]
    public async Task<IActionResult> DisplayPurchase(int? pageNumber, int pageSize = 10)
    {
        IEnumerable<Purchase> listOfPurchase = await _purchaseService.GetAllAsync();
        var purchases = listOfPurchase.AsQueryable();

        ViewBag.PageSize = pageSize;
        ViewBag.TotalCount = listOfPurchase.Count();

        return View(await Pagination<Purchase>.CreateAsync(purchases, pageNumber ?? 1, pageSize));
    }

    [HttpGet]
    public async Task<ActionResult> CreatePurchase()
    {
        var products = await _productService.GetAllAsync();
        List<string> productList = products.Select(p => p.ProductName).ToList();

        ViewBag.ProductName = new SelectList(productList);

        return View();
    }

    [HttpPost]
    public async Task<ActionResult> CreatePurchase(Purchase purchase)
    {
        if (!ModelState.IsValid)
        {
            return View(purchase);
        }

        Purchase newPurchase = new Purchase
        {
            PurchaseProduct = purchase.PurchaseProduct,
            PurchaseQuantity = purchase.PurchaseQuantity,
            PurchaseDate = purchase.PurchaseDate
        };

        await _purchaseService.AddAsync(newPurchase);

        return RedirectToAction("DisplayPurchase", "Purchase");
    }

    [HttpGet]
    public async Task<ActionResult> UpdatePurchase(int id)
    {
        Purchase purchase = await _purchaseService.GetByIdAsync(id);

        var products = await _productService.GetAllAsync();
        List<string> productList = products.Select(p => p.ProductName).ToList();

        ViewBag.ProductName = new SelectList(productList);

        return View(purchase);
    }

    [HttpPost]
    public async Task<ActionResult> UpdatePurchase(int id, Purchase purchase)
    {

        await _purchaseService.UpdateAsync(id, purchase);

        return RedirectToAction("DisplayPurchase", "Purchase");
    }

    [HttpGet]
    public async Task<ActionResult> PurchaseDetails(int id)
    {
        Purchase purchase = await _purchaseService.GetByIdAsync(id);

        return View(purchase);
    }

    [HttpGet]
    public async Task<ActionResult> DeletePurchase(int id)
    {
        Purchase purchase = await _purchaseService.GetByIdAsync(id);

        return View(purchase);
    }

    [HttpPost]
    public async Task<ActionResult> RemovePurchase(int id)
    {
        await _purchaseService.DeleteAsync(id);

        return RedirectToAction("DisplayPurchase", "Purchase");
    }

    public async Task<ActionResult> ExportToPdf()
    {
        var purchaseData = await _purchaseService.GetAllAsync();
        var dataList = purchaseData.ToList();

        using (MemoryStream memoryStream = new MemoryStream())
        {

            Document document = new Document(PageSize.A4, 25, 25, 30, 30);
            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
            document.Open();

            string webRootPath = _webHostEnvironment.WebRootPath;
            string imagePath = Path.Combine(webRootPath, "images", "inventory.jpg");

            Image logo = Image.GetInstance(imagePath);
            logo.ScaleToFit(70f, 70f);
            logo.Alignment = Element.ALIGN_CENTER;
            document.Add(logo);


            Font titleFont = FontFactory.GetFont("Arial", 18, Font.BOLD);
            Paragraph title = new Paragraph("Purchase Report", titleFont)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 10
            };
            document.Add(title);

            Font dateFont = FontFactory.GetFont("Arial", 10, Font.ITALIC);
            string currentDateTime = DateTime.Now.ToString("f");
            Paragraph dateParagraph = new Paragraph("Generated on: " + currentDateTime, dateFont)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 20
            };
            document.Add(dateParagraph);

            PdfPTable table = new PdfPTable(4);
            table.WidthPercentage = 100;
            table.SetWidths(new float[] { 1f, 2f,1f, 2f });


            Font tableHeaderFont = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.WHITE);
            PdfPCell cell = new PdfPCell(new Phrase("Product ID", tableHeaderFont))
            {
                BackgroundColor = BaseColor.GRAY,
                HorizontalAlignment = Element.ALIGN_CENTER
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Product Name", tableHeaderFont))
            {
                BackgroundColor = BaseColor.GRAY,
                HorizontalAlignment = Element.ALIGN_CENTER
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Product Quantity", tableHeaderFont))
            {
                BackgroundColor = BaseColor.GRAY,
                HorizontalAlignment = Element.ALIGN_CENTER
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Purchase Date", tableHeaderFont))
            {
                BackgroundColor = BaseColor.GRAY,
                HorizontalAlignment = Element.ALIGN_CENTER
            };
            table.AddCell(cell);

            // Add data rows
            Font tableDataFont = FontFactory.GetFont("Arial", 10);
            foreach (var product in dataList)
            {
                table.AddCell(new PdfPCell(new Phrase(product.Id.ToString(), tableDataFont))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                });
                table.AddCell(new PdfPCell(new Phrase(product.PurchaseProduct, tableDataFont))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                });
                table.AddCell(new PdfPCell(new Phrase(product.PurchaseQuantity.ToString(), tableDataFont))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                });
                table.AddCell(new PdfPCell(new Phrase(product.PurchaseDate.ToShortDateString(), tableDataFont))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                });
            }

            document.Add(table);


            writer.PageEvent = new PdfFooter();


            document.Close();

            byte[] pdfContent = memoryStream.ToArray();
            return File(pdfContent, "application/pdf", "PurchaseReport.pdf");
        }
    }
}
