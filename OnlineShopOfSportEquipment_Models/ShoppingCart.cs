using System.ComponentModel.DataAnnotations;

namespace OnlineShopOfSportEquipment_Models
{
    public class ShoppingCart
    {
        public Guid ProductId { get; set; }
        [Range(0, 100)]
        public uint ProductCount { get; set; }
    }
}
