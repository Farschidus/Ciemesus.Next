using Ciemesus.Core;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Ciemesus.Authentication.Extensions
{
    public static class ModelStateExtensions
    {
        public static void AddErrors(this ModelStateDictionary modelState, ICiemesusResponse response)
        {
            foreach (var error in response.Errors)
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
        }
    }
}
