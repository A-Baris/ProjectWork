using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class Waiter:BaseEntity
    {
        public virtual List<TableOfRestaurant> TableOfRestaurants { get; set; }
    }
}
