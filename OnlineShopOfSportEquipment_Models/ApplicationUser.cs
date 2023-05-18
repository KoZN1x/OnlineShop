using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;

namespace OnlineShopOfSportEquipment_Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string? FullName { get; set; }
        [Required]
        public string? Address { get; set; }

    }
}
