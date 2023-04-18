using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Dtos;
using FarmProduceManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FarmProduceManagement.Controllers
{
    public class ProduceController: Controller
    {
         private readonly IProduceService _produceService;
        public ProduceController(IProduceService produceService)
        {
            _produceService = produceService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(CreateProduceRequestModel model)
        {
           BaseResponse<ProduceDto> produce = _produceService.Create(model);
          // var produce = _produceService.Create(model);
          if(produce.Status)
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
           var produce = _produceService.Delete(id);
           TempData["message"] = produce.Message;
           if(produce.Status)
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
        public IActionResult List()
        {
            var produces = _produceService.GetAll();
            return View(produces.Data);
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