using ClientWPF.Repositories.Implementation.Manager;
using ClientWPF.Repositories.Interfaces;
using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWPF.Repositories.Implementation
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ModelsManager _dbManager;
        public UsersRepository()
        {
            _dbManager = new ModelsManager();
        }
        public async Task<int> AddUser(User newUser)
        {
            _dbManager.Users.Add(newUser);
            return await _dbManager.SaveChangesAsync();
        }

        public async Task<User> FindUserByName(string userName)
        {
            return await _dbManager.Users.Where(user => user.Name == userName).FirstOrDefaultAsync();
        }

        public void UpdateUser(User changedUser)
        {
            throw new NotImplementedException();
        }
    }
}
