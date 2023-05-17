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

        public Auth(IHttpContextAccessor httpContextAccessor, IFarmerService farmerService)
        {
            _httpContextAccessor = httpContextAccessor;
            _farmerService = farmerService;
        }


        public LoginUserModel GetLoginUser()
        {
            var userBalance = 0m;
            string userId = "";
            var role = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;

            if (role == "Farmer")
            {
                userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var farmer = _farmerService.Get(userId);
                userBalance = farmer.Data.Wallet;
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