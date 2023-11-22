using FluentValidation;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;

namespace Restaurant.MVC.Validators
{
    public class MenuVMValidator:AbstractValidator<MenuVM>
    {
        public MenuVMValidator()
        {
            RuleFor(x => x.MenuName)
                .NotEmpty().WithMessage("Menu Adı boş bırakılamaz")
                .MinimumLength(2).WithMessage("Menu Adı iki karakterden az olamaz");
        }
    }
}
