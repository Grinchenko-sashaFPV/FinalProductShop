using ModelsLibrary.Models;
using ClientWPF.Repositories.Implementation.Manager;
using ClientWPF.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ClientWPF.Repositories.Implementation
{
    public class ProductsRepository : IProductsRepository
    {
        public void AddNewProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public void DeleteProductById(int productId)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public Product GetProductById(int productId)
        {
            throw new NotImplementedException();
        }

        public Product GetProductByName(string productName)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProductsByCategoryId(int categoryId)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProductsByPriceAsc()
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProductsByPriceDesc()
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProductsByProducerId(int producerId)
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
