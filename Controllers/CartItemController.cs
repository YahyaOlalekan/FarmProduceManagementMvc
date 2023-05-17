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
    public class CartItemController : Controller
    {
        private readonly ICartItemService _cartItemService;
        public CartItemController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Add(CreateCartItemRequestModel model)
        {
            
            // var cartItem = _cartItemService.Create(model);
            if (ModelState.IsValid)
            {
                BaseResponse<CartItemDto> cartItem = _cartItemService.Create(model);
                return Json(cartItem);
               
            }
            return Json(new{ Status = false, Message = "error"});
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            var cartItem = _cartItemService.Delete(id);
            return Json(cartItem);
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var cartItem = _cartItemService.Get(id);

            return View(cartItem.Data);
        }


        [HttpGet]
        public IActionResult List()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = _cartItemService.GetAllByUserId(userId);
            if(result.Status)
            {
                return Json(Ok(new{
                    Status = true,
                    Data = result.Data,
                    TotalItem = result.Data.Select(c => c.QuantityToBuy).Sum(),
                    TotalPrice = result.Data.Select(c => c.CostPrice * (decimal)c.QuantityToBuy).Sum(),
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
        //     var updateRole = _cartItemService.Update(id, roleModel);
        //     TempData["message"] = updateRole.Message;
        //     if (updateRole.Status)
        //     {
        //         return RedirectToAction("List");
        //     }
        //     return View(roleModel);
        // }
    }
}