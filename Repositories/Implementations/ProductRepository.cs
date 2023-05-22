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
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(Context context)
        {
            _context = context;
        }

        public Product Get(string id)
        {
            return _context.Products
            .Where(prod => prod.IsDeleted == false)
            .Include(p => p.Produce)
            .Include(op => op.OrderProducts)
            .ThenInclude(o => o.Order)
            .FirstOrDefault(prod => prod.Id == id);
        }

       
        public Product Get(Expression<Func<Product, bool>> expression)
        {
            return _context.Products
            .Where(prod => prod.IsDeleted == false)
            .Include(p => p.Produce)
            .Include(op => op.OrderProducts)
            .ThenInclude(o => o.Order)
            .FirstOrDefault(expression);
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products
            .Where(prod => prod.IsDeleted == false)
            .Include(p => p.Produce)
            .Include(a => a.Category)
            .Include(a => a.OrderProducts)
            .ToList();
        }

      
        public IEnumerable<Product> GetSelected(Expression<Func<Product, bool>> expression)
        {
            return _context.Products
            .Include(p => p.Produce)
            .Include(a => a.OrderProducts)
            .ThenInclude(o => o.Order)
            .Where(expression)
            .ToList();
        }

        public IEnumerable<Product> GetSelected(List<string> ids)
        {
            return _context.Products
            .Include(p => p.Produce)
            .Include(a => a.OrderProducts)
            .ThenInclude(o => o.Order)
            .Where(a => ids.Contains(a.Id) && a.IsDeleted == false)
            .ToList();
        }

       
    }
}