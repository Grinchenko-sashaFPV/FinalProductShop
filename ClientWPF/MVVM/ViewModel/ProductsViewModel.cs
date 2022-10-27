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
        public ObservableCollection<Producer> Producers { get; set; }
        public ProductsViewModel()
        {
            _producersRepository = new ProducersRepository();
            Producers = new ObservableCollection<Producer>();
            LoadProducers();
        }
        private void LoadProducers()
        {
            Producers.Clear();
            var producers = _producersRepository.GetAllProducers().ToList();
            foreach (var producer in producers)
                Producers.Add(producer);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
