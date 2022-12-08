using ClientWPF.Core;
using ClientWPF.Repositories.Implementation;
using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWPF.MVVM.ViewModel
{
    class HomeViewModel : ObservableObject
    {
        private readonly ProducersRepository _producersRepository;
        private readonly ProductsRepository _productsRepository;
        public HomeViewModel(ProductsRepository productsRepository, ProducersRepository producersRepository)
        {
            _producersRepository = producersRepository;
            _productsRepository = productsRepository;
            LoadProducersByRate();
            ProductCounter = _productsRepository.GetAllProducts().Count();
        }

        #region Accessors
        private int _productCounter;
        public int ProductCounter
        {
            get { return _productCounter; }
            set 
            {
                _productCounter = value;
                OnPropertyChanged(nameof(ProductCounter));
            }
        }
        private string _topProducers;
        public string TopProducers
        {
            get { return _topProducers; }
            set
            {
                _topProducers = value; 
                OnPropertyChanged(nameof(TopProducers));
            }
        }
        #endregion

        #region Load data adapter
        private void LoadProducersByRate()
        {
            TopProducers = String.Empty;
            var producers = _producersRepository.GetProducersByRateAsc();
            if(producers.Count > 5)
            {
                for (int i = 0; i < 5; i++)
                    TopProducers += $"{i + 1}. {producers[i].Name} \r\n";
            }
        }
        #endregion
    }
}
