using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.ViewModel.ReactUI
{
    public class EditOrderLineViewData
    {
        public int OrderDetailID { get; set; }
        
        public int OrderHeaderID { get; set; }

        public int ProductID { get; set; }

        public int ProductQuantity { get; set; }
    }
}
