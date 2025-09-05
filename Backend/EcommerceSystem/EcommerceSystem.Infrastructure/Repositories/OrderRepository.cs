using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EcommerceSystem.Application.Interfaces.Repositories;
using EcommerceSystem.Domain.Entities;
using EcommerceSystem.Infrastructure.Persistence;
using EcommerceSystem.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceSystem.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public OrderRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<OrderEntity> AddAsync(OrderEntity orderentity)
        {
            var dborder = _mapper.Map<Order>(orderentity);
             _context.Orders.Add(dborder);
            //await  _context.SaveChangesAsync();
            return _mapper.Map<OrderEntity>(dborder);
        }


        public async Task<List<OrderEntity>> GetAllAsync()
        {
            var orders = await _context.Orders.Include(o => o.Orderdetails)
                .ToListAsync();
            return _mapper.Map<List<OrderEntity>>(orders);
        }

        public async Task<List<OrderEntity>> GetByCustomerIdAsync(int customerId)
        {
            var dborder = await _context.Orders
                .Include(o => o.Orderdetails)
                .Where(o => o.Customerid == customerId)
                .ToListAsync();

            return _mapper.Map<List<OrderEntity>>(dborder);
        }

        public async Task<OrderEntity?> GetByIdAsync(int id)
        {
            var  dborder = await _context.Orders
                .Include(o => o.Orderdetails)
                .FirstOrDefaultAsync(o => o.Orderid == id);
            return _mapper.Map<OrderEntity>(dborder);
        }

        public async Task<OrderEntity> UpdateAsync(OrderEntity order)
        {
            var dborder = _mapper.Map<Order>(order);
            _context.Orders.Update(dborder);
           // await _context.SaveChangesAsync();
            return _mapper.Map<OrderEntity>(dborder);
        }
    }
}
