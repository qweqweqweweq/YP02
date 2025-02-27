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
using YP02.Context;

namespace YP02.Pages.Status
{
    /// <summary>
    /// Логика взаимодействия для Status.xaml
    /// </summary>
    public partial class Status : Page
    {
        public StatusContext StatusContext = new();
        public Status()
        {
            InitializeComponent();
            parent.Children.Clear();
            foreach (Models.Status item in StatusContext.Status)
            {
                parent.Children.Add(new Item(item, this));
            }
        }

        private void KeyDown_Search(object sender, KeyEventArgs e)
        {
            string searchText = search.Text.ToLower();
            var result = StatusContext.Status.Where(x =>
                x.Name.ToLower().Contains(searchText)
            );
            parent.Children.Clear();
            foreach (var item in result)
            {
                parent.Children.Add(new Item(item, this));
            }
        }

        private void SortUp(object sender, RoutedEventArgs e)
        {
            var sortUp = StatusContext.Status.OrderBy(x => x.Name);
            parent.Children.Clear();
            foreach (var status in sortUp)
            {
                parent.Children.Add(new Item(status, this));
            }
        }

        private void SortDown(object sender, RoutedEventArgs e)
        {
            var sortDown = StatusContext.Status.OrderByDescending(x => x.Name);
            parent.Children.Clear();
            foreach (var status in sortDown)
            {
                parent.Children.Add(new Item(status, this));
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Menu());
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Status.Add(this, null));
        }
    }
}
