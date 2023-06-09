using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Dtos;
using FarmProduceManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FarmProduceManagement.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(CreateCategoryRequestModel model)
        {
            BaseResponse<CategoryDto> category = _categoryService.Create(model);
            // var category = _categoryService.Create(model);
            if (ModelState.IsValid)
            {
                if (category.Status)
                {
                    return RedirectToAction("List");
                }
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
            var category = _categoryService.Delete(id);
            TempData["message"] = category.Message;
            if (category.Status)
            {
                return RedirectToAction("List");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var category = _categoryService.Get(id);

            return View(category.Data);
        }
        [HttpGet]
        public IActionResult List()
        {
            var categories = _categoryService.GetAll();
            return View(categories.Data);
        }


        [HttpGet]
        public IActionResult Update(string id)
        {
            var result = _categoryService.Get(id);
            var model = new UpdateCategoryRequestModel
            {
                NameOfCategory = result.Data.NameOfCategory,
                DescriptionOfCategory = result.Data.DescriptionOfCategory,
            };

            return View(model);
        }
       
        [HttpPost]
        public IActionResult Update(string id, UpdateCategoryRequestModel categoryModel)
        {
           if(ModelState.IsValid)
            {
                 var result = _categoryService.Update(id, categoryModel);
                TempData["message"] = result.Message;
                if (result.Status)
                {
                    return RedirectToAction("List");
                }
           }
          
            return View(categoryModel);
        }
    }
}

 