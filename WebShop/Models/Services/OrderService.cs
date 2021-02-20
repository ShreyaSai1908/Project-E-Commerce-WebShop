using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Models.ViewModel;
using WebShop.Models.Repositorys;

namespace WebShop.Models.Services
{
    public class OrderService : IOrderService
    {

        private readonly IOrderRepo _orderRepo;

        public OrderService(IOrderRepo orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public OrderViewModel Add(OrderViewModel odvm) 
        {
            OrderHeader newOrdHdr = new OrderHeader();
            newOrdHdr = odvm.OrderHeader;

            OrderDetails newOrdDet = new OrderDetails();
            newOrdDet = odvm.OrderDetails;

            _orderRepo.Create(newOrdHdr, newOrdDet);
            return odvm;
        }
        public OrderViewModel All()
        {
            OrderViewModel ovm = new OrderViewModel();
            ovm.OrderHeaderList = _orderRepo.ReadAllOrders();            
            return ovm;
        }

        public OrderHeader ReadOrderHeader(int id)
        {
            OrderHeader ordHdr = new OrderHeader();
            ordHdr = _orderRepo.ReadOrderHeader(id);
            return ordHdr;
        }

        public List<OrderDetails> ReadOrderDetails(int id)
        {
            List<OrderDetails> orderDetailsList = new List<OrderDetails>();
            orderDetailsList = _orderRepo.ReadOrderDetails(id);
            return orderDetailsList;
        }

        public OrderDetails ReadOrderDetailLine(int id)
        {
            OrderDetails orderDetailLine = new OrderDetails();
            orderDetailLine = _orderRepo.ReadOrderDetailLine(id);
            return orderDetailLine;
        }

        public bool Remove(int findID)
        {
            bool result = false;
            OrderHeader removeOrder = _orderRepo.ReadOrderHeader(findID);
            result = _orderRepo.Delete(removeOrder);
            return result;
        }

        public bool RemoveOrderDetailLine(int findID) 
        {
            bool result = false;
            OrderDetails removeOrderDetailLine = _orderRepo.ReadOrderDetailLine(findID);
            result = _orderRepo.DeleteOrderDetailLine(removeOrderDetailLine);
            return result;
        }

        public OrderHeader Edit(OrderHeader ordHdr)
        {            
            OrderHeader editOrdHdr=_orderRepo.Update(ordHdr);
            return editOrdHdr;
        }

        public OrderDetails EditOrderDetailLine(OrderDetails ordDet) 
        {
            OrderDetails editOrdDet = _orderRepo.Update(ordDet);
            return editOrdDet;
        }
    }
}
