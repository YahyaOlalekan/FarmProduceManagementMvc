using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Dtos;
using FarmProduceManagement.Models.Enums;

namespace FarmProduceManagement.Services.Interfaces
{
    public interface IProduceService
    {
        BaseResponse<ProduceDto> Create(CreateProduceRequestModel model);
        BaseResponse<ProduceDto> Purchase(string id, PurchaseProduceRequestModel model);
        BaseResponse<ProduceDto> Update(string id, UpdateProduceRequestModel model);
        BaseResponse<ProduceDto> Get(string id);
        //BaseResponse<ProduceDto> GetByStatus(Status status);
        BaseResponse<IEnumerable<ProduceDto>> GetAll();
        BaseResponse<IEnumerable<ProduceDto>> GetByCategoryId(string categoryId);
        BaseResponse<ProduceDto> Delete(string id);
    }
}