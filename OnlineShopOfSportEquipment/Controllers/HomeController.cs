using Microsoft.AspNetCore.Mvc;
using OnlineShopOfSportEquipment_Data.Services.IService;
using OnlineShopOfSportEquipment_Models;
using OnlineShopOfSportEquipment_Models.ViewModels;
using OnlineShopOfSportEquipment_Utility;
using System.Diagnostics;

namespace OnlineShopOfSportEquipment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, ICategoryService categoryService)
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var homeViewModel = new HomeViewModel()
            {
                Products = await _productService.GetAllAsync(includeProperties: "Category,TrainingType"),
                Categories = await _categoryService.GetAllAsync()
            };
            return View(homeViewModel);
        }

        public async Task<IActionResult> DetailsAsync(Guid? id)
        {
            var shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart)!.Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

            var detailsViewModel = new DetailsViewModel()
            {
                Product = await _productService.FirstOrDefaultAsync(x => x.Id == id, inclideProperties: "Category,TrainingType"),
                ExistInCart = false
            };

            foreach (var item in shoppingCartList!)
            {
                if (item.ProductId == id)
                {
                    detailsViewModel.ExistInCart = true;
                }
            }
            return View(detailsViewModel);
        }

        [HttpPost, ActionName("Details")]
        public IActionResult DetailsPost(Guid? id, DetailsViewModel detailsViewModel)
        {
            var shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null 
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart)!.Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }
            shoppingCartList!.Add(new ShoppingCart { ProductId = (Guid)id!, ProductCount = detailsViewModel.Product!.TempCount });
            HttpContext.Session.Set(WC.SessionCart, shoppingCartList);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveFromCart(Guid? id)
        {
            var shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart)!.Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

            var itemToRemove = shoppingCartList!.SingleOrDefault(x => x.ProductId == id);
            if (itemToRemove != null)
            {
                shoppingCartList!.Remove(itemToRemove);
            }

            HttpContext.Session.Set(WC.SessionCart, shoppingCartList);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}