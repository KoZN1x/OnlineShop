using OnlineShopOfSportEquipment_Models;

namespace OnlineShopOfSportEquipment_Models.ViewModels
{
    public class ProductUserViewModel
    {
        public ApplicationUser? ApplicationUser { get; set; }
        public List<Product>? ProductList { get; set; }
        public ProductUserViewModel()
        {
            ProductList = new List<Product>();
        }
    }
}
