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
    public class ManagerRepository : BaseRepository<Manager>, IManagerRepository
    {
        public ManagerRepository(Context context)
        {
            _context = context;
        }

        public Manager Get(string id)
        {
            return _context.Managers
           .Include(a => a.Transactions)
           .Include(a => a.User)
           .FirstOrDefault(a => a.UserId == id && a.IsDeleted == false);
        }

        public Manager Get(Expression<Func<Manager, bool>> expression)
        {
            return _context.Managers
           .Where(a => a.IsDeleted == false)
           .Include(a => a.Transactions)
            .Include(a => a.User)
            .FirstOrDefault(expression);
        }

        public IEnumerable<Manager> GetAll()
        {
            return _context.Managers
            .Where(a => a.IsDeleted == false)
            .Include(a => a.User)
            .Include(a => a.Transactions)
            .ToList();
        }

        public IEnumerable<Manager> GetSelected(List<string> ids)
        {
            return _context.Managers
            .Include(a => a.User)
            .Include(a => a.Transactions)
            .Where(a => a.IsDeleted == false)
            .ToList();
        }


        public IEnumerable<Manager> GetSelected(Expression<Func<Manager, bool>> expression)
        {
            return _context.Managers
            .Where(expression)
            .Include(a => a.Transactions)
            .ToList();
        }





        // public Manager Get(string id)
        // {
        //     return _context.Managers
        //     .Where(a => a.IsDeleted == false)
        //     .Include(a => a.Transactions)
        //     .FirstOrDefault(a => a.Id == id);
        // }

        // public Manager Get(Expression<Func<Manager, bool>> expression)
        // {
        //     return _context.Managers
        //     .Where(a => a.IsDeleted == false)
        //     .Include(a => a.Transactions)
        //     .FirstOrDefault(expression);
        // }

        // public IEnumerable<Manager> GetAll()
        // {
        //     return _context.Managers
        //     .Where(a => a.IsDeleted == false)
        //     .Include(a => a.Transactions)
        //     //.ThenInclude(a => a.Farmer)
        //     .ToList();
        // }

        // public IEnumerable<Manager> GetSelected(List<string> ids)
        // {
        //     return _context.Managers
        //     .Where(a => ids.Contains(a.Id) && a.IsDeleted == false)
        //     .Include(a => a.Transactions)
        //    .ToList();
        // }

        // public IEnumerable<Manager> GetSelected(Expression<Func<Manager, bool>> expression)
        // {
        //     return _context.Managers
        //     .Where(expression)
        //     .Include(a => a.Transactions)
        //     .ToList();
        // }




        // public Manager Get(string id)
        // {
        //     return _context.Managers
        //     .FirstOrDefault(a => a.Id == id && a.IsDeleted == false);
        // }

        // public Manager Get(Expression<Func<Manager, bool>> expression)
        // {
        //     return _context.Managers
        //     .Where(a => a.IsDeleted == false)
        //     .FirstOrDefault(expression);
        // }
        // public IEnumerable<Manager> GetAll()
        // {
        //     return _context.Managers
        //    .Where(a => a.IsDeleted == false)
        //    .ToList();
        // }

        // public IEnumerable<Manager> GetSelected(List<string> ids)
        // {
        //     return _context.Managers
        //     .Where(a => ids.Contains(a.Id) && a.IsDeleted == false)
        //     .ToList();
        // }

        // public IEnumerable<Manager> GetSelected(Expression<Func<Manager, bool>> expression)
        // {
        //     return _context.Managers
        //     .Where(expression)
        //     .ToList();
        // }

    }
}