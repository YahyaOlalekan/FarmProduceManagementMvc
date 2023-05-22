using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Dtos;
using FarmProduceManagement.Models.Entities;
using FarmProduceManagement.Models.Enums;

namespace FarmProduceManagement.Services.Interfaces
{
    public interface ITransactionService
    {
        BaseResponse<TransactionDto> Approved(string userId, string id);
        BaseResponse<TransactionDto> Declined(string userId, string id);
        BaseResponse<TransactionDto> Create(CreateTransactionRequestModel model);
        BaseResponse<TransactionDto> Delivered(string id);
        BaseResponse<TransactionDto> NotDelivered(string id);
        BaseResponse<TransactionDto> Update(string id, UpdateTransactionRequestModel model);
        BaseResponse<TransactionDto> Get(string id);
        BaseResponse<IEnumerable<TransactionDto>> GetAll();
        BaseResponse<IEnumerable<TransactionDto>> GetAllByUserId(string userId);
        BaseResponse<IEnumerable<TransactionDto>> GetAllByStatus(TransactionStatus status);
        BaseResponse<TransactionDto> Delete(string id);
        BaseResponse<TransactionDto> Details(string transactionNum);
    }
}