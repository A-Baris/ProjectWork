using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class DrinkCategory : BaseEntity
    {
        public virtual List<Drink> Drinks { get; set; }
    }
}
