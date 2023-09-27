using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class Customer:BaseEntity
    {
        public int TableOfRestaurantId { get;set; }
        public TableOfRestaurant TableOfRestaurant { get;set; }

    }
}
