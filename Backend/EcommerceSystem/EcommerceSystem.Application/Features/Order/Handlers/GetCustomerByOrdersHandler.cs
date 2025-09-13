
using AutoMapper;
using EcommerceSystem.Application.DTOs.Responses.Order;
using EcommerceSystem.Application.Features.Order.Queries;
using EcommerceSystem.Application.Interfaces.Repositories;
using MediatR;

namespace EcommerceSystem.Application.Features.Order.Handlers
{
    public class GetCustomerByOrdersHandler : IRequestHandler<GetCustomerByOrdersQuery, List<OrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetCustomerByOrdersHandler(
            IOrderRepository orderRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        public async Task<List<OrderResponse>> Handle(GetCustomerByOrdersQuery request, CancellationToken cancellationToken)
        {
            var list = await _orderRepository.GetByCustomerIdAsync(request.id);
            return _mapper.Map<List<OrderResponse>>(list);
        }
    }
}
