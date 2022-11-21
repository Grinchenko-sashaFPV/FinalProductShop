using ClientWPF.Core;
using ClientWPF.Repositories.Implementation;
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
        public RelayCommand AddCategoryViewCommand { get; set; }
        public RelayCommand AddProducerViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public ProductsViewModel ProductsVM { get; set; }
        public AddProductViewModel AddProductVM { get; set; }
        public AddCategoryViewModel AddCategoryVM { get; set; }
        public AddProducerViewModel AddProducerVM { get; set; }

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

        public MainViewModel()
        {
            // Repositories
            _categoriesRepository = new CategoriesRepository();
            _producersRepository = new ProducersRepository();
            _productImagesRepository = new ProductImagesRepository();
            _productsRepository = new ProductsRepository();
            // ViewModels
            HomeVM = new HomeViewModel();
            ProductsVM = new ProductsViewModel(_productsRepository, _producersRepository, _categoriesRepository, _productImagesRepository);
            AddProductVM = new AddProductViewModel(_productImagesRepository, _categoriesRepository, _producersRepository, _productsRepository);
            AddCategoryVM = new AddCategoryViewModel(_categoriesRepository);
            AddProducerVM = new AddProducerViewModel(_categoriesRepository, _producersRepository);

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o => { CurrentView = HomeVM; });
            ProductsViewCommand = new RelayCommand(o => { CurrentView = ProductsVM; });
            AddProductViewCommand = new RelayCommand(o => { CurrentView = AddProductVM; });
            AddCategoryViewCommand = new RelayCommand(o => { CurrentView = AddCategoryVM; });
            AddProducerViewCommand = new RelayCommand(o => { CurrentView = AddProducerVM; });
        }
        public static readonly ICommand CloseCommand =
            new RelayCommand(o => ((Window)o).Close());
        
    }
}
