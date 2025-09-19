using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EcommerceSystem.Application.Features.Customer.Commands;
using EcommerceSystem.Application.Interfaces;
using EcommerceSystem.Application.Interfaces.Repositories;
using MediatR;

namespace EcommerceSystem.Application.Features.Customer.Handlers
{
    public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, bool>
    {

        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitofwork;



        public DeleteCustomerHandler(ICustomerRepository customerRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _unitofwork = unitOfWork;
        }
        

        
        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            _unitofwork.BeginTransactionAsync();
            var existingCustomer = await _customerRepository.GetByIdAsync(request.id);
            if (existingCustomer == null) return false;
            await _customerRepository.DeleteAsync(existingCustomer);
             _unitofwork.SaveChangesAsync();
            return true;
           
        }
    }
}
