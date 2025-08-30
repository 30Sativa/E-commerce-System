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
using BCrypt.Net;
using EcommerceSystem.Application.Common.Exceptions;


namespace EcommerceSystem.Application.Features.Customer.Handlers
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, CreateCustomerResponse>
    {

        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CreateCustomerHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }
        public async Task<CreateCustomerResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var existingEmail = await _customerRepository.GetByEmailAsync(request.Request.Email);
            if (existingEmail != null) 
            { 
                throw new BusinessException("Email already exist"); 
            }
            var entity = _mapper.Map<CustomerEntity>(request.Request);
            entity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Request.Password);
            entity.AuthProvider = "Local";
            entity.CreatedAt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);

            var created = await _customerRepository.AddAsync(entity);

            return _mapper.Map<CreateCustomerResponse>(created);

        }
    }
}
