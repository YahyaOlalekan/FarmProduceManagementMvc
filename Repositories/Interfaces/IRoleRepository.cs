using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Entities;

namespace FarmProduceManagement.Repositories.Interfaces
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        Role Get(string id);
        Role Get(Expression<Func<Role, bool>> expression);
        IEnumerable<Role> GetSelected(List<string> ids);
        IEnumerable<Role> GetSelected(Expression<Func<Role, bool>> expression);
        IEnumerable<Role> GetAll();
    }
}