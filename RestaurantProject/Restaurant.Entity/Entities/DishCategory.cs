using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class DishCategory:BaseEntity
    {
        public virtual List<Dish> Dishes { get; set; }
    }
}
