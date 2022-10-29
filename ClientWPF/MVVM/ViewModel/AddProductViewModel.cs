using ClientWPF.Core;
using ClientWPF.Repositories.Implementation;
using Microsoft.Win32;
using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ClientWPF.MVVM.ViewModel
{
    public class AddProductViewModel : INotifyPropertyChanged
    {
        private readonly ProductImagesRepository _productImagesRepository;
        private Product _product;
        private string _counter;
        public AddProductViewModel()
        {
            _productImagesRepository = new ProductImagesRepository();
            _product = new Product();
            _product.Description = "Default Description";
            _counter = "19jfdshg";
        }
        public string Counter
        {
            get { return _counter; }
            set 
            {
                _counter = value;
                OnPropertyChanged("Counter");
            }
        }
        public string[] Pathes
        {
            get { return _product.Pathes.ToArray(); }
            set 
            {
                _product.Pathes = value;
                OnPropertyChanged("Pathes");
            }
        }
        public string Description
        {
            get { return _product.Description; }
            set
            {
                _product.Description = value;
                OnPropertyChanged("Description");
            }
        }
        private readonly RelayCommand _openFileDialog;
        public RelayCommand OpenFileDialog
        {
            get
            {
                return _openFileDialog ?? (new RelayCommand(obj =>
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Multiselect = true;
                    ofd.Title = "Choose your product photos";
                    ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.apng;*.avif;*.gif;*.jfif;*.pjpeg";
                    ofd.ShowDialog();
                    _product.Pathes = ofd.FileNames;
                    //_productImagesRepository.AddImage(ofd.FileNames);
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
