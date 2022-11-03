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
        private string _starRatesImageSource;
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
        public string StarRatesImageSource
        {
            get => _starRatesImageSource;
            set
            {
                _starRatesImageSource = value;
                OnPropertyChanged("StarRatesImageSource");
            }
        }
        private void LoadProducts()
        {
            Products.Clear();
            var products = _productsRepository.GetAllProducts();
            foreach (var product in products)
            {
                Products.Add(product);
                var images = LoadImagesForProduct(product.Id);
                if (images.Count > 0)
                {
                    product.ProductImage = images;
                    // TODO
                    product.ImageBytes = product.ProductImage.ToList()[0].Image;
                }
                string rateImageSource = "";
                switch (product.Rate)
                {
                    case 0:
                        rateImageSource = "/Images/StarRates/Star_rating_0_of_5.png";
                        break;
                    case 0.5:
                        rateImageSource = "/Images/StarRates/Star_rating_0.5_of_5.png";
                        break;
                    case 1:
                        rateImageSource = "/Images/StarRates/Star_rating_1_of_5.png";
                        break;
                    case 1.5:
                        rateImageSource = "/Images/StarRates/Star_rating_1.5_of_5.png";
                        break;
                    case 2:
                        rateImageSource = "/Images/StarRates/Star_rating_2_of_5.png";
                        break;
                    case 2.5:
                        rateImageSource = "/Images/StarRates/Star_rating_2.5_of_5.png";
                        break;
                    case 3:
                        rateImageSource = "/Images/StarRates/Star_rating_3_of_5.png";
                        break;
                    case 3.5:
                        rateImageSource = "/Images/StarRates/Star_rating_3.5_of_5.png";
                        break;
                    case 4:
                        rateImageSource = "/Images/StarRates/Star_rating_4_of_5.png";
                        break;
                    case 4.5:
                        rateImageSource = "/Images/StarRates/Star_rating_4.5_of_5.png";
                        break;
                    case 5:
                        rateImageSource = "/Images/StarRates/Star_rating_5_of_5.png";
                        break;
                }
                product.CurrentRateSource = rateImageSource;   
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
