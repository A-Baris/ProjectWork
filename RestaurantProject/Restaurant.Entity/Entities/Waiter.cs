using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class Waiter:BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string TcNo { get;set; }
        public virtual List<TableOfRestaurant> TableOfRestaurants { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
