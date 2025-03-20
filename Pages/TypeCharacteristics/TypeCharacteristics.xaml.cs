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

namespace YP02.Pages.TypeCharacteristics
{
    /// <summary>
    /// Логика взаимодействия для TypeCharacteristics.xaml
    /// </summary>
    public partial class TypeCharacteristics : Page
    {
        public TypeCharacteristicsContext typeCharacteristicsContext = new TypeCharacteristicsContext();
        private Models.Users currentUser;

        public TypeCharacteristics()
        {
            InitializeComponent();

            currentUser = MainWindow.init.CurrentUser;
            if (currentUser != null && currentUser.Role == "Администратор")
            {
                addBtn.Visibility = Visibility.Visible;
            }

            parent.Children.Clear();

            foreach (Models.TypeCharacteristics item in typeCharacteristicsContext.TypeCharacteristics)
            {
                parent.Children.Add(new Item(item, this)); 
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.TypeCharacteristics.Add(this, null));
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Menu());
        }

        private void SortUp(object sender, RoutedEventArgs e)
        {
            var sortUp = typeCharacteristicsContext.TypeCharacteristics.OrderBy(x => x.Name);
            parent.Children.Clear();

            foreach (var typecharacteristics in sortUp)
            {
                parent.Children.Add(new Item(typecharacteristics, this));
            }
        }

        private void SortDown(object sender, RoutedEventArgs e)
        {
            var sortDown = typeCharacteristicsContext.TypeCharacteristics.OrderByDescending(x => x.Name);
            parent.Children.Clear();

            foreach (var typecharacteristics in sortDown)
            {
                parent.Children.Add(new Item(typecharacteristics, this));
            }
        }

        private void KeyDown_Search(object sender, KeyEventArgs e)
        {
            string searchText = search.Text.ToLower();

            var result = typeCharacteristicsContext.TypeCharacteristics.Where(x =>
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
