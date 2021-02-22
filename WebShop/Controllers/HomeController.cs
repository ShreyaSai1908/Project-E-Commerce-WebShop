using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShop.Models;
using WebShop.Models.Services;
using WebShop.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using WebShop.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace WebShop.Controllers
{
    public class HomeController : Controller
    {        
        private IProductService _productService;
        private IOrderService _orderService;
        private const string KEY_SHOPPINGCART = "SHOPPINGCART";
        private const string KEY_DELIVERY = "DELIVERY";
        private const string NEWORDER_DEFAULT_ORDERSTATUS = "CREATED";
        private const bool NEWORDER_DEFAULT_PAYMENTSTATUS = false;
        private const string NEWORDER_DEFAULT_CUSTOMERID = "DEFAULT_CUSTOMER_ID";

        public HomeController(IProductService productService, IOrderService orderService)
        {
            _productService = productService;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            ProductViewModel pvm = new ProductViewModel();            
            pvm= _productService.All();
            return View(pvm);
        }

        public IActionResult AddToCart(int id,int qty)
        {
            ShoppingCart cart = new ShoppingCart();
            cart.Product = _productService.FindBy(id);
            cart.ProductQty = qty;

            List<ShoppingCart> userCart = new List<ShoppingCart>();
            if (HttpContext.Session.Get<List<ShoppingCart>>(KEY_SHOPPINGCART) == null)
            {
                userCart.Add(cart);
                HttpContext.Session.Set<List<ShoppingCart>>(KEY_SHOPPINGCART, userCart);
            }
            else 
            {
                userCart = HttpContext.Session.Get<List<ShoppingCart>>(KEY_SHOPPINGCART);
                userCart.Add(cart);
                HttpContext.Session.Set<List<ShoppingCart>>(KEY_SHOPPINGCART, userCart);
            }
            
            return RedirectToAction(nameof(Index));
        }
                
        public IActionResult OrderSummary() 
        {
            List<ShoppingCart> userCart = new List<ShoppingCart>();
            userCart = HttpContext.Session.Get<List<ShoppingCart>>(KEY_SHOPPINGCART);
            ShoppingCartViewModel scvm = new ShoppingCartViewModel();
            scvm.ShoppingCart = userCart;
            return View(scvm);
        }

        [HttpGet]
        public IActionResult PlaceOrder() 
        {
            PlaceOrderViewModel povm = new PlaceOrderViewModel(); 
            List<ShoppingCart> userCart = new List<ShoppingCart>();
            userCart = HttpContext.Session.Get<List<ShoppingCart>>(KEY_SHOPPINGCART);
            povm.ShoppingCart = userCart;

            return View(povm);
        }

        [HttpPost]
        public IActionResult PlaceOrder(PlaceOrderViewModel povm)
        {
            Delivery delivery = new Delivery();
            delivery = povm.Delivery;
            
            HttpContext.Session.Set<Delivery>(KEY_DELIVERY, delivery);

            List<ShoppingCart> userCart = new List<ShoppingCart>();
            userCart = HttpContext.Session.Get<List<ShoppingCart>>(KEY_SHOPPINGCART);

            OrderViewModel odvm = new OrderViewModel();

            OrderHeader newOrdHdr = new OrderHeader();
            newOrdHdr.CreateDate = DateTime.Now;
            newOrdHdr.OrderStatus = NEWORDER_DEFAULT_ORDERSTATUS;
            newOrdHdr.PaymentStatus = NEWORDER_DEFAULT_PAYMENTSTATUS;
            newOrdHdr.CustomerID = NEWORDER_DEFAULT_CUSTOMERID;
            odvm.OrderHeader = newOrdHdr;

            List<OrderDetails> orderDetailsList = new List<OrderDetails>();
            foreach (ShoppingCart cart in userCart)
            {
                OrderDetails newOrdDet = new OrderDetails();
                newOrdDet.OrderHeader = newOrdHdr;
                newOrdDet.Product = cart.Product;
                newOrdDet.ProductQuantity = cart.ProductQty;
                orderDetailsList.Add(newOrdDet);
            }
            odvm.OrderDetailsList = orderDetailsList;

            odvm =_orderService.Add(odvm);            
            return RedirectToAction(nameof(Payment));
        }

        [HttpGet]
        public IActionResult Payment() 
        {
            PlaceOrderViewModel povm = new PlaceOrderViewModel();

            List<ShoppingCart> userCart = new List<ShoppingCart>();
            userCart = HttpContext.Session.Get<List<ShoppingCart>>(KEY_SHOPPINGCART);

            Delivery delivery = HttpContext.Session.Get<Delivery>(KEY_DELIVERY);

            Double orderTotal = 0;
            foreach (ShoppingCart cart in userCart) 
            {
                orderTotal += Convert.ToInt32(cart.Product.Price) * cart.ProductQty;
            }

            povm.ShoppingCart = userCart;
            povm.Delivery = delivery;
            povm.OrderTotal = orderTotal;

            return View(povm);
        }

        [HttpPost]
        public IActionResult Payment(int id)
        {
            List<ShoppingCart> userCart = new List<ShoppingCart>();
            userCart = HttpContext.Session.Get<List<ShoppingCart>>(KEY_SHOPPINGCART);
            userCart.Clear();
            Delivery delivery = null;

            HttpContext.Session.Set<List<ShoppingCart>>(KEY_SHOPPINGCART, userCart);
            HttpContext.Session.Set<Delivery>(KEY_DELIVERY, delivery);

            return RedirectToAction(nameof(Index));
        }

    }
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) :
                                  JsonConvert.DeserializeObject<T>(value);
        }
    }
}
