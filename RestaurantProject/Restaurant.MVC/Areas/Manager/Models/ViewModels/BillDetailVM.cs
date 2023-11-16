using Restaurant.Entity.Entities;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Areas.Manager.Models.ViewModels
{
    public class BillDetailVM
    {
        public int Id { get; set; }
        public TableOfRestaurant TableOfRestaurant { get; set; }
        public Product Product { get; set; }
        [Display(Name = "Ürün Adı")]
        public string ProductName { get; set; }
        [Display(Name = "Birim Fiyat")]
        public decimal Price { get; set; }
        [Display(Name = "Miktar Kg/Adet")]
        public decimal Quantity { get; set; }
        [Display(Name = "Toplam fiyat")]
        public decimal TotalPrice { get; set; }
        [Display(Name = "Masa Adı")]
        public string TableName { get; set; }
     


    }

}
