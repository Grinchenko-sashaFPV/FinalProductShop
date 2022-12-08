using ClientWPF.Core;
using ClientWPF.MVVM.View;
using ClientWPF.Repositories.Implementation;
using ClientWPF.Repositories.Interfaces;
using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClientWPF.MVVM.ViewModel
{
    internal class UsersViewModel : ObservableObject
    {
        private readonly UsersRepository _usersRepository;
        private readonly UserImagesRepository _userImagesRepository;
        private readonly BankAccountsRepository _bankAccountsRepository;
        public ObservableCollection<User> Users { get; set; }
        public UsersViewModel(UsersRepository usersRepository, UserImagesRepository userImagesRepository, BankAccountsRepository bankAccountsRepository)
        {
            _usersRepository = usersRepository;
            _userImagesRepository = userImagesRepository;
            _bankAccountsRepository = bankAccountsRepository;
            
            Users = new ObservableCollection<User>();
            LoadUsers();
            if (Users.Count > 0)
                SelectedUser = Users[0];
        }

        #region Selected Objects
        private User _selectedUser;
        public User SelectedUser
        {
            get { return _selectedUser; }
            set 
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }
        private string _searchedPhrase;
        public string SearchedPhrase
        {
            get { return _searchedPhrase; }
            set
            {
                _searchedPhrase = value;
                if (String.IsNullOrWhiteSpace(SearchedPhrase))
                    LoadUsers();
                else
                {
                    Users.Clear();
                    var sortedUsers = _usersRepository.GetUsersByContaintsLetters(SearchedPhrase);
                    foreach (var user in sortedUsers)
                    {
                        user.ImageBytes = _userImagesRepository.GetImageByUserId(user.Id).Image;
                        user.MoneyAmount = _bankAccountsRepository.GetBankAccountByUserId(user.Id).MoneyAmount;
                        Users.Add(user);
                    }
                    if (Users.Count > 0)
                        SelectedUser = Users[0];
                }
            }
        }
        #endregion

        #region Load data adapter
        private void LoadUsers()
        {
            Users.Clear();
            var users = _usersRepository.GetAllUsers(); // Returns only users with roleId 2 (simple users), not admins
            foreach (var user in users)
            {
                user.ImageBytes = _userImagesRepository.GetImageByUserId(user.Id).Image;
                user.MoneyAmount = _bankAccountsRepository.GetBankAccountByUserId(user.Id).MoneyAmount;
                Users.Add(user);
            }
            if(Users.Count > 0)
                SelectedUser = Users[0];
        }
        #endregion

        #region Commands
        private readonly RelayCommand _deleteUser;
        public RelayCommand DeleteUser
        {
            get
            {
                return _deleteUser ?? (new RelayCommand(obj =>
                {
                    MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete user: {SelectedUser.Name}?", "Question", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                    if(result == MessageBoxResult.Yes)
                    {
                        _userImagesRepository.DeleteImageByUserId(SelectedUser.Id);
                        _usersRepository.DeleteUser(SelectedUser.Id);
                        MessageBox.Show("User was successfully deleted!");
                        LoadUsers();
                        if (Users.Count > 0)
                            SelectedUser = Users[0];
                    }
                }));
            }
        }
        #endregion
    }
}
