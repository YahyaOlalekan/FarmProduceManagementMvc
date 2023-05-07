using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FarmProduceManagement.AppDbContext;
using FarmProduceManagement.Models.Entities;
using FarmProduceManagement.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FarmProduceManagement.Repositories.Implementations
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        TransactionRepository(Context context)
        {
            _context = context;
        }

        public Transaction Get(string id)
        {
            return _context.Transactions
            .Include(a => a.TransactionProduces)
            .SingleOrDefault(a => a.Id == id && a.IsDeleted == false);
        }

        public Transaction Get(Expression<Func<Transaction, bool>> expression)
        {
            return _context.Transactions
            .Where(a => a.IsDeleted == false)
            .Include(a => a.TransactionProduces)
            .SingleOrDefault(expression);
        }

        public IEnumerable<Transaction> GetAll()
        {
            return _context.Transactions
           .Where(a => a.IsDeleted == false)
           .ToList();
        }

        public IEnumerable<Transaction> GetSelected(List<string> ids)
        {
            return _context.Transactions
            .Where(a => ids.Contains(a.Id) && a.IsDeleted == false)
            .Include(a => a.TransactionProduces)
            .ToList();
        }

        public IEnumerable<Transaction> GetSelected(Expression<Func<Transaction, bool>> expression)
        {
            return _context.Transactions
            .Where(expression)
            .Include(a => a.TransactionProduces)
            .ToList();
        }

    }
}