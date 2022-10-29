using ClientWPF.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientWPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for AddProductView.xaml
    /// </summary>
    public partial class AddProductView : UserControl
    {
        public AddProductView()
        {
            InitializeComponent();
            //this.DataContext = new AddProductViewModel();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            producersList.SelectedIndex = 0;
            categoriesList.SelectedIndex = 0;
        }
    }
}
