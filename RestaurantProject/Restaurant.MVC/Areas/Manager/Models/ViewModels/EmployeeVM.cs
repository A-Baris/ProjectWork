using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Areas.Manager.Models.ViewModels
{
    public class EmployeeVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Boş bırakılamaz")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Boş bırakılamaz")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Boş bırakılamaz")]
        [Phone]
        public string Phone { get; set; }

   
        public string? TcNo { get; set; }
        [Required(ErrorMessage = "Boş bırakılamaz")]
        public string Title { get; set; }
       
        public string? Notes { get; set; }
    }
}
