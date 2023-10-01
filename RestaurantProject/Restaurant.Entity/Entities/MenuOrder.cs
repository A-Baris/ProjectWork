
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.Entities
{
    public class MenuOrder
    {
        public int MenuId { get; set; }
        public int OrderId {get; set; }
        public Menu Menu { get; set; }
        public Order Order { get; set; }
    }
}
