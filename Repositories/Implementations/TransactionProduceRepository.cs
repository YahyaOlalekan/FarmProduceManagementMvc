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
    public class TransactionProduceRepository : BaseRepository<TransactionProduce>, ITransactionProduceRepository
    {
        public TransactionProduceRepository(Context context)
        {
            _context = context;
        }

        public bool CreateTransactionProduce(List<TransactionProduce> transactions)
        {
            _context.AddRange(transactions);
            return true;
        }

        public TransactionProduce Get(string id)
        {
            return _context.TransactionProduces
            .Include(a => a.Transaction)
            .Include(a => a.Produce)
            .SingleOrDefault(a => a.Id == id && a.IsDeleted == false);
        }

        public TransactionProduce Get(Expression<Func<TransactionProduce, bool>> expression)
        {
            return _context.TransactionProduces
            .Where(a => a.IsDeleted == false)
            .Include(a => a.Transaction)
            .Include(a => a.Produce)
            .SingleOrDefault(expression);
        }

        public IEnumerable<TransactionProduce> GetAll()
        {
            return _context.TransactionProduces
           .Where(a => a.IsDeleted == false)
           .ToList();
        }

        public IEnumerable<TransactionProduce> GetSelected(List<string> ids)
        {
            return _context.TransactionProduces
            .Where(a => ids.Contains(a.Id) && a.IsDeleted == false)
            .Include(a => a.Transaction)
            .Include(a => a.Produce)
            .ToList();
        }

        public IEnumerable<TransactionProduce> GetSelected(Expression<Func<TransactionProduce, bool>> expression)
        {
            return _context.TransactionProduces
            .Where(expression)
            .Include(a => a.Transaction)
            .Include(a => a.Produce)
            .ToList();
        }

    }
}