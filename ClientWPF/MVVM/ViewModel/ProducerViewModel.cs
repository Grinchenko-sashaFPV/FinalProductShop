using ClientWPF.Core;
using ClientWPF.Repositories.Implementation;
using ClientWPF.Windows;
using FileLibrary;
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
    internal class ProducerViewModel : ObservableObject
    {
        private readonly CategoriesRepository _categoriesRepository;
        private readonly ProducersRepository _producersRepository;
        private readonly ProductsRepository _productsRepository;
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Producer> Producers { get; set; }
        public ProducerViewModel(CategoriesRepository categoriesRepository, ProducersRepository producersRepository)
        {
            _categoriesRepository = categoriesRepository;
            _producersRepository = producersRepository;
            _productsRepository = new ProductsRepository();

            Categories = new ObservableCollection<Category>();
            Producers = new ObservableCollection<Producer>();

            LoadCategories();
            LoadProducers();
        }

        #region Accessors
        private string _searchedPhrase;
        public string SearchedPhrase
        {
            get { return _searchedPhrase; }
            set
            {
                _searchedPhrase = value;
                if (String.IsNullOrWhiteSpace(_searchedPhrase) && Categories.Count == 0)
                    LoadProducers();
                else
                {
                    Producers.Clear();
                    var sortedProducers = _producersRepository.GetProducersByContaintsLetters(SearchedPhrase);
                    foreach (var producer in sortedProducers)
                        Producers.Add(producer);
                }
                OnPropertyChanged("SearchedPhrase");
            }
        }
        private string _newNameProducer;
        public string NewNameProducer
        {
            get { return _newNameProducer; }
            set
            {
                _newNameProducer = value;
                OnPropertyChanged("NewNameProducer");
            }
        }
        #endregion

        #region Selected objects
        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged("SelectedCategory");
            }
        }
        private Producer _selectedProducer;
        public Producer SelectedProducer
        {
            get { return _selectedProducer; }
            set
            {
                _selectedProducer = value;
                OnPropertyChanged("SelectedProducer");
            }
        }
        #endregion

        #region Load data adapter
        private void LoadCategories()
        {
            Categories.Clear();
            var categories = _categoriesRepository.GetAllCategories();
            foreach (var category in categories)
                Categories.Add(category);
        }
        private void LoadProducers()
        {
            Producers.Clear();
            var producers = _producersRepository.GetAllProducers();
            foreach (var producer in producers)
                Producers.Add(producer);
        }
        #endregion

        #region Commands
        private readonly RelayCommand _saveChangedProducer;
        public RelayCommand SaveChangedProducer
        {
            get
            {
                return _saveChangedProducer ?? (new RelayCommand(obj =>
                {
                    if (String.IsNullOrWhiteSpace(SelectedProducer.Name))
                        MessageBox.Show($"Name can not be null!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                    {
                        if (_producersRepository.UpdateProducer(_selectedProducer) == 1)
                            MessageBox.Show($"{SelectedProducer?.Name} was successfully saved!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }));
            }
        }
        private readonly RelayCommand _deleteSelectedProducer;
        public RelayCommand DeleteSelectedProducer
        {
            get
            {
                return _deleteSelectedProducer ?? (new RelayCommand(obj =>
                {
                    var result = MessageBox.Show($"Are you sure you want to delete {SelectedProducer.Name}?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes && SelectedProducer != null)
                    {
                        MessageBox.Show("!");
                        string name = SelectedProducer.Name;
                        var productsList = _productsRepository.GetProductsByProducerId(SelectedProducer.Id);
                        if (productsList?.Count > 0)
                        {
                            CustomMessageBox questionDialog = new CustomMessageBox($"{name} has ${productsList.Count} products! Delete them or make independent?",
                                "Delete product with childs", "Delete product without childs");
                            questionDialog.Focus();
                            questionDialog.ShowDialog();
                            if (questionDialog.option == 2)
                            {
                                if (productsList.Count > 0)
                                    _productsRepository.DeleteProductsByCategoryId(_selectedCategory.Id);
                                _producersRepository.DeleteProducerById(SelectedProducer.Id);
                                MessageBox.Show($"{name} was successfully deleted with all childs!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            if (questionDialog.option == 3)
                            {
                                // Save objects to binary file
                                if (productsList.Count > 0)
                                {
                                    // Serialize all
                                    BinaryFileManager.SaveObjectsToFile(@"../../Archive/products.dat", productsList);
                                    _productsRepository.DeleteProductsByProducerId(SelectedProducer.Id);
                                }
                                _producersRepository.DeleteProducerById(SelectedProducer.Id);
                                MessageBox.Show($"Only {name} was successfully deleted!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                        else
                        {
                            _producersRepository.DeleteProducerById(SelectedProducer.Id);
                            Producers.Remove(SelectedProducer);
                            if (Producers.Count > 0)
                                SelectedProducer = Producers[0];
                            MessageBox.Show($"{name} was successfully deleted!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }));
            }
        }
        private readonly RelayCommand _addProducer;
        public RelayCommand AddProducer
        {
            get
            {
                return _addProducer ?? (new RelayCommand(obj =>
                {
                    // TODO
                    if (!String.IsNullOrWhiteSpace(_newNameProducer) && SelectedCategory != null)
                    {
                        Producer newProducer = new Producer() { Name = NewNameProducer, CategoryId = SelectedCategory.Id };
                        try
                        {
                            _producersRepository.AddProducer(newProducer);
                            Producers.Add(newProducer);
                            MessageBox.Show($"{newProducer.Name} was successfully added!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                            NewNameProducer = String.Empty;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                        MessageBox.Show("Please input valid name!", "Attention", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }));
            }
        }
        private readonly RelayCommand _refreshCategories;
        public RelayCommand RefreshCategories
        {
            get
            {
                return _refreshCategories ?? (new RelayCommand(obj =>
                {
                    LoadCategories();
                }));
            }
        }
        #endregion
    }
}
