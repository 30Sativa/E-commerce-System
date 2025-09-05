using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceSystem.Application.DTOs.Requests.Order;
using EcommerceSystem.Application.DTOs.Responses.Order;
using MediatR;

namespace EcommerceSystem.Application.Features.Order.Commands
{
    public class CreateOrderCommand : IRequest<OrderResponse>
    {
        public CreateOrderRequest Order { get; set; }

        public CreateOrderCommand(CreateOrderRequest order)
        {
            Order = order;
        }
    }
}
