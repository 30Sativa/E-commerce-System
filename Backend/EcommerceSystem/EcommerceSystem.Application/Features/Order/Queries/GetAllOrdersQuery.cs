using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceSystem.Application.DTOs.Responses.Order;
using MediatR;

namespace EcommerceSystem.Application.Features.Order.Queries
{
    public class GetAllOrdersQuery : IRequest<IEnumerable<OrderResponse>>
    {
    }
}
