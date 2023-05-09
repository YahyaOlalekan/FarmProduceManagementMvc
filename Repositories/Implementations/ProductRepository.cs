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
            .Where(a => a.IsDeleted == false)
            .Include(a => a.OrderProducts)
            .FirstOrDefault(x => x.Id == id);
        }

       
        public Product Get(Expression<Func<Product, bool>> expression)
        {
            return _context.Products
            .Where(a => a.IsDeleted == false)
            .Include(a => a.OrderProducts)
            .FirstOrDefault(expression);
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products
            .Where(a => a.IsDeleted == false)
            .Include(a => a.Category)
            //.Include(a => a.OrderProducts)
            .ToList();
        }

      
        public IEnumerable<Product> GetSelected(Expression<Func<Product, bool>> expression)
        {
            return _context.Products
            .Include(a => a.OrderProducts)
            .Where(expression)
            .ToList();
        }

        public IEnumerable<Product> GetSelected(List<string> ids)
        {
            return _context.Products
            .Include(a => a.OrderProducts)
            .Where(a => ids.Contains(a.Id) && a.IsDeleted == false)
            .ToList();
        }

       
    }
}