using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Dtos;
using FarmProduceManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FarmProduceManagement.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(CreateRoleRequestModel model)
        {
           BaseResponse<RoleDto> role = _roleService.Create(model);
          // var role = _roleService.Create(model);
          if(role.Status)
          {
             return RedirectToAction("List");
          }
          return View(model); 
        }
        [HttpGet]
        public IActionResult Delete(string id)
        {
            return View();
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult ActualDelete(string id)
        {
           var role = _roleService.Delete(id);
           TempData["message"] = role.Message;
           if(role.Status)
           {
             return RedirectToAction("List");
           }
           return View();
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var role = _roleService.Get(id);

            return View(role.Data);
        }
        [HttpGet]
        public IActionResult List()
        {
            var roles = _roleService.GetAll();
            return View(roles.Data);
        }
        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Update(string id, UpdateRoleRequestModel roleModel)
        {
            var updateRole = _roleService.Update(id, roleModel);
            TempData["message"] = updateRole.Message;
            if (updateRole.Status)
            {
                return RedirectToAction("List");
            }
            return View(roleModel);
        }
    }
}