using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShopOfSportEquipment_Models
{
    public class Product
    {
        public Product()
        {
            TempCount = 1;
        }
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Display(Name = "Short Description")]
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public decimal Price { get; set; } 
        public string? Image { get; set; }
        [Display(Name = "Category Type")]
        public Guid CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }
        [Display(Name = "Training Type")]
        public Guid TrainingTypeId { get; set; }
        [ForeignKey("TrainingTypeId")]
        public virtual TrainingType? TrainingType { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public uint CountInStock { get; set; }
        [Range(1, 10, ErrorMessage = "Count must  be greater than 0 and less than 10!")]
        [NotMapped]
        public uint TempCount { get; set; }
    }
}
