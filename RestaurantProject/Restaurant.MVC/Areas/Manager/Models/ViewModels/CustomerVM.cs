using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Areas.Manager.Models.ViewModels
{
    public class CustomerVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "boş bırakılamaz")]
        public string Name { get; set; }
        [Required(ErrorMessage = "boş bırakılamaz")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "boş bırakılamaz")]
        public string? Adress { get; set; }
        [Required(ErrorMessage = "boş bırakılamaz")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "boş bırakılamaz")]
        public DateTime ReservationDate { get; set; }
        [Required(ErrorMessage = "boş bırakılamaz")]
        public string ReservationDescription { get; set; }

        public int? TableOfRestaurantId { get; set; }
    }
}
