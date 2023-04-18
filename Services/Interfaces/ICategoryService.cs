using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Dtos;

namespace FarmProduceManagement.Services.Interfaces
{
    public interface ICategoryService
    {
         BaseResponse<CategoryDto> Create(CreateCategoryRequestModel model);
        BaseResponse<CategoryDto> Update(string id, UpdateCategoryRequestModel model);
        BaseResponse<CategoryDto> Get(string id);
        BaseResponse<IEnumerable<CategoryDto>> GetAll();
        BaseResponse<CategoryDto> Delete(string id);
    }
}

