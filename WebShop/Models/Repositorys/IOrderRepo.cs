using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.Repositorys
{
    public interface IOrderRepo
    {
        public OrderHeader Create(OrderHeader ordHdr, List<OrderDetails> ordDetList);
        public OrderHeader ReadOrderHeader(int id);
        public List<OrderHeader> ReadAllOrders();
        public List<OrderDetails> ReadOrderDetails(int id);
        public OrderDetails ReadOrderDetailLine(int id);
        public OrderHeader Update(OrderHeader ordHdr);
        public OrderDetails Update(OrderDetails ordDet);
        public bool Delete(OrderHeader order);
        public bool DeleteOrderDetailLine(OrderDetails orderDetail);
    }
}
