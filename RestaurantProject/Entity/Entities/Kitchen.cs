using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class Kitchen:BaseEntity
    {
        public int OrderId { get;set; }
        public int IngredientId { get; set; }

        public Order Order { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
