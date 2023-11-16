using Restaurant.Entity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.DTOs
{
    public class ReservationDTO
    {
        [Display(Name = "Rezervasyon Tarih")]
        public DateTime ReservationDate { get; set; }
        [Display(Name = "Açıklama")]
        public string? Description { get; set; }
      
       







    }
}
