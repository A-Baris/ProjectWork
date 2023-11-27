using Restaurant.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.ViewModels
{
    public class ReservationCreateVM
    {
        [DisplayName("Rezervasyon Tarihi")]
        public DateTime ReservationDate { get; set; }
        [DisplayName("Masa")]
        public int? TableOfRestaurantId { get; set; }
        [DisplayName("Müşteri")]
        public int? CustomerId { get; set; }
        [DisplayName("Açıklama")]
        public string? Description { get; set; }
        [DisplayName("Misafir Sayısı")]
        public int GuestNumber { get; set; }
      
        public CustomerVM CustomerVM { get; set; }



    }
}
