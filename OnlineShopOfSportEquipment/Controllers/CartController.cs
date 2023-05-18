using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShopOfSportEquipment_Data;
using OnlineShopOfSportEquipment_Utility;
using OnlineShopOfSportEquipment_Models.ViewModels;
using System.Security.Claims;
using OnlineShopOfSportEquipment_Models;
using OnlineShopOfSportEquipment_Data.Services.IService;

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

        public IActionResult Summary()
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
            IEnumerable<Product> productsList = _productService.GetAll(x => productsInCart.Contains(x.Id));

            ProductUserViewModel = new ProductUserViewModel()
            {
                ApplicationUser = _applicationUserService.FirstOrDefault(x => x.Id == claim!.Value),
                ProductList = productsList.ToList()
            };

            return View(ProductUserViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Summary")]
        public IActionResult SummaryPost(ProductUserViewModel productUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity!;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                OrderHeader orderHeader = new OrderHeader()
                {
                    ApplicationUserId = claim!.Value,
                    FullName = productUserViewModel.ApplicationUser!.FullName,
                    OrderDate = DateTime.Now,
                    Email = productUserViewModel.ApplicationUser!.Email,
                    PhoneNumber = productUserViewModel.ApplicationUser!.PhoneNumber,
                    Address = productUserViewModel.ApplicationUser!.Address,
                };
                _orderHeaderService.Add(orderHeader);
                _orderHeaderService.Save();
                foreach (var product in ProductUserViewModel!.ProductList!)
                {
                    OrderDetail orderDetail = new OrderDetail()
                    {
                        OrderHeaderId = orderHeader.Id,
                        ProductId = product.Id
                    };
                    _orderDetailService.Add(orderDetail);
                }
                _orderDetailService.Save();
                return RedirectToAction(nameof(OrderConfirmation));
            }
            return View(productUserViewModel);
        }

        public IActionResult OrderConfirmation(int id=0)
        {
            OrderHeader orderHeader = _orderHeaderService.FirstOrDefault(x => x.Id == id);
            HttpContext.Session.Clear();
            return View(orderHeader);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public IActionResult IndexPost()
        {
            return RedirectToAction(nameof(Summary));
        }

        public IActionResult Index()
        {
            var shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart)!.Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);

            }
            var productsInCart = shoppingCartList!.Select(x => x.ProductId).ToList();
            IEnumerable<Product> productsList = _productService.GetAll(x => productsInCart.Contains(x.Id));
            return View(productsList);
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
    }
}
