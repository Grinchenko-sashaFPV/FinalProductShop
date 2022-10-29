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
    internal class ProductsViewModel
    {
        private readonly ProducersRepository _producersRepository;
        private readonly CategoriesRepository _categoryRepository;
        public ObservableCollection<Producer> Producers { get; set; }
        public ObservableCollection<Category> Categories { get; set; }
        public ProductsViewModel()
        {
            _producersRepository = new ProducersRepository();
            _categoryRepository = new CategoriesRepository();

            Producers = new ObservableCollection<Producer>();
            Categories = new ObservableCollection<Category>();

            LoadProducers();
            LoadCategories();
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
            var categories = _categoryRepository.GetAllCategories();
            foreach (var category in categories)
                Categories.Add(category);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
