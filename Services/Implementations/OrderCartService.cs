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
    public class OrderCartService : IOrderCartService
    {
      private readonly IOrderCartRepository _orderCartRepository;
         private readonly IHttpContextAccessor _httpAccessor;

        public OrderCartService(IOrderCartRepository orderCartRepository, IHttpContextAccessor httpAccessor)
        {
            _orderCartRepository = orderCartRepository;
            _httpAccessor = httpAccessor;
        }

        public BaseResponse<OrderCartDto> Create(CreateOrderCartRequestModel model)
        {
            var loginId = _httpAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var orderCart = _orderCartRepository.Get(a => a.ProductId == model.ProductId && a.UserId == loginId);
            if (orderCart == null)
            {
                orderCart = new OrderCart{
                    NameOfCategory = model.CategoryId,
                    Price = model.Price,
                    ProductId = model.ProductId,
                    Quantity = model.Quantity,
                    TotalPrice = (decimal)model.Quantity * model.Price,
                    UnitOfMeasurement = model.UnitOfMeasurement,
                    UserId = loginId,
                    CreatedBy = loginId,
                };

                 _orderCartRepository.Create(orderCart);
            }
            else
            {
                 orderCart.Quantity += model.Quantity;
            }

             
            _orderCartRepository.Save();

            return new BaseResponse<OrderCartDto>
            {
                Message = "Successful",
                Status = true,
                Data = new OrderCartDto
                {
                    Id = orderCart.Id,
                    NameOfCategory = orderCart.NameOfCategory,
                    Price = orderCart.Price,
                    ProductId = orderCart.ProductId,
                    Product = orderCart.Product,
                    Quantity = orderCart.Quantity,
                    UnitOfMeasurement = orderCart.UnitOfMeasurement,
                    UserId = orderCart.UserId,
                    User = orderCart.User,
                }
            };
            
           
        }

        public BaseResponse<OrderCartDto> Delete(string id)
        {
            var orderCart = _orderCartRepository.Get(id);
            if (orderCart is null)
            {
                return new BaseResponse<OrderCartDto>
                {
                    Message = "The cart Item does not exist",
                    Status = false
                };
            }
          
            _orderCartRepository.Delete(orderCart);
            _orderCartRepository.Save();

            return new BaseResponse<OrderCartDto>
            {
                Message = "Cart Item Deleted Successfully ",
                Status = true
            };

        }

        public BaseResponse<OrderCartDto> Get(string id)
        {
            var orderCart = _orderCartRepository.Get(id);
            if (orderCart == null)
            {
                return new BaseResponse<OrderCartDto>
                {
                    Message = "Not found",
                    Status = false,
                };
            }
            return new BaseResponse<OrderCartDto>
            {
                Message = "Found",
                Status = true,
                Data = new OrderCartDto
                {
                    Id = orderCart.Id,
                    NameOfCategory = orderCart.NameOfCategory
                }
            };

        }

        public BaseResponse<IEnumerable<OrderCartDto>> GetAll()
        {
            var orderCarts = _orderCartRepository.GetAll();
            if (orderCarts == null)
            {
                return new BaseResponse<IEnumerable<OrderCartDto>>
                {
                    Message = "Not found",
                    Status = false,
                };
            }
            return new BaseResponse<IEnumerable<OrderCartDto>>
            {
                Message = "Found",
                Status = true,
                Data = orderCarts.Select(c => new OrderCartDto
                {
                    Id = c.Id,
                    NameOfCategory = c.NameOfCategory
                })
            };
        }



        public BaseResponse<IEnumerable<OrderCartDto>> GetAllByUserId(string userId)
        {
            var orderCarts = _orderCartRepository.GetSelected(a => a.UserId == userId);
            if (orderCarts is not null)
            {

                return new BaseResponse<IEnumerable<OrderCartDto>>
                {
                    Message = "Success",
                    Status = true,
                    Data = orderCarts.Select(c => new OrderCartDto
                    {
                        Id = c.Id,
                        NameOfCategory = c.NameOfCategory,
                        Price = c.Price,
                        ProductId = c.ProductId,
                        Product = c.Product,
                        Produce = c.Product.Produce,
                        Quantity = c.Quantity,
                        UnitOfMeasurement = c.UnitOfMeasurement,
                        UserId = c.UserId,
                        User = c.User,
                    })
                };
            }

            return new BaseResponse<IEnumerable<OrderCartDto>>
            {
                Message = "Not found",
                Status = false,
            };
        }


    }
}