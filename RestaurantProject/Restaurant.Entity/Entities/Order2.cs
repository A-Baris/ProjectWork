using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.Entities
{
    public class Order2
    {
        public Dictionary<int,OrderItem> _item = new Dictionary<int,OrderItem>();
        public void AddItem(OrderItem orderItem)
        {
            if(_item.ContainsKey(orderItem.Id))
            {
                _item[orderItem.Id].Quantity += 1;
                return;
            }
            _item.Add(orderItem.Id, orderItem);
        }

    }
}
