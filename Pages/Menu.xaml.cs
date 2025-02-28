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
using YP02.Models;
using YP02.Pages.RasxodMaterials;

namespace YP02.Pages
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Authorization());
        }

        private void OpenEquip(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Oborudovanie.Oborudovanie());
        }

        private void OpenEqType(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new OborType.OborType());
        }

        private void OpenAudien(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Auditories.Auditories());
        }

        private void OpenProgr(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Programs.Programs());
        }

        private void OpenDevel(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Developers.Developers());
        }

        private void OpenRMat(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new RasxodMaterials.RasxodMaterials());
        }

        private void OpenCharRMat(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Characteristics.Characteristics());
        }

        private void OpenModType(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new ViewModel.ViewModel());
        }

        private void OpenInvent(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Inventory.Inventory());
        }

        private void OpenSettings(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new NetworkSettings.NetworkSettings());
        }

        private void OpenUsers(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Users.Users());
        }

        private void OpenDirection(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Napravlenie.Napravlenie());
        }

        private void OpenStatus(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Status.Status());
        }

        private void OpenTypeChar(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new TypeCharacteristics.TypeCharacteristics());
        }
    }
}
