using Restaurant.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.Entities
{
    public class Drink : BaseEntity
    {
        [MaxLength(100)]
        public string DrinkName { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public int DrinkCategoryId { get; set; }
        public DrinkCategory DrinkCategory { get; set; }
        public int KitchenId { get; set; }
        public Kitchen Kitchen { get; set; }
        public IEnumerable<MenuDrink> MenuDrinks { get; set; }
        
    }
}
