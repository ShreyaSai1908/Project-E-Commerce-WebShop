using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.Repositorys
{
    public interface IProductRepo
    {
        public Product Create(Product product);
        public List<Product> Read();
        public Product Read(int id);
        public Product Update(Product product);
        public bool Delete(Product product);
    }
}
