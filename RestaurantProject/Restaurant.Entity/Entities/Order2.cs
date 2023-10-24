using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.Entities
{
    public class Order2:BaseEntity
    {
        public Dictionary<int, Order> _item = new Dictionary<int, Order>();
        public void AddItem(Order orderItem)
        {
            if (_item.ContainsKey(orderItem.ProductId))
            {
                _item[orderItem.ProductId].Quantity += 1;
                return;
            }
            _item.Add(orderItem.ProductId, orderItem);
        }
        public int TableOfRestaurantId { get; set; } 
        public string Status { get; set; }
        public TableOfRestaurant TableOfRestaurant { get; set; }

    }
}
