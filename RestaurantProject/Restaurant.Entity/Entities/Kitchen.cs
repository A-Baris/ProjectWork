using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.Entities
{
    public class Kitchen:BaseEntity
    {
      public virtual List<Order> Orders { get; set; }
      public virtual List<Dish> Dishes { get; set; }
      public virtual List<Ingredient> Ingredients { get; set; }
      
    }
}
