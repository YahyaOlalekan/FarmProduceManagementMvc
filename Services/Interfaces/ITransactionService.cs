using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Dtos;
using FarmProduceManagement.Models.Entities;

namespace FarmProduceManagement.Services.Interfaces
{
    public interface ITransactionService
    {
        BaseResponse<Transaction> Create(CreateTransactionRequestModel model);
        BaseResponse<Transaction> Update(string id, UpdateTransactionRequestModel model);
        BaseResponse<Transaction> Get(string id);
        BaseResponse<IEnumerable<Transaction>> GetAll();
        BaseResponse<Transaction> Delete(string id);
    }
}