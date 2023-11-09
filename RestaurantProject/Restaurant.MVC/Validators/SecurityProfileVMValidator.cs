using FluentValidation;
using Restaurant.MVC.Models.ViewModels;

namespace Restaurant.MVC.Validators
{
    public class SecurityProfileVMValidator:AbstractValidator<SecurityProfileVM>
    {
        public SecurityProfileVMValidator()
        {
            RuleFor(x => x.PasswordNow).NotNull().NotEmpty().WithMessage("Güncel şifre boş bırakılamaz");
            RuleFor(x => x.PasswordHash).NotNull().NotEmpty().WithMessage("Yeni şifre boş bırakılamaz");
            RuleFor(x => x.PasswordConfirmed).NotNull().WithMessage("Şifre tekrar şifre boş bırakılamaz");
            RuleFor(x => x.PasswordConfirmed).Equal(x => x.PasswordHash).WithMessage("Şifreler uyuşmuyor");
        }
    }
}
