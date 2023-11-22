using FluentValidation;
using Restaurant.Entity.ViewModels;

namespace Restaurant.MVC.Validators
{
    public class IngredientVMValidator:AbstractValidator<IngredientVM>
    {
        public IngredientVMValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Malzeme Adı boş bırakılamaz");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Lütfen bir Kategori seçiniz");

            RuleFor(x => x.SupplierId)
                .NotEmpty().WithMessage("Lütfen bir Tedarikçi seçiniz");

            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Miktar KG/Adet boş geçilemez")
                .GreaterThanOrEqualTo(1).WithMessage("Miktar KG/Adet 1 'den küçük olamaz");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Fiyat boş geçilemez")
                .GreaterThanOrEqualTo(1).WithMessage("Fiyat 1 'den küçük olamaz");

        }
    }
}
