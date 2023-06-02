using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Dtos;
using FarmProduceManagement.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace FarmProduceManagement.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IFarmerService _farmerService;
        private readonly IManagerService _managerService;
        private readonly ICustomerService _customerService;
        private readonly IAuth _auth;

        public UserController(IUserService userService, IFarmerService farmerService, IManagerService managerService, ICustomerService customerService, IAuth auth)
        {
            _userService = userService;
            _farmerService = farmerService;
            _managerService = managerService;
            _customerService = customerService;
            _auth = auth;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginUserRequestModel model)
        {
            var user = _userService.Login(model);
            TempData["message"] = user.Message;
            if (user.Status)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Data.Id),
                    new Claim(ClaimTypes.Email, user.Data.Email),
                    new Claim(ClaimTypes.Name, user.Data.FirstName +" "+ user.Data.LastName),
                    new Claim(ClaimTypes.Role, user.Data.RoleName)
                };

                var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authenticationProperty = new AuthenticationProperties();

                var claimsPrincipal = new ClaimsPrincipal(claimIdentity);

                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    claimsPrincipal,
                    authenticationProperty
                );
                if (user.Status == true)
                {
                    if (user.Data.RoleName.Contains("Admin"))
                    {
                        return RedirectToAction("Super");
                    }
                    else if (user.Data.RoleName.Contains("Manager"))
                    {
                        return RedirectToAction("Manager");
                    }
                    else if (user.Data.RoleName.Contains("Farmer"))
                    {
                        return RedirectToAction("Farmer");
                    }
                    else if (user.Data.RoleName.Contains("Customer"))
                    {
                        return RedirectToAction("Customer");
                    }
                }

            }
            return View();

        }

        public IActionResult List()
        {
          // var email =  User.FindFirst(ClaimTypes.Email).Value;
            var users = _userService.GetAll();
            return View(users.Data);
        }

        public IActionResult Super()
        {
            // var users = _userService.GetAll().Data.Count();
            var users = _userService.CountUser();
            var managers = _managerService.GetAll().Data.Count();
            var pendingFarmers = _farmerService.GetPendingFarmers().Data.Count();
            var approvedFarmers = _farmerService.ApprovedFarmers().Data.Count();
            var customers = _customerService.GetAll().Data.Count();
           // var balance = 

            var model = new AdminDashboardModel{
                NoOfUsers = users,
                NoOfManagers = managers,
                NoOfPendingFarmers = pendingFarmers,
                NoOfVerifiedFarmers = approvedFarmers,
                NoOfCustomers = customers,
               // CompanyBalance = balance,
            };

            return View(model);
        }
        public IActionResult Manager()
        {
            return View();
        }
        public IActionResult Farmer()
        {
            
            var user = _auth.GetLoginUser();
            TempData["balance"] = user.Balance;

            return View();
        }
        public IActionResult Customer()
        {
            var user = _auth.GetLoginUser();
            TempData["balance"] = user.Balance;

            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}