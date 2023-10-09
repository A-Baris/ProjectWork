using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.Entities
{
    public class Waiter:BaseEmployee
    {
      
        public virtual List<TableOfRestaurant> TableOfRestaurants { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
