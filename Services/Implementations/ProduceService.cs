

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
    public class ProduceService : IProduceService
    {
        private readonly IProduceRepository _produceRepository;
        private readonly IHttpContextAccessor _httpAccessor;

        public ProduceService(IProduceRepository produceRepository, IHttpContextAccessor httpAccessor)
        {
            _produceRepository = produceRepository;
            _httpAccessor = httpAccessor;
        }

        public BaseResponse<ProduceDto> Create(CreateProduceRequestModel model)
        {
            var loginId = _httpAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var produceExist = _produceRepository.Get(a => a.ProduceName == model.ProduceName);
            if (produceExist == null)
            {
                var produce = new Produce();
                produce.ProduceName = model.ProduceName;
                produce.Category.NameOfCategory = model.NameOfCategory;
                produce.Price = model.Price;
                produce.UnitOfMeasurement = model.UnitOfMeasurement;
                produce.QuantityAvailable = model.QuantityAvailable;
                produce.CreatedBy = loginId;

                _produceRepository.Create(produce);
                _produceRepository.Save();

                return new BaseResponse<ProduceDto>
                {
                    Message = "Successful",
                    Status = true,
                    Data = new ProduceDto
                    {
                        Id = produce.Id,
                        ProduceName = produce.ProduceName,
                        NameOfCategory = produce.Category.NameOfCategory,
                        Price = produce.Price,
                        UnitOfMeasurement = produce.UnitOfMeasurement,
                        QuantityAvailable = produce.QuantityAvailable,
                        // TransactionProduces = produce.TransactionProduces new TransactionProduceDto
                        // {
                        //     Id =
                        // }

                    }
                };
            }
            return new BaseResponse<ProduceDto>
            {
                Message = "Already exists",
                Status = false
            };

        }

        public BaseResponse<ProduceDto> Delete(string id)
        {
            var produce = _produceRepository.Get(id);
            if (produce is null)
            {
                return new BaseResponse<ProduceDto>
                {
                    Message = "The produce does not exist",
                    Status = false
                };
            }
            produce.IsDeleted = true;

            _produceRepository.Update(produce);
            _produceRepository.Save();
            return new BaseResponse<ProduceDto>
            {
                Message = "Produce Deleted Successfully ",
                Status = true
            };

        }


        public BaseResponse<ProduceDto> Get(string id)
        {
            var produce = _produceRepository.Get(id);
            if (produce == null)
            {
                return new BaseResponse<ProduceDto>
                {
                    Message = "Not found",
                    Status = false,
                };
            }
            return new BaseResponse<ProduceDto>
            {
                Message = "Found",
                Status = true,
                Data = new ProduceDto
                {
                    Id = produce.Id,
                    ProduceName = produce.ProduceName,
                    NameOfCategory = produce.Category.NameOfCategory,
                    Price = produce.Price,
                    UnitOfMeasurement = produce.UnitOfMeasurement,
                    QuantityAvailable = produce.QuantityAvailable,
                    
                    // TransactionProduces = produce.TransactionProduces new TransactionProduceDto
                    // {
                    //     Id =
                    // }
                }
            };

        }

        public BaseResponse<IEnumerable<ProduceDto>> GetAll()
        {
            var produce = _produceRepository.GetAll();
            if (produce == null)
            {
                return new BaseResponse<IEnumerable<ProduceDto>>
                {
                    Message = "Not found",
                    Status = false,
                };
            }
            return new BaseResponse<IEnumerable<ProduceDto>>
            {
                Message = "Found",
                Status = true,
                Data = produce.Select(p => new ProduceDto
                {
                    Id = p.Id,
                    ProduceName = p.ProduceName,
                    NameOfCategory = p.Category.NameOfCategory,
                    Price = p.Price,
                    UnitOfMeasurement = p.UnitOfMeasurement,
                    QuantityAvailable = p.QuantityAvailable,
                    // TransactionProduces = produce.TransactionProduces new TransactionProduceDto
                    // {
                    //     Id =
                    // }
                })
            };
        }


        public BaseResponse<ProduceDto> Update(string id, UpdateProduceRequestModel model)
        {
            var produce = _produceRepository.Get(a => a.Id == id);
            if (produce is not null)
            {

                produce.ProduceName = model.ProduceName;
                produce.Price = model.Price;
                produce.QuantityAvailable = model.QuantityAvailable;
                produce.UnitOfMeasurement = model.UnitOfMeasurement;
                produce.Category.NameOfCategory = model.UnitOfMeasurement;
                produce.Category.NameOfCategory = model.NameOfCategory;


                _produceRepository.Update(produce);
                _produceRepository.Save();

                return new BaseResponse<ProduceDto>
                {
                    Message = "Produce Updated Successfully",
                    Status = true,
                    Data = new ProduceDto
                    {
                        Id = produce.Id,
                        ProduceName = produce.ProduceName,
                        NameOfCategory = produce.Category.NameOfCategory,
                        Price = produce.Price,
                        UnitOfMeasurement = produce.UnitOfMeasurement,
                        QuantityAvailable = produce.QuantityAvailable,
                        // TransactionProduces = produce.TransactionProduces new TransactionProduceDto
                        // {
                        //     Id =
                        // }
                    }
                };
            }
            return new BaseResponse<ProduceDto>
            {
                Message = "Unable to Update",
                Status = false,
            };



        }
    }
}











