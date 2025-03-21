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

namespace YP02.Pages.Oborudovanie
{
    /// <summary>
    /// Логика взаимодействия для Oborudovanie.xaml
    /// </summary>
    public partial class Oborudovanie : Page
    {
        public OborudovanieContext OborudovanieContext = new OborudovanieContext();
        private Models.Users currentUser;

        public Oborudovanie()
        {
            InitializeComponent();

            currentUser = MainWindow.init.CurrentUser;
            if (currentUser != null && currentUser.Role == "Администратор")
            {
                addBtn.Visibility = Visibility.Visible;
            }

            parent.Children.Clear();
            foreach (Models.Oborudovanie item in OborudovanieContext.Oborudovanie)
            {
                parent.Children.Add(new Item(item, this)); 
            }
        }

        private void KeyDown_Search(object sender, KeyEventArgs e)
        {
            string searchText = search.Text.ToLower();
            var result = OborudovanieContext.Oborudovanie.Where(x =>
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

        private void SortUp(object sender, RoutedEventArgs e)
        {
            var sortUp = OborudovanieContext.Oborudovanie.OrderBy(x => x.Name);
            parent.Children.Clear();
            foreach (var oborudovanie in sortUp)
            {
                parent.Children.Add(new Item(oborudovanie, this));
            }
        }

        private void SortDown(object sender, RoutedEventArgs e)
        {
            var sortDown = OborudovanieContext.Oborudovanie.OrderBy(x => x.Name);
            parent.Children.Clear();
            foreach (var oborudovanie in sortDown)
            {
                parent.Children.Add(new Item(oborudovanie, this));
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Oborudovanie.Add(this, null));
        }
    }
}
