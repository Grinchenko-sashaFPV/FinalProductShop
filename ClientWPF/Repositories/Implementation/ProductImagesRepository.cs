using ClientWPF.Repositories.Implementation.Manager;
using ClientWPF.Repositories.Interfaces;
using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace ClientWPF.Repositories.Implementation
{
    public class ProductImagesRepository : IProductImagesRepository
    {
        private readonly ModelsManager _dbManager;
        public ProductImagesRepository()
        {
            _dbManager = new ModelsManager();
        }

        public void AddImages(string[] pathes, int productId)
        {
            if(pathes.Length > 0)
            { 
                byte[] buff;
                for (int i = 0; i < pathes.Length; i++)
                {
                    if(File.Exists(pathes[i]))
                    {
                        buff = File.ReadAllBytes(pathes[i]);
                        Image img = Image.FromFile(pathes[i]);
                        Bitmap resizedImage = new Bitmap(img, new System.Drawing.Size(256, 256));
                        using (var stream = new MemoryStream())
                        {
                            resizedImage.Save(stream, ImageFormat.Jpeg);
                            byte[] bytes = stream.ToArray();

                            _dbManager.ProductImages.Add(new ProductImage()
                            {
                                FileExtension = Path.GetExtension(pathes[i]),
                                Image = bytes,
                                Size = bytes.Length,
                                ProductId = productId
                            });
                        }
                    }
                }
                _dbManager.SaveChanges();
            }
        }

        public void DeleteProductImagesByProductId(int productId)
        {
            var productImages = _dbManager.ProductImages.Where(img => img.ProductId == productId);
            foreach (var img in productImages)
                _dbManager.ProductImages.Remove(img);
            _dbManager.SaveChanges();
        }

        public IEnumerable<ProductImage> GetImagesByProductId(int productId)
        {
            return _dbManager.ProductImages.Where(p => p.ProductId == productId);
        }
    }
}
