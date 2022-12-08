using ClientWPF.Repositories.Implementation.Manager;
using ClientWPF.Repositories.Interfaces;
using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public void DeleteUser(int userId)
        {
            var user = _dbManager.Users.Find(userId);
            if(user != null)
                _dbManager.Users.Remove(user);
            _dbManager.SaveChanges();
        }

        public User FindUserByName(string userName)
        {
            return  _dbManager.Users.Where(user => user.Name == userName).FirstOrDefault();
        }

        public List<User> GetAllUsers()
        {
            return _dbManager.Users.Where(user => user.RoleId == 2).ToList();
        }

        public List<User> GetUsersByContaintsLetters(string letters)
        {
            return _dbManager.Users.Where(user => user.Name.Contains(letters)).ToList();
        }

        public void UpdateUser(User changedUser)
        {
            var user = _dbManager.Users.Find(changedUser.Id);
            if(user != null)
            {
                _dbManager.Users.AddOrUpdate(changedUser);
                _dbManager.SaveChanges();
            }
        }
    }
}
