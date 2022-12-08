using ModelsLibrary.Models;
using System.Collections.Generic;

namespace ClientWPF.Repositories.Interfaces
{
    public interface IProductsRepository
    {
        List<Product> GetAllProducts();
        List<Product> GetProductsByProducerId(int producerId);
        List<Product> GetProductsByCategoryId(int categoryId);
        List<Product> GetProductsByProducerAndCategoryId(int producerId, int categoryId);
        List<Product> GetProductsByPriceAsc(int producerId, int categoryId);
        List<Product> GetProductsByPriceDesc(int producerId, int categoryId);
        Product GetProductById(int productId);
        Product GetProductByName(string productName);
        void AddNewProduct(Product product);
        void UpdateProduct(Product changedProduct);
        void DeleteProductById(int productId);
        void DeleteProductsByCategoryId(int categoryId);
        void DeleteProductsByProducerId(int producerId);
    }
}
