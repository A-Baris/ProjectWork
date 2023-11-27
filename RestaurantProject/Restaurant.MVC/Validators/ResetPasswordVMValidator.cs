using FluentValidation;
using Restaurant.MVC.Models.ViewModels;

namespace Restaurant.MVC.Validators
{
    public class ResetPasswordVMValidator:AbstractValidator<ResetPasswordVM>
    {
        public ResetPasswordVMValidator()
        {
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre boş bırakılamaz")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakterden oluşmalıdır");

            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Şifre Tekrar boş bırakılamaz")
            .Equal(x=>x.Password).WithMessage("Şifreler uyuşmuyor");
        }
           
    }
}
