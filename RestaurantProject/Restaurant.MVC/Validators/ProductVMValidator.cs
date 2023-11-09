using FluentValidation;
using Restaurant.Entity.ViewModels;

namespace Restaurant.MVC.Validators
{
    public class ProductVMValidator: AbstractValidator<ProductVM>
    {
        public ProductVMValidator()
        {
            RuleFor(x => x.ProductName).NotNull().WithMessage("null bırakılamaz");
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("Boş bırakılamaz");
            RuleFor(x => x.Price).NotNull().NotEmpty().InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} 0 dan büyük olmalıdır");
        }
    }
}
