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
    public class CategoryViewModel : INotifyPropertyChanged
    {
        private Category _category;
        public CategoryViewModel(Category category)
        {
            _category = category;
        }

        #region Accessors
        public int Id 
        {
            get { return _category.Id; }
            set { 
                _category.Id = value;
                OnPropertyChanged("Id");
            }
        }
        public string Name
        {
            get { return _category.Name; }
            set
            {
                _category.Name = value;
                OnPropertyChanged("Name");
            }
        }
        public double Popularity
        {
            get { return _category.Popularity; }
            set
            {
                _category.Popularity = value;
                OnPropertyChanged("Popularity");
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
