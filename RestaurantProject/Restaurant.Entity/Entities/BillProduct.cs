using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.Entities
{
    public class BillProduct
    {
        public int ProductId { get; set; }
        public int BillId { get; set; }
        public Product Product { get; set; }
        public Bill Bill { get; set; }
    }
}
