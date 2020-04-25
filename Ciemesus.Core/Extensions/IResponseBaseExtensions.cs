using Ciemesus.Core.Contracts;
using FluentValidation.Results;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace Ciemesus.Core.Extensions
{
    public static class IResponseBaseExtensions
    {
        public static async void AddErrors(this IResponseBase result, HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.InternalServerError || response.StatusCode == HttpStatusCode.NotFound)
            {
                result.AddErrors(responseContent, "Internal Server Error", isRetryMessage: true);
                return;
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden)
            {
                var errors = new List<ValidationFailure> { new ValidationFailure("Unauthorized", "Unauthorized or Forbidden to access the service. Please contact administrator.") };
                result.Errors = errors;
                return;
            }

            if (response.StatusCode == HttpStatusCode.UnsupportedMediaType)
            {
                var errors = new List<ValidationFailure> { new ValidationFailure("UnsupportedMediaType", "The request is in unsupported format. Please contact administrator.") };
                result.Errors = errors;
                return;
            }

            result.AddErrors(responseContent);
        }

        public static void AddErrors(this IResponseBase result, IEnumerable<ValidationFailure> errors)
        {
            result.Errors = errors;
        }

        private static void AddErrors(this IResponseBase result, string responseContent)
        {
            var errors = JsonConvert.DeserializeObject<Dictionary<string, List<object>>>(responseContent);
            var errorResponse = new List<ValidationFailure>();
            if(errors != null)
            {
                foreach (var field in errors)
                {
                    foreach (var error in field.Value)
                    {
                        errorResponse.Add(new ValidationFailure(field.Key, error.ToString()));
                    }
                }

                result.Errors = errorResponse;
            }
        }

        private static void AddErrors(this IResponseBase result, string responseContent, string propertyName, bool isRetryMessage = false)
        {
            var errors = new List<ValidationFailure> { new ValidationFailure(propertyName, responseContent) { ErrorCode = isRetryMessage ? "RetryMessage" : string.Empty } };
            result.Errors = errors;
        }
    }
}
