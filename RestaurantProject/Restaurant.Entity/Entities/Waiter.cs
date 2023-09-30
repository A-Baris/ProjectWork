using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class Waiter:BaseEntity
    {
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Surname { get; set; }
        [MaxLength(11)]
        public string? Phone { get; set; }
        [MaxLength(11)]
        public string? TcNo { get;set; }
        public virtual List<TableOfRestaurant> TableOfRestaurants { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
