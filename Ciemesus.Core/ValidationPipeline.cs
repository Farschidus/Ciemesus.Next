using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ciemesus.Core
{
    public class ValidationPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICiemesusRequest<TResponse>
        where TResponse : ICiemesusResponse
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private TResponse _response;

        public ValidationPipeline(IEnumerable<IValidator<TRequest>> validators, TResponse response)
        {
            _validators = validators;
            _response = response;
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next)
        {
            var failures = _validators
                .Select(v => v.ValidateAsync(request).Result)
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Any())
            {
                _response.Errors = failures;
                return Task.FromResult(_response);
            }

            return next();
        }
    }
}
