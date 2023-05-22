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
    public class TransactionController : Controller
    {
        private readonly IAuth _auth;
        private readonly IFarmerService _farmerService;
        private readonly ITransactionService _transactionService;
        private readonly IUserService _userService;

        public TransactionController(IAuth auth, IFarmerService farmerService, ITransactionService transactionService, IUserService userService)
        {
            _auth = auth;
            _farmerService = farmerService;
            _transactionService = transactionService;
            _userService = userService;
        }

        public IActionResult Approve(string id)
        {
            var user = _auth.GetLoginUser();
            // // TempData["balance"] = user.Balance;

            var response = _transactionService.Approved(user.UserId, id);

            if (response.Status)
            {
                return RedirectToAction("Details", new { id = response.Data.Id });
            }
            return RedirectToAction("Details", new { id = id });
        }

        public IActionResult Approved(string id)
        {
            var response = _transactionService.GetAllByStatus(TransactionStatus.Approved);
            if (response.Status)
            {
                return View(response.Data);
            }
            return View();
        }
       
        public IActionResult Decline(string id)
        {
            var user = _auth.GetLoginUser();
            var response = _transactionService.Declined(user.UserId, id);
            if (response.Status)
            {
                return RedirectToAction("Details", new{id = id});
            }
            return RedirectToAction("Details", new{id = id});
        }
       
        public IActionResult Declined(string id)
        {
            var response = _transactionService.GetAllByStatus(TransactionStatus.Rejected);
            if (response.Status)
            {
                return View(response.Data);
            }
            return View();
        }

        public IActionResult Delivered(string id)
        {
            var response = _transactionService.Delivered(id);
            TempData["message"] = response.Message;
            if (response.Status)
            {
                return RedirectToAction("Details", new { id = id });
            }
            return RedirectToAction("Details", new { id = id });
        }

        [HttpGet]
        [Route("/Transaction/Delivered/All/{id?}")]
        public IActionResult AllDelivered(string id)
        {
            var response = _transactionService.GetAllByStatus(TransactionStatus.Delivered);
            if (response.Status)
            {
                return View(response.Data);
            }
            return View();
        }


         public IActionResult NotDelivered(string id)
        {
            var response = _transactionService.NotDelivered(id);
            TempData["message"] = response.Message;
            if (response.Status)
            {
                return RedirectToAction("Details", new { id = id });
            }
            return RedirectToAction("Details", new { id = id });
        }

        [HttpGet]
        [Route("/Transaction/Delivered/Not/All/{id?}")]
        public IActionResult AllNotDelivered(string id)
        {
            var response = _transactionService.GetAllByStatus(TransactionStatus.NotDelivered);
            if (response.Status)
            {
                return View(response.Data);
            }
            return View();
        }


        public IActionResult List(string id)
        {
            var user = _auth.GetLoginUser();
            TempData["balance"] = user.Balance;

            var response = _transactionService.GetAllByUserId(id);
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


            var response = _transactionService.Details(id);
            if (response.Status)
            {
                return View(response.Data);
            }
            return View();
        }

        public IActionResult Pending(string id)
        {
            var response = _transactionService.GetAllByStatus(TransactionStatus.Pending);
            if (response.Status)
            {
                return View(response.Data);
            }
            return View();
        }

        // public IActionResult Approve(string id)
        // {
        //     var response = _transactionService.GetAllByStatus(TransactionStatus.Successful);
        //     if(response.Status)
        //     {
        //         return View(response.Data);
        //     }
        //     return View();
        // }
        // public IActionResult Reject(string id)
        // {
        //     var response = _transactionService.GetAllByStatus(TransactionStatus.Rejected);
        //     if(response.Status)
        //     {
        //         return View(response.Data);
        //     }
        //     return View();
        // }
    }
}