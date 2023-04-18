using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Entities;

namespace FarmProduceManagement.Repositories.Interfaces
{
    public interface IProduceRepository : IBaseRepository<Produce>
    {
        Produce Get(string id);
        Produce Get(Expression<Func<Produce, bool>> expression);
        IEnumerable<Produce> GetSelected(List<string> ids);
        IEnumerable<Produce> GetSelected(Expression<Func<Produce, bool>> expression);
        IEnumerable<Produce> GetAll();
    }
}
