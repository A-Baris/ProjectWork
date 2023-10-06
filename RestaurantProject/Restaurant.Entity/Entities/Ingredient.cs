using Restaurant.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.Entities
{
    public class Ingredient:BaseEntity
    {
        [MaxLength(100)]
        public string IngredientName { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        [MaxLength(200)]
        public string? Description { get;set; }
       public int IngredientCatgoryId { get; set; } 
        public IngredientCategory IngredientCategory { get; set; }
        public IEnumerable<DishIngredient> DishIngredients { get; set; }
        public int KitchenId { get; set; }
        public Kitchen Kitchen { get; set; }
    }
}
