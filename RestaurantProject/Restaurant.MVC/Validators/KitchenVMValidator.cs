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
            .WithMessage("Mutfak Adı boş bırakılamaz");
            RuleFor(x => x.KitchenName)
          .NotNull()
          .WithMessage("Mutfak Adı boş bırakılamaz");

          
        }
    }
}
