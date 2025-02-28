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

namespace YP02.Pages.ViewModel
{
    /// <summary>
    /// Логика взаимодействия для ViewModel.xaml
    /// </summary>
    public partial class ViewModel : Page
    {
        // Контекст для работы с моделями представления
        public ViewModelContext ViewModelContext = new ViewModelContext();

        public ViewModel()
        {
            InitializeComponent(); 
            parent.Children.Clear(); // Очистка родительского контейнера перед добавлением новых элементов

            // Заполнение родительского контейнера элементами Item для каждой модели представления из контекста
            foreach (Models.ViewModel item in ViewModelContext.ViewModel)
            {
                parent.Children.Add(new Item(item, this)); // Добавление нового элемента Item для каждой модели представления
            }
        }

        // Обработчик события нажатия клавиши в поле поиска
        private void KeyDown_Search(object sender, KeyEventArgs e)
        {
            string searchText = search.Text.ToLower(); // Получение текста поиска в нижнем регистре

            // Поиск моделей представления, имя которых содержит текст поиска
            var result = ViewModelContext.ViewModel.Where(x =>
                x.Name.ToLower().Contains(searchText)
            );

            parent.Children.Clear(); // Очистка родительского контейнера перед добавлением результатов поиска

            // Добавление найденных моделей представления в родительский контейнер
            foreach (var item in result)
            {
                parent.Children.Add(new Item(item, this)); // Создание элемента Item для каждой найденной модели представления
            }
        }

        // Обработчик события нажатия кнопки "Назад"
        private void Back(object sender, RoutedEventArgs e)
        {
            // Переход на страницу меню
            MainWindow.init.OpenPages(new Menu());
        }

        // Обработчик события нажатия кнопки "Добавить"
        private void Add(object sender, RoutedEventArgs e)
        {
            // Переход на страницу добавления новой модели представления
            MainWindow.init.OpenPages(new Pages.ViewModel.Add(this, null));
        }
    }
}
