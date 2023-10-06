using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.Entities
{
    public class OrderDrink
    {
        public int DrinkId { get; set; }
        public int OrderId { get; set; }
        public Drink Drink { get; set; }
        public Order Order { get; set; }
    }
}
