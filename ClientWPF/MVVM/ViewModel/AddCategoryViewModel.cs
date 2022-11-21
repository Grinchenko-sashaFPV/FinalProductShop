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

namespace ClientWPF.MVVM.ViewModel
{
    internal class AddCategoryViewModel : INotifyPropertyChanged
    {
        private readonly CategoriesRepository _categoriesRepository;

        public ObservableCollection<Category> Categories { get; set; }

        private Category _selectedCategory;

        public AddCategoryViewModel(CategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;

            Categories = new ObservableCollection<Category>();
            _selectedCategory = new Category();
            //
            LoadCategories();
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
        //
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
