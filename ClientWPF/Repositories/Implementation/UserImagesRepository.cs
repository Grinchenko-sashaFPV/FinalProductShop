using ClientWPF.Repositories.Implementation.Manager;
using ClientWPF.Repositories.Interfaces;
using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Data.Entity.Migrations;

namespace ClientWPF.Repositories.Implementation
{
    
    public class UserImagesRepository : IUserImagesRepository
    {

        private readonly ModelsManager _dbManager;
        public UserImagesRepository()
        {
            _dbManager = new ModelsManager();
        }
        public void AddImageByUserId(string path, int userId)
        {
            if (path.Length > 0)
            {
                byte[] buff;
                if (File.Exists(path))
                {
                    buff = File.ReadAllBytes(path);
                    Image img = Image.FromFile(path);
                    Bitmap resizedImage = new Bitmap(img, new System.Drawing.Size(256, 256));
                    using (var stream = new MemoryStream())
                    {
                        resizedImage.Save(stream, ImageFormat.Jpeg);
                        byte[] bytes = stream.ToArray();
                        // User image

                        UserImage userImage = new UserImage();
                        userImage.FileExtension = Path.GetExtension(path);
                        userImage.Image = bytes;
                        userImage.Size = bytes.Length;
                        userImage.UserId = userId;
                        _dbManager.UserImages.Add(userImage);
                    }
                }
                _dbManager.SaveChanges();
            }
        }
        public void DeleteImageByUserId(int userId)
        {
            var img = _dbManager.UserImages.Where(i => i.UserId == userId).First();
            _dbManager.UserImages.Remove(img);
            _dbManager.SaveChanges();
        }

        public UserImage GetImageByUserId(int userId)
        {
            return _dbManager.UserImages.Where(img => img.UserId == userId).FirstOrDefault();
        }

        public void UpdateImageByUserId(int userId, UserImage image)
        {
            var findImage = _dbManager.UserImages.Where(img => img.UserId == userId).FirstOrDefault();
            if(findImage != null) 
            {
                _dbManager.UserImages.Remove(findImage);
                _dbManager.UserImages.Add(image);
                _dbManager.SaveChanges();
            }
        }
    }
    
}
