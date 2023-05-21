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
        private string cardValue = "";
        [NotMapped]
        [CreditCard]
        [Required]
        public string CreditCardNumber
        {
            get { return cardValue; }
            set
            {
                if (isValidCardNumberLength(value))
                {
                    cardValue = value;
                }
            }
        }
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

        private bool isValidCardNumberLength(string creditCardNumber)
        {
            creditCardNumber = creditCardNumber.Replace(" ", "");
            if (creditCardNumber.Length == 16)
            {
                return LunaValidation(creditCardNumber);
            }
            return false;
        }

        private bool LunaValidation(string creditCardNumber)
        {
            int[] number = new int[16];
            int sum = 0;
            for (int i = 0; i < 15; i += 2)
            {
                var doubledValue = int.Parse(creditCardNumber[i].ToString()) * 2;
                number[i] = doubledValue > 9 ? doubledValue - 9 : doubledValue;
            }
            for (int i = 1; i < 16; i += 2)
            {
                number[i] = int.Parse(creditCardNumber[i].ToString());
            }
            foreach (var symbol in number)
            {
                sum += symbol;
            }
            return sum % 10 == 0;
        }
    }
}
