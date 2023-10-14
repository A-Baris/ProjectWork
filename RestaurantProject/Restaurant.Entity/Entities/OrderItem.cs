using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.Entities
{
    public class OrderItem
    {
        public OrderItem()
        {
            Quantity = 1;
        }
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal Subtotal
        {
            get
            {
                return Quantity * Price;
            }

        }
    }
}
