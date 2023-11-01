using Restaurant.Entity.Entities;
using Restaurant.Entity.Enums;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Areas.Manager.Models.ViewModels
{
    public class ReservationCreateVM
    {
        [Required(ErrorMessage = "Tarih boş bırakılamaz")]
        public DateTime ReservationDate { get; set; }
        public int? TableOfRestaurantId { get; set; }
        public int? CustomerId { get; set; }
        [Required(ErrorMessage = "Açıklama boş bırakılamaz")]
        public string Description { get; set; }
        public string Email { get; set; }
        public ReservationStatus? ReservationStatus { get; set; }

     


     
        public string? Name { get; set; }
     
        public string? Surname { get; set; }

        public string? Adress { get; set; }
       
        public string? Phone { get; set; }
    }
}
