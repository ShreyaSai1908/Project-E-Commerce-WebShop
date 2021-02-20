using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.ViewModel
{
    public class OrderViewModel
    {
        [Key]
        public int OrderDetailID { get; set; }
        public OrderHeader OrderHeader { get; set; }
        public OrderDetails OrderDetails { get; set; }
        public Product Product { get; set; }
        public List<OrderHeader> OrderHeaderList { get; set; }
        public List<OrderDetails> OrderDetailsList { get; set; }
        public List<Product> ProductList { get; set; }
        public int ProductQuantity { get; set; }
    }
}
