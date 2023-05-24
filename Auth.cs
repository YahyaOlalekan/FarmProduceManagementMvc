using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Dtos;
using FarmProduceManagement.Models.Entities;
using FarmProduceManagement.Services.Interfaces;

namespace FarmProduceManagement
{

    public class Auth : IAuth
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFarmerService _farmerService;
        private readonly ICustomerService _customerService;

        public Auth(IHttpContextAccessor httpContextAccessor, IFarmerService farmerService, ICustomerService customerService)
        {
            _httpContextAccessor = httpContextAccessor;
            _farmerService = farmerService;
            _customerService = customerService;
        }

        public LoginUserModel GetLoginUser()
        {
            var userBalance = 0m;
            var role = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (role == "Farmer")
            {
                var farmer = _farmerService.Get(userId);
                userBalance = Math.Round(farmer.Data.Wallet, 4);
            }
            else if(role == "Customer")
            {
                
                var customer = _customerService.Get(userId);
                userBalance = Math.Round(customer.Data.Wallet, 4);
            }

            return new LoginUserModel
            {
                UserId = userId,
                Balance = userBalance,
            };
        }
    }

    
    public interface IAuth
    {
        LoginUserModel GetLoginUser();
    }
}