using MediatR;

namespace Ciemesus.Core
{
    public interface ICiemesusRequest<out TResponse> : IRequest<TResponse>
        where TResponse : ICiemesusResponse
    {
    }
}
