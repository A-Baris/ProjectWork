using Restaurant.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class Menu:BaseEntity
    {
      public IEnumerable<MenuDish> MenuDishes { get; set; }
      public IEnumerable<MenuDrink> MenuDrinks { get; set; }
    }
}
