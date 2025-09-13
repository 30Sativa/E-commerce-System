
using EcommerceSystem.Application.DTOs.Responses.Customer;
using MediatR;

namespace EcommerceSystem.Application.Features.Customer.Queries
{
    public record GetCustomerByIdQuery(int id) : IRequest<CustomerResponse?>; 
   
}
