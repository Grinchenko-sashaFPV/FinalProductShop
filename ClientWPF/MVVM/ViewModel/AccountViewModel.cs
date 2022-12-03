using ClientWPF.Core;
using ClientWPF.MVVM.View;
using ClientWPF.Repositories.Implementation;
using HashGenerators;
using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace ClientWPF.MVVM.ViewModel
{
    internal class AccountViewModel : INotifyPropertyChanged
    {
        private const string _secret = "Я люблю Грінченко"; 
        private int attempts;
        private readonly UsersRepository _usersRepository;
        public ObservableCollection<User> Users { get; set; }
        public AccountViewModel()
        {
            _usersRepository = new UsersRepository();
            IsAdmin = false;
            attempts = 3;
        }
        private string _name;
        public string Name 
        {
            get { return _name; }
            set 
            { 
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        private string _secretPhrase;
        public string SecretPhrase
        {
            get { return _secretPhrase; }
            set
            {
                _secretPhrase = value;
                OnPropertyChanged(nameof(SecretPhrase));
            }
        }
        private bool _isAdmin;
        public bool IsAdmin
        {
            get { return _isAdmin; }
            set
            {
                _isAdmin = value;
                SecretPhrase = String.Empty;
                OnPropertyChanged(nameof(IsAdmin));
            }
        }

        private readonly RelayCommand _signIn;
        public RelayCommand SignIn
        {
            get
            {
                // TODO !! Delete checkBox IsAdmin and move to registration window. It is not neccessary!
                return _signIn ?? (new RelayCommand(async obj =>
                {
                    User user = await _usersRepository.FindUserByName(Name);
                    string pass = MD5Generator.ProduceMD5Hash(Password);
                    if (user != null && attempts > 0)
                    {
                        
                        // Admin or user
                        if (IsAdmin && SecretPhrase.ToLower().Trim() == "я" && user.Password == pass && user.Name == Name
                        || !IsAdmin && user.Name == Name && user.Password == pass)
                        {
                            // Success
                            MainWindow window = new MainWindow(user);
                            window.Show();
                            App.Current.MainWindow = window;
                            App.Current.Windows[0].Close(); // Close Authorization window
                        }
                    }
                    else
                    {
                        if (attempts > 0)
                            MessageBox.Show($"No users found! Attemps left: {attempts--}");
                        else
                        {
                            MessageBox.Show("Try later.");
                            App.Current.MainWindow.Close();
                        }
                    }
                }));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
