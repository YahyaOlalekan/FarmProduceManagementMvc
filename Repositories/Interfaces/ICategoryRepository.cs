using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Entities;

namespace FarmProduceManagement.Repositories.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Category Get(string id);
        Category Get(Expression<Func<Category, bool>> expression);
        IEnumerable<Category> GetSelected(List<string> ids);
        IEnumerable<Category> GetSelected(Expression<Func<Category, bool>> expression);
        IEnumerable<Category> GetAll();
    }
}
