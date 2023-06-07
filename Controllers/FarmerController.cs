using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Dtos;
using FarmProduceManagement.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FarmProduceManagement.Controllers
{
    public class FarmerController : Controller
    {

        private readonly ILogger<FarmerController> _logger;
        private readonly IFarmerService _farmerService;
        private readonly IHttpContextAccessor _httpAccessor;
        private readonly IAuth _auth;

        public FarmerController(ILogger<FarmerController> logger, IFarmerService farmerService, IHttpContextAccessor httpAccessor, IAuth auth)
        {
            _logger = logger;
            _farmerService = farmerService;
            _httpAccessor = httpAccessor;
            _auth = auth;
        }


        public IActionResult Pending()
        {
            var result = _farmerService.GetPendingFarmers();
            //   if(result == null)
            //   {
            //     return RedirectToAction("User" ,"Super");
            //   }
            return View(result.Data);

        }


        [HttpPost]
        public IActionResult Verify(ApproveFarmerDto model)
        {
            var result = _farmerService.VerifyFarmers(model);
            
            if(result.Status)
            { 
                return RedirectToAction("Approved");
            }

            return RedirectToAction("Pending");

        }


        public IActionResult Approved()
        {
            var result = _farmerService.ApprovedFarmers();
            // if(result.Status)
            // {
            //     return RedirectToAction("User" ,"Super");
            // }
            return View(result.Data);

        }
        public IActionResult Declined()
        {
            var result = _farmerService.GetDeclinedFarmers();
           
            return View(result.Data);
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Register(CreateFarmerRequestModel model)
        {

            var result = _farmerService.Create(model);
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
            BaseResponse<FarmerDto> result = _farmerService.Get(id);
            // var result = _farmerService.Get(id);
            return View(result.Data);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult RealDelete(string id)
        {
            var result = _farmerService.Delete(id);
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
            
            var result = _farmerService.Get(id);
            return View(result.Data);
        }

        public IActionResult List()
        {
            var result = _farmerService.GetAll();
            return View(result.Data);
        }


        public IActionResult Update(string id)
        {
            var user = _auth.GetLoginUser();
            TempData["balance"] = $"{user.Balance}";

            var result = _farmerService.Get(id);

            var model = new UpdateFarmerRequestModel{
                Address = result.Data.Address,
                Email = result.Data.Email,
                FirstName = result.Data.FirstName,
                LastName = result.Data.LastName,
                PhoneNumber = result.Data.PhoneNumber
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(string id, UpdateFarmerRequestModel model)
        {
            var user = _auth.GetLoginUser();
            TempData["balance"] = $"{user.Balance}";

            if(ModelState.IsValid)
            {
                var result = _farmerService.Update(id, model);
                TempData["message"] = result.Message;
                if (result.Status)
                {
                    return RedirectToAction("Details", new{id = id});
                }
            }
            return View(model);
        }


        //   [HttpGet]
        // public IActionResult Register()
        // {
        //     return View();
        // }

        // [HttpPost]
        // public IActionResult Register(string loginId, CreateFarmerRequestModel model)
        // {
        //     var farmer = _farmerService.Create(loginId, model);

        //     if(farmer is not null)
        //     {
        //         TempData["Exist"] = "Farmer created Successfully";
        //     }
        //     return RedirectToAction("Login" ,"User");
        // }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}