using ClientWPF.Core;
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
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand ProductsViewCommand { get; set; }
        public RelayCommand AddProductViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public ProductsViewModel ProductsVM { get; set; }
        public AddProductViewModel AddProductVM { get; set; }

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
            HomeVM = new HomeViewModel();
            ProductsVM = new ProductsViewModel();
            AddProductVM = new AddProductViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o => { CurrentView = HomeVM; });
            ProductsViewCommand = new RelayCommand(o => { CurrentView = ProductsVM; });
            AddProductViewCommand = new RelayCommand(o => { CurrentView = AddProductVM; });
        }
        public static readonly ICommand CloseCommand =
            new RelayCommand(o => ((Window)o).Close());
        
    }
}
