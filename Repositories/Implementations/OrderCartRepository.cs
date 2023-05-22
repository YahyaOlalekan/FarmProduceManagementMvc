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
    public class OrderCartRepository : BaseRepository<OrderCart>, IOrderCartRepository
    {
        public OrderCartRepository(Context context)
        {
            _context = context;
        }

        public OrderCart Get(string id)
        {
            return _context.OrderCarts
            .Include(c => c.Product)
            .ThenInclude(c => c.Produce)
            .SingleOrDefault(a => a.Id == id && a.IsDeleted == false);
        }

        public OrderCart Get(Expression<Func<OrderCart, bool>> expression)
        {
            return _context.OrderCarts
            .Where(a => a.IsDeleted == false)
            .Include(c => c.Product)
            .ThenInclude(c => c.Produce)
            .SingleOrDefault(expression);
        }

        public IEnumerable<OrderCart> GetAll()
        {
            return _context.OrderCarts
           .Where(a => a.IsDeleted == false)
           .Include(c => c.Product)
            .ThenInclude(c => c.Produce)
           .ToList();
        }

        public IEnumerable<OrderCart> GetSelected(List<string> ids)
        {
            return _context.OrderCarts
            .Where(a => ids.Contains(a.Id) && a.IsDeleted == false)
            .Include(c => c.Product)
            .ThenInclude(c => c.Produce)
            .ToList();
        }

        public IEnumerable<OrderCart> GetSelected(Expression<Func<OrderCart, bool>> expression)
        {
            return _context.OrderCarts
            .Where(expression)
            .Include(c => c.Product)
            .ThenInclude(c => c.Produce)
            .ToList();
        }
    }
}