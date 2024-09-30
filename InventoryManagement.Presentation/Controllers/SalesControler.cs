using InventoryManagement.Presentation.Helpers;
using InventoryManagement.Presentation.Models;
using InventoryManagement.DataAccess.Models;
using InventoryManagement.Services.Contacts;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using iTextSharp.text.pdf;
using iTextSharp.text;
using InventoryManagement.Services.Mappers;
using InventoryManagement.Services.DTOs;
using InventoryManagement.Services.Services;

namespace InventoryManagement.Presentation.Controllers;

public class SalesController : Controller
{
	private readonly ISalesService _salesService;
	private readonly IProductService _productService;
    private readonly ISalesMapper _mapper;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public SalesController(ISalesService salesService, IProductService productService, IWebHostEnvironment webHostEnvironment,ISalesMapper mapper)
    {
        _salesService = salesService;
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
    public async Task<IActionResult> DisplaySales(int? pageNumber, int pageSize = 10)
    {
        IEnumerable<Sale> listOfSales = await _salesService.GetAllAsync();
        var sales = listOfSales.AsQueryable();

        ViewBag.PageSize = pageSize;
        ViewBag.TotalCount = listOfSales.Count();

        return View(await Pagination<Sale>.CreateAsync(sales, pageNumber ?? 1, pageSize));
    }

	[HttpGet]
	public async Task<ActionResult> CreateSales()
	{
		var products = await _productService.GetAllAsync();
		List<string> productList = products.Select(p => p.ProductName).ToList();

		ViewBag.ProductName = new SelectList(productList);

		return View();
	}

	[HttpPost]
	public async Task<ActionResult> CreateSales(SalesDTO salesDto)
	{
		if (!ModelState.IsValid)
		{
			return View(salesDto);
		}

		await _salesService.AddAsync(salesDto);

		return RedirectToAction("DisplaySales", "Sales");
	}

	[HttpGet]
	public async Task<ActionResult> UpdateSales(int id)
	{
		Sale sale = await _salesService.GetByIdAsync(id);
        SalesDTO salesDto = _mapper.MapToDTO(sale);

		var products = await _productService.GetAllAsync();
		List<string> productList = products.Select(p => p.ProductName).ToList();

        ViewBag.Id = id;
		ViewBag.ProductName = new SelectList(productList);

		return View(salesDto);
	}

	[HttpPost]
	public async Task<ActionResult> UpdateSales(int id, Sale sale)
	{
        var salesDto = _mapper.MapToDTO(sale);
        await _salesService.UpdateAsync(id, salesDto);

		return RedirectToAction("DisplaySales", "Sales");
	}

	[HttpGet]
	public async Task<ActionResult> SalesDetails(int id)
    {

        Sale sale = await _salesService.GetByIdAsync(id);
        SalesDTO salesDto = _mapper.MapToDTO(sale);

        ViewBag.Id = id;

		return View(salesDto);
	}

	[HttpGet]
	public async Task<ActionResult> DeleteSales(int id)
	{
		Sale sale = await _salesService.GetByIdAsync(id);

		return View(sale);
	}

	[HttpPost]
	public async Task<ActionResult> RemoveSales(int id)
	{
		await _salesService.DeleteAsync(id);

		return RedirectToAction("DisplaySales", "Sales");
	}

    public async Task<ActionResult> ExportToPdf()
    {
        var salesData = await _salesService.GetAllAsync();
        var dataList = salesData.ToList();

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
            Paragraph title = new Paragraph("Sales Report", titleFont)
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
            table.SetWidths(new float[] { 1f, 2f, 1f, 2f });


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

            cell = new PdfPCell(new Phrase("Sales Date", tableHeaderFont))
            {
                BackgroundColor = BaseColor.GRAY,
                HorizontalAlignment = Element.ALIGN_CENTER
            };
            table.AddCell(cell);

            
            Font tableDataFont = FontFactory.GetFont("Arial", 10);
            foreach (var product in dataList)
            {
                table.AddCell(new PdfPCell(new Phrase(product.Id.ToString(), tableDataFont))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                });
                table.AddCell(new PdfPCell(new Phrase(product.SaleProduct, tableDataFont))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                });
                table.AddCell(new PdfPCell(new Phrase(product.SaleQuantity.ToString(), tableDataFont))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                });
                table.AddCell(new PdfPCell(new Phrase(product.SaleDate.ToShortDateString(), tableDataFont))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                });
            }

            document.Add(table);


            writer.PageEvent = new PdfFooter();


            document.Close();

            byte[] pdfContent = memoryStream.ToArray();
            return File(pdfContent, "application/pdf", "SalesReport.pdf");
        }
    }
}
