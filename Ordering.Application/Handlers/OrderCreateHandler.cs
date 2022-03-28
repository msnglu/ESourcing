using AutoMapper;
using MediatR;
using Ordering.Application.Commands.OrderCreate;
using Ordering.Application.Responses;
using Ordering.Domain.Entities;
using Ordering.Domain.Repositories;

namespace Ordering.Application.Handlers
{
    public class OrderCreateHandler : IRequestHandler<OrderCreateCommand, OrderResponse>
    {
        private readonly IOrderRepository _orderRespository;
        private readonly IMapper _mapper;
        public OrderCreateHandler(IOrderRepository orderRespository,
            IMapper mapper)
        {
            _mapper = mapper;
            _orderRespository = orderRespository;
        }
        public async Task<OrderResponse> Handle(OrderCreateCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<Order>(request);
            if (orderEntity == null)
                throw new ApplicationException("Entity could not be mapped");
            var order = await _orderRespository.AddAsync(orderEntity);
            var orderResponse = _mapper.Map<OrderResponse>(order);
            return orderResponse;

        }
    }
}
