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
    internal class AddProducerViewModel : INotifyPropertyChanged
    {
        private readonly CategoriesRepository _categoriesRepository;
        private readonly ProducersRepository _producersRepository;
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Producer> Producers { get; set; }
        private Category _selectedCategory;
        private Producer _selectedProducer;
        public AddProducerViewModel(CategoriesRepository categoriesRepository, ProducersRepository producersRepository)
        {
            _categoriesRepository = new CategoriesRepository();
            _producersRepository = new ProducersRepository();

            Categories = new ObservableCollection<Category>();
            Producers = new ObservableCollection<Producer>();
            _selectedCategory = new Category();
            _selectedProducer = new Producer();

            LoadCategories();
            LoadProducers();
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
            Producers.Add(new Producer() { Id = -2, Name = "Всі виробники", Rate = -1 });
            var producers = _producersRepository.GetAllProducers();
            foreach (var producer in producers)
                Producers.Add(producer);
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
        #endregion
        //
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
