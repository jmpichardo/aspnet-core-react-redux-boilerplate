using Microsoft.EntityFrameworkCore.Storage;
using Renetdux.Infrastructure.DataModel;
using System;
using System.Threading.Tasks;

namespace Renetdux.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RenetduxDatabaseContext _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(RenetduxDatabaseContext context)
        {
            _context = context;
        }

        public async Task CommitAsync()
        {
            if (_transaction != null)
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Rolling back because of: " + e.Message);
                    _transaction.Rollback();
                }
            }
            else
            {
                await _context.SaveChangesAsync();
            }
        }

        public async Task StartTransaction()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public void CommitTransaction()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }
    }
}
