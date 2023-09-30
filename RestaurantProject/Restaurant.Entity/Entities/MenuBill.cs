using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.Entities
{
    public class MenuBill
    {
        public int MenuId { get; set; }
        public int BillId { get; set;}

        public Menu Menu { get; set; }
        public Bill Bill { get; set; }
    }
}
