using MediatR;
using Ordering.Application.Responses;

namespace Ordering.Application.Queries
{
    public class GetOrdersBySellerUsernameQuery : IRequest<IEnumerable<OrderResponse>>
    {
        public string  Username { get; set; }

        public GetOrdersBySellerUsernameQuery(string username)
        {
            Username = username;
        }
    }
}
