using Restaurant.Entity.Entities;
using Restaurant.Entity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.ViewModels
{
    public class OrderVM
    {
       
        public int TableofRestaurantId { get; set; }
        [Display(Name = "Sipariş Adet")]
        public int Quantity { get; set; }
        [Display(Name = "Açıklama")]
        public string? Description { get; set; }

        public int ProductId { get; set; }
        public int EmployeeId { get; set; }
        [Display(Name = "Toplam Fiyat")]
        public decimal TotalPrices { get; set; }
        [Display(Name = "Hazırlanma Durumu")]
        public OrderStatus StatusOfOrder { get; set; }
        public Product? Product { get; set; }
    }
}
