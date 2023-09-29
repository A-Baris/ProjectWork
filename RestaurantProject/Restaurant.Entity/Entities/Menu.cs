using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class Menu:BaseEntity
    {
      public ICollection<MenuDish> MenuDishes { get; set; }
    }
}
