﻿using System;
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
using YP02.Context;

namespace YP02.Pages.ViewModel
{
    /// <summary>
    /// Логика взаимодействия для ViewModel.xaml
    /// </summary>
    public partial class ViewModel : Page
    {
        public ViewModelContext ViewModelContext = new ViewModelContext();
        public ViewModel()
        {
            InitializeComponent();
            parent.Children.Clear();
            foreach (Models.ViewModel item in ViewModelContext.ViewModel)
            {
                parent.Children.Add(new Item(item, this));
            }
        }

        private void KeyDown_Search(object sender, KeyEventArgs e)
        {
            string searchText = search.Text.ToLower();
            var result = ViewModelContext.ViewModel.Where(x =>
                x.Name.ToLower().Contains(searchText)
            );
            parent.Children.Clear();
            foreach (var item in result)
            {
                parent.Children.Add(new Item(item, this));
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Menu());
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.ViewModel.Add(this, null));
        }
    }
}
