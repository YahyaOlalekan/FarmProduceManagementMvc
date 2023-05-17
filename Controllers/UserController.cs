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

        public UserController(IUserService userService)
        {
            _userService = userService;
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
            return View();
        }
        public IActionResult Manager()
        {
            return View();
        }
        public IActionResult Farmer()
        {
            return View();
        }
        public IActionResult Customer()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}