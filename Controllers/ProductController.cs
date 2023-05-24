using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Dtos;
using FarmProduceManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FarmProduceManagement.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private  readonly IAuth  _auth;

        public ProductController(IProductService productService, ICategoryService categoryService, IAuth auth)
        {
            _productService = productService;
            _categoryService = categoryService;
            _auth = auth;
        }

        [HttpGet]
        public IActionResult Add()
        {
            var categories = _categoryService.GetAll();
            ViewData["allCategories"] = new SelectList(categories.Data, "Id", "NameOfCategory");
            return View();
        }
        [HttpPost]
        public IActionResult Add(CreateProductRequestModel model)
        {
            BaseResponse<ProductDto> product = _productService.Create(model);

            if (ModelState.IsValid)
            {
                if (product.Status)
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
            var product = _productService.Delete(id);
            TempData["message"] = product.Message;
            if (product.Status)
            {
                return RedirectToAction("List");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var product = _productService.Get(id);

            return View(product.Data);
        }

        [HttpGet]
        public IActionResult GetProduce(string id)
        {
            var product = _productService.GetByCategoryId(id);

            return Json(product);
        }



        [HttpGet]
        public IActionResult List()
        {
            
            var user = _auth.GetLoginUser();
            TempData["balance"] = user.Balance;
            
            var product = _productService.GetAll();
            return View(product.Data);
        }



        // [HttpGet]
        // public IActionResult Sell()
        // {

        //     return View();
        // }

       
        [HttpPost]
        public IActionResult Sell(SellProductRequestModel model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if(ModelState.IsValid)
            {
                BaseResponse<ProductDto> product = _productService.Sell(model);

                TempData["message"] = product.Message;
                
                if (product.Status)
                {
                    return RedirectToAction("List","Order", new{id = userId});
                }
            }

            return RedirectToAction("List","Product");
        }


        [HttpGet]
        public IActionResult Update(string id)
        {
             var result = _productService.Get(id);
            var model = new UpdateProductRequestModel
            {
               ProduceName = result.Data.ProduceName,
               SellingPrice = result.Data.SellingPrice,
               QuantityToSell = result.Data.QuantityToSell,
               UnitOfMeasurement = result.Data.UnitOfMeasurement,
            };
             return View(model);
        }
       
       
        [HttpPost]
        public IActionResult Update(string id, UpdateProductRequestModel model)
        {
            if(ModelState.IsValid)
           {
                 var result = _productService.Update(id, model);
                TempData["message"] = result.Message;
                if (result.Status)
                {
                    return RedirectToAction("List");
                }
               
           }
            return View(model);
        }





        //         [HttpGet]
        // public IActionResult Purchase()
        // {
        //     // var categories = _categoryService.GetAll();

        //     // var model = new PurchaseProduceRequestModel
        //     // {
        //     //     CategoryList = new SelectList(categories.Data, "Id", "NameOfCategory"),
        //     // };

        //     return View();
        // }



                // [HttpGet]
        // public IActionResult ProduceNumber()
        // {
        //     return View();
        // }

        // [HttpPost]
        // public IActionResult ProduceNumber()
        // {
        //     return RedirectToAction();
        // }


                 // [HttpPost]
        // public IActionResult Sell(SellProduceRequestModel model)
        // {
        //     // var produce = _produceService.Create(model);
            
        //     if(ModelState.IsValid)
        //     {
        //         BaseResponse<ProduceDto> produce = _produceService.Sell(model);
                
        //         if (produce.Status)
        //         {
        //             return RedirectToAction("List");
        //         }
        //     }

        //     // var categories = _categoryService.GetAll();
        //     // model.CategoryList = new SelectList(categories.Data, "Id", "NameOfCategory");
        //     // return View(model);

        //     var categories = _categoryService.GetAll();

        //     var modell = new SellProduceRequestModel
        //     {
        //         CategoryList = new SelectList(categories.Data, "Id", "NameOfCategory"),
        //     };

        //     return View(modell);
        // }


    }
}