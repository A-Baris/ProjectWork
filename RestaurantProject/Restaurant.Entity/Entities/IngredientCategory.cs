using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.Entities
{
    public class IngredientCategory:BaseEntity
    {
        public string CategoryName { get; set; }
        public virtual List<Ingredient> Ingredients { get; set; }
    }
}
