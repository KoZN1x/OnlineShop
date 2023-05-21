using OnlineShopOfSportEquipment_Models;
using OnlineShopOfSportEquipment_Models.ViewModels;

namespace OnlineShopOfSportEquipment.Services
{
    public static class OrderTotalCounter
    {
        public static decimal Count (ProductUserViewModel productUserViewModel)
        {
            var orderTotal = 0M;
            foreach (var product in productUserViewModel.ProductList!)
            {
                orderTotal += product.Price * product.TempCount;
                product.CountInStock -= product.TempCount;
            }
            return orderTotal;
        }
    }
}
