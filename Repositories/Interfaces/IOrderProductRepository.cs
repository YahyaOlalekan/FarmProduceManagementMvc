using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Entities;

namespace FarmProduceManagement.Repositories.Interfaces
{
    public interface IOrderProductRepository : IBaseRepository<OrderProduct>
    {
        bool CreateOrderProduct(List<OrderProduct> orders);
        OrderProduct Get(string id);
        OrderProduct Get(Expression<Func<OrderProduct, bool>> expression);
        IEnumerable<OrderProduct> GetSelected(List<string> ids);
        IEnumerable<OrderProduct> GetSelected(Expression<Func<OrderProduct, bool>> expression);
        IEnumerable<OrderProduct> GetAll();
    }
}