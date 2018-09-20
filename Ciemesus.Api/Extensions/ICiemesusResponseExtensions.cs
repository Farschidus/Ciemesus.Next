using Ciemesus.Api.Contracts;
using Ciemesus.Core;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Ciemesus.Api.Extensions
{
    public static class ICiemesusResponseExtensions
    {
        public static IActionResult HandledResult<T>(this ICiemesusResponse<T> response)
        {
            if (response.IsValid)
            {
                return new OkObjectResult(response.Result);
            }
            else
            {
                var errors = response.Errors.Select(e => e.ToApiValidationFailure());
                var failureResult = new ApiValidationFailureResult(errors);

                return new BadRequestObjectResult(failureResult);
            }
        }

        public static IActionResult HandledResult(this ICiemesusResponse response)
        {
            if (response.IsValid)
            {
                return new OkResult();
            }
            else
            {
                var errors = response.Errors.Select(e => e.ToApiValidationFailure());
                var failureResult = new ApiValidationFailureResult(errors);

                return new BadRequestObjectResult(failureResult);
            }
        }
    }
}
