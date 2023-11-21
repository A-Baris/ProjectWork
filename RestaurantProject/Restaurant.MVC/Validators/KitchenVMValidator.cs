using FluentValidation;
using Restaurant.Entity.ViewModels;

namespace Restaurant.MVC.Validators
{
    public class KitchenVMValidator: AbstractValidator<KitchenVM>
    {
        public KitchenVMValidator()
        {
            
            RuleFor(x => x.KitchenName)
            .NotEmpty()
            .WithMessage("Mutfak Adı boş bırakılamaz!!")
            .MinimumLength(5).WithMessage("Açıklama 3 karakterden az olamaz");

            RuleFor(x => x.Description)
           .NotEmpty()
           .WithMessage("Açıklama boş bırakılamaz!!")
           .MinimumLength(5).WithMessage("Açıklama 5 karakterden az olamaz");
          

        }
    }
}
