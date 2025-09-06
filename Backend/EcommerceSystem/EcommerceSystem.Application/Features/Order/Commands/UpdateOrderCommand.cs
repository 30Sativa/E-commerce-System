using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceSystem.Application.DTOs.Requests.Order;
using EcommerceSystem.Application.DTOs.Responses.Order;
using EcommerceSystem.Domain.Common.Enum;
using MediatR;

namespace EcommerceSystem.Application.Features.Order.Commands
{
    public class UpdateOrderCommand : IRequest<OrderResponse>
    {
        public UpdateOrderRequest Order { get; }

        public UpdateOrderCommand(UpdateOrderRequest order)
        {
            Order = order;
        }
    }
}
