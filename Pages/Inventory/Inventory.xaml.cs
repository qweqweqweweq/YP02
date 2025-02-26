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

namespace YP02.Pages.Inventory
{
    /// <summary>
    /// Логика взаимодействия для Inventory.xaml
    /// </summary>
    public partial class Inventory : Page
    {
        public InventoryContext InventoryContext = new InventoryContext();

        public Inventory()
        {
            InitializeComponent();
            parent.Children.Clear();
            foreach (Models.Inventory item in InventoryContext.Inventory)
            {
                parent.Children.Add(new Item(item, this));
            }
        }

        private void KeyDown_Search(object sender, KeyEventArgs e)
        {
            string searchText = search.Text.ToLower();
            var result = InventoryContext.Inventory.Where(x =>
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

        }

        private void SortDown(object sender, RoutedEventArgs e)
        {

        }

        private void Back(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Menu());
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Inventory.Add(this, null));
        }
    }
}
