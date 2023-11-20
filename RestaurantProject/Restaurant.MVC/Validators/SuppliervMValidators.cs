using FluentValidation;
using Restaurant.Entity.Entities;

namespace Restaurant.MVC.Validators
{
    public class SuppliervMValidators:AbstractValidator<Supplier>
    {
        public SuppliervMValidators()
        {
            RuleFor(x => x.ContactPerson)
                .NotNull().WithMessage("Yekili Adı Boş geçilemez");

            //araştır 
        }
    }
}
