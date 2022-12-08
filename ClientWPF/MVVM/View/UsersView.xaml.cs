﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for UsersView.xaml
    /// </summary>
    public partial class UsersView : UserControl
    {
        public UsersView()
        {
            InitializeComponent();
            var dp = DependencyPropertyDescriptor.FromProperty(
             TextBlock.TextProperty,
             typeof(TextBlock));
            dp.AddValueChanged(SelectedUserName, (sender, args) =>
            {
                if (String.IsNullOrEmpty(SelectedUserName.Text.Trim()))
                    DeleteUserButton.Visibility = Visibility.Hidden;
                else
                    DeleteUserButton.Visibility = Visibility.Visible;
            });
        }
    }
}
