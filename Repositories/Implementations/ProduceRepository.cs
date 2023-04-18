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
    public class ProduceRepository : BaseRepository<Produce>, IProduceRepository
    {
        public ProduceRepository(Context context)
        {
            _context = context;
        }

        public Produce Get(string id)
        {
            return _context.Produces
            .Where(a => a.IsDeleted == false)
            .Include(a => a.TransactionProduces)
            //.ThenInclude(a => a.Transaction)
             .FirstOrDefault(x => x.Id == id);
        }

       
        public Produce Get(Expression<Func<Produce, bool>> expression)
        {
            return _context.Produces
            .Where(a => a.IsDeleted == false)
            .Include(a => a.TransactionProduces)
           // .ThenInclude(a => a.Transaction)
            .FirstOrDefault(expression);
        }

        public IEnumerable<Produce> GetAll()
        {
            return _context.Produces
            .Where(a => a.IsDeleted == false)
            .Include(a => a.TransactionProduces)
           // .ThenInclude(a => a.Transaction)
            .ToList();
        }

      
        public IEnumerable<Produce> GetSelected(Expression<Func<Produce, bool>> expression)
        {
            return _context.Produces
            .Include(a => a.TransactionProduces)
           // .ThenInclude(a => a.Transaction)
            .Where(expression)
            .ToList();
        }

        public IEnumerable<Produce> GetSelected(List<string> ids)
        {
            return _context.Produces
            .Include(a => a.TransactionProduces)
           // .ThenInclude(a => a.Transaction)
            .Where(a => ids.Contains(a.Id) && a.IsDeleted == false)
            .ToList();
        }

       
    }
}