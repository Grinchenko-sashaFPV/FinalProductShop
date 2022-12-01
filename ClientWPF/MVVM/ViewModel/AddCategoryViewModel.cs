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

namespace ClientWPF.MVVM.ViewModel
{
    internal class AddCategoryViewModel : INotifyPropertyChanged
    {
        private readonly CategoriesRepository _categoriesRepository;
        private readonly ProducersRepository _producerRepository;
        private ObservableCollection<Category> _categories;
        public ObservableCollection<Category> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                OnPropertyChanged("Categories");
            }
        }

        private Category _selectedCategory;

        public AddCategoryViewModel(CategoriesRepository categoriesRepository, ProducersRepository producersRepository)
        {
            _categories = new ObservableCollection<Category>();
            
            _categoriesRepository = categoriesRepository;
            _producerRepository = producersRepository;
            Categories = new ObservableCollection<Category>();
            _selectedCategory = new Category();
            //
            LoadCategories();
        }

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

        #region Selected objects
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
                        // TODO CHECK AND SOLVE CASCADE PROBLEM !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                        string name = SelectedCategory.Name;
                        _categoriesRepository.DeleteCategory(_selectedCategory.Id);
                        Categories.Remove(_selectedCategory);
                        MessageBox.Show($"{name} was successfully deleted!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
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
        //
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
