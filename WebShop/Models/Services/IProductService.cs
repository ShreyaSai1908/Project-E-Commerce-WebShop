using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Models.ViewModel;

namespace WebShop.Models.Services
{
    public interface IProductService
    {
        public Product Add(ProductViewModel productView);
        public Product FindBy(int findID);
        public ProductViewModel All();
        public Product Edit(int id, Product product);
        public bool Remove(int id);

    }
}
