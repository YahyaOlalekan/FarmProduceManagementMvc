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
    public class CartItemRepository : BaseRepository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(Context context)
        {
            _context = context;
        }

        public CartItem Get(string id)
        {
            return _context.CartItems
            .Include(c => c.Produce)
            .SingleOrDefault(a => a.Id == id && a.IsDeleted == false);
        }

        public CartItem Get(Expression<Func<CartItem, bool>> expression)
        {
            return _context.CartItems
            .Where(a => a.IsDeleted == false)
            .Include(c => c.Produce)
            .SingleOrDefault(expression);
        }

        public IEnumerable<CartItem> GetAll()
        {
            return _context.CartItems
           .Where(a => a.IsDeleted == false)
           .Include(c => c.Produce)
           .ToList();
        }

        public IEnumerable<CartItem> GetSelected(List<string> ids)
        {
            return _context.CartItems
            .Where(a => ids.Contains(a.Id) && a.IsDeleted == false)
            .Include(c => c.Produce)
            .ToList();
        }

        public IEnumerable<CartItem> GetSelected(Expression<Func<CartItem, bool>> expression)
        {
            return _context.CartItems
            .Where(expression)
            .Include(c => c.Produce)
            .ToList();
        }
    }
}