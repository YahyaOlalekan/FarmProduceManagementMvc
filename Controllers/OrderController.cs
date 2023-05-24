using FarmProduceManagement.Models.Dtos;
using FarmProduceManagement.Models.Enums;
using FarmProduceManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FarmProduceManagement.Controllers
{
    public class OrderController : Controller
    {
        private readonly IAuth _auth;
        private readonly ICustomerService _customerService;
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;

        public OrderController(IAuth auth, ICustomerService customerService, IUserService userService, IOrderService orderService)
        {
            _auth = auth;
            _customerService = customerService;
            _userService = userService;
            _orderService = orderService;
        }

        public IActionResult List(string id)
        {
            var user = _auth.GetLoginUser();
            TempData["balance"] = user.Balance;

            var response = _orderService.GetAllByUserId(id);
            if (response.Status)
            {
                return View(response.Data);
            }
            return View();
        }

        public IActionResult Details(string id)
        {
            var user = _auth.GetLoginUser();
            TempData["balance"] = user.Balance;


            var response = _orderService.Details(id);
            if (response.Status)
            {
                return View(response.Data);
            }
            return View();
        }

       
    }
}