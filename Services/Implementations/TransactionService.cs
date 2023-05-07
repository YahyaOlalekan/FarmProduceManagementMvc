using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Dtos;
using FarmProduceManagement.Models.Entities;
using FarmProduceManagement.Repositories.Interfaces;
using FarmProduceManagement.Services.Interfaces;

namespace FarmProduceManagement.Services.Implementations
{
    public class TransactionService : ITransactionService
    {
         private readonly ITransactionRepository _transactionRepository;
        private readonly IHttpContextAccessor _httpAccessor;

        public TransactionService(ITransactionRepository transactionRepository, IHttpContextAccessor httpAccessor)
        {
            _transactionRepository = transactionRepository;
            _httpAccessor = httpAccessor;
        }

        public BaseResponse<TransactionDto> Create(CreateTransactionRequestModel model)
        {
            var loginId = _httpAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //var transactionExist = _transactionRepository.Get(a => a.TransactionNum == model.Transaction);
            //if (transactionExist == null)
            //{
            //    var transaction = new Transaction
            //    {
            //        TransactionNum = GenerateTransactionRegNum(),
            //        FarmerId = loginId,
            //       // Price = model.Price,
            //       // ProduceName = model.ProduceName,
            //       // UnitOfMeasurement = model.UnitOfMeasurement,
            //        //CreatedBy = loginId
            //    };
            //    _transactionRepository.Create(transaction);
            //    _transactionRepository.Save();

            //    return new BaseResponse<TransactionDto>
            //    {
            //        Message = "Successful",
            //        Status = true,
            //        Data = new TransactionDto
            //        {
            //            Id = transaction.Id,
            //            Price = model.Price,
            //            TransactionNum = GenerateTransactionRegNum(),
            //            ProduceName = model.ProduceName,
            //            UnitOfMeasurement = model.UnitOfMeasurement,

            //        }
            //    };
            //}
            return new BaseResponse<TransactionDto>
            {
                Message = "Already exists",
                Status = false
            };

        }


        private string GenerateTransactionRegNum()
        {
            return "FPM/TRA/00" + $"{_transactionRepository.GetAll().Count() + 1}";
        }


        public BaseResponse<TransactionDto> Delete(string id)
        {
            var transaction = _transactionRepository.Get(id);
            if (transaction is null)
            {
                return new BaseResponse<TransactionDto>
                {
                    Message = "The transaction does not exist",
                    Status = false
                };
            }
            transaction.IsDeleted = true;

            _transactionRepository.Update(transaction);
            _transactionRepository.Save();
            return new BaseResponse<TransactionDto>
            {
                Message = "Transaction Deleted Successfully ",
                Status = true
            };

        }

        public BaseResponse<TransactionDto> Get(string id)
        {
            var transaction = _transactionRepository.Get(id);
            if (transaction == null)
            {
                return new BaseResponse<TransactionDto>
                {
                    Message = "Not found",
                    Status = false,
                };
            }
            return new BaseResponse<TransactionDto>
            {
                Message = "Found",
                Status = true,
                Data = new TransactionDto
                {
                    Id = transaction.Id,
                    TransactionNum = GenerateTransactionRegNum(),
                   
                }
            };

        }

        public BaseResponse<IEnumerable<TransactionDto>> GetAll()
        {
            var transaction = _transactionRepository.GetAll();
            if (transaction == null)
            {
                return new BaseResponse<IEnumerable<TransactionDto>>
                {
                    Message = "Not found",
                    Status = false,
                };
            }
            return new BaseResponse<IEnumerable<TransactionDto>>
            {
                Message = "Found",
                Status = true,
                Data = transaction.Select(c => new TransactionDto
                {
                    Id = c.Id,
                    TransactionNum = GenerateTransactionRegNum(),
                   
                })
            };
        }



        public BaseResponse<TransactionDto> Update(string id, UpdateTransactionRequestModel model)
        {
            var transaction = _transactionRepository.Get(a => a.Id == id);
            if (transaction is not null)
            {

                transaction.TransactionNum = GenerateTransactionRegNum();
               


                _transactionRepository.Update(transaction);
                _transactionRepository.Save();

                return new BaseResponse<TransactionDto>
                {
                    Message = "Transaction Updated Successfully",
                    Status = true,
                    Data = new TransactionDto
                    {
                       
                        TransactionNum = GenerateTransactionRegNum(),
                        ProduceName = model.ProduceName,
                        UnitOfMeasurement = model.UnitOfMeasurement,
                        Id = transaction.Id,
                    }
                };
            }
            return new BaseResponse<TransactionDto>
            {
                Message = "Unable to Update",
                Status = false,
            };
        }

        BaseResponse<Transaction> ITransactionService.Create(CreateTransactionRequestModel model)
        {
            throw new NotImplementedException();
        }

        BaseResponse<Transaction> ITransactionService.Update(string id, UpdateTransactionRequestModel model)
        {
            throw new NotImplementedException();
        }

        BaseResponse<Transaction> ITransactionService.Get(string id)
        {
            throw new NotImplementedException();
        }

        BaseResponse<IEnumerable<Transaction>> ITransactionService.GetAll()
        {
            throw new NotImplementedException();
        }

        BaseResponse<Transaction> ITransactionService.Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}