using Entity.Enum;
using Restaurant.Entity.Entities;
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
        public int KitchenId { get;set; }
     
        public Kitchen Kitchen { get; set; }
        public Waiter Waiter { get;set; }
        public TableOfRestaurant TableOfRestaurant { get; set; }
        public IEnumerable<MenuOrder> MenuOrders { get; set; }


    }
}
