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
                .NotEmpty().WithMessage("Boş bırakılamaz")
                .GreaterThanOrEqualTo(1).WithMessage("Değer 0 dan büyük olmalıdır");

            RuleFor(x=>x.CategoryId)
               .NotEmpty().WithMessage("Lütfen Bir Kategori Seçiniz");

            RuleFor(x => x.MenuId)
             .NotEmpty().WithMessage(" Lütfen Bir Menu Seçiniz");
             





          
        }
    }
}
