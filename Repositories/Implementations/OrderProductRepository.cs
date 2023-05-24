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
    public class OrderProductRepository : BaseRepository<OrderProduct>, IOrderProductRepository
    {
        public OrderProductRepository(Context context)
        {
            _context = context;
        }


        public bool CreateOrderProduct(List<OrderProduct> OrderProducts)
        {
            _context.AddRange(OrderProducts);
            return true;
        }

        public OrderProduct Get(string id)
        {
            return _context.OrderProducts
            .Include(a => a.Order)
            .Include(a => a.Product)
            .ThenInclude(a =>a.Produce)
            .SingleOrDefault(a => a.Id == id && a.IsDeleted == false);
        }

        public OrderProduct Get(Expression<Func<OrderProduct, bool>> expression)
        {
            return _context.OrderProducts
            .Where(a => a.IsDeleted == false)
            .Include(a => a.Order)
            .Include(a => a.Product)
             .ThenInclude(a =>a.Produce)
            .SingleOrDefault(expression);
        }

        public IEnumerable<OrderProduct> GetAll()
        {
            return _context.OrderProducts
           .Where(a => a.IsDeleted == false)
           .ToList();
        }

        public IEnumerable<OrderProduct> GetSelected(List<string> ids)
        {
            return _context.OrderProducts
            .Where(a => ids.Contains(a.Id) && a.IsDeleted == false)
            .Include(a => a.Order)
            .Include(a => a.Product)
             .ThenInclude(a =>a.Produce)
            .ToList();
        }

        public IEnumerable<OrderProduct> GetSelected(Expression<Func<OrderProduct, bool>> expression)
        {
            return _context.OrderProducts
            .Where(expression)
            .Include(a => a.Order)
            .Include(a => a.Product)
             .ThenInclude(a =>a.Produce)
            .ToList();
        }

    }
}