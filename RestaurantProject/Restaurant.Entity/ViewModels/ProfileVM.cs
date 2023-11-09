using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Models.ViewModels
{
    public class ProfileVM
    {
    
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string CustomerName { get;set; }
        [Required]
        public string CustomerSurname { get;set; }
    }
}
