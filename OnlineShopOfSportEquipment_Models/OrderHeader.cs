using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShopOfSportEquipment_Models
{
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }
        public string? ApplicationUserId { get; set; }
        public decimal FinalOrderTotal { get; set; }
        [ForeignKey(nameof(ApplicationUserId))]
        public virtual ApplicationUser? User { get; set; }
        public DateTime OrderDate { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? State { get; set; }
        [DataType(DataType.PostalCode)]
        public string? PostalCode { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        public string? FullName { get; set; }
        [Required]
        public string? OrderStatus { get; set; }
    }
}
