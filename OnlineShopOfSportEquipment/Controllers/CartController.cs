using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShopOfSportEquipment.Services;
using OnlineShopOfSportEquipment_Data.Services.IService;
using OnlineShopOfSportEquipment_Models;
using OnlineShopOfSportEquipment_Models.ViewModels;
using OnlineShopOfSportEquipment_Utility;
using System.Security.Claims;

namespace OnlineShopOfSportEquipment.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly IApplicationUserService _applicationUserService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IOrderHeaderService _orderHeaderService;

        [BindProperty]
        public ProductUserViewModel? ProductUserViewModel { get; set; }

        public CartController(IProductService productService, IApplicationUserService applicationUserService,
            IOrderDetailService orderDetailService, IOrderHeaderService orderHeaderService)
        {
            _productService = productService;
            _applicationUserService = applicationUserService;
            _orderDetailService = orderDetailService;
            _orderHeaderService = orderHeaderService;
        }

        public async Task<IActionResult> Index()
        {
            var shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart)!.Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }
            var productsInCart = shoppingCartList!.Select(x => x.ProductId).ToList();
            var productsListTemp = await _productService.GetAllAsync(x => productsInCart.Contains(x.Id));
            var productsList = new List<Product>();
            foreach (var productInCart in shoppingCartList!)
            {
                var productTemp = productsListTemp.FirstOrDefault(x => x.Id == productInCart.ProductId);
                productTemp!.TempCount = productInCart.ProductCount;
                productsList.Add(productTemp);
            }
            return View(productsList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public IActionResult IndexPost(List<Product> productList)
        {
            var shoppingCartList = new List<ShoppingCart>();
            foreach (var product in productList)
            {
                shoppingCartList.Add(new ShoppingCart() { ProductId = product.Id, ProductCount = product.TempCount });
            }
            HttpContext.Session.Set(WC.SessionCart, shoppingCartList);
            return RedirectToAction(nameof(Summary));
        }

        public async Task<IActionResult> Summary()
        {
            var claimsIdentity = (ClaimsIdentity?)User.Identity;
            var claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);

            var shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart)!.Count() > 0)
            {
                //session exists
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }
            var productsInCart = shoppingCartList!.Select(x => x.ProductId).ToList();
            IEnumerable<Product> productsList = await _productService.GetAllAsync(x => productsInCart.Contains(x.Id));

            ProductUserViewModel = new ProductUserViewModel()
            {
                ApplicationUser = await _applicationUserService.FirstOrDefaultAsync(x => x.Id == claim!.Value)
            };

            foreach (var item in shoppingCartList!)
            {
                var productTemp = await _productService.FirstOrDefaultAsync(x => x.Id == item.ProductId);
                productTemp!.TempCount = item.ProductCount;
                ProductUserViewModel.ProductList!.Add(productTemp);
            }
            return View(ProductUserViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Summary")]
        public async Task<IActionResult> SummaryPost(ProductUserViewModel productUserViewModel)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity!;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            foreach (var product in ProductUserViewModel!.ProductList!)
            {
                if (product.CountInStock < product.TempCount)
                {
                    ErrorViewModel error = new ErrorViewModel()
                    {
                        RequestId = "Not enough goods in stock!"
                    };
                    return View("Error", error);
                }
            }
            var orderTotal = OrderTotalCounter.Count(ProductUserViewModel);
            var orderHeader = new OrderHeader()
            {
                ApplicationUserId = claim!.Value,
                FullName = productUserViewModel.ApplicationUser!.FullName,
                OrderDate = DateTime.Now,
                Email = productUserViewModel.ApplicationUser!.Email,
                PhoneNumber = productUserViewModel.ApplicationUser!.PhoneNumber,
                Address = productUserViewModel.ApplicationUser!.StreetAddress,
                City = productUserViewModel.ApplicationUser!.City,
                State = productUserViewModel.ApplicationUser!.State,
                PostalCode = productUserViewModel.ApplicationUser!.PostalCode,
                OrderStatus = WC.StatusActive,
                FinalOrderTotal = orderTotal
            };
            await _orderHeaderService.AddAsync(orderHeader);
            await _orderHeaderService.SaveAsync();
            foreach (var product in ProductUserViewModel!.ProductList!)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    OrderHeaderId = orderHeader.Id,
                    ProductId = product.Id,
                    Count = product.TempCount,
                    Price = product.Price,
                };
                await _orderDetailService.AddAsync(orderDetail);
            }
            await _orderDetailService.SaveAsync();
            return RedirectToAction(nameof(OrderConfirmation), new { id = orderHeader.Id });
        }

        public async Task<IActionResult> OrderConfirmation(int id = 0)
        {
            var orderHeader = await _orderHeaderService.FirstOrDefaultAsync(x => x.Id == id);
            HttpContext.Session.Clear();
            return View(orderHeader);
        }

        public IActionResult Remove(Guid? id)
        {
            var shoppingCartList = new List<ShoppingCart?>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart)!.Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart?>>(WC.SessionCart);
            }
            shoppingCartList?.Remove(shoppingCartList.FirstOrDefault(x => x?.ProductId == id));
            HttpContext.Session.Set(WC.SessionCart, shoppingCartList);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ClearCart()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
