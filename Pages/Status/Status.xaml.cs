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
        // Контекст для работы со статусами
        public StatusContext StatusContext = new();

        public Status()
        {
            InitializeComponent(); 
            parent.Children.Clear(); // Очистка родительского контейнера перед добавлением новых элементов

            // Заполнение родительского контейнера элементами Item для каждого статуса из контекста
            foreach (Models.Status item in StatusContext.Status)
            {
                parent.Children.Add(new Item(item, this)); // Добавление нового элемента Item для каждого статуса
            }
        }

        // Обработчик события нажатия клавиши в поле поиска
        private void KeyDown_Search(object sender, KeyEventArgs e)
        {
            string searchText = search.Text.ToLower(); // Получение текста поиска в нижнем регистре

            // Поиск статусов, имя которых содержит текст поиска
            var result = StatusContext.Status.Where(x =>
                x.Name.ToLower().Contains(searchText)
            );

            parent.Children.Clear(); // Очистка родительского контейнера перед добавлением результатов поиска

            // Добавление найденных статусов в родительский контейнер
            foreach (var item in result)
            {
                parent.Children.Add(new Item(item, this)); // Создание элемента Item для каждой найденной записи
            }
        }

        // Обработчик события нажатия кнопки "Сортировать по возрастанию"
        private void SortUp(object sender, RoutedEventArgs e)
        {
            // Сортировка статусов по имени в порядке возрастания
            var sortUp = StatusContext.Status.OrderBy(x => x.Name);
            parent.Children.Clear(); // Очистка родительского контейнера перед добавлением отсортированных статусов

            // Добавление отсортированных статусов в родительский контейнер
            foreach (var status in sortUp)
            {
                parent.Children.Add(new Item(status, this)); // Создание элемента Item для каждой отсортированной записи
            }
        }

        // Обработчик события нажатия кнопки "Сортировать по убыванию"
        private void SortDown(object sender, RoutedEventArgs e)
        {
            // Сортировка статусов по имени в порядке убывания
            var sortDown = StatusContext.Status.OrderByDescending(x => x.Name);
            parent.Children.Clear(); // Очистка родительского контейнера перед добавлением отсортированных статусов

            // Добавление отсортированных статусов в родительский контейнер
            foreach (var status in sortDown)
            {
                parent.Children.Add(new Item(status, this)); // Создание элемента Item для каждой отсортированной записи
            }
        }

        // Обработчик события нажатия кнопки "Назад"
        private void Back(object sender, RoutedEventArgs e)
        {
            // Переход на страницу меню
            MainWindow.init.OpenPages(new Pages.Menu());
        }

        // Обработчик события нажатия кнопки "Добавить"
        private void Add(object sender, RoutedEventArgs e)
        {
            // Переход на страницу добавления нового статуса
            MainWindow.init.OpenPages(new Pages.Status.Add(this, null));
        }
    }
}
