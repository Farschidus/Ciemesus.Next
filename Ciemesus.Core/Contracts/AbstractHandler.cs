using FluentValidation.Results;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ciemesus.Core.Contracts
{
    public abstract class AbstractHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

        protected virtual ResponseBase<TResult> Response<TResult>(TResult result)
            where TResult : new()
        {
            var response = new ResponseBase<TResult>
            {
                Result = result,
            };

            return response;
        }

        protected virtual ResponseBase<TResult> Response<TResult>(TResult result, IEnumerable<ValidationFailure> errors)
            where TResult : new()
        {
            var response = new ResponseBase<TResult>
            {
                Result = result,
                Errors = errors,
            };

            return response;
        }

        protected virtual ResponseBase Response(IEnumerable<ValidationFailure> errors)
        {
            var response = new ResponseBase
            {
                Errors = errors,
            };

            return response;
        }

        protected virtual ResponseBase Response()
        {
            var response = new ResponseBase();

            return response;
        }
    }

    public abstract class AbstractHandler<TRequest> : IRequestHandler<TRequest>
        where TRequest : IRequest
    {
        async Task<Unit> IRequestHandler<TRequest, Unit>.Handle(TRequest request, CancellationToken cancellationToken)
        {
            await Handle(request, cancellationToken).ConfigureAwait(false);
            return Unit.Value;
        }

        protected abstract Task Handle(TRequest request, CancellationToken cancellationToken);
    }
}
