using Restaurant.Entity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.Entities
{
    public class Reservation:BaseEntity
    {
        public DateTime ReservationDate { get; set; }
        public int? TableOfRestaurantId { get; set; }
        public int? CustomerId { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }

        public ReservationStatus ReservationStatus { get; set; }

        public TableOfRestaurant? TableOfRestaurant { get; set; }
        public Customer? Customer { get; set; }
    }
}
