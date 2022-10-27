using ModelsLibrary.Models;
using System.Collections.Generic;

namespace ClientWPF.Repositories.Interfaces
{
    public interface IProductsRepository
    {
        List<Product> GetAllProducts();
        List<Product> GetProductsByProducerId(int producerId);
        List<Product> GetProductsByCategoryId(int categoryId);
        List<Product> GetProductsByPriceAsc();
        List<Product> GetProductsByPriceDesc();
        Product GetProductById(int productId);
        Product GetProductByName(string productName);
        void AddNewProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProductById(int productId);
    }
}
