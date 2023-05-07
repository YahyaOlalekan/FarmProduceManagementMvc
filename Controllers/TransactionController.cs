using FarmProduceManagement.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmProduceManagement.Controllers
{
    public class TransactionController : Controller
    {
        public IActionResult CreateTransaction()
        {
            return View();
        }

        public IActionResult TransactionDetails()
        {
            return View();
        }


        [HttpPost]
        public IActionResult TransactionDetails(CreateTransactionRequestModel model)
        {

            /*var result = _customerService.Create(model);
            ViewBag.Message = result.Message;
            if (result.Status)
            {
                return RedirectToAction("Login", "User");
            }*/
            return View(model);
        }

    }
}