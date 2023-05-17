using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmProduceManagement.Repositories.Interfaces
{
    public interface IBaseRepository<T>
    {
        T Create(T entity);
        T Delete(T entity);
        T Update(T entity);
        int Save();
    }
}