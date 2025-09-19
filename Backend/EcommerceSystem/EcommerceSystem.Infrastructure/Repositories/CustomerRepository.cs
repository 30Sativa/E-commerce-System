using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using EcommerceSystem.Application.Interfaces.Repositories;
using EcommerceSystem.Domain.Entities;
using EcommerceSystem.Infrastructure.Persistence;
using EcommerceSystem.Infrastructure.Persistence.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcommerceSystem.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly AppDbContext _context;
        private IMapper _mapper;
         

        public CustomerRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CustomerEntity> AddAsync(CustomerEntity customerEntity)
        {
            var customer = _mapper.Map<Customer>(customerEntity);
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            return _mapper.Map<CustomerEntity>(customer);

        }

        public async Task Changepassword(CustomerEntity customerEntity)
        {
            var customer = await _context.Customers.FindAsync(customerEntity.Id);
            if (customer == null)
            {
                throw new KeyNotFoundException("Customer not found");
            }
            customer.Passwordhash = customerEntity.PasswordHash;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(CustomerEntity customerEntity)
        {
            var customer = await _context.Customers.FindAsync(customerEntity.Id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }
        }

        public async Task<IEnumerable<CustomerEntity>> GetAllAsync()
        {
            var customers = await _context.Customers.ToListAsync();
            return _mapper.Map<IEnumerable<CustomerEntity>>(customers);
        }

        public async Task<CustomerEntity?> GetByEmailAsync(string email)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(e => e.Email.Equals(email));
            return _mapper.Map<CustomerEntity?>(customer);
        }

        public async Task<CustomerEntity?> GetByIdAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            return _mapper.Map<CustomerEntity?>(customer);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(CustomerEntity customerEntity)
        {
            var customer = await _context.Customers.FindAsync(customerEntity.Id);
            if (customer == null) return false;

            if (!string.IsNullOrWhiteSpace(customerEntity.Name))
                customer.Name = customerEntity.Name;

            if (!string.IsNullOrWhiteSpace(customerEntity.Role))
                customer.Role = customerEntity.Role;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
