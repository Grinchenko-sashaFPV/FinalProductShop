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
        private readonly BankAccountsRepository _bankAccountsRepository;

        private Product _product;
        private User _currentUser;
        private Category _selectedCategory;
        private Producer _selectedProducer;

        public List<double> RatesValues { get; set; }
        public ObservableCollection<Producer> Producers { get; set; }
        public ObservableCollection<Category> Categories { get; set; }
        public ProductDetailsViewModel() { }
        public ProductDetailsViewModel(Product product, User currentUser)
        {
            _productImagesRepository = new ProductImagesRepository();
            _categoriesRepository = new CategoriesRepository();
            _producersRepository = new ProducersRepository();
            _productsRepository = new ProductsRepository();
            _bankAccountsRepository = new BankAccountsRepository();

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
            _currentUser = currentUser;

            _product.Pathes = new List<string>() { "/Images/Icons/defaultProductImage.png" }; // Default image for new product

            Producers = new ObservableCollection<Producer>();
            Categories = new ObservableCollection<Category>();

            // Loading producers and categories for dropdown lists
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
                    _product.ProducerId = SelectedProducer.Id;
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
        #endregion

        #region Commands
        private readonly RelayCommand _saveChangesProductCommand;
        public RelayCommand SaveChangesProductCommand
        {
            get
            {
                return _saveChangesProductCommand ?? (new RelayCommand(obj =>
                {
                    MessageBoxResult result = MessageBox.Show($"Are you sure you want to save changes?", "Question", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        if (SelectedProducer != null)
                        {
                            _productsRepository.UpdateProduct(_product);
                            MessageBox.Show($"{_product.Name} was successfully updated!");
                        }
                        else
                            MessageBox.Show("Select producer!");
                    }
                }));
            }
        }
        private readonly RelayCommand _deleteProductCommand;
        public RelayCommand DeleteProductCommand
        {
            get
            {
                return _deleteProductCommand ?? (new RelayCommand(obj =>
                {
                    MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete current product?", "Question", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        _productImagesRepository.DeleteProductImagesByProductId(_product.Id);
                        _productsRepository.DeleteProductById(_product.Id);
                        MessageBox.Show("Product was successfully deleted!");
                        App.Current.Windows[1].Close();
                    }
                }));
            }
        }
        private readonly RelayCommand _buyProductCommand;
        public RelayCommand BuyProductCommand
        {
            get
            {
                return _buyProductCommand ?? (new RelayCommand(obj =>
                {
                    var account =_bankAccountsRepository.GetBankAccountByUserId(_currentUser.Id);
                    if (account.MoneyAmount > _product.Price)
                    {
                        account.MoneyAmount = account.MoneyAmount - _product.Price;
                        _bankAccountsRepository.UpdateBankAccount(account.Id, account);
                        MessageBox.Show($"{_product.Name} was ordered! Thank you. Your deposit: {account.MoneyAmount}$");
                    }
                    else
                        MessageBox.Show($"You can't afford it. Your deposit: {account.MoneyAmount}$, but product price: {_product.Price}$");
                }));
            }
        }
        #endregion
    }
}
