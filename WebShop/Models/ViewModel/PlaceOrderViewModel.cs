using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.ViewModel
{
    public class PlaceOrderViewModel
    {
        public List<ShoppingCart> ShoppingCart { get; set; }
        public Delivery Delivery { get; set; }

        public Double OrderTotal { get; set; }
    }
}
