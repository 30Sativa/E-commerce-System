using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceSystem.Application.DTOs.Responses.Customer;
using MediatR;

namespace EcommerceSystem.Application.Features.Customer.Queries
{
    public record GetAllCustomersQuery : IRequest<IEnumerable<CustomerResponse>>;
    
}
