using ClientWPF.Core;
using ClientWPF.Repositories.Implementation;
using ClientWPF.Repositories.Interfaces;
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
        private readonly CategoriesRepository _categoriesRepository;
        private readonly ProducersRepository _producersRepository;
        private readonly ProductsRepository _productsRepository;

        private Product _product;
        public AddProductViewModel()
        {
            _productImagesRepository = new ProductImagesRepository();
            _categoriesRepository = new CategoriesRepository();
            _producersRepository = new ProducersRepository();
            _productsRepository = new ProductsRepository();

            _product = new Product();
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
        public string Name
        {
            get { return _product.Name; }
            set
            {
                _product.Name = value;
                OnPropertyChanged("Name");
            }
        }
        public double Price
        {
            get { return _product.Price; }
            set
            {
                _product.Price = value;
                OnPropertyChanged("Price");
            }
        }
        public int Quantity
        {
            get { return _product.Quantity; }
            set
            {
                _product.Quantity = value;
                OnPropertyChanged("Quantity");
            }
        }
        public double Rate
        {
            get { return _product.Rate; }
            set
            {
                _product.Rate = value;
                OnPropertyChanged("Rate");
            }
        }

        private readonly RelayCommand _addProduct;
        public RelayCommand AddProduct
        {
            get
            {
                return _addProduct ?? (new RelayCommand(obj =>
                {
                    _product.CreationDate = DateTime.Now;
                    _product.CategoryId = 10;                                   // TODO
                    _product.ProducerId = 2;                                   // TODO
                    _productsRepository.AddNewProduct(_product);
                    MessageBox.Show(_product.Id.ToString());
                    var dbProduct = _productsRepository.GetProductByName(_product.Name);
                    if(dbProduct != null)
                    {
                    _productImagesRepository.AddImages(Pathes, dbProduct.Id);
                        MessageBox.Show("All okey!");
                    }
                    else
                    {
                        MessageBox.Show("Smth went wrong...");
                    }
                    //_productImagesRepository.AddImage(ofd.FileNames);
                }));
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
