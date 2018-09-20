using Ciemesus.Api.Contracts;
using FluentValidation.Results;

namespace Ciemesus.Api.Extensions
{
    public static class ValidationFailureExtensions
    {
        public static ApiValidationFailureResult.Error ToApiValidationFailure(this ValidationFailure validationFailure)
        {
            return new ApiValidationFailureResult.Error()
            {
                PropertyName = validationFailure.PropertyName,
                ErrorMessage = validationFailure.ErrorMessage,
                AttemptedValue = validationFailure.AttemptedValue,
                ErrorCode = validationFailure.ErrorCode
            };
        }
    }
}
