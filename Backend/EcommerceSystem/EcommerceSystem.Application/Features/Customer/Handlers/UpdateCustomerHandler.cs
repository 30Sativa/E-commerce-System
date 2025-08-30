using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EcommerceSystem.Application.DTOs.Responses.Customer;
using EcommerceSystem.Application.Features.Customer.Commands;
using EcommerceSystem.Application.Interfaces.Repositories;
using EcommerceSystem.Domain.Entities;
using MediatR;

namespace EcommerceSystem.Application.Features.Customer.Handlers
{
    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, CustomerResponse>
    {

        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public UpdateCustomerHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }
        public async Task<CustomerResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.id);
            if (customer == null) return null; // controller sẽ check null => 404

            if (!string.IsNullOrWhiteSpace(request.Request.Name))
                customer.Name = request.Request.Name;

            if (!string.IsNullOrWhiteSpace(request.Request.Role))
                customer.Role = request.Request.Role;

            await _customerRepository.UpdateAsync(customer);

            var updated = await _customerRepository.GetByIdAsync(request.id);
            return _mapper.Map<CustomerResponse>(updated);
        }

    }
}
