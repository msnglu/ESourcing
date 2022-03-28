using Ordering.Application.Responses;

namespace Ordering.Application.Handlers
{
    public interface IGetOrdersByUsernameHandler
    {
        Task<IEnumerable<OrderResponse>> Handle(GetOrdersByUsernameHandler request, CancellationToken cancellationToken);
    }
}