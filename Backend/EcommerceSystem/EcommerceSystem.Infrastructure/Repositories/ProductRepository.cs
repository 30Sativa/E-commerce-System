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
    public class ProductRepository : IProductRepository
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;


        public ProductRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<ProductEntity> AddAsync(ProductEntity productEntity)
        {
            var product = _mapper.Map<Product>(productEntity);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductEntity>(product);
        }
            
        public async Task DeleteAsync(ProductEntity productEntity)
        {
            var product = await _context.Products.FindAsync(productEntity.Id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
        }

        public async Task<IEnumerable<ProductEntity>> GetAllAsync()
        {
            var product = await _context.Products.Include(cate => cate.Category).ToListAsync();
            return _mapper.Map<IEnumerable<ProductEntity>>(product);
        }

        public async Task<ProductEntity?> GetByIdAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Productid == id);

            return _mapper.Map<ProductEntity?>(product);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(ProductEntity productEntity)
        {
            var product = await _context.Products.FindAsync(productEntity.Id);
            if (product == null) return false;
            _mapper.Map(productEntity, product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
