using ModelsLibrary.Models;
using ClientWPF.Repositories.Implementation.Manager;
using ClientWPF.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Data.Entity;

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
            _dbManager.Products.Remove(GetProductById(productId));
            _dbManager.SaveChanges();
        }

        public List<Product> GetAllProducts()
        {
            return _dbManager.Products.ToList();
        }

        public Product GetProductById(int productId)
        {
            return _dbManager.Products.Find(productId);
        }

        public Product GetProductByName(string productName)
        {
            return _dbManager.Products.Where(p => p.Name.Equals(productName)).FirstOrDefault();
        }
        public List<Product> GetProductsByCategoryId(int categoryId)
        {
            return _dbManager.Products.Where(p => p.CategoryId == categoryId).ToList();
        }

        public List<Product> GetProductsByPriceAsc(int producerId, int categoryId)
        {
            return _dbManager.Products.ThenBy(p => p.Price).Where(p => p.ProducerId == producerId && p.CategoryId == categoryId).ToList();
        }

        public List<Product> GetProductsByPriceDesc(int producerId, int categoryId)
        {
            return _dbManager.Products.ThenByDescending(p => p.Price).Where(p => p.ProducerId == producerId && p.CategoryId == categoryId).ToList();
        }
        public List<Product> GetProductsByProducerAndCategoryId(int producerId, int categoryId)
        {
            return _dbManager.Products.Where(p => p.ProducerId == producerId && p.CategoryId == categoryId).ToList();
        }

        public List<Product> GetProductsByProducerId(int producerId)
        {
            return _dbManager.Products.Where(p => p.ProducerId == producerId).ToList();
        }

        public void UpdateProduct(Product changedProduct)
        {
            var product = _dbManager.Products.Find(changedProduct.Id);
            product.Name = changedProduct.Name;
            product.Price = changedProduct.Price;
            product.Description = changedProduct.Description;
            product.Rate = changedProduct.Rate;
            product.CreationDate = changedProduct.CreationDate;
            product.Quantity = changedProduct.Quantity;
            product.ProducerId = changedProduct.ProducerId;
            product.CategoryId = changedProduct.CategoryId;
            product.ProductImage = changedProduct.ProductImage;
            _dbManager.Entry(product).State = EntityState.Modified;
            _dbManager.SaveChanges();
        }
    }
}
