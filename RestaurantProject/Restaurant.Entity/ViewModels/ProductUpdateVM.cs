using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.ViewModels
{
    public class ProductUpdateVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "boş bırakılamaz")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "boş geçilemez")]
        [Range(0, int.MaxValue, ErrorMessage = "The property must be a non-negative integer.")]
        public decimal Price { get; set; }

        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public int? SupplierId { get; set; }
        public int? KitchenId { get; set; }
        public int? MenuId { get; set; }
    }
}
