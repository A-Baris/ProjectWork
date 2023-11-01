using Restaurant.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.DTOs
{
    public class ReservationDTO
    {
        public DateTime ReservationDate { get; set; }
        public string? Description { get; set; }
        public int? CustomerId { get; set; }
        public string? Email { get; set; }







    }
}
