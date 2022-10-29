using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClientWPF.MVVM.ViewModel
{
    public class ProducerViewModel : INotifyPropertyChanged
    {
        private Producer _producer;
        public ProducerViewModel(Producer producer)
        {
            _producer = new Producer();
        }

        #region Accessors
        public int Id
        {
            get { return _producer.Id; }
            set
            {
                _producer.Id = value;
                OnPropertyChanged("Id");
            }
        }
        public string Name
        {
            get { return _producer.Name; }
            set
            {
                _producer.Name = value;
                OnPropertyChanged("Name");
            }
        }
        public double Popularity
        {
            get { return _producer.Rate; }
            set
            {
                _producer.Rate = value;
                OnPropertyChanged("Rate");
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
