using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceSystem.Application.Interfaces;
using EcommerceSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Storage;

namespace EcommerceSystem.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync()
        {
            if(_transaction == null)
            {
                _transaction = await _context.Database.BeginTransactionAsync();
            }
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                await _transaction?.CommitAsync();
            }
            finally
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                }
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if(_transaction == null) return;
            try
            {
                await _transaction?.RollbackAsync();

            }
            finally
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                }
                _transaction = null;
            }
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
