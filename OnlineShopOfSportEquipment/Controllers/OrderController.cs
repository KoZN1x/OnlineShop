using Microsoft.AspNetCore.Mvc;
using OnlineShopOfSportEquipment_Data.Services.IService;

namespace OnlineShopOfSportEquipment.Controllers
{

    public class OrderController : Controller
    {
        private readonly IOrderHeaderService _orderHeaderService;
        private readonly IOrderDetailService _orderDetailService;
        public OrderController(IOrderHeaderService orderHeaderService, IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
            _orderHeaderService = orderHeaderService;
        }
        public IActionResult Index()
        {

            return View();
        }


    }
}
