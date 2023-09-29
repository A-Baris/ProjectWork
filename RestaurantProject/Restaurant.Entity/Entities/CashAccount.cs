using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class CashAccount:BaseEntity
    {
        public virtual List<Bill> Bills { get; set; }
    }
}
