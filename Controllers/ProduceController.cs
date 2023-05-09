using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Dtos;
using FarmProduceManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FarmProduceManagement.Controllers
{
    public class ProduceController : Controller
    {
        private readonly IProduceService _produceService;
        private readonly ICategoryService _categoryService;

        public ProduceController(IProduceService produceService, ICategoryService categoryService)
        {
            _produceService = produceService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            var categories = _categoryService.GetAll();
            ViewData["allCategories"] = new SelectList(categories.Data, "Id", "NameOfCategory");
            return View();
        }
        [HttpPost]
        public IActionResult Add(CreateProduceRequestModel model)
        {
            BaseResponse<ProduceDto> produce = _produceService.Create(model);
            // var produce = _produceService.Create(model);
            if (ModelState.IsValid)
            {
                if (produce.Status)
                {
                    return RedirectToAction("List");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ProduceNumber()
        {
            return View();
        }

        // [HttpPost]
        // public IActionResult ProduceNumber()
        // {
        //     return RedirectToAction();
        // }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            return View();
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult ActualDelete(string id)
        {
            var produce = _produceService.Delete(id);
            TempData["message"] = produce.Message;
            if (produce.Status)
            {
                return RedirectToAction("List");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var produce = _produceService.Get(id);

            return View(produce.Data);
        }

        [HttpGet]
        public IActionResult GetProduce(string id)
        {
            var produce = _produceService.GetByCategoryId(id);

            return Json(produce);
        }



        [HttpGet]
        public IActionResult List()
        {
            var produces = _produceService.GetAll();
            return View(produces.Data);
        }



        [HttpGet]
        public IActionResult Sell()
        {
            var categories = _categoryService.GetAll();

            var model = new SellProduceRequestModel
            {
                CategoryList = new SelectList(categories.Data, "Id", "NameOfCategory"),
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Sell(string id, SellProduceRequestModel model)
        {
            
            // var produce = _produceService.Create(model);
            
            if(ModelState.IsValid)
            {
                BaseResponse<ProduceDto> produce = _produceService.Sell(id, model);
                
                if (produce.Status)
                {
                    return RedirectToAction("List");
                }
            }

            var categories = _categoryService.GetAll();
            model.CategoryList = new SelectList(categories.Data, "Id", "NameOfCategory");
            return View(model);
        }

        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Update(string id, UpdateProduceRequestModel produceModel)
        {
            var updateProduce = _produceService.Update(id, produceModel);
            TempData["message"] = updateProduce.Message;
            if (updateProduce.Status)
            {
                return RedirectToAction("List");
            }
            return View(produceModel);
        }
    }
}