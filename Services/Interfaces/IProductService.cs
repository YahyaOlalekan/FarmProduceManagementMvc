using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Dtos;
using FarmProduceManagement.Models.Enums;

namespace FarmProduceManagement.Services.Interfaces
{
    public interface IProductService
    {
        BaseResponse<ProductDto> Create(CreateProductRequestModel model);
        BaseResponse<ProductDto> Sell(SellProductRequestModel model);
        BaseResponse<ProductDto> Update(string id, UpdateProductRequestModel model);
        BaseResponse<ProductDto> Get(string id);
        BaseResponse<IEnumerable<ProductDto>> GetAll();
        BaseResponse<IEnumerable<ProductDto>> GetByCategoryId(string categoryId);
        BaseResponse<ProductDto> Delete(string id);
    }
}