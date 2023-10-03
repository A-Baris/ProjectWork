using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Areas.Manager.Models.ViewModels
{
    public class WaiterVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Boş bırakılamaz")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Boş bırakılamaz")]

        public string Surname { get; set; }
        [Required(ErrorMessage = "Boş bırakılamaz")]
        [Phone]

        public string Phone { get; set; }
        [Required(ErrorMessage = "Boş bırakılamaz")]
        public string TcNo { get; set; }
    }
}
