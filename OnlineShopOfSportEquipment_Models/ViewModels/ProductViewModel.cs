using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShopOfSportEquipment_Models;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopOfSportEquipment_Models.ViewModels
{
    public class ProductViewModel
    {
        public Product? Product { get; set; }
        public IEnumerable<SelectListItem>? CategorySelectList { get; set; }
        public IEnumerable<SelectListItem>? TrainingTypeSelectList { get; set; }
    }
}
