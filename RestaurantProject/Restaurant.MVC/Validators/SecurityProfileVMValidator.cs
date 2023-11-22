using FluentValidation;
using Restaurant.MVC.Models.ViewModels;

namespace Restaurant.MVC.Validators
{
    public class SecurityProfileVMValidator:AbstractValidator<SecurityProfileVM>
    {
        public SecurityProfileVMValidator()
        {
            RuleFor(x => x.PasswordNow)
                .NotEmpty().WithMessage("Güncel Şifre boş geçilemez");


            RuleFor(x => x.PasswordHash)
                .NotEmpty().WithMessage("Yeni Şifre boş geçilemez");

            RuleFor(x => x.PasswordConfirmed)
                .NotEmpty().WithMessage("Yeni Şifre(Tekrar) boş geçilemez")
                .Equal(x => x.PasswordHash).WithMessage("Şifreler uyuşmuyor");
        }
    }
}
