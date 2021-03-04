using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Models.Repositorys;
using Microsoft.EntityFrameworkCore;

namespace WebShop.Models.Database
{
    public class DatabaseOrderRepo : IOrderRepo
    {
        private readonly WebDbContext _webDbContext;

        public DatabaseOrderRepo(WebDbContext webDbContext)
        {
            _webDbContext = webDbContext;
        }

        public OrderHeader Create(OrderHeader ordHdr, List<OrderDetails> ordDetList)
        {
            OrderHeader addingOrdHdr = ordHdr;
            _webDbContext.OrderHeader.Add(addingOrdHdr);

            foreach (OrderDetails addingOrdDet in ordDetList)
            {
                _webDbContext.Attach(addingOrdDet.Product);
                _webDbContext.OrderDetails.Add(addingOrdDet);
            }

            _webDbContext.SaveChanges();
            return addingOrdHdr;
        }
        public OrderHeader ReadOrderHeader(int id)
        {
            return _webDbContext.OrderHeader.SingleOrDefault(w => w.OrderID == id);
        }

        public List<OrderHeader> ReadAllOrders()
        {
            //return _webDbContext.OrderDetails.Include(x => x.OrderHeader).Include(y => y.Product).ToList();
            return _webDbContext.OrderHeader.ToList();
        }
        public List<OrderHeader> ReadAllCustOrders(string customerID)
        {
            return _webDbContext.OrderHeader.Where(w => w.CustomerID == customerID).ToList();
        }
        public List<OrderDetails> ReadOrderDetails(int id)
        {
            return _webDbContext.OrderDetails.Include(x => x.OrderHeader).Include(y => y.Product).Where(z => z.OrderHeader.OrderID == id).ToList();
        }

        public OrderDetails ReadOrderDetailLine(int id) 
        {
            return _webDbContext.OrderDetails.SingleOrDefault(w => w.OrderDetailID == id);
        }
        public bool Delete(OrderHeader order)
        {
            bool delete = true;

            if (delete == true)
            {
                _webDbContext.OrderHeader.Remove(order);
                _webDbContext.SaveChanges();
            }

            return delete;
        }

        public bool DeleteOrderDetailLine(OrderDetails orderDetail)
        {
            bool delete = true;

            if (delete == true)
            {
                _webDbContext.OrderDetails.Remove(orderDetail);
                _webDbContext.SaveChanges();
            }

            return delete;
        }

        public OrderHeader Update(OrderHeader ordHdr)
        {
            _webDbContext.Update(ordHdr);
            _webDbContext.SaveChanges();
            return (ordHdr);
        }
        public OrderDetails Update(OrderDetails ordDet)
        {
            _webDbContext.Update(ordDet);
            _webDbContext.SaveChanges();
            return (ordDet);
        }
    }
}

