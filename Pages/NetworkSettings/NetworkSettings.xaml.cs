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

namespace YP02.Pages.NetworkSettings
{
    /// <summary>
    /// Логика взаимодействия для NetworkSettings.xaml
    /// </summary>
    public partial class NetworkSettings : Page
    {
        public NetworkSettingsContext NetworkSettingsContext = new NetworkSettingsContext();
        private Models.Users currentUser;

        public NetworkSettings()
        {
            InitializeComponent();

            currentUser = MainWindow.init.CurrentUser;
            if (currentUser != null && currentUser.Role == "Администратор")
            {
                addBtn.Visibility = Visibility.Visible;
            }

            parent.Children.Clear(); 

            foreach (Models.NetworkSettings item in NetworkSettingsContext.NetworkSettings)
            {
                parent.Children.Add(new Item(item, this));
            }
        }

        private void KeyDown_Search(object sender, KeyEventArgs e)
        {
            string searchText = search.Text.ToLower();

            var result = NetworkSettingsContext.NetworkSettings.Where(x =>
                x.IpAddress.ToLower().Contains(searchText)
            );

            parent.Children.Clear();

            foreach (var item in result)
            {
                parent.Children.Add(new Item(item, this));
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Menu());
        }

        private void SortUp(object sender, RoutedEventArgs e)
        {
            var sortUp = NetworkSettingsContext.NetworkSettings.OrderBy(x => x.IpAddress);
            parent.Children.Clear(); 

            foreach (var networkSettings in sortUp)
            {
                parent.Children.Add(new Item(networkSettings, this));
            }
        }

        private void SortDown(object sender, RoutedEventArgs e)
        {
            var sortDown = NetworkSettingsContext.NetworkSettings.OrderByDescending(x => x.IpAddress);
            parent.Children.Clear();

            foreach (var networkSettings in sortDown)
            {
                parent.Children.Add(new Item(networkSettings, this));
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.NetworkSettings.Add(this, null));
        }
    }
}
