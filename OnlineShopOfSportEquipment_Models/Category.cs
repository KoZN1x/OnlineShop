﻿using System.ComponentModel.DataAnnotations;

namespace OnlineShopOfSportEquipment_Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public virtual IEnumerable<Product> Products { get; set; } = new List<Product>();

    }
}
