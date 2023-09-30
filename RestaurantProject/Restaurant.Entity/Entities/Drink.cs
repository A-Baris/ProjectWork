using Restaurant.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class Drink : BaseEntity
    {
        public string DrinkName { get; set; }
        public decimal Price { get; set; }
        public int DrinkCategoryId { get; set; }
        public DrinkCategory DrinkCategory { get; set; }
        public int KitchenId { get; set; }
        public Kitchen Kitchen { get; set; }
        public IEnumerable<MenuDrink> MenuDrinks { get; set; }
        
    }
}
