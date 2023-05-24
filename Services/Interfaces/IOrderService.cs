using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Dtos;
using FarmProduceManagement.Models.Entities;
using FarmProduceManagement.Models.Enums;

namespace FarmProduceManagement.Services.Interfaces
{
    public interface IOrderService
    {
        
        BaseResponse<OrderDto> Create(CreateOrderRequestModel model);
         BaseResponse<OrderDto> Update(string id, UpdateOrderRequestModel model);
        BaseResponse<OrderDto> Get(string id);
        BaseResponse<IEnumerable<OrderDto>> GetAll();
        BaseResponse<OrderDto> Delete(string id);
        BaseResponse<OrderDto> Details(string OrderNumber);
         BaseResponse<IEnumerable<OrderDto>> GetAllByUserId(string userId);
    }
}