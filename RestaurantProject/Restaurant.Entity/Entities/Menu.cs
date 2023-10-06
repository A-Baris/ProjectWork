using Restaurant.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.Entities
{
    public class Menu : BaseEntity
    {
        [MaxLength(100)]
        public string MenuName { get; set; }
        public List<Dish> Dishes { get; set; }
        public List<Drink> Drinks { get; set; }
    }
     

}