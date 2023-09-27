using Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class Order:BaseEntity
    {
        public string Description { get; set; }
        public OrderStatus Status { get; set; }
        public int TableofRestaurantId { get;set; }
        public int WaiterId { get;set; }
        

        public Waiter Waiter { get;set; }
        public TableOfRestaurant TableOfRestaurant { get; set; }
       
    }
}
