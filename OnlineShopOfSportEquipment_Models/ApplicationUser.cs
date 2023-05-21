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
        [NotMapped]
        public CreditCard? CreditCard { get; set; }

    }
}
