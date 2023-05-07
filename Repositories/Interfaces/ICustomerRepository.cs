using FarmProduceManagement.Models.Entities;
using System.Linq.Expressions;

namespace FarmProduceManagement.Repositories.Interfaces
{
    public interface ICustomerRepository: IBaseRepository<Customer>
    {
        Customer Get(string id);
        Customer Get(Expression<Func<Customer, bool>> expression);
        IEnumerable<Customer> GetSelected(List<string> ids);
        IEnumerable<Customer> GetSelected(Expression<Func<Customer, bool>> expression);
        IEnumerable<Customer> GetAll(Func<Customer, bool> expression);
        IEnumerable<Customer> GetAll();
    }
}
