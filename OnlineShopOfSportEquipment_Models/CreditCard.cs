using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopOfSportEquipment_Models
{
    public class CreditCard
    {
        [NotMapped]
        [Required(ErrorMessage = "Credit card number is required!")]
        [CreditCard(ErrorMessage = "Input correct credit card number!")]
        public string? CreditCardNumber { get; set; }

        [NotMapped]
        [Required]
        [Range(1, 12)]
        public int CardValidMonth { get; set; }

        [NotMapped]
        [Required]
        [Range(23, 33)]
        public int CardValidYear { get; set; }

        [NotMapped]
        [Required]
        [Range(100, 999)]
        public int CVV { get; set; }
    }
}
