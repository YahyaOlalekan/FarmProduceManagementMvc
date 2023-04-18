using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Entities;
using FarmProduceManagement.Repositories.Implementations;

namespace FarmProduceManagement.Repositories.Interfaces
{
    public interface IFarmerRepository : IBaseRepository<Farmer>
    {
 
        Farmer Get(string id);
        Farmer Get(Expression<Func<Farmer, bool>> expression);
        IEnumerable<Farmer> GetSelected(List<string> ids);
        IEnumerable<Farmer> GetSelected(Expression<Func<Farmer, bool>> expression);
        IEnumerable<Farmer> GetAll(Func<Farmer, bool> expression);
        IEnumerable<Farmer> GetAll(); 
    }
}