using Restaurant.Entity.Entities;
using Restaurant.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.ViewModels
{
    public class OrderVM
    {
       
        public int TableofRestaurantId { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }

        public int ProductId { get; set; }
        public int EmployeeId { get; set; }
        public decimal TotalPrices { get; set; }
        public OrderStatus StatusOfOrder { get; set; }
        public Product? Product { get; set; }
    }
}
