using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShopOfSportEquipment_Models
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }
        public int OrderHeaderId { get; set; }
        public Guid ProductId { get; set; }
        public uint Count { get; set; }
        public decimal Price { get; set; }
        [Required]
        [ForeignKey(nameof(OrderHeaderId))]
        public virtual OrderHeader? OrderHeader { get; set; }
        [Required]
        [ForeignKey(nameof(ProductId))]
        public virtual Product? Product { get; set; }

    }
}
