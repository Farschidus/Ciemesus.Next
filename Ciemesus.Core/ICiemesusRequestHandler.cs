using MediatR;

namespace Ciemesus.Core
{
    public interface ICiemesusRequestHandler<in TRequest, out TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : ICiemesusRequest<TResponse>
        where TResponse : ICiemesusResponse
    {
    }
}
