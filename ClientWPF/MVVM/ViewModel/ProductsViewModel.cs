using ClientWPF.Repositories.Implementation;
using ClientWPF.Repositories.Interfaces;
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

namespace ClientWPF.MVVM.ViewModel
{
    internal class ProductsViewModel
    {
        private readonly ProductsRepository _productsRepository;
        private readonly ProducersRepository _producersRepository;
        private readonly CategoriesRepository _categoriesRepository;
        private readonly ProductImagesRepository _productImagesRepository;

        public ObservableCollection<Producer> Producers { get; set; }
        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<Category> Categories { get; set; }

        private Category _selectedCategory;
        private Producer _selectedProducer;
        public ProductsViewModel()
        {
            _productsRepository = new ProductsRepository();
            _producersRepository = new ProducersRepository();
            _categoriesRepository = new CategoriesRepository();
            _productImagesRepository = new ProductImagesRepository();

            _selectedProducer = new Producer();
            _selectedCategory = new Category();

            Producers = new ObservableCollection<Producer>();
            Categories = new ObservableCollection<Category>();
            Products = new ObservableCollection<Product>();

            LoadProducts();
            LoadProducers();
            LoadCategories();
        }
        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged("SelectedCategory");
            }
        }
        public Producer SelectedProducer
        {
            get => _selectedProducer;
            set
            {
                _selectedProducer = value;
                OnPropertyChanged("SelectedProducer");
            }
        }
        private void LoadProducers()
        {
            Producers.Clear();
            var producers = _producersRepository.GetAllProducers();
            foreach (var producer in producers)
                Producers.Add(producer);
        }
        private void LoadCategories()
        {
            Categories.Clear();
            var categories = _categoriesRepository.GetAllCategories();
            foreach (var category in categories)
                Categories.Add(category);
        }
        private void LoadProducts()
        {
            Products.Clear();
            var products = _productsRepository.GetAllProducts();
            foreach (var product in products)
            {
                Products.Add(product);
                //var images = LoadImagesForProduct(product.Id);
                //
            }
        }
        private List<ProductImage> LoadImagesForProduct(int productId)
        {
            return _productImagesRepository.GetImagesById(productId).ToList();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
