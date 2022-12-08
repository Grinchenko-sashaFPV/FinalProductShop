using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWPF.Repositories.Interfaces
{
    public interface IProductImagesRepository
    {
        void AddImages(string[] pathes, int productId);
        IEnumerable<ProductImage> GetImagesByProductId(int productId);
        void DeleteProductImagesByProductId(int productId);
    }
}
