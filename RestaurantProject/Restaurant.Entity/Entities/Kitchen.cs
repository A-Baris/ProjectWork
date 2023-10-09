using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.Entities
{
    public class Kitchen : BaseEntity
    {
       
        [MaxLength(50)]
        public string KitchenName { get; set; }
        [MaxLength(250)]
        public string? Description { get; set; }
  
        public virtual List<Order> Orders { get; set; }
        public virtual List<Product> Products { get; set; }
        

    }
}
