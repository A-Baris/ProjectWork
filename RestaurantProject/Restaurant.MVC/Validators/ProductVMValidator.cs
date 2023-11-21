using FluentValidation;
using Restaurant.Entity.ViewModels;

namespace Restaurant.MVC.Validators
{
    public class ProductVMValidator: AbstractValidator<ProductVM>
    {
        public ProductVMValidator()
        {

            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Boş bırakılamaz");
                
            RuleFor(x => x.Price)
                .NotNull().NotEmpty().WithMessage("Boş bırakılamaz")
                .InclusiveBetween(1, int.MaxValue).WithMessage(" 0 dan büyük olmalıdır");

            RuleFor(x=>x.CategoryId)
                .NotNull().NotEmpty().WithMessage("Lütfen Bir Kategori Seçiniz");

            RuleFor(x => x.MenuId)
             .NotNull().NotEmpty().WithMessage(" Lütfen Bir Menu Seçiniz");
             





          
        }
    }
}
