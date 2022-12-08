using ClientWPF.Core;
using ClientWPF.MVVM.View;
using ClientWPF.Repositories.Implementation;
using ClientWPF.Repositories.Interfaces;
using HashGenerators;
using Microsoft.Win32;
using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace ClientWPF.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        private readonly UsersRepository _usersRepository;
        private readonly CategoriesRepository _categoriesRepository;
        private readonly ProducersRepository _producersRepository;
        private readonly ProductImagesRepository _productImagesRepository;
        private readonly ProductsRepository _productsRepository;
        private readonly UserImagesRepository _userImageRepository;
        private readonly BankAccountsRepository _bankAccountsRepository;

        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand ProductsViewCommand { get; set; }
        public RelayCommand AddProductViewCommand { get; set; }
        public RelayCommand CategoryViewCommand { get; set; }
        public RelayCommand ProducerViewCommand { get; set; }
        public RelayCommand ProfileViewCommand { get; set; }
        public RelayCommand UsersViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public ProductsViewModel ProductsVM { get; set; }
        public AddProductViewModel ProductVM { get; set; }
        public CategoryViewModel CategoryVM { get; set; }
        public ProducerViewModel ProducerVM { get; set; }
        public AccountViewModel AccountVM { get; set; }
        public ProfileViewModel ProfileVM { get; set; }
        public UsersViewModel UsersVM { get; set; }

        #region Accessors
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        private User _currentUser;
        public User CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }
        private Visibility _adminOrUser;
        public Visibility AdminOrUser
        {
            get { return _adminOrUser; }
            set
            {
                _adminOrUser = value;
                OnPropertyChanged(nameof(AdminOrUser));
            }
        }
        private object _imagePath;
        public object ImagePath
        {
            get { return _imagePath; }
            set
            {
                _imagePath = value;
                OnPropertyChanged(nameof(ImagePath));
            }
        }
        #endregion
        public MainViewModel() {}
        public MainViewModel(User user)
        {
            CurrentUser = new User() {
                Id = user.Id,
                Password = user.Password,
                Role = user.Role,
                Name = user.Name, 
                RoleId = user.RoleId,
                BankAccounts = user.BankAccounts, 
                RegistrationDate = user.RegistrationDate,
                UserImages = user.UserImages
               };

            if (CurrentUser.Role.Name == "Admin")
                AdminOrUser = Visibility.Visible;
            else
                AdminOrUser = Visibility.Collapsed;
            // Repositories
            _categoriesRepository = new CategoriesRepository();
            _producersRepository = new ProducersRepository();
            _productImagesRepository = new ProductImagesRepository();
            _productsRepository = new ProductsRepository();
            _usersRepository = new UsersRepository();
            _userImageRepository = new UserImagesRepository();
            _bankAccountsRepository = new BankAccountsRepository();

            // Set User profile image
            UserImage userImage = _userImageRepository.GetImageByUserId(user.Id);
            if (userImage != null)
                ImagePath = ConvertByteArrayToBitMapImage(userImage.Image);
            else
                ImagePath = @"../../Images/defUser.png";
            // ViewModels
            HomeVM = new HomeViewModel(_productsRepository, _producersRepository);
            ProductsVM = new ProductsViewModel(_productsRepository, _producersRepository, _categoriesRepository, _productImagesRepository, user);
            ProductVM = new AddProductViewModel(_productImagesRepository, _categoriesRepository, _producersRepository, _productsRepository);
            CategoryVM = new CategoryViewModel(_categoriesRepository, _producersRepository, _productsRepository, _productImagesRepository);
            ProducerVM = new ProducerViewModel(_categoriesRepository, _producersRepository);
            ProfileVM = new ProfileViewModel(_usersRepository, _userImageRepository, CurrentUser, ImagePath);
            UsersVM = new UsersViewModel(_usersRepository, _userImageRepository, _bankAccountsRepository);
            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o => { CurrentView = HomeVM; });
            ProductsViewCommand = new RelayCommand(o => { CurrentView = ProductsVM; });
            AddProductViewCommand = new RelayCommand(o => { CurrentView = ProductVM; });
            CategoryViewCommand = new RelayCommand(o => { CurrentView = CategoryVM; });
            ProducerViewCommand = new RelayCommand(o => { CurrentView = ProducerVM; });
            ProfileViewCommand = new RelayCommand(o => { CurrentView = ProfileVM; });
            UsersViewCommand = new RelayCommand(o => { CurrentView = UsersVM; });
        }
        #region Commands
        private readonly RelayCommand _closeCommand;
        public RelayCommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (new RelayCommand(obj =>
                {

                    App.Current.MainWindow.Close();
                    for (int i = 0; i < App.Current.Windows.Count; i++)
                        App.Current.Windows[i].Close();
                }));
            }
        }
        private readonly RelayCommand _signOut;
        public RelayCommand SignOut
        {
            get
            {
                return _signOut ?? (new RelayCommand(obj =>
                {
                    MessageBoxResult result = MessageBox.Show($"Are you sure you want to exit?", "Question", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        AuthorizationView authWindow = new AuthorizationView();
                        authWindow.Show();
                        App.Current.MainWindow.Close();
                    }
                }));
            }
        }
        #endregion
        private BitmapImage ConvertByteArrayToBitMapImage(byte[] imageByteArray)
        {
            BitmapImage img = new BitmapImage();
            using (MemoryStream memStream = new MemoryStream(imageByteArray))
            {
                img.BeginInit();
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.StreamSource = memStream;
                img.EndInit();
                img.Freeze();
            }
            return img;
        }
    }
}
