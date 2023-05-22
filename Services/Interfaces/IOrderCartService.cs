using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Dtos;

namespace FarmProduceManagement.Services.Interfaces
{
    public interface IOrderCartService
    {
         BaseResponse<OrderCartDto> Create(CreateOrderCartRequestModel model);
        //BaseResponse<OrderCartDto> Update(string id, UpdateOrderCartRequestModel model);
        BaseResponse<OrderCartDto> Get(string id);
        BaseResponse<IEnumerable<OrderCartDto>> GetAll();
        BaseResponse<IEnumerable<OrderCartDto>> GetAllByUserId(string userId);
        BaseResponse<OrderCartDto> Delete(string id);
    }
}