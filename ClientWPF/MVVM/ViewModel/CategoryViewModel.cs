using ClientWPF.Core;
using ClientWPF.Repositories.Implementation;
using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ClientWPF.Windows;
using FileLibrary;

namespace ClientWPF.MVVM.ViewModel
{
    internal class CategoryViewModel : ObservableObject
    {
        private readonly CategoriesRepository _categoriesRepository;
        private readonly ProducersRepository _producerRepository;
        private readonly ProductsRepository _productsRepository;
        private readonly ProductImagesRepository _productsImagesRepository;
        public ObservableCollection<Category> Categories { get; set; }
        public CategoryViewModel(CategoriesRepository categoriesRepository, ProducersRepository producersRepository,
            ProductsRepository productsRepository, ProductImagesRepository productImagesRepository)
        {
            _categoriesRepository = categoriesRepository;
            _producerRepository = producersRepository;
            _productsRepository = productsRepository;
            _productsImagesRepository = productImagesRepository;
            Categories = new ObservableCollection<Category>();
            _selectedCategory = new Category();
            //
            LoadCategories();
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
                    LoadCategories();
                else
                {
                    Categories.Clear();
                    var sortedCategories = _categoriesRepository.GetCategoriesByContaintsLetters(_searchedPhrase);
                    foreach (var category in sortedCategories)
                        Categories.Add(category);
                }
                OnPropertyChanged("SearchedCategoryText");
            }
        }
        private string _newNameCategory;
        public string NewNameCategory
        {
            get { return _newNameCategory; }
            set
            {
                _newNameCategory = value;
                OnPropertyChanged("NewNameCategory");
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
        #endregion

        #region Load data adapter
        private void LoadCategories()
        {
            Categories.Clear();
            var categories = _categoriesRepository.GetAllCategories();
            foreach (var category in categories)
                Categories.Add(category);
        }
        #endregion

        #region Commands
        private readonly RelayCommand _saveChangedCategory;
        public RelayCommand SaveChangedCategory
        {
            get
            {
                return _saveChangedCategory ?? (new RelayCommand(obj =>
                {
                    if(String.IsNullOrWhiteSpace(_selectedCategory.Name))
                        MessageBox.Show($"Name can not be null!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                    {
                        if(_categoriesRepository.UpdateCategory(_selectedCategory) == 1)
                            MessageBox.Show($"{_selectedCategory?.Name} was successfully saved!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }));
            }
        }
        private readonly RelayCommand _deleteSelectedCategory;
        public RelayCommand DeleteSelectedCategory
        {
            get
            {
                return _deleteSelectedCategory ?? (new RelayCommand(obj =>
                {
                    var result = MessageBox.Show($"Are you sure you want to delete {_selectedCategory.Name}?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes && _selectedCategory != null)
                    {
                        string name = SelectedCategory.Name;
                        var producerList = _producerRepository.GetAllProducersByCategoryId(SelectedCategory.Id);
                        var productsList = _productsRepository.GetProductsByCategoryId(SelectedCategory.Id);
                        if(producerList?.Count > 0)
                        {
                            CustomMessageBox questionDialog = new CustomMessageBox($"{name} has ${producerList.Count} producers and ${productsList.Count} products! Delete them or make independent?",
                                "Delete category with childs", "Delete category without childs");
                            questionDialog.Focus();
                            questionDialog.ShowDialog();
                            if (questionDialog.option == 2)
                            {
                                if(productsList.Count > 0)
                                    _productsRepository.DeleteProductsByCategoryId(_selectedCategory.Id);
                                if(producerList.Count > 0)
                                    _producerRepository.DeleteProducersByCategoryId(_selectedCategory.Id);
                                _categoriesRepository.DeleteCategory(_selectedCategory.Id);
                                Categories.Remove(_selectedCategory);
                                MessageBox.Show($"{name} was successfully deleted with all childs!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            if (questionDialog.option == 3)
                            {
                                // Save objects to binary file
                                if (producerList.Count > 0)
                                    BinaryFileManager.SaveObjectsToFile(@"../../Archive/producers.dat", producerList);
                                if(productsList.Count > 0)
                                {
                                    /*
                                    foreach (var product in productsList)
                                    {
                                        // TODO !!! CHECK Images, smth can be not working!
                                        // Get images of that products and add them to product
                                        var imagesOfProduct = _productsImagesRepository.GetImagesByProductId(product.Id);
                                        if(imagesOfProduct.ToList().Count > 0)
                                        {
                                            product.ProductImage = new List<ProductImage>();
                                            foreach (var image in imagesOfProduct)
                                                product.ProductImage.ToList().Add(image);
                                        }
                                    }*/
                                    // Serialize all
                                    BinaryFileManager.SaveObjectsToFile(@"../../Archive/products.dat", productsList);
                                }
                                // Then delete by cascade all models
                                if (productsList.Count > 0)
                                    _productsRepository.DeleteProductsByCategoryId(_selectedCategory.Id);
                                if (producerList.Count > 0)
                                    _producerRepository.DeleteProducersByCategoryId(_selectedCategory.Id);
                                _categoriesRepository.DeleteCategory(_selectedCategory.Id);
                                Categories.Remove(_selectedCategory);
                                MessageBox.Show($"Only {name} was successfully deleted!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                        else
                        {
                            _categoriesRepository.DeleteCategory(_selectedCategory.Id);
                            Categories.Remove(_selectedCategory);
                            MessageBox.Show($"{name} was successfully deleted!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }));
            }
        }
        private readonly RelayCommand _addCategory;
        public RelayCommand AddCategory
        {
            get
            {
                return _addCategory ?? (new RelayCommand(obj =>
                {
                    if(!String.IsNullOrWhiteSpace(_newNameCategory))
                    {
                        Category newCategory = new Category() { Name = _newNameCategory, Popularity = 0 };
                        try
                        {
                            _categoriesRepository.AddCategory(newCategory);
                            Categories.Add(newCategory);
                            MessageBox.Show($"{newCategory.Name} was successfully added!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                            NewNameCategory = String.Empty;
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                        MessageBox.Show("Please input valid name!", "Attention", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }));
            }
        }
        #endregion
    }
}
