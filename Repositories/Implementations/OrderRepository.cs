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
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(Context context)
        {
            _context = context;
        }

        public bool CreateOrder(List<Order> orders)
        {
            _context.AddRange(orders);
            return true;
        }

        public Order Get(string id)
        {
            return _context.Orders
            .Include(a => a.Customer)
            .Include(a => a.OrderProducts)
            .ThenInclude(a => a.Product)
            .SingleOrDefault(a => a.Id == id && a.IsDeleted == false);
        }

        public Order Get(Expression<Func<Order, bool>> expression)
        {
            return _context.Orders
            .Where(a => a.IsDeleted == false)
            .Include(a => a.Customer)
            .Include(a => a.OrderProducts)
            .ThenInclude(a => a.Product)
            .SingleOrDefault(expression);
        }

        public IEnumerable<Order> GetAll()
        {
            return _context.Orders
           .Where(a => a.IsDeleted == false)
            .Include(a => a.Customer)
            .Include(a => a.OrderProducts)
            .ThenInclude(a => a.Product)
           .ToList();
        }

        public IEnumerable<Order> GetSelected(List<string> ids)
        {
            return _context.Orders
            .Where(a => ids.Contains(a.Id) && a.IsDeleted == false)
            .Include(a => a.Customer)
            .Include(a => a.OrderProducts)
            .ThenInclude(a => a.Product)
            .ToList();
        }

        public IEnumerable<Order> GetSelected(Expression<Func<Order, bool>> expression)
        {
            return _context.Orders
            .Where(expression)
            .Include(a => a.Customer)
            .Include(a => a.OrderProducts)
            .ThenInclude(a => a.Product)
            .ToList();
        }

        public string GenerateOrderNumber()
        {
            return "FPM/ODR/00" + $"{GetAll().Count() + 1}";
        }

       
    }
}