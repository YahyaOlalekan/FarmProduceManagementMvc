using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Entities;

namespace FarmProduceManagement.Repositories.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Product Get(string id);
        Product Get(Expression<Func<Product, bool>> expression);
        IEnumerable<Product> GetSelected(List<string> ids);
        IEnumerable<Product> GetSelected(Expression<Func<Product, bool>> expression);
        IEnumerable<Product> GetAll();
    }
}
