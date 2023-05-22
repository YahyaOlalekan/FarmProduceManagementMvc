

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
    public class ProductService : IProductService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IProductRepository _productRepository;
        private readonly IHttpContextAccessor _httpAccessor;
        private readonly IProduceRepository _produceRepository;
        private readonly IOrderCartRepository _orderCartRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderProductRepository _orderProductRepository;

        public ProductService(IAdminRepository adminRepository, IProductRepository productRepository, IHttpContextAccessor httpAccessor, IProduceRepository produceRepository, IOrderCartRepository orderCartRepository, ICustomerRepository customerRepository, IOrderRepository orderRepository, IOrderProductRepository orderProductRepository)
        {
            _adminRepository = adminRepository;
            _productRepository = productRepository;
            _httpAccessor = httpAccessor;
            _produceRepository = produceRepository;
            _orderCartRepository = orderCartRepository;
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
            _orderProductRepository = orderProductRepository;
        }

        public BaseResponse<ProductDto> Create(CreateProductRequestModel model)
        {
            var loginId = _httpAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var produce = _produceRepository.Get(model.ProduceId);
            if (produce == null)
            {
                return new BaseResponse<ProductDto>
                {
                    Message = "Produce not found",
                    Status = false,
                };
            }

            var product = _productRepository.Get(a => a.ProduceId == model.ProduceId);

            if (product == null)
            {

                product = new Product
                {
                    ProduceId = model.ProduceId,
                    CategoryId = model.CategoryId,
                    SellingPrice = produce.SellingPrice,
                    UnitOfMeasurement = model.UnitOfMeasurement,
                    CreatedBy = loginId
                };

                var productSaved = _productRepository.Create(product);
            }
            else
            {
                product.QuantityToSell += model.QuantityToSell;
            }

            _productRepository.Save();

            return new BaseResponse<ProductDto>
            {
                Message = "Successful",
                Status = true,
                Data = new ProductDto
                {
                    QuantityToSell = model.QuantityToSell,
                    CategoryId = model.CategoryId,
                    SellingPrice = produce.SellingPrice,
                    UnitOfMeasurement = model.UnitOfMeasurement,
                }
            };

        }

        public BaseResponse<ProductDto> Delete(string id)
        {
            var product = _productRepository.Get(id);
            if (product is null)
            {
                return new BaseResponse<ProductDto>
                {
                    Message = "The product does not exist",
                    Status = false
                };
            }
            product.IsDeleted = true;

            _productRepository.Update(product);
            _productRepository.Save();
            return new BaseResponse<ProductDto>
            {
                Message = "Product Deleted Successfully ",
                Status = true
            };

        }


        public BaseResponse<ProductDto> Get(string id)
        {
            var product = _productRepository.Get(id);
            if (product == null)
            {
                return new BaseResponse<ProductDto>
                {
                    Message = "Not found",
                    Status = false,
                };
            }
            return new BaseResponse<ProductDto>
            {
                Message = "Found",
                Status = true,
                Data = new ProductDto
                {
                    Id = product.Id,
                    NameOfCategory = product.Category.NameOfCategory,
                    SellingPrice = product.SellingPrice,
                    UnitOfMeasurement = product.UnitOfMeasurement,
                    QuantityToSell = product.QuantityToSell,
                }
            };

        }

        public BaseResponse<IEnumerable<ProductDto>> GetAll()
        {
            var product = _productRepository.GetAll();
            if (product == null)
            {
                return new BaseResponse<IEnumerable<ProductDto>>
                {
                    Message = "Not found",
                    Status = false,
                };
            }
            return new BaseResponse<IEnumerable<ProductDto>>
            {
                Message = "Found",
                Status = true,
                Data = product.Select(p => new ProductDto
                {
                    Id = p.Id,
                    ProduceId = p.ProduceId,
                    NameOfCategory = p.Category.NameOfCategory,
                    SellingPrice = p.SellingPrice,
                    UnitOfMeasurement = p.UnitOfMeasurement,
                    QuantityToSell = p.QuantityToSell,
                    Produce = new ProduceDto
                    {
                        Id = p.Produce.Id,
                        ProduceName = p.Produce.ProduceName,
                        NameOfCategory = p.Produce.Category.NameOfCategory,
                        SellingPrice = p.SellingPrice,
                        UnitOfMeasurement = p.UnitOfMeasurement,
                        QuantityToBuy = p.QuantityToSell, 
                    }
                })
            };
        }


        public BaseResponse<IEnumerable<ProductDto>> GetByCategoryId(string id)
        {
            var product = _productRepository.GetSelected(p => p.CategoryId == id || p.Id == id);
            if (product == null)
            {
                return new BaseResponse<IEnumerable<ProductDto>>
                {
                    Message = "Not found",
                    Status = false,
                };
            }
            return new BaseResponse<IEnumerable<ProductDto>>
            {
                Message = "Found",
                Status = true,
                Data = product.Select(p => new ProductDto
                {
                    Id = p.Id,
                    NameOfCategory = p.Category.NameOfCategory,
                    SellingPrice = p.SellingPrice,
                    UnitOfMeasurement = p.UnitOfMeasurement,
                    QuantityToSell = p.QuantityToSell,
                })
            };
        }



        public BaseResponse<ProductDto> Sell(SellProductRequestModel model)
        {
            var loginId = _httpAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var orderCarts = _orderCartRepository.GetSelected(p => p.UserId == loginId);

            if (orderCarts == null)
            {
                return new BaseResponse<ProductDto>
                {
                    Message = "Product is not available",
                    Status = false
                };
            }


            
            var customer = _customerRepository.Get(f => f.UserId == loginId);

            
            var totalAmount = orderCarts.Select(p => p.TotalPrice).Sum();
            var totalQuantity = orderCarts.Select(p => p.Quantity).Sum();

            customer.Wallet -= totalAmount;

            if(customer.Wallet < 1){
                
                return new BaseResponse<ProductDto>
                {
                    Message = "You dont have enough balance",
                    Status = false
                };
            }



            var order = new Order
            {
                OrderNumber = _orderRepository.GenerateOrderNumber(),
                CreatedBy = loginId,
                CustomerId = customer.Id,
                TotalAmount = totalAmount,
                TotalQuantity = totalQuantity,
            };


            // var product = _productRepository.GetSelected(p => model.ProduceId.Contains(p.ProduceId) && );

            // var order = new Order();

            for (int i = 0; i < model.ProductId.Count; i++)
            {
                var product = _productRepository.Get(p => p.Id == model.ProductId[i] && p.QuantityToSell >= model.Quantity[i]);

                if (product == null)
                {
                    return new BaseResponse<ProductDto>
                    {
                        Message = "The product is not available!",
                        Status = false,
                    };
                }
                else{

                    product.QuantityToSell -= model.Quantity[i];
                }


                var orderProduct = new OrderProduct
                {
                    OrderId = order.Id,
                    ProductId = model.ProductId[i],
                    Quantity = model.Quantity[i],
                    Price = product.SellingPrice,
                    CreatedBy = loginId,
                };


                _orderProductRepository.Create(orderProduct);

            }


            var admin = _adminRepository.GetAdmin();
            admin.CompanyWallet += totalAmount;

            // var order = new Order
            // {
            //     OrderNumber = _orderRepository.GenerateOrderNumber(),
            //     CreatedBy = loginId,
            //     CustomerId = customer.Id,
            //     TotalAmount = totalAmount,
            //     TotalQuantity = totalQuantity,
            // };

            // var orderProduct = cartItems.Select(p => new OrderProduct
            // {
            //     OrderId = order.Id,
            //     ProductId = product.Id,
            //     Quantity = p.Quantity,
            //     Price = p.Price,
            //     CreatedBy = loginId,
            // });

            // foreach (var item in orderProduct)
            // {
            //     _orderProductRepository.Create(item);
            // }

            _orderRepository.Create(order);

            foreach (var item in orderCarts)
            {
                _orderCartRepository.Delete(item);
            }

            _productRepository.Save();

            // cartItems.Select(item => _cartItemRepository.Delete(item));

            return new BaseResponse<ProductDto>
            {
                Message = "Successful",
                Status = true,
            };
        }

        public BaseResponse<ProductDto> Update(string id, UpdateProductRequestModel model)
        {
            var product = _productRepository.Get(a => a.Id == id);
            if (product is not null)
            {

               // product.ProduceName = model.ProduceName;
                product.SellingPrice = model.SellingPrice;
                product.QuantityToSell = model.QuantityToSell;
                product.UnitOfMeasurement = model.UnitOfMeasurement;
                // product.Category.NameOfCategory = model.NameOfCategory;

                _productRepository.Update(product);
                _productRepository.Save();

                return new BaseResponse<ProductDto>
                {
                    Message = "Product Updated Successfully",
                    Status = true,
                    Data = new ProductDto
                    {
                        Id = product.Id,
                        //ProduceName = product.ProduceName,
                        // NameOfCategory = produce.Category.NameOfCategory,
                        SellingPrice = product.SellingPrice,
                        UnitOfMeasurement = product.UnitOfMeasurement,
                        QuantityToSell = product.QuantityToSell,

                    }
                };
            }
            return new BaseResponse<ProductDto>
            {
                Message = "Unable to Update",
                Status = false,
            };
        }



    }
}











