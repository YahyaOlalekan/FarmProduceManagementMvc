using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Entities;

namespace FarmProduceManagement.Repositories.Interfaces
{
    public interface ITransactionProduceRepository : IBaseRepository<TransactionProduce>
    {
        bool CreateTransactionProduce(List<TransactionProduce> transactions);
        TransactionProduce Get(string id);
        TransactionProduce Get(Expression<Func<TransactionProduce, bool>> expression);
        IEnumerable<TransactionProduce> GetSelected(List<string> ids);
        IEnumerable<TransactionProduce> GetSelected(Expression<Func<TransactionProduce, bool>> expression);
        IEnumerable<TransactionProduce> GetAll();
    }
}