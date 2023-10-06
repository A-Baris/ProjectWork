
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.Entities
{
    public class Dish : BaseEntity
    {
        [MaxLength(100)]
        public string DishName { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        [MaxLength(200)]
        public string? Description { get; set; }
        public int DishCategoryId { get; set; }
        public int KitchenId { get; set; }
        public Kitchen Kitchen { get; set; }
        public int MenuId { get; set; }
        public Menu Menu { get; set; }
        public IEnumerable<DishIngredient> DishIngredients { get; set; }
        public IEnumerable<OrderDish> OrderDishes { get; set; }
        public IEnumerable<BillDish> BillDishes { get; set; }

        public DishCategory DishCategory { get; set; }
    }
}
