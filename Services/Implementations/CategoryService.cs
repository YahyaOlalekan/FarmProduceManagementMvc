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
    public class CategoryService : ICategoryService
    {
      private readonly ICategoryRepository _categoryRepository;
         private readonly IHttpContextAccessor _httpAccessor;

        public CategoryService(ICategoryRepository categoryRepository, IHttpContextAccessor httpAccessor)
        {
            _categoryRepository = categoryRepository;
            _httpAccessor = httpAccessor;
        }

        public BaseResponse<CategoryDto> Create(CreateCategoryRequestModel model)
        {
            var loginId = _httpAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var categoryExist = _categoryRepository.Get(a => a.NameOfCategory == model.NameOfCategory);
            if (categoryExist == null)
            {
                var category = new Category();
                category.NameOfCategory = model.NameOfCategory;
                category.DescriptionOfCategory = model.DescriptionOfCategory;
                category.CreatedBy = loginId;

                _categoryRepository.Create(category);
                _categoryRepository.Save();

                return new BaseResponse<CategoryDto>
                {
                    Message = "Successful",
                    Status = true,
                    Data = new CategoryDto
                    {
                        Id = category.Id,
                        NameOfCategory = category.NameOfCategory,
                        DescriptionOfCategory = category.DescriptionOfCategory
                    }
                };
            }
            return new BaseResponse<CategoryDto>
            {
                Message = "Already exists",
                Status = false
            };

        }

        public BaseResponse<CategoryDto> Delete(string id)
        {
            var category = _categoryRepository.Get(id);
            if (category is null)
            {
                return new BaseResponse<CategoryDto>
                {
                    Message = "The category does not exist",
                    Status = false
                };
            }
            category.IsDeleted = true;
          
            _categoryRepository.Update(category);
            _categoryRepository.Save();
            return new BaseResponse<CategoryDto>
            {
                Message = "Category Deleted Successfully ",
                Status = true
            };

        }

        public BaseResponse<CategoryDto> Get(string id)
        {
            var category = _categoryRepository.Get(id);
            if (category == null)
            {
                return new BaseResponse<CategoryDto>
                {
                    Message = "Not found",
                    Status = false,
                };
            }
            return new BaseResponse<CategoryDto>
            {
                Message = "Found",
                Status = true,
                Data = new CategoryDto
                {
                    Id = category.Id,
                    NameOfCategory = category.NameOfCategory,
                    DescriptionOfCategory = category.DescriptionOfCategory
                }
            };

        }

        public BaseResponse<IEnumerable<CategoryDto>> GetAll()
        {
            var category = _categoryRepository.GetAll();
            if (category == null)
            {
                return new BaseResponse<IEnumerable<CategoryDto>>
                {
                    Message = "Not found",
                    Status = false,
                };
            }
            return new BaseResponse<IEnumerable<CategoryDto>>
            {
                Message = "Found",
                Status = true,
                Data = category.Select(c => new CategoryDto
                {
                    Id = c.Id,
                    NameOfCategory = c.NameOfCategory,
                    DescriptionOfCategory = c.DescriptionOfCategory
                })
            };
        }



        public BaseResponse<CategoryDto> Update(string id, UpdateCategoryRequestModel model)
        {
            var category = _categoryRepository.Get(a => a.Id == id);
            if (category is not null)
            {

                category.NameOfCategory = model.NameOfCategory;
                category.DescriptionOfCategory = model.DescriptionOfCategory;

                _categoryRepository.Update(category);
                _categoryRepository.Save();

                return new BaseResponse<CategoryDto>
                {
                    Message = "Category Updated Successfully",
                    Status = true,
                    Data = new CategoryDto
                    {
                        NameOfCategory = category.NameOfCategory,
                        DescriptionOfCategory = category.DescriptionOfCategory,
                        Id = category.Id,
                    }
                };
            }
            return new BaseResponse<CategoryDto>
            {
                Message = "Unable to Update",
                Status = false,
            };
        }


    }
}