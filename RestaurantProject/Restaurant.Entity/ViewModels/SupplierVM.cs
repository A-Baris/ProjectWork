using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Areas.Manager.Models.ViewModels
{
    public class SupplierVM
    {
    
        public string CompanyName { get; set; }
       
        public string Adress { get; set; }
      
        public string Phone { get; set; }
      
        public string Email { get; set; }
      
        public string ContactPerson { get; set; }
    
        public string Title { get; set; }
     
    
       
        public string? Description { get; set; }
    }
}
