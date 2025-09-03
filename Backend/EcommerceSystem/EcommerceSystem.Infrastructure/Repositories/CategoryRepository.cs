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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoryEntity> AddAsync(CategoryEntity categoryEntity)
        {
            var category = _mapper.Map<Category>(categoryEntity);
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return _mapper.Map<CategoryEntity>(category);
        }

        public Task DeleteAsync(CategoryEntity categoryEntity)
        {
            var category = _mapper.Map<Category>(categoryEntity);
            _context.Categories.Remove(category);
            return _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryEntity>> GetAllAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return _mapper.Map<IEnumerable<CategoryEntity>>(categories);
        }

        public async Task<CategoryEntity?> GetByIdAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            return category == null ? null : _mapper.Map<CategoryEntity>(category);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(CategoryEntity category)
        {
            var categories = await _context.Categories.FindAsync(category.Categoryid);
            if (categories == null) return false;
            _mapper.Map(category, categories);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
