using MediatR;

namespace Ciemesus.Core
{
    public interface ICiemesusAsyncRequestHandler<in TRequest, TResponse> : IAsyncRequestHandler<TRequest, TResponse>
    where TRequest : ICiemesusRequest<TResponse>
    where TResponse : ICiemesusResponse
    {
    }
}
