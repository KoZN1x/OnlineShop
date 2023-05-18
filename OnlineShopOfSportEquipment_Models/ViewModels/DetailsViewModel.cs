using OnlineShopOfSportEquipment_Models;

namespace OnlineShopOfSportEquipment_Models.ViewModels
{
    public class DetailsViewModel
    {
        public Product? Product { get; set; }
        public bool ExistInCart { get; set; }
        public DetailsViewModel() 
        {
            Product = new Product();
        }
        
    }
}
