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


        public async Task<IActionResult> IndexAsync()
        {
            var orderListViewModel = new OrderListViewModel()
            {
                OrderHeaderList = await _orderHeaderService.GetAllAsync(),
                StatusList = WC.StatusList.ToList().Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = x,
                    Value = x
                })
            };
            return View(orderListViewModel);
        }

        public async Task<ActionResult> DetailsAsync(int Id)
        {
            orderViewModel = new OrderViewModel()
            {
                OrderHeader = await _orderHeaderService.FirstOrDefaultAsync(x => x.Id == Id),
                OrderDetail = await _orderDetailService.GetAllAsync(x => x.OrderHeaderId == Id, includeProperties: "Product")
            };
            return View(orderViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> StartProcessingAsync()
        {
            var orderHeader = await _orderHeaderService.FirstOrDefaultAsync(x => x.Id == orderViewModel!.OrderHeader!.Id);
            orderHeader.OrderStatus = WC.StatusProcessing;
            await _orderHeaderService.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> CompleteOrderAsync()
        {
            var orderHeader = await _orderHeaderService.FirstOrDefaultAsync(x => x.Id == orderViewModel!.OrderHeader!.Id);
            orderHeader.OrderStatus = WC.StatusCompleted;
            await _orderHeaderService.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveOrderAsync()
        {
            var orderHeader = await _orderHeaderService.FirstOrDefaultAsync(x => x.Id == orderViewModel!.OrderHeader!.Id);
            _orderHeaderService.Remove(orderHeader);
            await _orderHeaderService.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrderDetailsAsync()
        {
            var orderHeaderFromDb = await _orderHeaderService.FirstOrDefaultAsync(x => x.Id == orderViewModel!.OrderHeader!.Id);
            orderHeaderFromDb.FullName = orderViewModel!.OrderHeader!.FullName;
            orderHeaderFromDb.PhoneNumber = orderViewModel!.OrderHeader!.PhoneNumber;
            orderHeaderFromDb.Address = orderViewModel!.OrderHeader!.Address;
            orderHeaderFromDb.City = orderViewModel!.OrderHeader!.City;
            orderHeaderFromDb.State = orderViewModel!.OrderHeader!.State;
            orderHeaderFromDb.PostalCode = orderViewModel!.OrderHeader!.PostalCode;
            orderHeaderFromDb.Email = orderViewModel!.OrderHeader!.Email;
            await _orderHeaderService.SaveAsync();
            return RedirectToAction("Details", "Order", new { id = orderHeaderFromDb.Id});
        }
    }
}
