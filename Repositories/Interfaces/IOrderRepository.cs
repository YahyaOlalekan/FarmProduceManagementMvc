using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Entities;

namespace FarmProduceManagement.Repositories.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        bool CreateOrder(List<Order> orders);
        Order Get(string id);
        Order Get(Expression<Func<Order, bool>> expression);
        IEnumerable<Order> GetSelected(List<string> ids);
        IEnumerable<Order> GetSelected(Expression<Func<Order, bool>> expression);
        IEnumerable<Order> GetAll();
        string GenerateOrderNumber();
    }
}