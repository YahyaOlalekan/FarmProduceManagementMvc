﻿using FarmProduceManagement.Models.Dtos;
using FarmProduceManagement.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FarmProduceManagement.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;
        private readonly IHttpContextAccessor _httpAccessor;
        private readonly IAuth _auth;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService, IHttpContextAccessor httpAccessor, IAuth auth)
        {
            _logger = logger;
            _customerService = customerService;
            _httpAccessor = httpAccessor;
            _auth = auth;
        }
    
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(CreateCustomerRequestModel model)
        {

            var result = _customerService.Create(model);
            ViewBag.Message = result.Message;
            if (ModelState.IsValid)
            {
                if (result.Status)
                {
                    return RedirectToAction("Login", "User");
                }
            }
            return View(model);
        }

        public IActionResult Delete(string id)
        {
            BaseResponse<CustomerDto> result = _customerService.Get(id);
            // var result = _customerService.Get(id);
            return View(result.Data);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult RealDelete(string id)
        {
            var user = _auth.GetLoginUser();
            TempData["balance"] = $"{user.Balance}";

            var result = _customerService.Delete(id);
            TempData["message"] = result.Message;
            if (result.Status)
            {
                return RedirectToAction("List");
            }
            return View(result);
        }

        public IActionResult Details(string id)
        {
            var user = _auth.GetLoginUser();
            TempData["balance"] = $"{user.Balance}";
           
            var result = _customerService.Get(id);
            return View(result.Data);
        }

        public IActionResult List()
        {
            var user = _auth.GetLoginUser();
            TempData["balance"] = $"{user.Balance}";
           
            var result = _customerService.GetAll();
            return View(result.Data);
        }


        public IActionResult Update(string id)
        {
            var user = _auth.GetLoginUser();
            TempData["balance"] = $"{user.Balance}";

            var result = _customerService.Get(id);
            var model = new UpdateCustomerRequestModel
            {
                Address = result.Data.Address,
                Email = result.Data.Email,
                FirstName = result.Data.FirstName,
                LastName = result.Data.LastName,
                PhoneNumber = result.Data.PhoneNumber,
                // ProfilePicture = result.Data.ProfilePicture,
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(string id, UpdateCustomerRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _customerService.Update(id, model);
                TempData["message"] = result.Message;
                if (result.Status)
                {
                    return RedirectToAction("Details", new{id = id});
                }
            }
             return View(model);
        }

         public IActionResult Wallet()
        {
            var user = _auth.GetLoginUser();
            TempData["balance"] = $"{user.Balance}";

            return View();
        }

        [HttpPost]
        public IActionResult Wallet(string id, FundWalletRequestModel model)
        {
            if(ModelState.IsValid)
            {
                var result = _customerService.FundWallet(id, model);
                TempData["message"] = result.Message;
                if(result.Status)
                {
                    // return  View(result.Data);
                    return RedirectToAction("Customer", "User");
                }
            }
            return View(model);
        }

        // public IActionResult Balance(string id)
        // {
        //     var user = _auth.GetLoginUser();
        //     TempData["balance"] = $"{user.Balance}";
           
        //     var result = _customerService.FundWallet(id, amount);
        //     return View(result.Data);
        // }

        /*

                // GET: CustomerController
                public ActionResult Index()
                {
                    return View();
                }

                // GET: CustomerController/Details/5
                public ActionResult Details(int id)
                {
                    return View();
                }

                // GET: CustomerController/Create
                public ActionResult Create()
                {
                    return View();
                }

                // POST: CustomerController/Create
                [HttpPost]
                [ValidateAntiForgeryToken]
                public ActionResult Create(IFormCollection collection)
                {
                    try
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    catch
                    {
                        return View();
                    }
                }

                // GET: CustomerController/Edit/5
                public ActionResult Edit(int id)
                {
                    return View();
                }

                // POST: CustomerController/Edit/5
                [HttpPost]
                [ValidateAntiForgeryToken]
                public ActionResult Edit(int id, IFormCollection collection)
                {
                    try
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    catch
                    {
                        return View();
                    }
                }

                // GET: CustomerController/Delete/5
                public ActionResult Delete(int id)
                {
                    return View();
                }

                // POST: CustomerController/Delete/5
                [HttpPost]
                [ValidateAntiForgeryToken]
                public ActionResult Delete(int id, IFormCollection collection)
                {
                    try
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    catch
                    {
                        return View();
                    }
                }*/
    }
}
