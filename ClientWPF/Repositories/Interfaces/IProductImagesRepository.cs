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
        void AddImage(string[] pathes);
        IEnumerable<ProductImage> GetImagesById(int productId);
    }
}
