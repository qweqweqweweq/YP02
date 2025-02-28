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

namespace YP02.Pages.Napravlenie
{
    /// <summary>
    /// Логика взаимодействия для Napravlenie.xaml
    /// </summary>
    public partial class Napravlenie : Page
    {
        // Контекст для работы с направлениями
        public NapravlenieContext NapravlenieContext = new();

        public Napravlenie()
        {
            InitializeComponent(); 
            parent.Children.Clear(); // Очистка родительского контейнера перед добавлением новых элементов

            // Заполнение родительского контейнера элементами Item для каждого направления из контекста
            foreach (Models.Napravlenie item in NapravlenieContext.Napravlenie)
            {
                parent.Children.Add(new Item(item, this)); // Добавление нового элемента Item для каждого направления
            }
        }

        // Обработчик события нажатия клавиши в поле поиска
        private void KeyDown_Search(object sender, KeyEventArgs e)
        {
            string searchText = search.Text.ToLower(); // Получение текста поиска в нижнем регистре

            // Поиск направлений, имя которых содержит текст поиска
            var result = NapravlenieContext.Napravlenie.Where(x =>
                x.Name.ToLower().Contains(searchText)
            );

            parent.Children.Clear(); // Очистка родительского контейнера перед добавлением результатов поиска

            // Добавление найденных направлений в родительский контейнер
            foreach (var item in result)
            {
                parent.Children.Add(new Item(item, this)); // Создание элемента Item для каждой найденной записи
            }
        }

        // Обработчик события нажатия кнопки "Сортировать по возрастанию"
        private void SortUp(object sender, RoutedEventArgs e)
        {
            // Сортировка направлений по имени в порядке возрастания
            var sortUp = NapravlenieContext.Napravlenie.OrderBy(x => x.Name);
            parent.Children.Clear(); // Очистка родительского контейнера перед добавлением отсортированных направлений

            // Добавление отсортированных направлений в родительский контейнер
            foreach (var napravlenie in sortUp)
            {
                parent.Children.Add(new Item(napravlenie, this)); // Создание элемента Item для каждой отсортированной записи
            }
        }

        // Обработчик события нажатия кнопки "Сортировать по убыванию"
        private void SortDown(object sender, RoutedEventArgs e)
        {
            // Сортировка направлений по имени в порядке убывания
            var sortDown = NapravlenieContext.Napravlenie.OrderByDescending(x => x.Name);
            parent.Children.Clear(); // Очистка родительского контейнера перед добавлением отсортированных направлений

            // Добавление отсортированных направлений в родительский контейнер
            foreach (var napravlenie in sortDown)
            {
                parent.Children.Add(new Item(napravlenie, this)); // Создание элемента Item для каждой отсортированной записи
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
            // Переход на страницу добавления нового направления
            MainWindow.init.OpenPages(new Pages.Napravlenie.Add(this, null));
        }
    }
}
