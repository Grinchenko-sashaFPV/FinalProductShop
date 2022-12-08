using ClientWPF.Core;
using ClientWPF.MVVM.View;
using ClientWPF.Repositories.Implementation;
using ClientWPF.Repositories.Interfaces;
using ClientWPF.Windows;
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
    internal class ProductsViewModel : ObservableObject
    {
        private User _currentUser;
        private readonly ProductsRepository _productsRepository;
        private readonly ProducersRepository _producersRepository;
        private readonly CategoriesRepository _categoriesRepository;
        private readonly ProductImagesRepository _productImagesRepository;

        public ObservableCollection<Producer> Producers { get; set; }
        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<Category> Categories { get; set; }

        private Category _selectedCategory;
        private Producer _selectedProducer;
        private Product _selectedProduct;
        private string _starRatesImageSource;
        public ProductsViewModel(ProductsRepository productsRepository, ProducersRepository producersRepository,
            CategoriesRepository categoriesRepository, ProductImagesRepository productImagesRepository, User currentUser)
        {
            _productsRepository = productsRepository;
            _producersRepository = producersRepository;
            _categoriesRepository = categoriesRepository;
            _productImagesRepository = productImagesRepository;

            _selectedProducer = new Producer();
            _selectedCategory = new Category();
            _selectedProduct = new Product();
            _currentUser = currentUser;

            Producers = new ObservableCollection<Producer>();
            Categories = new ObservableCollection<Category>();
            Products = new ObservableCollection<Product>();

            LoadProducers();
            LoadCategories();
        }

        #region Selected objects
        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                LoadProducersByCategoryId(SelectedCategory.Id);
                if(SelectedProducer != null)
                    LoadProductsByProducerAndCategory(SelectedProducer.Id, SelectedCategory.Id);
                OnPropertyChanged("SelectedCategory");
            }
        }
        public Producer SelectedProducer
        {
            get => _selectedProducer;
            set
            {
                _selectedProducer = value;
                if(SelectedProducer != null)
                    LoadProductsByProducerAndCategory(SelectedProducer.Id, SelectedCategory.Id);
                OnPropertyChanged("SelectedProducer");
            }
        }
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                var buff_pathes = LoadImagesForProduct(_selectedProduct.Id);
                SelectedProduct.ProductImage = buff_pathes.ToList();
                //
                //ProductDetailsViewModel viewModel = new ProductDetailsViewModel(_selectedProduct);
                if(SelectedProduct != null)
                {
                    ProductDetailsView productDetailsView = new ProductDetailsView(SelectedProduct, _currentUser);
                    productDetailsView.ShowDialog();
                }
                OnPropertyChanged("SelectedProduct");
            }
        }
        #endregion

        #region Load data adapter
        private void LoadProducers()
        {
            Producers.Clear();
            Producers.Add(new Producer() { Id = -2, Name = "Всі виробники", Rate = -1 });
            var producers = _producersRepository.GetAllProducers();
            foreach (var producer in producers)
                Producers.Add(producer);
        }
        private void LoadCategories()
        {
            Categories.Clear();
            Categories.Add(new Category() { Id = -2, Name = "Всі категорії", Popularity = -1 });
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
        private void LoadProductsByProducerAndCategory(int producerId, int categoryId)
        {
            Products.Clear();
            if(producerId != -2 && categoryId != -2)
            {
                var products = _productsRepository.GetProductsByProducerAndCategoryId(producerId, categoryId);
                foreach (var product in products)
                {
                    Products.Add(product);
                    var images = LoadImagesForProduct(product.Id);
                    if (images.Count > 0)
                    {
                        product.ProductImage = images;
                        // TODO
                        product.ImageBytes = product.ProductImage.ToList()[0].Image;
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
                OnPropertyChanged("Products");
            }
            else if (producerId == -2 && categoryId != -2)
            {
                var products = _productsRepository.GetProductsByCategoryId(categoryId);
                foreach (var product in products)
                {
                    Products.Add(product);
                    var images = LoadImagesForProduct(product.Id);
                    if (images.Count > 0)
                    {
                        product.ProductImage = images;
                        // TODO
                        product.ImageBytes = product.ProductImage.ToList()[0].Image;
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
                OnPropertyChanged("Products");
            }
            else if(producerId != -2 && categoryId == -2)
            {
                var products = _productsRepository.GetProductsByProducerId(producerId);
                foreach (var product in products)
                {
                    Products.Add(product);
                    var images = LoadImagesForProduct(product.Id);
                    if (images.Count > 0)
                    {
                        product.ProductImage = images;
                        // TODO
                        product.ImageBytes = product.ProductImage.ToList()[0].Image;
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
                OnPropertyChanged("Products");
            }
            else
                LoadProducts();
        }
        private void LoadProducersByCategoryId(int categoryId)
        {
            if (categoryId != -2)
            {
                Producers.Clear();
                Producers.Add(new Producer() { Id = -2, Name = "Всі виробники", Rate = -1 });
                var producers = _producersRepository.GetAllProducersByCategoryId(categoryId);
                foreach (var producer in producers)
                    Producers.Add(producer);
                OnPropertyChanged("Producers");
            }
            else
                LoadProducers();
        }
        private List<ProductImage> LoadImagesForProduct(int productId)
        {
            return _productImagesRepository.GetImagesByProductId(productId).ToList();
        }
        #endregion

        #region Star image source adapter & load images func
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

        #region Commands
        private readonly RelayCommand _refreshCategories;
        public RelayCommand RefreshCategories
        {
            get
            {
                return _refreshCategories ?? (new RelayCommand(obj =>
                {
                    LoadCategories();
                    if(Categories.Count > 0)
                        SelectedCategory = Categories[0];
                }));
            }
        }
        private readonly RelayCommand _refreshProducers;
        public RelayCommand RefreshProducers
        {
            get
            {
                return _refreshProducers ?? (new RelayCommand(obj =>
                {
                    if(SelectedCategory != null)
                        LoadProducersByCategoryId(_selectedCategory.Id);
                    else 
                        LoadProducers();
                    //
                    if (Producers.Count > 0)
                        SelectedProducer = Producers[0];
                }));
            }
        }
        #endregion
    }
}
