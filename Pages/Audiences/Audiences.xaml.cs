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

namespace YP02.Pages.Audiences
{
    /// <summary>
    /// Логика взаимодействия для Audiences.xaml
    /// </summary>
    public partial class Audiences : Page
    {
        public Audiences()
        {
            InitializeComponent();
        }       

        private void KeyDown_Search(object sender, KeyEventArgs e)
        {

        }

        private void SortUp(object sender, RoutedEventArgs e)
        {

        }

        private void SortDown(object sender, RoutedEventArgs e)
        {

        }

        private void Back(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Main());
        }        
    }
}
