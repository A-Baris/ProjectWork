using Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class TableOfRestaurant : BaseEntity
    {
        public ReservationStatus Status { get; set; }


        public int WaiterId { get; set; }



        public Waiter Waiter { get; set; }
        public virtual List<Customer> Customers { get; set; }
        public virtual List<Bill> Bills { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
