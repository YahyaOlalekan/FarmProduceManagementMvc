using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Entities;

namespace FarmProduceManagement.Repositories.Interfaces
{
    public interface ICartItemRepository : IBaseRepository<CartItem>
    {
        CartItem Get(string id);
        CartItem Get(Expression<Func<CartItem, bool>> expression);
        IEnumerable<CartItem> GetSelected(List<string> ids);
        IEnumerable<CartItem> GetSelected(Expression<Func<CartItem, bool>> expression);
        IEnumerable<CartItem> GetAll();
    }
}