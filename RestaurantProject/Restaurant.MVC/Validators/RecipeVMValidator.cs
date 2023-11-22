using FluentValidation;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;

namespace Restaurant.MVC.Validators
{
    public class RecipeVMValidator:AbstractValidator<RecipeVM>
    {
        public RecipeVMValidator()
        {

            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Miktar KG/Adet boş bırakılamaz")
                .GreaterThan(0).WithMessage("Miktar KG/Adet 0'dan küçük olamaz");

            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Lütfen Ürün seçimi yapınız");


            RuleFor(x => x.IngredientId)
                .NotEmpty().WithMessage("Lütfen Malzeme seçimi yapınız");



        }
    }
}
