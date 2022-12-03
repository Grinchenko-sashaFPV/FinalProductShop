using ClientWPF.Core;
using ClientWPF.Repositories.Implementation;
using ClientWPF.Repositories.Interfaces;
using Microsoft.Win32;
using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ClientWPF.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        private readonly CategoriesRepository _categoriesRepository;
        private readonly ProducersRepository _producersRepository;
        private readonly ProductImagesRepository _productImagesRepository;
        private readonly ProductsRepository _productsRepository;

        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand ProductsViewCommand { get; set; }
        public RelayCommand AddProductViewCommand { get; set; }
        public RelayCommand CategoryViewCommand { get; set; }
        public RelayCommand ProducerViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public ProductsViewModel ProductsVM { get; set; }
        public AddProductViewModel ProductVM { get; set; }
        public CategoryViewModel CategoryVM { get; set; }
        public ProducerViewModel ProducerVM { get; set; }
        public AccountViewModel AccountVM { get; set; }
        #region
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
        #endregion
        public MainViewModel() {}
        public MainViewModel(User user)
        {
            CurrentUser = new User() { Name = user.Name, Id = user.Id, Password = user.Password, Role = user.Role, RoleId = user.RoleId };
            if (CurrentUser.Role.Name == "Admin")
                AdminOrUser = Visibility.Visible;
            else
                AdminOrUser = Visibility.Collapsed;
            // Repositories
            _categoriesRepository = new CategoriesRepository();
            _producersRepository = new ProducersRepository();
            _productImagesRepository = new ProductImagesRepository();
            _productsRepository = new ProductsRepository();
            // ViewModels
            HomeVM = new HomeViewModel();
            ProductsVM = new ProductsViewModel(_productsRepository, _producersRepository, _categoriesRepository, _productImagesRepository);
            ProductVM = new AddProductViewModel(_productImagesRepository, _categoriesRepository, _producersRepository, _productsRepository);
            CategoryVM = new CategoryViewModel(_categoriesRepository, _producersRepository, _productsRepository, _productImagesRepository);
            ProducerVM = new ProducerViewModel(_categoriesRepository, _producersRepository);

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o => { CurrentView = HomeVM; });
            ProductsViewCommand = new RelayCommand(o => { CurrentView = ProductsVM; });
            AddProductViewCommand = new RelayCommand(o => { CurrentView = ProductVM; });
            CategoryViewCommand = new RelayCommand(o => { CurrentView = CategoryVM; });
            ProducerViewCommand = new RelayCommand(o => { CurrentView = ProducerVM; });
        }
        public static readonly ICommand CloseCommand =
            new RelayCommand(o => ((Window)o).Close());
        
    }
}
