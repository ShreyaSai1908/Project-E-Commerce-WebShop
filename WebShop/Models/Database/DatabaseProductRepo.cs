using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Models.Repositorys;

namespace WebShop.Models.Database
{
    public class DatabaseProductRepo : IProductRepo
    {
        private readonly WebDbContext _webDbContext;

        public DatabaseProductRepo(WebDbContext webDbContext)
        {
            _webDbContext = webDbContext;
        }

        public Product Create(Product product)
        {
            Product addingProduct = product;
            _webDbContext.GetProductList.Add(addingProduct);
            _webDbContext.SaveChanges();
            return addingProduct;
        }

        public bool Delete(Product product)
        {
            bool delete = true;

            if (delete == true)
            {
                _webDbContext.GetProductList.Remove(product);
                _webDbContext.SaveChanges();
            }

            return delete;
        }

        public List<Product> Read()
        {
            return _webDbContext.GetProductList.ToList();
        }

        public Product Read(int id)
        {
            return _webDbContext.GetProductList.SingleOrDefault(getProductList => getProductList.ProductID == id);
        }

        public Product Update(Product product)
        {
            _webDbContext.Update(product);
            _webDbContext.SaveChanges();
            return (product);
        }
    }
}
