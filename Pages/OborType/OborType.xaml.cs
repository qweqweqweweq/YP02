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

namespace YP02.Pages.OborType
{
    /// <summary>
    /// Логика взаимодействия для OborType.xaml
    /// </summary>
    public partial class OborType : Page
    {
        public OborTypeContext OborTypeContext = new OborTypeContext();        
        public OborType()
        {
            InitializeComponent();            
            parent.Children.Clear();
            foreach(Models.OborType item in OborTypeContext.OborType)
            {
                parent.Children.Add(new Item(item, this));
            }
            parent.Children.Add(new Add_itm(new Add(this, null)));
        }

        private void KeyDown_Search(object sender, KeyEventArgs e)
        {
            string searchText = search.Text.ToLower();
            var result = OborTypeContext.OborType.Where(x =>
                x.Name.ToLower().Contains(searchText)
            );            
            parent.Children.Clear();
            foreach(var item in result)
            {
                parent.Children.Add(new Item(item, this));
            }
        }
                
        private void Back(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Menu());
        }
    }
}
