using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace OnlineShopOfSportEquipment_Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string? FullName { get; set; }
        [NotMapped]
        public string? StreetAddress { get; set; }
        [NotMapped]
        public string? City { get; set; }
        [NotMapped]
        public string? State { get; set; }
        [NotMapped]
        [DataType(DataType.PostalCode)]
        public string? PostalCode { get; set; }
        //[NotMapped]
        //[CreditCard]
        ////[DataType(DataType.CreditCard)]
        //[Required]
        //public int? CreditCardNumber { get; set; }
        //[NotMapped]
        //[Required]
        //[DataType(DataType.Date)]
        ////[DisplayFormat(DataFormatString = "{MM/yy}", ApplyFormatInEditMode = true)]
        //public DateTime CardDate { get; set; }
        //[NotMapped]
        //[Required]
        //[Range(100, 999)]
        //public int CVV { get; set; }

    }
}
