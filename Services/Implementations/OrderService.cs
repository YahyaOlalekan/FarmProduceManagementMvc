using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Dtos;
using FarmProduceManagement.Models.Entities;
using FarmProduceManagement.Models.Enums;
using FarmProduceManagement.Repositories.Interfaces;
using FarmProduceManagement.Services.Interfaces;

namespace FarmProduceManagement.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IHttpContextAccessor _httpAccessor;
        private readonly IProduceRepository _produceRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;

        public OrderService(IAdminRepository adminRepository, IOrderRepository orderRepository, IHttpContextAccessor httpAccessor, IProduceRepository produceRepository, IProductRepository productRepository, ICustomerRepository customerRepository)
        {
            _adminRepository = adminRepository;
            _orderRepository = orderRepository;
            _httpAccessor = httpAccessor;
            _produceRepository = produceRepository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
        }

        public BaseResponse<OrderDto> Create(CreateOrderRequestModel model)
        {
            var loginId = _httpAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var orderExist = _orderRepository.Get(a => a.OrderNumber == model.OrderNumber);
            OrderProduct OrderProduct = new();
            if (orderExist == null)
            {
                List<Order> orders = new();
                foreach (var product in model.Product)
                {
                    var order = new Order
                    {
                        OrderNumber = _orderRepository.GenerateOrderNumber(),
                        CustomerId = loginId,
                        CreatedBy = loginId,
                        DateCreated = DateTime.Now,
                    };
                    orders.Add(order);
                }

                _orderRepository.CreateOrder(orders);
                _orderRepository.Save();

                return new BaseResponse<OrderDto>
                {
                    Message = "Successful",
                    Status = true,
                };
            }
            return new BaseResponse<OrderDto>
            {
                Message = "Already exists",
                Status = false,
            };

        }

       

        public BaseResponse<OrderDto> Details(string id)
        {
            var order = _orderRepository.Get(id);
            if (order == null)
            {
                return new BaseResponse<OrderDto>
                {
                    Message = "No Order found",
                    Status = false,
                };
            }

            return new BaseResponse<OrderDto>
            {
                Message = "Found",
                Status = true,
                Data = new OrderDto
                {
                    Id = order.Id,
                    DateCreated = order.DateCreated,
                    TotalQuantity = order.TotalQuantity,
                    TotalAmount = order.TotalAmount,
                    OrderNumber = order.OrderNumber,
                    OrderProducts = order.OrderProducts.Select(t => new OrderProductDto
                    {
                        Price = t.Price,
                        Quantity = t.Quantity,
                       // ProduceId = t.Product.ProduceId,
                        Produce = t.Product.Produce
                        
                    }).ToList()
                }
            };

        }


                public BaseResponse<IEnumerable<OrderDto>> GetAllByUserId(string userId)
        {
            var customer = _customerRepository.Get(f => f.UserId == userId);
            var orders = _orderRepository.GetSelected(t => t.CustomerId == customer.Id);

            if (orders == null)
            {

                return new BaseResponse<IEnumerable<OrderDto>>
                {
                    Message = "Order not found",
                    Status = false,
                };
            }


            return new BaseResponse<IEnumerable<OrderDto>>
            {
                Message = "Successful",
                Status = true,
                Data = orders.Select(t => new OrderDto
                {
                    Id = t.Id,
                    CreatedBy = t.CreatedBy,
                    DateCreated = t.DateCreated,
                    Customer = new CustomerDto
                    {
                        Id = t.Customer.Id,
                        User = t.Customer.User,
                        UserId = t.Customer.UserId,
                        Wallet = t.Customer.Wallet,
                    },
                    CustomerId = t.CustomerId,
                    ModifiedBy = t.ModifiedBy,
                    ModifiedOn = t.ModifiedOn,
                    TotalQuantity = t.TotalQuantity,
                    TotalAmount = t.TotalAmount,
                    OrderNumber = t.OrderNumber,
                    OrderProducts = t.OrderProducts.Select(p => new OrderProductDto
                    {
                        Id = p.Id,
                        OrderId = p.OrderId,
                        Quantity = p.Quantity,
                        Price = p.Price,
                    }).ToList(),

                })
            };
        }




        public BaseResponse<OrderDto> Delete(string id)
        {
            var order = _orderRepository.Get(id);
            if (order is null)
            {
                return new BaseResponse<OrderDto>
                {
                    Message = "The Order does not exist",
                    Status = false
                };
            }
            order.IsDeleted = true;

            _orderRepository.Update(order);
            _orderRepository.Save();
            return new BaseResponse<OrderDto>
            {
                Message = "Order Deleted Successfully ",
                Status = true
            };

        }

        public BaseResponse<OrderDto> Get(string id)
        {
            var order = _orderRepository.Get(id);
            if (order == null)
            {
                return new BaseResponse<OrderDto>
                {
                    Message = "Not found",
                    Status = false,
                };
            }

            return new BaseResponse<OrderDto>
            {
                Message = "Found",
                Status = true,
                Data = new OrderDto
                {
                    Id = order.Id,
                    CustomerId = order.CustomerId,
                    Customer = new CustomerDto
                    {
                        Id = order.Customer.Id,
                        User = order.Customer.User,
                        UserId = order.Customer.UserId,
                        Wallet = order.Customer.Wallet,
                    },
                    DateCreated = order.DateCreated,
                    TotalAmount = order.TotalAmount,
                    TotalQuantity = order.TotalQuantity,
                    OrderNumber = order.OrderNumber,
                    OrderProducts = order.OrderProducts.Select(p => new OrderProductDto
                    {
                        Id = p.Id,
                        OrderId = p.OrderId,
                        Quantity = p.Quantity,
                        Price = p.Price,
                    }).ToList(),

                }
            };

        }

        public BaseResponse<IEnumerable<OrderDto>> GetAll()
        {
            var order = _orderRepository.GetAll();
            if (order == null)
            {
                return new BaseResponse<IEnumerable<OrderDto>>
                {
                    Message = "Not found",
                    Status = false,
                };
            }
            return new BaseResponse<IEnumerable<OrderDto>>
            {
                Message = "Found",
                Status = true,
                Data = order.Select(c => new OrderDto
                {
                    Id = c.Id,
                    OrderNumber = _orderRepository.GenerateOrderNumber(),

                })
            };
        }

       
        public BaseResponse<OrderDto> Update(string id, UpdateOrderRequestModel model)
        {
            var order = _orderRepository.Get(a => a.Id == id);
            if (order is not null)
            {

                order.OrderNumber = _orderRepository.GenerateOrderNumber();


                _orderRepository.Update(order);
                _orderRepository.Save();

                return new BaseResponse<OrderDto>
                {
                    Message = "Order Updated Successfully",
                    Status = true,
                    Data = new OrderDto
                    {

                        OrderNumber = _orderRepository.GenerateOrderNumber(),
                        ProduceName = model.ProduceName,
                        UnitOfMeasurement = model.UnitOfMeasurement,
                        Id = order.Id,
                    }
                };
            }
            return new BaseResponse<OrderDto>
            {
                Message = "Unable to Update",
                Status = false,
            };
        }

    }
}