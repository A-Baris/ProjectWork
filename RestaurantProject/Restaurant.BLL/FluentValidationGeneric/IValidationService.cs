

using Restaurant.MVC.Utility.ModelStateHelper;

namespace Restaurant.MVC.Validators
{
    public interface IValidationService<T> 
    {
        IEnumerable<ValidationError> GetValidationErrors(T model);

    }
}
