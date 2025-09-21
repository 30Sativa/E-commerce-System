using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EcommerceSystem.Application.DTOs.Responses.Customer;
using EcommerceSystem.Application.Features.Customer.Commands;
using EcommerceSystem.Application.Interfaces;
using EcommerceSystem.Application.Interfaces.Repositories;
using MediatR;

namespace EcommerceSystem.Application.Features.Customer.Handlers
{
    public class ChangePasswordHanlder : IRequestHandler<ChangePasswordCommand, ChangePasswordResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitofwork;

        public ChangePasswordHanlder(ICustomerRepository customerRepository, IMapper mapper, IUnitOfWork unitofwork)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _unitofwork = unitofwork;
        }
        public async Task<ChangePasswordResponse> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            // Assuming ChangePasswordCommand has a CustomerId property
            var user = await _customerRepository.GetByIdAsync(request.request.CustomerId);
            if (user == null)
                throw new Exception("User not found");

            // Validate old password
            //Console.WriteLine($"OldPassword sent: '{request.request.OldPassword}'");
            //Console.WriteLine($"PasswordHash in DB: '{user.PasswordHash}'");
            bool isValidate = BCrypt.Net.BCrypt.Verify(request.request.OldPassword, user.PasswordHash);
            //Console.WriteLine($"Password verify result: {isValidate}");
            if (!isValidate)
                throw new Exception("Old Password is incorrect");

            // Update password hash with new password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.request.NewPassword);
            await _customerRepository.UpdateAsync(user);
            await _unitofwork.SaveChangesAsync();

            // Map to response
            var response = _mapper.Map<ChangePasswordResponse>(user);
            return response;
        }
    }
}
