using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EcommerceSystem.Application.Features.Customer.Commands;
using EcommerceSystem.Application.Interfaces.Repositories;
using MediatR;

namespace EcommerceSystem.Application.Features.Customer.Handlers
{
    public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, bool>
    {

        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        

        public DeleteCustomerHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }
        

        
        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var existingCustomer = await _customerRepository.GetByIdAsync(request.id);
            if (existingCustomer == null) return false;
            await _customerRepository.DeleteAsync(existingCustomer);
            var row = await _customerRepository.SaveChangesAsync();
            return row > 0;
        }
    }
}
