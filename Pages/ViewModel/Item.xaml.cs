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
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        ViewModel MainViewModel;
        Models.ViewModel ViewModel;
        OborTypeContext oborTypeContext = new OborTypeContext();
        public Item(Models.ViewModel ViewModel, ViewModel MainViewModel)
        {
            InitializeComponent();
            this.ViewModel = ViewModel;
            this.MainViewModel = MainViewModel;
            lb_Name.Content = ViewModel.Name;
            lb_OborType.Content = oborTypeContext.OborType.Where(x => x.Id == ViewModel.Id).First().Name;
        }

        private void Click_redact(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.ViewModel.Add(MainViewModel, ViewModel));
        }

        private void Click_remove(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("При удалении все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MainViewModel.ViewModelContext.ViewModel.Remove(ViewModel);
                MainViewModel.ViewModelContext.SaveChanges();
                (this.Parent as Panel).Children.Remove(this);
            }
            else MessageBox.Show("Действие отменено.");
        }
    }
}
