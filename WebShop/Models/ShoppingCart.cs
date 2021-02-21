using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class ShoppingCart
    {
        public Product Product { get; set; }
        public int ProductQty { get; set; }
    }
}
