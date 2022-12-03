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
        Task<User> FindUserByName(string userName);
    }
}
