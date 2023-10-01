using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.Entities
{
    public class BillCustomer
    {
        public int BillId { get; set; }
        public int CustomerId { get; set; }

        public Bill Bill { get; set; }
        public Customer Customer { get; set; }
    }
}
