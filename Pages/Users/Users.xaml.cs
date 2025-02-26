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

namespace YP02.Pages.Users
{
    /// <summary>
    /// Логика взаимодействия для Users.xaml
    /// </summary>
    public partial class Users : Page
    {
        public UsersContext UsersContext = new();
        public Users()
        {
            InitializeComponent();
            parent.Children.Clear();
            foreach (Models.Users item in UsersContext.Users)
            {
                parent.Children.Add(new Item(item, this));
            }
        }

        private void KeyDown_Search(object sender, KeyEventArgs e)
        {
            string searchText = search.Text.ToLower();
            var result = UsersContext.Users.Where(x =>
                x.FIO.ToLower().Contains(searchText)
            );
            parent.Children.Clear();
            foreach (var item in result)
            {
                parent.Children.Add(new Item(item, this));
            }
        }

        private void SortUp(object sender, RoutedEventArgs e)
        {
            var sortUp = UsersContext.Users.OrderBy(x => x.FIO);
            parent.Children.Clear();
            foreach (var users in sortUp)
            {
                parent.Children.Add(new Item(users, this));
            }
        }

        private void SortDown(object sender, RoutedEventArgs e)
        {
            var sortDown = UsersContext.Users.OrderByDescending(x => x.FIO);
            parent.Children.Clear();
            foreach (var users in sortDown)
            {
                parent.Children.Add(new Item(users, this));
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Menu());
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Users.Add(this, null));
        }
    }
}
