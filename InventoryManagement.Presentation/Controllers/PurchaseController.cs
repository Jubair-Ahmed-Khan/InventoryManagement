using InventoryManagement.DataAccess.Models;
using InventoryManagement.Services.Contacts;
using InventoryManagement.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagement.Presentation.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly IPurchaseService _purchaseService;
        private readonly IProductService _productService;

        public PurchaseController(IPurchaseService purchaseService, IProductService productService)
        {
            _purchaseService = purchaseService;
            _productService = productService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 30)]
        public async Task<ActionResult<IEnumerable<Purchase>>> DisplayPurchase()
        {
            IEnumerable<Purchase> listOfPurchases = await _purchaseService.GetAllAsync();

            return View(listOfPurchases);
        }

        [HttpGet]
        public async Task<ActionResult> CreatePurchase()
        {
            var products = await _productService.GetAllAsync();

            // Convert the IEnumerable<Product> to List<string> (e.g., based on ProductName)
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
    }
}
