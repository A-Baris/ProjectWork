using FluentValidation;
using Restaurant.Entity.ViewModels;

namespace Restaurant.MVC.Validators
{
    public class CategoryVMValidator:AbstractValidator<CategoryVm>
    {
        public CategoryVMValidator()
        {
            RuleFor(x => x.CategoryName)
                .NotEmpty().NotNull().WithMessage("Kategori Adı boş bırakılamaz");
        }
    }
}
