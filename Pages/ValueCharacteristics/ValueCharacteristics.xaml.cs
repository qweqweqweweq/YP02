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

namespace YP02.Pages.ValueCharacteristics
{
    /// <summary>
    /// Логика взаимодействия для ValueCharacteristics.xaml
    /// </summary>
    public partial class ValueCharacteristics : Page
    {
        public ValueCharacteristicsContext ValueCharacteristicsContext = new ValueCharacteristicsContext();
        private Models.Users currentUser;

        public ValueCharacteristics()
        {
            InitializeComponent();

            currentUser = MainWindow.init.CurrentUser;
            if (currentUser != null && currentUser.Role == "Администратор")
            {
                addBtn.Visibility = Visibility.Visible;
            }

            parent.Children.Clear(); // Очистка родительского контейнера перед добавлением новых элементов

            foreach (Models.ValueCharacteristics item in ValueCharacteristicsContext.ValueCharacteristics)
            {
                parent.Children.Add(new Item(item, this));
            }
        }

        private void KeyDown_Search(object sender, KeyEventArgs e)
        {
            string searchText = search.Text.ToLower(); // Получение текста поиска в нижнем регистре

            var result = ValueCharacteristicsContext.ValueCharacteristics.Where(x =>
                x.Znachenie.ToLower().Contains(searchText)
            );

            parent.Children.Clear(); // Очистка родительского контейнера перед добавлением результатов поиска

            foreach (var item in result)
            {
                parent.Children.Add(new Item(item, this));
            }
        }

        private void SortUp(object sender, RoutedEventArgs e)
        {
            var sortUp = ValueCharacteristicsContext.ValueCharacteristics.OrderBy(x => x.Znachenie);
            parent.Children.Clear();

            foreach (var valueCharacteristics in sortUp)
            {
                parent.Children.Add(new Item(valueCharacteristics, this));
            }
        }

        private void SortDown(object sender, RoutedEventArgs e)
        {
            var sortDown = ValueCharacteristicsContext.ValueCharacteristics.OrderByDescending(x => x.Znachenie);
            parent.Children.Clear();

            foreach (var valueCharacteristics in sortDown)
            {
                parent.Children.Add(new Item(valueCharacteristics, this));
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Menu());
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.ValueCharacteristics.Add(this, null));
        }
    }
}
