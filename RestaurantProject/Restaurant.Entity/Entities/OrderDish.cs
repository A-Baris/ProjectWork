using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.Entities
{
    public class OrderDish
    {
        public int DishId { get; set; }
        public int OrderId { get; set; }
        public Dish Dish { get; set; }
        public Order Order { get; set; }
    }
}
