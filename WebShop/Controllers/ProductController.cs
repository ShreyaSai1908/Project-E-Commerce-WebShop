using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebShop.Models;
using WebShop.Models.Services;
using WebShop.Models.ViewModel;

namespace WebShop.Controllers
{
    [Authorize(Roles = "Admin")]

    public class ProductController : Controller
    {
        // private readonly ILogger<ProductController> _logger;
        private IProductService _productService;

        public ProductController(IProductService productService)
        {

            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ShowProduct()
        {
            ProductViewModel pvm = _productService.All();
            return View(pvm);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ProductViewModel pvm = _productService.All();
            Product editProduct = _productService.FindBy(id);
            pvm.ProductName = editProduct.ProductName;
            pvm.Price = editProduct.Price;
            pvm.Description = editProduct.Description;
            pvm.ProductID = editProduct.ProductID;

            return View(pvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product edit)
        {
            _productService.Edit(edit.ProductID, edit);
            return RedirectToAction(nameof(ShowProduct));
        }

        public ActionResult Delete(int id)
        {
            _productService.Remove(id);
            return RedirectToAction(nameof(ShowProduct));
        }

        [HttpGet]
        public ActionResult Create()
        {
            ProductViewModel pvm = new ProductViewModel();            
            pvm = _productService.All();            
            return View(pvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel createProduct)
        {
            if (ModelState.IsValid)
            {
                Product product = _productService.Add(createProduct);

                if (product == null)
                {
                    ModelState.AddModelError("msg", "Database Problem");
                    return View(createProduct);
                }

                return RedirectToAction(nameof(ShowProduct));
            }
            else
            {
                return View(createProduct);
            }
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            ProductViewModel pvm = new ProductViewModel();
            Product Details = _productService.FindBy(id);
            pvm.ProductName = Details.ProductName;
            pvm.Price = Details.Price;
            pvm.Description = Details.Description;           
            return View(pvm);
        }
    }
}
