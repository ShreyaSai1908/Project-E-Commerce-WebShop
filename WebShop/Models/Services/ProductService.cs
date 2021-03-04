using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Models.Repositorys;
using WebShop.Models.ViewModel;

namespace WebShop.Models.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;               

        public ProductService(IProductRepo productRepo)
        {
            _productRepo = productRepo;            
        }

        public Product Add(ProductViewModel productView)
        {
            Product newProduct = new Product();
            newProduct.ProductName = productView.ProductName;
            newProduct.Price = productView.Price;
            newProduct.Description = productView.Description;

            newProduct=_productRepo.Create(newProduct);
            return newProduct;
            
        }

        public ProductViewModel All()
        {
            ProductViewModel productViewModel = new ProductViewModel();
            productViewModel.ProductList = _productRepo.Read();
            return productViewModel;
        }

        public Product FindBy(int findID)
        {
             List<Product> allProducts = new List<Product>();
            allProducts = _productRepo.Read();

            Product searchProduct = new Product();

            foreach(Product product in allProducts)
            {
                if(product.ProductID== findID)
                {
                    searchProduct = product;
                }
            }

            return searchProduct;
        }

        public Product Edit(int id, Product product)
        {
            Product editProduct = FindBy(id);
            editProduct.ProductName = product.ProductName;
            editProduct.Price = product.Price;
            editProduct.Description = product.Description;
            _productRepo.Update(editProduct);

            return editProduct;
        }

        public bool Remove(int findID)
        {
            bool result = false;

            Product removeProduct = _productRepo.Read(findID);
            result = _productRepo.Delete(removeProduct);

            return result;
        }
    }
}
