using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.Entities
{
    public class BillDish
    {
        public int DishId { get; set; }
        public int BillId { get; set; }
        public Dish Dish { get; set; }
        public Bill Bill { get; set; }
    }
}
