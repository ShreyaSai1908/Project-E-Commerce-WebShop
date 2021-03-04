using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.ViewModel.ReactUI
{
    public class PlaceOrderViewData
    {
        public List<Product> ProductInCart { get; set; }
        public Delivery Delivery { get; set; }
    }
}
