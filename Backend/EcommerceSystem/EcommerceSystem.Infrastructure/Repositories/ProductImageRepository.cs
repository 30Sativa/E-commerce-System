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
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductImageRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ProductImageEntity> AddAsync(ProductImageEntity entity)
        {
            var model = _mapper.Map<Productimage>(entity);
            if (model.Ismain)
            {
                //Bỏ cờ main của các ảnh khác cùng products
                var others = await _context.Productimages.Where(x=>x.Productid == model.Productid && x.Ismain).ToListAsync();
                foreach (var other in others)
                {
                    other.Ismain = false;
                }
            }
            model.Createdat = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            await _context.Productimages.AddAsync(model);
            return _mapper.Map<ProductImageEntity>(model);
        }

        public async Task<bool> DeleteAsync(int imageId)
        {
            var model =     await _context.Productimages.FirstOrDefaultAsync(pi => pi.Imageid == imageId);
            if(model == null) return false;
            _context.Productimages.Remove(model);
            return true;
        }

        public async Task<ProductImageEntity?> GetByIdAsync(int imageId)
        {
            var model = await _context.Productimages.FirstOrDefaultAsync(pi => pi.Imageid == imageId);
            return model == null ? null : _mapper.Map<ProductImageEntity>(model);
        }

        public async Task<IEnumerable<ProductImageEntity>> GetByProductIdAsync(int productId)
        {
            return await _context.Productimages
                .Where(pi => pi.Productid == productId)
                .Select(pi => _mapper.Map<ProductImageEntity>(pi))
                .ToListAsync();
        }

        public async Task<bool> SetMainAsync(int productId, int imageId)
        {
            var target  = await _context.Productimages.FirstOrDefaultAsync(pi => pi.Imageid == imageId && pi.Productid == productId);
            if(target == null) return false;

            var currentMains = await _context.Productimages.Where(pi => pi.Productid == productId && pi.Ismain).ToListAsync();
            foreach (var current in currentMains)
            {
                current.Ismain = false;
            }
            target.Ismain = true;
            return true;
        }
    }
}
