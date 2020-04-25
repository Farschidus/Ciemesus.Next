using Ciemesus.Api.Contracts;
using Ciemesus.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Ciemesus.Api.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult HandledResult<T>(this Controller controller, IResponseBase<T> response, string redirectAction)
        {
            if (response.IsValid)
            {
                return new OkObjectResult(new
                {
                    redirect = controller.Url.Action(redirectAction),
                });
            }
            else
            {
                return new BadRequestObjectResult(GetValidationFailureResult(response));
            }
        }

        public static IActionResult HandledResult(this Controller controller, IResponseBase response, string redirectAction)
        {
            if (response.IsValid)
            {
                return new OkObjectResult(new
                {
                    redirect = controller.Url.Action(redirectAction),
                });
            }
            else
            {
                return new BadRequestObjectResult(GetValidationFailureResult(response));
            }
        }

        public static IActionResult HandledResult<T>(this Controller controller, IResponseBase<T> response)
        {
            if (response.IsValid && controller != null)
            {
                return new OkObjectResult(response.Result);
            }
            else
            {
                return new BadRequestObjectResult(GetValidationFailureResult(response));
            }
        }

        public static IActionResult HandledResult(this Controller controller, IResponseBase response)
        {
            if (response.IsValid && controller != null)
            {
                return new NoContentResult();
            }
            else
            {
                return new BadRequestObjectResult(GetValidationFailureResult(response));
            }
        }

        private static ValidationFailureResult GetValidationFailureResult<T>(IResponseBase<T> response)
        {
            var errors = response.Errors.Select(e => e.ToValidationFailure());
            var failureResult = new ValidationFailureResult(errors);

            return failureResult;
        }

        private static ValidationFailureResult GetValidationFailureResult(IResponseBase response)
        {
            var errors = response.Errors.Select(e => e.ToValidationFailure());
            var failureResult = new ValidationFailureResult(errors);

            return failureResult;
        }
    }
}
