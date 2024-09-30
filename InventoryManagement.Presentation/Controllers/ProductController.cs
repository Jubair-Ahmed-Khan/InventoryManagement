using InventoryManagement.Presentation.Helpers;
using InventoryManagement.Presentation.Models;
using InventoryManagement.DataAccess.Models;
using InventoryManagement.Services.Contacts;
using InventoryManagement.Services.Mappers;
using InventoryManagement.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace InventoryManagement.Presentation.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IProductMapper _mapper;

    public ProductController(IProductService productService, IWebHostEnvironment webHostEnvironment,IProductMapper mapper)
    {
        _productService = productService;
        _webHostEnvironment = webHostEnvironment;
        _mapper = mapper;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 30)]
    public async Task<IActionResult> DisplayProduct(int? pageNumber, int pageSize = 10)
    {
        IEnumerable<Product> listOfProducts = await _productService.GetAllAsync();
        var products = listOfProducts.AsQueryable();

        ViewBag.PageSize = pageSize;
        ViewBag.TotalCount = listOfProducts.Count();

        return View(await Pagination<Product>.CreateAsync(products, pageNumber ?? 1, pageSize));
    }

    [HttpGet]
    public ActionResult CreateProduct()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> CreateProduct(ProductDTO product)
    {
        if (!ModelState.IsValid)
        {
            return View(product);
        }

        await _productService.AddAsync(product);

        return RedirectToAction("DisplayProduct", "Product");
    }

    [HttpGet]
    public async Task<ActionResult> UpdateProduct(int id)
    {
        Product product = await _productService.GetByIdAsync(id);
        ProductDTO productDto = _mapper.MapToDTO(product);

        ViewBag.Id = id;

        return View(productDto);
    }

    [HttpPost]
    public async Task<ActionResult> UpdateProduct(int id,Product product)
    {
        var productDto = _mapper.MapToDTO(product);
       
        await _productService.UpdateAsync(id, productDto);

        return RedirectToAction("DisplayProduct", "Product");
    }

    [HttpGet]
    public async Task<ActionResult> ProductDetails(int id)
    {
        Product product = await _productService.GetByIdAsync(id);
        ProductDTO productDto = _mapper.MapToDTO(product);

        ViewBag.Id = id;

        return View(productDto);
    }

    [HttpGet]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        Product product = await _productService.GetByIdAsync(id);

        return View(product);
    }

    [HttpPost]
    public async Task<ActionResult> RemoveProduct(int id)
    {
        await _productService.DeleteAsync(id);

        return RedirectToAction("DisplayProduct", "Product");
    }

    public async Task<ActionResult> ExportToPdf()
    {
        var inventoryData = await _productService.GetAllAsync();
        var dataList = inventoryData.ToList();

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
            Paragraph title = new Paragraph("Product Report", titleFont)
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

            PdfPTable table = new PdfPTable(3);
            table.WidthPercentage = 100;
            table.SetWidths(new float[] { 1f, 3f, 1f });

            
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

            cell = new PdfPCell(new Phrase("Quantity", tableHeaderFont))
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
                table.AddCell(new PdfPCell(new Phrase(product.ProductName, tableDataFont))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                });
                table.AddCell(new PdfPCell(new Phrase(product.ProductQuantity.ToString(), tableDataFont))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                });
            }

            document.Add(table);

            
            writer.PageEvent = new PdfFooter();

            
            document.Close();

            byte[] pdfContent = memoryStream.ToArray();
            return File(pdfContent, "application/pdf", "ProductReport.pdf");
        }
    }
}
