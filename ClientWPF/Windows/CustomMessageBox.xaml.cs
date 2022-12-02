using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClientWPF.Windows
{
    /// <summary>
    /// Interaction logic for CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        public int option;
        public CustomMessageBox(string question, string answer1, string answer2)
        {
            InitializeComponent();
            option = -1;
            Question.Text = question;
            Button1.Content = answer1;
            Button2.Content = answer2;
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            this.option = 2;
            this.Close();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            this.option = 3;
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.option = 1;
            this.Close();
        }
    }
}
