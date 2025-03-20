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

namespace YP02.Pages.Characteristics
{
    /// <summary>
    /// Логика взаимодействия для Characteristics.xaml
    /// </summary>
    public partial class Characteristics : Page
    {
        public CharacteristicsContext characteristicsContext = new CharacteristicsContext();
        private Models.Users currentUser;

        public Characteristics()
        {
            InitializeComponent();

            currentUser = MainWindow.init.CurrentUser;
            if (currentUser != null && currentUser.Role == "Администратор")
            {
                addBtn.Visibility = Visibility.Visible;
            }

            parent.Children.Clear();
            foreach (Models.Characteristics item in characteristicsContext.Characteristics)
            {
                parent.Children.Add(new Item(item, this));
            }
        }

        private void SortUp(object sender, RoutedEventArgs e)
        {
            var sortUp = characteristicsContext.Characteristics.OrderBy(x => x.Name);
            parent.Children.Clear();

            foreach (var program in sortUp)
            {
                parent.Children.Add(new Item(program, this));
            }
        }

        private void SortDown(object sender, RoutedEventArgs e)
        {
            var sortDown = characteristicsContext.Characteristics.OrderByDescending(x => x.Name);
            parent.Children.Clear();

            foreach (var program in sortDown)
            {
                parent.Children.Add(new Item(program, this));
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Menu());
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Characteristics.Add(this, null));
        }

        private void KeyDown_Search(object sender, KeyEventArgs e)
        {
            string searchText = search.Text.ToLower();
            var result = characteristicsContext.Characteristics.Where(x =>
                x.Name.ToLower().Contains(searchText)
            );
            parent.Children.Clear();
            foreach (var item in result)
            {
                parent.Children.Add(new Item(item, this));
            }
        }
    }
}
