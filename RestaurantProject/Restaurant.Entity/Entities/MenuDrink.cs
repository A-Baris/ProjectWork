
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.Entities
{
    public class MenuDrink
    {
        public int DrinkId { get; set; }
        public int MenuId { get; set; }

        public Drink Drink { get; set; }
        public Menu Menu { get; set; }
    }
}
