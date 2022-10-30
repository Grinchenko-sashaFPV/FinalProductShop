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
        private readonly ModelsManager _dbManager;
        public ProductsRepository()
        {
            _dbManager = new ModelsManager();
        }
        public void AddNewProduct(Product product)
        {
            _dbManager.Products.Add(product);
            _dbManager.SaveChanges();
        }

        public void DeleteProductById(int productId)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllProducts()
        {
            return _dbManager.Products.ToList();
        }

        public Product GetProductById(int productId)
        {
            throw new NotImplementedException();
        }

        public Product GetProductByName(string productName)
        {
            return _dbManager.Products.Where(p => p.Name.Equals(productName)).FirstOrDefault();
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
