using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Models.ViewModel;

namespace WebShop.Models.Services
{
    public interface IOrderService
    {
        public OrderViewModel Add(OrderViewModel odvm);
        public OrderViewModel All();
        public OrderHeader ReadOrderHeader(int id);
        public List<OrderDetails> ReadOrderDetails(int id);
        public OrderDetails ReadOrderDetailLine(int id);
        public OrderHeader Edit(OrderHeader ordHdr);
        public OrderDetails EditOrderDetailLine(OrderDetails ordDet);
        public bool Remove(int id);
        public bool RemoveOrderDetailLine(int findID);
    }
}
