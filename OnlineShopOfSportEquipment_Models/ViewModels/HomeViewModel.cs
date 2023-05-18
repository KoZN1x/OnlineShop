using OnlineShopOfSportEquipment_Models;

namespace OnlineShopOfSportEquipment_Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Product>? Products { get; set; }
        public IEnumerable<Category>? Categories { get; set; }
        public IEnumerable<Category>? TrainingTypes { get; set; }
    }
}
