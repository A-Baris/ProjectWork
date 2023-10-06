using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.Entities
{
    public class BillDrink
    {
        public int DrinkId { get; set; }
        public int BillId { get; set; }
        public Drink Drink { get; set; }
        public Bill Bill { get; set; }
    }
}
