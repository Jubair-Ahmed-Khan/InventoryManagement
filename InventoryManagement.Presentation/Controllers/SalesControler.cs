using InventoryManagement.DataAccess.Models;
using InventoryManagement.Services.Contacts;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Presentation.Controllers;

public class SalesController : Controller
{
	private readonly ISalesService _salesService;
	private readonly IProductService _productService;

	public SalesController(ISalesService salesService, IProductService productService)
	{
		_salesService = salesService;
		_productService = productService;
	}

	public IActionResult Index()
	{
		return View();
	}

	[HttpGet]
	[ResponseCache(Location = ResponseCacheLocation.Client, Duration = 30)]
	public async Task<ActionResult<IEnumerable<Sale>>> DisplaySales()
	{
		IEnumerable<Sale> listOfSales = await _salesService.GetAllAsync();

		return View(listOfSales);
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
	public async Task<ActionResult> CreateSales(Sale sale)
	{
		if (!ModelState.IsValid)
		{
			return View(sale);
		}

		Sale newSales = new Sale
		{
			SaleProduct = sale.SaleProduct,
			SaleQuantity = sale.SaleQuantity,
			SaleDate = sale.SaleDate
		};

		await _salesService.AddAsync(newSales);

		return RedirectToAction("DisplaySales", "Sales");
	}

	[HttpGet]
	public async Task<ActionResult> UpdateSales(int id)
	{
		Sale sale = await _salesService.GetByIdAsync(id);

		var products = await _productService.GetAllAsync();
		List<string> productList = products.Select(p => p.ProductName).ToList();

		ViewBag.ProductName = new SelectList(productList);

		return View(sale);
	}

	[HttpPost]
	public async Task<ActionResult> UpdateSales(int id, Sale sale)
	{

		await _salesService.UpdateAsync(id, sale);

		return RedirectToAction("DisplaySales", "Sales");
	}

	[HttpGet]
	public async Task<ActionResult> SalesDetails(int id)
	{
		Sale sale = await _salesService.GetByIdAsync(id);

		return View(sale);
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
}
