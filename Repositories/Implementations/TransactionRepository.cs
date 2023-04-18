using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Transactions;
using FarmProduceManagement.AppDbContext;
using FarmProduceManagement.Repositories.Interfaces;

namespace FarmProduceManagement.Repositories.Implementations
{
    // public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    // {
    //     public Transaction Get(string id)
    //     {
    //         throw new NotImplementedException();
    //     }

    //     public Transaction Get(Expression<Func<Transaction, bool>> expression)
    //     {
    //         throw new NotImplementedException();
    //     }

    //     public IEnumerable<Transaction> GetAll()
    //     {
    //         throw new NotImplementedException();
    //     }

    //     public IEnumerable<Transaction> GetSelected(List<string> ids)
    //     {
    //         throw new NotImplementedException();
    //     }

    //     public IEnumerable<Transaction> GetSelected(Expression<Func<Transaction, bool>> expression)
    //     {
    //         throw new NotImplementedException();
    //     }
    // }

}