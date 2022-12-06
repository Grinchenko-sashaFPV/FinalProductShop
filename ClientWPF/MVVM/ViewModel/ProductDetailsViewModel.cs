using ClientWPF.Core;
using ClientWPF.MVVM.View;
using ClientWPF.Repositories.Implementation;
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
    internal class ProductDetailsViewModel : ObservableObject
    {
        private readonly ProductImagesRepository _productImagesRepository;
        private readonly CategoriesRepository _categoriesRepository;
        private readonly ProducersRepository _producersRepository;
        private readonly ProductsRepository _productsRepository;

        private Product _product;
        private Category _selectedCategory;
        private Producer _selectedProducer;
        private string _starRatesImageSource;

        public List<double> RatesValues { get; set; }
        public ObservableCollection<Producer> Producers { get; set; }
        public ObservableCollection<Category> Categories { get; set; }
        public ProductDetailsViewModel() { }
        public ProductDetailsViewModel(Product product, int roleId)
        {
            _productImagesRepository = new ProductImagesRepository();
            _categoriesRepository = new CategoriesRepository();
            _producersRepository = new ProducersRepository();
            _productsRepository = new ProductsRepository();

            _selectedCategory = new Category();
            _selectedProducer = new Producer();

            _product = new Product()
            {
                Description = product.Description,
                Quantity = product.Quantity,
                CurrentRateSource = product.CurrentRateSource,
                CreationDate = product.CreationDate,
                Id = product.Id,
                ImageBytes = product.ImageBytes,
                Name = product.Name,
                Pathes = product.Pathes,
                Price = product.Price,
                ProductImage = product.ProductImage,
                Rate = product.Rate,
                ProducerId = product.ProducerId
            };

            _product.Pathes = new List<string>() { "/Images/Icons/defaultProductImage.png" }; // Default image for new product

            RatesValues = new List<double>() { 0, 0.5, 1, 1.5, 2, 2.5, 3, 3.5, 4, 4.5, 5 };

            Producers = new ObservableCollection<Producer>();
            Categories = new ObservableCollection<Category>();

            // Loading producers and categories for dropdown lists
            LoadProducers();
            LoadCategories();
            ProductDetailsView view = new ProductDetailsView();
            view.Show();
        }

        #region Selected objects
        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                LoadProducersByCategoryId(SelectedCategory.Id);
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
        #endregion

        #region Load data adapter
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
        private void LoadProducersByCategoryId(int categoryId)
        {
            if (categoryId != -2)
            {
                Producers.Clear();
                var producers = _producersRepository.GetAllProducersByCategoryId(categoryId);
                foreach (var producer in producers)
                    Producers.Add(producer);
                OnPropertyChanged("Producers");
            }
            else
                LoadProducers();
        }
        #endregion

        #region Accessors
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
                bool isNormal = (_product.Rate >= 0 && _product.Rate < 6);
                if (!isNormal)
                    MessageBox.Show("Invalid Rate!", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    _product.Rate = value;
                    switch (value)
                    {
                        case 0:
                            StarRatesImageSource = "/Images/StarRates/Star_rating_0_of_5.png";
                            break;
                        case 0.5:
                            StarRatesImageSource = "/Images/StarRates/Star_rating_0.5_of_5.png";
                            break;
                        case 1:
                            StarRatesImageSource = "/Images/StarRates/Star_rating_1_of_5.png";
                            break;
                        case 1.5:
                            StarRatesImageSource = "/Images/StarRates/Star_rating_1.5_of_5.png";
                            break;
                        case 2:
                            StarRatesImageSource = "/Images/StarRates/Star_rating_2_of_5.png";
                            break;
                        case 2.5:
                            StarRatesImageSource = "/Images/StarRates/Star_rating_2.5_of_5.png";
                            break;
                        case 3:
                            StarRatesImageSource = "/Images/StarRates/Star_rating_3_of_5.png";
                            break;
                        case 3.5:
                            StarRatesImageSource = "/Images/StarRates/Star_rating_3.5_of_5.png";
                            break;
                        case 4:
                            StarRatesImageSource = "/Images/StarRates/Star_rating_4_of_5.png";
                            break;
                        case 4.5:
                            StarRatesImageSource = "/Images/StarRates/Star_rating_4.5_of_5.png";
                            break;
                        case 5:
                            StarRatesImageSource = "/Images/StarRates/Star_rating_5_of_5.png";
                            break;
                    }
                    OnPropertyChanged("Rate");
                }
            }
        }
        public DateTime CreationDate
        {
            get { return DateTime.Now; }
            set
            {
                _product.CreationDate = DateTime.Now;
                OnPropertyChanged("CreationDate");
            }
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
        #endregion
    }
}
