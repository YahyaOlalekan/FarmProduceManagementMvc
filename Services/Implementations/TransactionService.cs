using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Dtos;
using FarmProduceManagement.Models.Entities;
using FarmProduceManagement.Models.Enums;
using FarmProduceManagement.Repositories.Interfaces;
using FarmProduceManagement.Services.Interfaces;

namespace FarmProduceManagement.Services.Implementations
{
    public class TransactionService : ITransactionService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IHttpContextAccessor _httpAccessor;
        private readonly IProduceRepository _produceRepository;
        private readonly IProductRepository _productRepository;
        private readonly IFarmerRepository _farmerRepository;

        public TransactionService(IAdminRepository adminRepository, ITransactionRepository transactionRepository, IHttpContextAccessor httpAccessor, IProduceRepository produceRepository, IProductRepository productRepository, IFarmerRepository farmerRepository)
        {
            _adminRepository = adminRepository;
            _transactionRepository = transactionRepository;
            _httpAccessor = httpAccessor;
            _produceRepository = produceRepository;
            _productRepository = productRepository;
            _farmerRepository = farmerRepository;
        }

        public BaseResponse<TransactionDto> Create(CreateTransactionRequestModel model)
        {
            var loginId = _httpAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var transactionExist = _transactionRepository.Get(a => a.TransactionNum == model.TransactionNum);
            TransactionProduce transactionProduce = new();
            if (transactionExist == null)
            {
                List<Transaction> transactions = new();
                foreach (var produce in model.Produce)
                {
                    var transaction = new Transaction
                    {
                        TransactionNum = _transactionRepository.GenerateTransactionRegNum(),
                        FarmerId = loginId,
                        CreatedBy = loginId,
                        DateCreated = DateTime.Now,
                    };
                    transactions.Add(transaction);
                }

                _transactionRepository.CreateTransactions(transactions);
                _transactionRepository.Save();

                return new BaseResponse<TransactionDto>
                {
                    Message = "Successful",
                    Status = true,
                };
            }
            return new BaseResponse<TransactionDto>
            {
                Message = "Already exists",
                Status = false,
            };

        }

        public BaseResponse<TransactionDto> ApproveTransaction(string userId, string id)
        {

            var transaction = _transactionRepository.Get(id);

            
            if (transaction == null)
            {

                return new BaseResponse<TransactionDto>
                {
                    Message = "Transaction not found",
                    Status = false,
                };
            }


            if (transaction.Status == TransactionStatus.Approved)
            {

                return new BaseResponse<TransactionDto>
                {
                    Message = "Transaction already approved",
                    Status = false,
                };
                
            }


            transaction.Status = TransactionStatus.Approved;
            transaction.ModifiedBy = userId;

            _transactionRepository.Save();

            return new BaseResponse<TransactionDto>
            {
                Message = "Successful",
                Status = true,
                Data = new TransactionDto
                {
                    Id = transaction.Id,
                    DateCreated = transaction.DateCreated,
                    Status = transaction.Status,
                    TotalQuantity = transaction.TotalQuantity,
                    TotalAmount = transaction.TotalAmount,
                    TransactionNum = transaction.TransactionNum,
                    TransactionProduces = transaction.TransactionProduces.Select(t => new TransactionProduceDto
                    {
                        Price = t.Price,
                        Quantity = t.Quantity,
                        Produce = t.Produce,
                        ProduceId = t.ProduceId,

                    }).ToList()
                }
            };

        }


        // private string GenerateTransactionRegNum()
        // {
        //     return "FPM/TRA/00" + $"{_transactionRepository.GetAll().Count() + 1}";
        // }

        public BaseResponse<TransactionDto> Details(string id)
        {
            var transaction = _transactionRepository.Get(id);
            if (transaction == null)
            {
                return new BaseResponse<TransactionDto>
                {
                    Message = "No transaction found",
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
                    DateCreated = transaction.DateCreated,
                    Status = transaction.Status,
                    TotalQuantity = transaction.TotalQuantity,
                    TotalAmount = transaction.TotalAmount,
                    TransactionNum = transaction.TransactionNum,
                    TransactionProduces = transaction.TransactionProduces.Select(t => new TransactionProduceDto
                    {
                        Price = t.Price,
                        Quantity = t.Quantity,
                        Produce = t.Produce,
                        ProduceId = t.ProduceId,

                    }).ToList()
                }
            };

        }

        public BaseResponse<TransactionDto> DeliveredTransaction(string id)
        {
            var loginId = _httpAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var transaction = _transactionRepository.Get(id);

            
            if (transaction == null)
            {

                return new BaseResponse<TransactionDto>
                {
                    Message = "Transaction not found",
                    Status = false,
                };
            }


            if (transaction.Status == TransactionStatus.Delivered)
            {

                return new BaseResponse<TransactionDto>
                {
                    Message = "Transaction already delivered",
                    Status = false,
                };
                
            }


            

            var admin = _adminRepository.GetAdmin();
            // var companyWallet = _adminRepository.GetCompanyWallet();
            admin.CompanyWallet -= transaction.TotalAmount;

            if(admin.CompanyWallet <= 0)
            {
                
                return new BaseResponse<TransactionDto>
                {
                    Message = "Error, Try again",
                    Status = false,
                };
            }



            transaction.Status = TransactionStatus.Delivered;
            transaction.ModifiedBy = loginId;

            foreach (var item in transaction.TransactionProduces)
            {
                var product = _productRepository.Get(p => p.ProduceId == item.Produce.Id);

                if (product == null)
                {
                    product = new Product{
                        CategoryId = item.Produce.CategoryId,
                        CreatedBy = transaction.FarmerId,
                        QuantityToSell = item.Quantity,
                        SellingPrice = item.Produce.SellingPrice,
                        UnitOfMeasurement = item.Produce.UnitOfMeasurement,
                        IsAvailable = true,
                        ModifiedBy = loginId,
                        ProduceId = item.Produce.Id,
                        Status = Status.Approved
                    };
                    _productRepository.Create(product);
                }
                else
                {
                    product.QuantityToSell += item.Quantity;
                    product.ModifiedBy = loginId;
                    product.SellingPrice = item.Produce.SellingPrice;
                    product.IsAvailable = true;
                }

                
            }


            var farmer = _farmerRepository.Get(transaction.FarmerId);
            if(farmer == null)
            {

                return new BaseResponse<TransactionDto>
                {
                    Message = "Farmer not found",
                    Status = false,
                };
            }

            farmer.Wallet += transaction.TotalAmount;


            _transactionRepository.Save();

            return new BaseResponse<TransactionDto>
            {
                Message = "Successful",
                Status = true,
                Data = new TransactionDto
                {
                    Id = transaction.Id,
                    DateCreated = transaction.DateCreated,
                    Status = transaction.Status,
                    TotalQuantity = transaction.TotalQuantity,
                    TotalAmount = transaction.TotalAmount,
                    TransactionNum = transaction.TransactionNum,
                    TransactionProduces = transaction.TransactionProduces.Select(t => new TransactionProduceDto
                    {
                        Price = t.Price,
                        Quantity = t.Quantity,
                        Produce = t.Produce,
                        ProduceId = t.ProduceId,

                    }).ToList()
                }
            };

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
                    FarmerId = transaction.FarmerId,
                    Farmer = new FarmerDto
                    {
                        Id = transaction.Farmer.Id,
                        FarmerRegStatus = transaction.Farmer.FarmerRegStatus,
                        User = transaction.Farmer.User,
                        UserId = transaction.Farmer.UserId,
                        Wallet = transaction.Farmer.Wallet,
                    },
                    DateCreated = transaction.DateCreated,
                    TotalAmount = transaction.TotalAmount,
                    TotalQuantity = transaction.TotalQuantity,
                    TransactionNum = transaction.TransactionNum,
                    TransactionProduces = transaction.TransactionProduces.Select(p => new TransactionProduceDto
                    {
                        Id = p.Id,
                        TransactionId = p.TransactionId,
                        Quantity = p.Quantity,
                        Price = p.Price,
                    }).ToList(),

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
                    TransactionNum = _transactionRepository.GenerateTransactionRegNum(),

                })
            };
        }

        public BaseResponse<IEnumerable<TransactionDto>> GetAllByUserId(string userId)
        {
            var farmer = _farmerRepository.Get(f => f.UserId == userId);
            var transactions = _transactionRepository.GetSelected(t => t.FarmerId == farmer.Id);

            if (transactions == null)
            {

                return new BaseResponse<IEnumerable<TransactionDto>>
                {
                    Message = "Transaction not found",
                    Status = false,
                };
            }


            return new BaseResponse<IEnumerable<TransactionDto>>
            {
                Message = "Successful",
                Status = true,
                Data = transactions.Select(t => new TransactionDto
                {
                    Id = t.Id,
                    CreatedBy = t.CreatedBy,
                    DateCreated = t.DateCreated,
                    Farmer = new FarmerDto
                    {
                        Id = t.Farmer.Id,
                        FarmerRegStatus = t.Farmer.FarmerRegStatus,
                        User = t.Farmer.User,
                        UserId = t.Farmer.UserId,
                        Wallet = t.Farmer.Wallet,
                    },
                    FarmerId = t.FarmerId,
                    ModifiedBy = t.ModifiedBy,
                    ModifiedOn = t.ModifiedOn,
                    Status = t.Status,
                    TotalQuantity = t.TotalQuantity,
                    TotalAmount = t.TotalAmount,
                    TransactionNum = t.TransactionNum,
                    TransactionProduces = t.TransactionProduces.Select(p => new TransactionProduceDto
                    {
                        Id = p.Id,
                        TransactionId = p.TransactionId,
                        Quantity = p.Quantity,
                        Price = p.Price,
                    }).ToList(),

                })
            };
        }


        public BaseResponse<IEnumerable<TransactionDto>> GetAllByStatus(TransactionStatus status)
        {
            // var farmer = _farmerRepository.Get(f => f.UserId == userId);
            var transactions = _transactionRepository.GetSelected(t => t.Status == status);

            if (transactions == null)
            {

                return new BaseResponse<IEnumerable<TransactionDto>>
                {
                    Message = "Transaction not found",
                    Status = false,
                };
            }


            return new BaseResponse<IEnumerable<TransactionDto>>
            {
                Message = "Successful",
                Status = true,
                Data = transactions.Select(t => new TransactionDto
                {
                    Id = t.Id,
                    CreatedBy = t.CreatedBy,
                    DateCreated = t.DateCreated,
                    FarmerId = t.FarmerId,
                    ModifiedBy = t.ModifiedBy,
                    ModifiedOn = t.ModifiedOn,
                    TotalQuantity = t.TotalQuantity,
                    TotalAmount = t.TotalAmount,
                    TransactionNum = t.TransactionNum,
                    Farmer = new FarmerDto
                    {
                        Id = t.Farmer.Id,
                        FarmerRegStatus = t.Farmer.FarmerRegStatus,
                        User = t.Farmer.User,
                        UserId = t.Farmer.UserId,
                        Wallet = t.Farmer.Wallet,
                    },

                    TransactionProduces = t.TransactionProduces.Select(p => new TransactionProduceDto
                    {
                        Id = p.Id,
                        TransactionId = p.TransactionId,
                        Quantity = p.Quantity,
                        Price = p.Price,
                        Produce = p.Produce,
                    }).ToList(),
                    

                })
            };
        }

        public BaseResponse<TransactionDto> Update(string id, UpdateTransactionRequestModel model)
        {
            var transaction = _transactionRepository.Get(a => a.Id == id);
            if (transaction is not null)
            {

                transaction.TransactionNum = _transactionRepository.GenerateTransactionRegNum();


                _transactionRepository.Update(transaction);
                _transactionRepository.Save();

                return new BaseResponse<TransactionDto>
                {
                    Message = "Transaction Updated Successfully",
                    Status = true,
                    Data = new TransactionDto
                    {

                        TransactionNum = _transactionRepository.GenerateTransactionRegNum(),
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

    }
}