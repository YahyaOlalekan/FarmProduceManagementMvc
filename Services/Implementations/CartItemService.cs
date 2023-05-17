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
    public class CartItemService : ICartItemService
    {
      private readonly ICartItemRepository _cartItemRepository;
         private readonly IHttpContextAccessor _httpAccessor;

        public CartItemService(ICartItemRepository cartItemRepository, IHttpContextAccessor httpAccessor)
        {
            _cartItemRepository = cartItemRepository;
            _httpAccessor = httpAccessor;
            
        }

        public BaseResponse<CartItemDto> Create(CreateCartItemRequestModel model)
        {
            var loginId = _httpAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var cartItem = _cartItemRepository.Get(a => a.ProduceId == model.ProduceId && a.UserId == loginId);
            if (cartItem == null)
            {
                cartItem = new CartItem{
                    NameOfCategory = model.CategoryId,
                    CostPrice = model.CostPrice,
                    ProduceId = model.ProduceId,
                    QuantityToBuy = model.QuantityToBuy,
                    TotalCostPrice = (decimal)model.QuantityToBuy * model.CostPrice,
                    UnitOfMeasurement = model.UnitOfMeasurement,
                    UserId = loginId,
                    CreatedBy = loginId,
                };

                 _cartItemRepository.Create(cartItem);
            }
            else
            {
                 cartItem.QuantityToBuy += model.QuantityToBuy;
            }

             
            _cartItemRepository.Save();

            return new BaseResponse<CartItemDto>
            {
                Message = "Successful",
                Status = true,
                Data = new CartItemDto
                {
                    Id = cartItem.Id,
                    NameOfCategory = cartItem.NameOfCategory,
                    CostPrice = cartItem.CostPrice,
                    ProduceId = cartItem.ProduceId,
                    Produce = cartItem.Produce,
                    QuantityToBuy = cartItem.QuantityToBuy,
                    UnitOfMeasurement = cartItem.UnitOfMeasurement,
                    UserId = cartItem.UserId,
                    User = cartItem.User,
                }
            };
            
           
        }

        public BaseResponse<CartItemDto> Delete(string id)
        {
            var cartItem = _cartItemRepository.Get(id);
            if (cartItem is null)
            {
                return new BaseResponse<CartItemDto>
                {
                    Message = "The cart Item does not exist",
                    Status = false
                };
            }
          
            _cartItemRepository.Delete(cartItem);
            _cartItemRepository.Save();

            return new BaseResponse<CartItemDto>
            {
                Message = "Cart Item Deleted Successfully ",
                Status = true
            };

        }

        public BaseResponse<CartItemDto> Get(string id)
        {
            var cartItem = _cartItemRepository.Get(id);
            if (cartItem == null)
            {
                return new BaseResponse<CartItemDto>
                {
                    Message = "Not found",
                    Status = false,
                };
            }
            return new BaseResponse<CartItemDto>
            {
                Message = "Found",
                Status = true,
                Data = new CartItemDto
                {
                    Id = cartItem.Id,
                    NameOfCategory = cartItem.NameOfCategory
                }
            };

        }

        public BaseResponse<IEnumerable<CartItemDto>> GetAll()
        {
            var cartItems = _cartItemRepository.GetAll();
            if (cartItems == null)
            {
                return new BaseResponse<IEnumerable<CartItemDto>>
                {
                    Message = "Not found",
                    Status = false,
                };
            }
            return new BaseResponse<IEnumerable<CartItemDto>>
            {
                Message = "Found",
                Status = true,
                Data = cartItems.Select(c => new CartItemDto
                {
                    Id = c.Id,
                    NameOfCategory = c.NameOfCategory
                })
            };
        }



        public BaseResponse<IEnumerable<CartItemDto>> GetAllByUserId(string userId)
        {
            var cartItems = _cartItemRepository.GetSelected(a => a.UserId == userId);
            if (cartItems is not null)
            {

                return new BaseResponse<IEnumerable<CartItemDto>>
                {
                    Message = "Success",
                    Status = true,
                    Data = cartItems.Select(c => new CartItemDto
                    {
                        Id = c.Id,
                        NameOfCategory = c.NameOfCategory,
                        CostPrice = c.CostPrice,
                        ProduceId = c.ProduceId,
                        Produce = c.Produce,
                        QuantityToBuy = c.QuantityToBuy,
                        UnitOfMeasurement = c.UnitOfMeasurement,
                        UserId = c.UserId,
                        User = c.User,
                    })
                };
            }

            return new BaseResponse<IEnumerable<CartItemDto>>
            {
                Message = "Not found",
                Status = false,
            };
        }


    }
}