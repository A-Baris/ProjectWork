using Restaurant.Entity.Entities;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Areas.Manager.Models.ViewModels
{
    public class IngredientVM
    {
        public int Id { get; set; }
        public string IngredientName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public int IngredientCatgoryId { get; set; }
        public int KitchenId { get; set; }
        
    }
}
