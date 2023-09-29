using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class Dish:BaseEntity
    {
        public int DishCategoryId { get;set; }
        public int KitchenId { get; set; }
        public Kitchen Kitchen { get; set; }
        public IEnumerable<MenuDish> MenuDishes { get; set; }
        public IEnumerable<DishIngredient> DishIngredients { get; set; }

        public DishCategory DishCategory { get;set; }
    }
}
