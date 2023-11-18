using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.DTOs
{
    public class ReservationUpdateDTO
    {
        public int Id { get; set; }
        public DateTime ReservationDate { get; set; }
      
        public string? Description { get; set; }
        
        public int GuestNumber { get; set; }
    }
}
