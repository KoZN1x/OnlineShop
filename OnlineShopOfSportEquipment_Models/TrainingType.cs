using System.ComponentModel.DataAnnotations;

namespace OnlineShopOfSportEquipment_Models
{
    public class TrainingType
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public virtual ICollection<Product>? Products { get; set; } = new List<Product>();
    }
}
