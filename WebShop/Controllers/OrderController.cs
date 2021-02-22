using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShop.Models;
using WebShop.Models.Services;
using WebShop.Models.ViewModel;

namespace WebShop.Controllers
{
    [Authorize(Roles = "Admin")]

    public class OrderController : Controller
    {
        private IProductService _productService;
        private IOrderService _orderService;
        private const string NEWORDER_DEFAULT_ORDERSTATUS= "CREATED";
        private const bool NEWORDER_DEFAULT_PAYMENTSTATUS = false;

        public OrderController(IProductService productService,IOrderService orderService)
        {
            _productService = productService;
            _orderService = orderService;
        }

        public IActionResult ShowOrders()
        {
            OrderViewModel odvm = _orderService.All();
            return View(odvm);

        }

        public IActionResult Details(int id)
        {
            OrderViewModel odvm = new OrderViewModel();
            odvm.OrderDetailsList = _orderService.ReadOrderDetails(id);            
            return View(odvm);
        }

        [HttpGet]
        public IActionResult EditOrderHeader(int id)
        {            
            OrderViewModel odvm = new OrderViewModel();
            odvm.OrderHeader = _orderService.ReadOrderHeader(id);
            return View(odvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditOrderHeader(OrderViewModel edit)
        {
            OrderHeader newOrdHdr = new OrderHeader();
            newOrdHdr = _orderService.ReadOrderHeader(edit.OrderHeader.OrderID);
            newOrdHdr.OrderStatus = edit.OrderHeader.OrderStatus;
            newOrdHdr.PaymentStatus = edit.OrderHeader.PaymentStatus;
            _orderService.Edit(newOrdHdr);

            return RedirectToAction(nameof(ShowOrders));
        }

        [HttpGet]
        public IActionResult EditOrderDetail(int id)
        {
            ProductViewModel pdvm = new ProductViewModel();
            OrderViewModel odvm = new OrderViewModel();
            odvm.OrderDetails = _orderService.ReadOrderDetailLine(id);
            pdvm = _productService.All();
            odvm.ProductList = pdvm.ProductList;
            return View(odvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditOrderDetail(OrderViewModel edit)
        {
            OrderDetails newOrdDet = new OrderDetails();
            newOrdDet = _orderService.ReadOrderDetailLine(edit.OrderDetails.OrderDetailID);
            newOrdDet.Product = _productService.FindBy(edit.OrderDetails.Product.ProductID);
            newOrdDet.ProductQuantity = edit.OrderDetails.ProductQuantity;
            _orderService.EditOrderDetailLine(newOrdDet);

            return RedirectToAction(nameof(ShowOrders));
        }

        public IActionResult DeleteOrderHeader(int id)
        {
            _orderService.Remove(id);
            return RedirectToAction(nameof(ShowOrders));
        }

        public IActionResult DeleteOrderDetail(int id)
        {
            _orderService.RemoveOrderDetailLine(id);
            return RedirectToAction(nameof(ShowOrders));
        }

        [HttpGet]
        public ActionResult Create()
        {
            ProductViewModel pdvm = new ProductViewModel();                        
            OrderViewModel odvm = new OrderViewModel();

            OrderHeader newOrdHdr = new OrderHeader();
            odvm.OrderDetails = new OrderDetails();
            newOrdHdr.CreateDate = DateTime.Now;
            newOrdHdr.OrderStatus = NEWORDER_DEFAULT_ORDERSTATUS;
            newOrdHdr.PaymentStatus = NEWORDER_DEFAULT_PAYMENTSTATUS;
            odvm.OrderDetails.OrderHeader = newOrdHdr;

            pdvm = _productService.All();
            odvm.ProductList = pdvm.ProductList;

            return View(odvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderViewModel createOrder)
        {
            Product assignedProduct = _productService.FindBy(createOrder.OrderDetails.Product.ProductID);
            createOrder.OrderDetails.Product = assignedProduct;

            OrderHeader newOrdHdr = new OrderHeader();
            newOrdHdr.CreateDate = DateTime.Now;
            newOrdHdr.OrderStatus = NEWORDER_DEFAULT_ORDERSTATUS;
            newOrdHdr.PaymentStatus = NEWORDER_DEFAULT_PAYMENTSTATUS;
            newOrdHdr.CustomerID = createOrder.OrderDetails.OrderHeader.CustomerID;
            createOrder.OrderHeader = newOrdHdr;

            OrderDetails newOrdDet = new OrderDetails();
            newOrdDet.OrderHeader = newOrdHdr;
            newOrdDet.Product = assignedProduct;
            newOrdDet.ProductQuantity = createOrder.OrderDetails.ProductQuantity;
            createOrder.OrderDetails = newOrdDet;

            OrderViewModel createdOrder = _orderService.Add(createOrder);

            if (createdOrder == null)
            {
                ModelState.AddModelError("msg", "Database Problem");
                return View(createOrder);
            }
            else 
            {
                return RedirectToAction(nameof(ShowOrders));
            }

            /*if (ModelState.IsValid)
            {
                
                OrderViewModel createdOrder = _orderService.Add(createOrder);

                if (createdOrder == null)
                {
                    ModelState.AddModelError("msg", "Database Problem");
                    return View(createOrder);
                }

                return RedirectToAction(nameof(ShowOrders));
            }
            else
            {
                return View(createOrder);
            }*/

        }
    }
}