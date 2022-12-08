using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWPF.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        Task<int> AddUser(User newUser);
        void UpdateUser(User changedUser);
        User FindUserByName(string userName);
        void DeleteUser(int userId);
        List<User> GetAllUsers();
        List<User> GetUsersByContaintsLetters(string letters);
    }
}
