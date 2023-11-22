using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Restaurant.MVC.Utility.ModelStateHelper
{
    public static class ModelStateHelper
    {
        public static void AddErrorsToModelState(ModelStateDictionary modelState, IEnumerable<ValidationError> errors)
        {
            foreach (var error in errors)
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
        }
    }
}
