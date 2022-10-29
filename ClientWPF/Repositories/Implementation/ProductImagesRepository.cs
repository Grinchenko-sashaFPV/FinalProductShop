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
                        _dbManager.ProductImages.Add(new ProductImage() 
                        {
                            FileExtension = Path.GetExtension(pathes[i]),
                            Image = buff,
                            Size = buff.Length,
                            ProductId = productId
                        });
                    }
                }
                _dbManager.SaveChanges();
            }
        }

        public IEnumerable<ProductImage> GetImagesById(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
