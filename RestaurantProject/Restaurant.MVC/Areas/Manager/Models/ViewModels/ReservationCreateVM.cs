using Restaurant.Entity.Entities;
using Restaurant.Entity.Enums;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Areas.Manager.Models.ViewModels
{
    public class ReservationCreateVM
    {
        public DateTime ReservationDate { get; set; }
        public int? TableOfRestaurantId { get; set; }
        public int? CustomerId { get; set; }
     
        public string? Description { get; set; }
        public ReservationStatus? ReservationStatus { get; set; }

     


     
        public string? Name { get; set; }
     
        public string? Surname { get; set; }

        public string? Adress { get; set; }
       
        public string? Phone { get; set; }
    }
}
