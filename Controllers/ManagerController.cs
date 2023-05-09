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
    public class ManagerController : Controller
    {
        private readonly ILogger<ManagerController> _logger;
        private readonly IManagerService _managerService;
        private readonly IHttpContextAccessor  _httpAccessor;

        public ManagerController(ILogger<ManagerController> logger, IManagerService managerService, IHttpContextAccessor httpAccessor)
        {
            _logger = logger;
            _managerService = managerService;
            _httpAccessor = httpAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(CreateManagerRequestModel model)
        {
             var loginId =  _httpAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = _managerService.Create(loginId, model);
            ViewBag.Message = result.Message;
            if (ModelState.IsValid)
            {
                if (result.Status)
                {
                    return RedirectToAction("List");
                }
            }
            return View(model);
        }

        public IActionResult Delete(string id)
        {
            BaseResponse<ManagerDto> result = _managerService.Get(id);
           // var result = _managerService.Get(id);
            return View(result.Data);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult RealDelete(string id)
        {
             var result = _managerService.Delete(id);
             TempData["message"] = result.Message;
            if (result.Status)
            {
                return RedirectToAction("List");
            }
            return View(result);
        }

        public IActionResult Details(string id)
        {
            var result = _managerService.Get(id);
            return View(result.Data);
        }

        public IActionResult List()
        {
            var result = _managerService.GetAll();
            return View(result.Data);
        }


        public IActionResult Update(string id)
        {
            var result = _managerService.Get(id);
            return View(result.Data);
        }

        [HttpPost]
        public IActionResult Update(string id, UpdateManagerRequestModel model)
        {
            var result = _managerService.Update(id, model);
            TempData["message"] = result.Message;
            if (result.Status)
            {
                return RedirectToAction("List");
            }
            return View(result.Data);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}