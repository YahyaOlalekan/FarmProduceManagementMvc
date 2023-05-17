using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Dtos;

namespace FarmProduceManagement.Services.Interfaces
{
    public interface ICartItemService
    {
         BaseResponse<CartItemDto> Create(CreateCartItemRequestModel model);
        //BaseResponse<CartItemDto> Update(string id, UpdateCartItemRequestModel model);
        BaseResponse<CartItemDto> Get(string id);
        BaseResponse<IEnumerable<CartItemDto>> GetAll();
        BaseResponse<IEnumerable<CartItemDto>> GetAllByUserId(string userId);
        BaseResponse<CartItemDto> Delete(string id);
    }
}