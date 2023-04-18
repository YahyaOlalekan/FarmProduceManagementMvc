using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Dtos;

namespace FarmProduceManagement.Services.Interfaces
{
    public interface IProduceService
    {
        BaseResponse<ProduceDto> Create(CreateProduceRequestModel model);
        BaseResponse<ProduceDto> Update(string id, UpdateProduceRequestModel model);
        BaseResponse<ProduceDto> Get(string id);
        BaseResponse<IEnumerable<ProduceDto>> GetAll();
        BaseResponse<ProduceDto> Delete(string id);
    }
}