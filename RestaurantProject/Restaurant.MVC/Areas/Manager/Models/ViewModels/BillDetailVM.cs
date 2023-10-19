using Restaurant.Entity.Entities;

namespace Restaurant.MVC.Areas.Manager.Models.ViewModels
{
    public class BillDetailVM
    {
        public TableOfRestaurant TableOfRestaurant { get; set; }
        public Product Product { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal TotalPrice { get; set; }
   
        public string TableName { get; set; }
     


    }

}
