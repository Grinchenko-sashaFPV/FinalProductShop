using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWPF.Repositories.Interfaces
{
    public interface IUserImagesRepository
    {
        void AddImageByUserId(string path, int userId);
        void DeleteImageByUserId(int userId);
        UserImage GetImageByUserId(int userId);
        void UpdateImageByUserId(int userId, UserImage image);
    }
}
