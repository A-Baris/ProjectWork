using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class Drink : BaseEntity
    {
        public DrinkCategory DrinkCategoryId { get; set; }
        public DrinkCategory DrinkCategory { get; set; }
        public int KitchenId { get; set; }
        public Kitchen Kitchen { get; set; }
        
    }
}
