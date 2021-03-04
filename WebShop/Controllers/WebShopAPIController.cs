using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebShop.Models.Identity;
using WebShop.Models.Services;
using WebShop.Models.ViewModel;
using WebShop.Models.ViewModel.ReactUI;
using WebShop.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebShop.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WebShopAPIController : ControllerBase
    {
        private readonly UserManager<UserIdentity> _userManager;
        private readonly SignInManager<UserIdentity> _signInManager;
        private readonly IProductService _productService;
        private IOrderService _orderService;
        private const string NEWORDER_DEFAULT_ORDERSTATUS = "CREATED";
        private const bool NEWORDER_DEFAULT_PAYMENTSTATUS = false;
        private const string NEWORDER_DEFAULT_CUSTOMERID = "DEFAULT_CUSTOMER_ID";
        public WebShopAPIController(UserManager<UserIdentity> userManager, SignInManager<UserIdentity> signInManager, IProductService productService, IOrderService orderService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _productService = productService;
            _orderService = orderService;
        }

        /* URL: https://localhost:44392/api/WebShopAPI/Login/?usrName=WebShopAdmin&usrPassword=Qwerty@123456 */
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel formData)
        {
            LoginViewModel userLoginData = new LoginViewModel();
            userLoginData.UserName = formData.UserName;
            userLoginData.Password = formData.Password;

            var result = await _signInManager.PasswordSignInAsync(userLoginData.UserName, userLoginData.Password, false, false); //username, password, persistlogin, logout

            if (result.Succeeded)
            {
                UserIdentity user = await _userManager.FindByNameAsync(userLoginData.UserName);
                userLoginData.UserName = user.FirstName;
                userLoginData.Password = "";
                userLoginData.CustomerID = NEWORDER_DEFAULT_CUSTOMERID;
                return Ok(userLoginData);
            }
            else
            {
                userLoginData.UserName = "";
                userLoginData.Password = "";
                userLoginData.CustomerID = "";
                return BadRequest(userLoginData);
            }
        }

        [HttpGet]
        public List<Product> GetProducts()
        {
            ProductViewModel pdvm = new ProductViewModel();
            pdvm = _productService.All();
            return pdvm.ProductList;
        }

        [HttpGet]
        public List<OrderHeader> GetOrders(string customerID)
        {
            return _orderService.ReadAllCustOrderHeader(customerID);
        }

        [HttpGet]
        public List<OrderHeader> GetOrdersForAdmin()
        {
            OrderViewModel ovm = new OrderViewModel();
            ovm= _orderService.All();
            return ovm.OrderHeaderList;
        }

        [HttpGet]
        public List<OrderDetails> GetOrderDetails(int orderID)
        {
            /*List < OrderDetails > ordDetList= _orderService.ReadOrderDetails(orderID);
            List<Product> productsInOrder = new List<Product>();
            foreach (OrderDetails ordDet in ordDetList)
            {
                productsInOrder.Add(ordDet.Product);
            }

            return productsInOrder;*/
            return _orderService.ReadOrderDetails(orderID);
        }

        [HttpPost]
        public OrderHeader PlaceOrder(PlaceOrderViewData povd)
        {
            OrderViewModel odvm = new OrderViewModel();

            OrderHeader newOrdHdr = new OrderHeader();
            newOrdHdr.CreateDate = DateTime.Now;
            newOrdHdr.OrderStatus = NEWORDER_DEFAULT_ORDERSTATUS;
            newOrdHdr.PaymentStatus = NEWORDER_DEFAULT_PAYMENTSTATUS;
            newOrdHdr.CustomerID = NEWORDER_DEFAULT_CUSTOMERID;
            odvm.OrderHeader = newOrdHdr;

            List<OrderDetails> orderDetailsList = new List<OrderDetails>();
            foreach (Product product in povd.ProductInCart)
            {
                OrderDetails newOrdDet = new OrderDetails();
                newOrdDet.OrderHeader = newOrdHdr;
                newOrdDet.Product = product;
                newOrdDet.ProductQuantity = 1;
                orderDetailsList.Add(newOrdDet);
            }
            odvm.OrderDetailsList = orderDetailsList;

            odvm = _orderService.Add(odvm);

            return odvm.OrderHeader;
        }

        [HttpPost]
        public IActionResult AddProduct(Product newProduct)
        {
            ProductViewModel pdvm = new ProductViewModel();
            pdvm.ProductName = newProduct.ProductName;
            pdvm.Description = newProduct.Description;
            pdvm.Price = newProduct.Price;
            Product product = _productService.Add(pdvm);

            if (product != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public IActionResult EditProduct(Product newProduct)
        {
            Product product = _productService.Edit(newProduct.ProductID, newProduct);

            if (product != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult DeleteProduct(Product deleteProduct)
        {
            bool result = _productService.Remove(deleteProduct.ProductID);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult EditOrderHeader(OrderHeader newOrder)
        {
            OrderHeader orderHeader = _orderService.Edit(newOrder);

            if (orderHeader != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult EditOrderDetailLine(EditOrderLineViewData editOrderDetails)
        {
            int productID = editOrderDetails.ProductID;
            int orderHeaderID = editOrderDetails.OrderHeaderID;
            int productQty = editOrderDetails.ProductQuantity;
            int orderDetailsID = editOrderDetails.OrderDetailID;

            OrderDetails toEditOrderDetails = _orderService.ReadOrderDetailLine(orderDetailsID);
            OrderHeader orderHeader = _orderService.ReadOrderHeader(orderHeaderID);
            Product product = _productService.FindBy(productID);

            toEditOrderDetails.OrderHeader = orderHeader;
            toEditOrderDetails.Product = product;
            toEditOrderDetails.ProductQuantity = productQty;

            OrderDetails ordDet = _orderService.EditOrderDetailLine(toEditOrderDetails);

            if (ordDet != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult DeleteOrderHeader(int orderID)
        {
            bool result = _orderService.Remove(orderID);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult DeleteOrderDetailLine(int orderDetailID)
        {
            bool result = _orderService.RemoveOrderDetailLine(orderDetailID);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
