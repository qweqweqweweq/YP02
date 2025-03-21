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

namespace YP02.Pages.RasxodMaterials
{
    /// <summary>
    /// Логика взаимодействия для RasxodMaterials.xaml
    /// </summary>
    public partial class RasxodMaterials : Page
    {
        public RasxodMaterialsContext rasxodMaterialsContext = new RasxodMaterialsContext();
        private Models.Users currentUser;

        public RasxodMaterials()
        {
            InitializeComponent();

            currentUser = MainWindow.init.CurrentUser;
            if (currentUser != null && currentUser.Role == "Администратор")
            {
                addBtn.Visibility = Visibility.Visible;
            }

            parent.Children.Clear();
            foreach (Models.RasxodMaterials item in rasxodMaterialsContext.RasxodMaterials)
            {
                parent.Children.Add(new Item(item, this));
            }
        }

        private void KeyDown_Search(object sender, KeyEventArgs e)
        {
            string searchText = search.Text.ToLower();
            var result = rasxodMaterialsContext.RasxodMaterials.Where(x =>
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
            var sortUp = rasxodMaterialsContext.RasxodMaterials.OrderBy(x => x.Name);
            parent.Children.Clear();

            foreach (var auditories in sortUp)
            {
                parent.Children.Add(new Item(auditories, this)); 
            }
        }

        private void SortDown(object sender, RoutedEventArgs e)
        {
            var sortDown = rasxodMaterialsContext.RasxodMaterials.OrderByDescending(x => x.Name);
            parent.Children.Clear(); 

            foreach (var auditories in sortDown)
            {
                parent.Children.Add(new Item(auditories, this)); 
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.RasxodMaterials.Add(this, null));
        }
    }
}
