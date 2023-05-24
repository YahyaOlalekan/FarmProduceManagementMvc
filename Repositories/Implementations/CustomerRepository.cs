using FarmProduceManagement.AppDbContext;
using FarmProduceManagement.Models.Entities;
using FarmProduceManagement.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FarmProduceManagement.Repositories.Implementations
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(Context context)
        {
            _context = context;
        }

        public Customer Get(string id)
        {
            return _context.Customers
            .Include(a => a.Orders)
             .Include(a => a.User)
             .FirstOrDefault(a => a.UserId == id && a.IsDeleted == false);
        }

        public Customer Get(Expression<Func<Customer, bool>> expression)
        {
            return _context.Customers
            .Where(a => a.IsDeleted == false)
            .Include(a => a.Orders)
            .Include(a => a.User)
            .FirstOrDefault(expression);
        }

        public IEnumerable<Customer> GetAll(Func<Customer, bool> expression)
        {
            return _context.Customers
            .Where(a => !a.IsDeleted)
            .Include(a => a.User)
            .Include(a => a.Orders)
            .Where(expression)
            .ToList();
        }



        public IEnumerable<Customer> GetAll()
        {
            return _context.Customers
            .Where(a => !a.IsDeleted)
            .Include(a => a.User)
            .Include(a => a.Orders)
            .ToList();
        }


        public IEnumerable<Customer> GetSelected(List<string> ids)
        {
            return _context.Customers
            .Where(a => ids.Contains(a.Id) && a.IsDeleted == false)
            .Include(a => a.User)
            .Include(a => a.Orders)
            .ToList();
        }



        public IEnumerable<Customer> GetSelected(Expression<Func<Customer, bool>> expression)
        {
            return _context.Customers
            .Where(expression)
            //.Include(a => a.Orders)
            .Include(a => a.User)
            .ToList();
        }

    }
}
