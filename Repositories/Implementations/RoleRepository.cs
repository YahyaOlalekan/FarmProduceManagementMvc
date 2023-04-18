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
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(Context context)
        {
            _context = context;
        }

        public Role Get(string id)
        {
            return _context.Roles
        .SingleOrDefault(a => a.Id == id && a.IsDeleted == false);
        }

        public Role Get(Expression<Func<Role, bool>> expression)
        {
            return _context.Roles
            .Where(a => a.IsDeleted == false)
            .SingleOrDefault(expression);
        }

        public IEnumerable<Role> GetAll()
        {
            return _context.Roles
           .Where(a => a.IsDeleted == false)
           .ToList();
        }

        public IEnumerable<Role> GetSelected(List<string> ids)
        {
            return _context.Roles
            .Where(a => ids.Contains(a.Id) && a.IsDeleted == false)
            .ToList();
        }

        public IEnumerable<Role> GetSelected(Expression<Func<Role, bool>> expression)
        {
            return _context.Roles
            .Where(expression)
            .ToList();
        }
    }
}