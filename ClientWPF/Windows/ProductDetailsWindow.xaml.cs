using ClientWPF.Core;
using ClientWPF.Repositories.Implementation;
using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace ClientWPF.Windows
{
    /// <summary>
    /// Interaction logic for ProductDetailsWindow.xaml
    /// </summary>
    public partial class ProductDetailsWindow : Window
    {
        private List<ProductImage> _images = new List<ProductImage>();
        private int _currentIndexPath = 0;
        private BitmapImage _currentImage = new BitmapImage();
        private ProductsRepository _productsRepository;
        private Product _changedProduct;
        public ProductDetailsWindow(Product product)
        {
            InitializeComponent();
            this.DataContext = product;

            // Initialize fields
            _images = new List<ProductImage>();
            _currentIndexPath = 0;
            _currentImage = new BitmapImage();
            _productsRepository = new ProductsRepository();
            _changedProduct = product;
            // Gallery logic
            if(product.ProductImage.Count() > 0)
            {
                _images.AddRange(product.ProductImage);
                imageProduct.ImageSource = ConvertByteArrayToBitMapImage(_images[0].Image);
            }
            else
            {
                // Hide arrows for image gallery and set default image to image source
                prevImage.Visibility = Visibility.Hidden;
                nextImage.Visibility = Visibility.Hidden;
                imageProduct.ImageSource = new BitmapImage(new Uri(@"../../Images/Icons/defaultProductImage.png", UriKind.Relative));
            }
        }

        private void PrevImage_Click(object sender, RoutedEventArgs e)
        {
            if (_currentIndexPath < _images.Count && _currentIndexPath > 0)
                imageProduct.ImageSource = ConvertByteArrayToBitMapImage(_images[--_currentIndexPath].Image);
            else
            {
                _currentIndexPath = _images.Count - 1;
                imageProduct.ImageSource = ConvertByteArrayToBitMapImage(_images[_currentIndexPath].Image);
            }
        }
        private void NextImage_Click(object sender, RoutedEventArgs e)
        {
            if (_currentIndexPath < _images.Count - 1)
                imageProduct.ImageSource = ConvertByteArrayToBitMapImage(_images[++_currentIndexPath].Image);
            else
            {
                _currentIndexPath = 0;
                imageProduct.ImageSource = ConvertByteArrayToBitMapImage(_images[_currentIndexPath].Image);
            }
        }
        private void saveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            _productsRepository.UpdateProduct(_changedProduct);
            MessageBox.Show($"{_changedProduct.Name} was successfully changed!");
            this.Close();
        }

        public BitmapImage ConvertByteArrayToBitMapImage(byte[] imageByteArray)
        {
            BitmapImage img = new BitmapImage();
            using (MemoryStream memStream = new MemoryStream(imageByteArray))
            {
                img.BeginInit();
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.StreamSource = memStream;
                img.EndInit();
                img.Freeze();
            }
            return img;
        }

    }
}
