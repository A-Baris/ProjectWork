using Restaurant.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class Ingredient:BaseEntity
    {
        public string IngredientName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
       public int IngredientCatgoryId { get; set; } 
        public IngredientCategory IngredientCategory { get; set; }
        public IEnumerable<DishIngredient> DishIngredients { get; set; }
        public int KitchenId { get; set; }
        public Kitchen Kitchen { get; set; }
    }
}
