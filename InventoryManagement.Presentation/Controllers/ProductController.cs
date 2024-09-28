using InventoryManagement.DataAccess.Models;
using InventoryManagement.Services.Contacts;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Presentation.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 30)]
    public async Task<ActionResult<IEnumerable<Product>>> DisplayProduct()
    {
        IEnumerable<Product> listOfProducts = await _productService.GetAllAsync();

        return View(listOfProducts);
    }

    [HttpGet]
    public ActionResult CreateProduct()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> CreateProduct(Product product)
    {
        if (!ModelState.IsValid)
        {
            return View(product);
        }

        Product newProduct = new Product
        {
            ProductName = product.ProductName,
            ProductQuantity = product.ProductQuantity,
        };

        await _productService.AddAsync(newProduct);

        return RedirectToAction("DisplayProduct", "Product");
    }

    [HttpGet]
    public async Task<ActionResult> UpdateProduct(int id)
    {
        Product product = await _productService.GetByIdAsync(id);

        return View(product);
    }

    [HttpPost]
    public async Task<ActionResult> UpdateProduct(int id,Product product)
    {
       
        await _productService.UpdateAsync(id,product);
        return RedirectToAction("DisplayProduct", "Product");
    }

    [HttpGet]
    public async Task<ActionResult> ProductDetails(int id)
    {
        Product product = await _productService.GetByIdAsync(id);

        return View(product);
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
}
 