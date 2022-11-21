using ClientWPF.Core;
using ClientWPF.Repositories.Implementation;
using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
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
        private List<ProductImage> _images;
        private int _currentIndexPath = 0;
        private BitmapImage _currentImage;
        private ProductsRepository _productsRepository;
        private Product _changedProduct;
        public string RateImageSource { get; set; }
        public ProductDetailsWindow(Product product)
        {
            InitializeComponent();
            this.DataContext = product;

            // Initialize fields
            _images = new List<ProductImage>();
            _currentIndexPath = 0;
            _currentImage = new BitmapImage();
            _productsRepository = new ProductsRepository();
            _changedProduct = new Product()
            {
                Id = product.Id,
                Name = product.Name,
                CurrentRateSource = product.CurrentRateSource,
                CategoryId = product.CategoryId,
                CreationDate = product.CreationDate,
                Description = product.Description,
                ImageBytes = product.ImageBytes
            };

            //
            switch (product.Rate)
            {
                case 0:
                    RateImageSource = "/Images/StarRates/Star_rating_0_of_5.png";
                    break;
                case 0.5:
                    RateImageSource = "/Images/StarRates/Star_rating_0.5_of_5.png";
                    break;
                case 1:
                    RateImageSource = "/Images/StarRates/Star_rating_1_of_5.png";
                    break;
                case 1.5:
                    RateImageSource = "/Images/StarRates/Star_rating_1.5_of_5.png";
                    break;
                case 2:
                    RateImageSource = "/Images/StarRates/Star_rating_2_of_5.png";
                    break;
                case 2.5:
                    RateImageSource = "/Images/StarRates/Star_rating_2.5_of_5.png";
                    break;
                case 3:
                    RateImageSource = "/Images/StarRates/Star_rating_3_of_5.png";
                    break;
                case 3.5:
                    RateImageSource = "/Images/StarRates/Star_rating_3.5_of_5.png";
                    break;
                case 4:
                    RateImageSource = "/Images/StarRates/Star_rating_4_of_5.png";
                    break;
                case 4.5:
                    RateImageSource = "/Images/StarRates/Star_rating_4.5_of_5.png";
                    break;
                case 5:
                    RateImageSource = "/Images/StarRates/Star_rating_5_of_5.png";
                    break;
            }
            product.CurrentRateSource = RateImageSource;
            // Gallery logic
            if (product.ProductImage.Count() > 0)
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


        private void Rates_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem typeItem = (ComboBoxItem)Rates.SelectedItem;
            string value = typeItem.Content.ToString().Replace('.', ',');
            double rate = Convert.ToDouble(value);
            _changedProduct.Rate = rate;

            switch (rate)
            {
                case 0:
                    RateImageSource = "/Images/StarRates/Star_rating_0_of_5.png";
                    break;
                case 0.5:
                    RateImageSource = "/Images/StarRates/Star_rating_0.5_of_5.png";
                    break;
                case 1:
                    RateImageSource = "/Images/StarRates/Star_rating_1_of_5.png";
                    break;
                case 1.5:
                    RateImageSource = "/Images/StarRates/Star_rating_1.5_of_5.png";
                    break;
                case 2:
                    RateImageSource = "/Images/StarRates/Star_rating_2_of_5.png";
                    break;
                case 2.5:
                    RateImageSource = "/Images/StarRates/Star_rating_2.5_of_5.png";
                    break;
                case 3:
                    RateImageSource = "/Images/StarRates/Star_rating_3_of_5.png";
                    break;
                case 3.5:
                    RateImageSource = "/Images/StarRates/Star_rating_3.5_of_5.png";
                    break;
                case 4:
                    RateImageSource = "/Images/StarRates/Star_rating_4_of_5.png";
                    break;
                case 4.5:
                    RateImageSource = "/Images/StarRates/Star_rating_4.5_of_5.png";
                    break;
                case 5:
                    RateImageSource = "/Images/StarRates/Star_rating_5_of_5.png";
                    break;
            }
        }
    }
}
