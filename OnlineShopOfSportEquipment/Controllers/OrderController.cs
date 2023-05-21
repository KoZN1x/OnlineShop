using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShopOfSportEquipment_Data.Services.IService;
using OnlineShopOfSportEquipment_Models.ViewModels;
using OnlineShopOfSportEquipment_Utility;

namespace OnlineShopOfSportEquipment.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class OrderController : Controller
    { 
        private readonly IOrderHeaderService _orderHeaderService;
        private readonly IOrderDetailService _orderDetailService;
        [BindProperty]
        public OrderViewModel? orderViewModel { get; set; }
        public OrderController(IOrderHeaderService orderHeaderService, IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
            _orderHeaderService = orderHeaderService;
        }
        public IActionResult Index()
        {
            var orderListViewModel = new OrderListViewModel()
            {
                OrderHeaderList = _orderHeaderService.GetAll(),
                StatusList = WC.StatusList.ToList().Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = x,
                    Value = x
                })
            };
            return View(orderListViewModel);
        }

        public ActionResult Details(int Id)
        {
            orderViewModel = new OrderViewModel()
            {
                OrderHeader = _orderHeaderService.FirstOrDefault(x => x.Id == Id),
                OrderDetail = _orderDetailService.GetAll(x => x.OrderHeaderId == Id, includeProperties: "Product")
            };
            return View(orderViewModel);
        }

        [HttpPost]
        public IActionResult StartProcessing()
        {
            var orderHeader = _orderHeaderService.FirstOrDefault(x => x.Id == orderViewModel!.OrderHeader!.Id);
            orderHeader.OrderStatus = WC.StatusProcessing;
            _orderHeaderService.Save();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult CompleteOrder()
        {
            var orderHeader = _orderHeaderService.FirstOrDefault(x => x.Id == orderViewModel!.OrderHeader!.Id);
            orderHeader.OrderStatus = WC.StatusCompleted;
            _orderHeaderService.Save();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult RemoveOrder()
        {
            var orderHeader = _orderHeaderService.FirstOrDefault(x => x.Id == orderViewModel!.OrderHeader!.Id);
            _orderHeaderService.Remove(orderHeader);
            _orderHeaderService.Save();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult UpdateOrderDetails()
        {
            var orderHeaderFromDb = _orderHeaderService.FirstOrDefault(x => x.Id == orderViewModel!.OrderHeader!.Id);
            orderHeaderFromDb.FullName = orderViewModel!.OrderHeader!.FullName;
            orderHeaderFromDb.PhoneNumber = orderViewModel!.OrderHeader!.PhoneNumber;
            orderHeaderFromDb.Address = orderViewModel!.OrderHeader!.Address;
            orderHeaderFromDb.City = orderViewModel!.OrderHeader!.City;
            orderHeaderFromDb.State = orderViewModel!.OrderHeader!.State;
            orderHeaderFromDb.PostalCode = orderViewModel!.OrderHeader!.PostalCode;
            orderHeaderFromDb.Email = orderViewModel!.OrderHeader!.Email;
            _orderHeaderService.Save();
            return RedirectToAction("Details", "Order", new { id = orderHeaderFromDb.Id});
        }
    }
}
