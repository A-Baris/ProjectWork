using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.Entities
{
    public class DrinkCategory : BaseEntity
    {
        [MaxLength(100)]
        public string CategoryName { get; set; }
        [MaxLength(200)]
        public string? Description { get; set; }
        public virtual List<Drink> Drinks { get; set; }
    }
}
