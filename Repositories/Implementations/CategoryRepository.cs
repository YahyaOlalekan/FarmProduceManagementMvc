using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FarmProduceManagement.AppDbContext;
using FarmProduceManagement.Models.Entities;
using FarmProduceManagement.Repositories.Interfaces;

namespace FarmProduceManagement.Repositories.Implementations
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(Context context)
        {
            _context = context;
        }

        public Category Get(string id)
        {
            return _context.Categories
        .SingleOrDefault(a => a.Id == id && a.IsDeleted == false);
        }

        public Category Get(Expression<Func<Category, bool>> expression)
        {
            return _context.Categories
            .Where(a => a.IsDeleted == false)
            .SingleOrDefault(expression);
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories
           .Where(a => a.IsDeleted == false)
           .ToList();
        }

        public IEnumerable<Category> GetSelected(List<string> ids)
        {
            return _context.Categories
            .Where(a => ids.Contains(a.Id) && a.IsDeleted == false)
            .ToList();
        }

        public IEnumerable<Category> GetSelected(Expression<Func<Category, bool>> expression)
        {
            return _context.Categories
            .Where(expression)
            .ToList();
        }
    }
}