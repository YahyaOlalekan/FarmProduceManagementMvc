using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Dtos;
using FarmProduceManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FarmProduceManagement.Controllers
{
    public class OrderCartController : Controller
    {
        private readonly IOrderCartService _orderCartService;
        public OrderCartController(IOrderCartService orderCartService)
        {
            _orderCartService = orderCartService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Add(CreateOrderCartRequestModel model)
        {
            
            // var cartItem = _orderCartService.Create(model);
            if (ModelState.IsValid)
            {
                BaseResponse<OrderCartDto> orderCart = _orderCartService.Create(model);
                return Json(orderCart);
               
            }
            return Json(new{ Status = false, Message = "error"});
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            var orderCart = _orderCartService.Delete(id);
            return Json(orderCart);
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var orderCart = _orderCartService.Get(id);

            return View(orderCart.Data);
        }


        [HttpGet]
        public IActionResult List()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = _orderCartService.GetAllByUserId(userId);
            if(result.Status)
            {
                return Json(Ok(new{
                    Status = true,
                    Data = result.Data,
                    TotalItem = result.Data.Select(c => c.Quantity).Sum(),
                    TotalPrice = result.Data.Select(c => c.Price * (decimal)c.Quantity).Sum(),
                }));
            }

            return Json(result);
        }
        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }
        // [HttpPost]
        // public IActionResult Update(string id, UpdateCartItemRequestModel roleModel)
        // {
        //     var updateRole = _orderCartService.Update(id, roleModel);
        //     TempData["message"] = updateRole.Message;
        //     if (updateRole.Status)
        //     {
        //         return RedirectToAction("List");
        //     }
        //     return View(roleModel);
        // }
    }
}