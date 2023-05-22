using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Entities;

namespace FarmProduceManagement.Repositories.Interfaces
{
    public interface IOrderCartRepository : IBaseRepository<OrderCart>
    {
        OrderCart Get(string id);
        OrderCart Get(Expression<Func<OrderCart, bool>> expression);
        IEnumerable<OrderCart> GetSelected(List<string> ids);
        IEnumerable<OrderCart> GetSelected(Expression<Func<OrderCart, bool>> expression);
        IEnumerable<OrderCart> GetAll();
    }
}