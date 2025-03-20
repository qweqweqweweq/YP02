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
using YP02.Models;
using YP02.Pages.Users;

namespace YP02.Pages.Auditories
{
    /// <summary>
    /// Логика взаимодействия для Auditories.xaml
    /// </summary>
    public partial class Auditories : Page
    {
        // Контекст для работы с аудиториями
        public AuditoriesContext auditoriesContext = new AuditoriesContext();
        private Models.Users currentUser;

        // Конструктор класса, инициализирующий страницу
        public Auditories()
        {
            InitializeComponent(); // Инициализация компонентов страницы

            currentUser = MainWindow.init.CurrentUser;
            if (currentUser != null && currentUser.Role == "Администратор")
            {
                addBtn.Visibility = Visibility.Visible;
            }

            parent.Children.Clear(); // Очистка родительского контейнера перед добавлением новых элементов

            // Заполнение родительского контейнера элементами Item для каждой аудитории из контекста
            foreach (Models.Auditories item in auditoriesContext.Auditories)
            {
                parent.Children.Add(new Item(item, this)); // Добавление нового элемента Item для каждой аудитории
            }
        }

        // Обработчик события нажатия клавиши в поле поиска
        private void KeyDown_Search(object sender, KeyEventArgs e)
        {
            string searchText = search.Text.ToLower(); // Получение текста поиска в нижнем регистре

            // Поиск аудиторий, имя которых содержит текст поиска
            var result = auditoriesContext.Auditories.Where(x =>
                x.Name.ToLower().Contains(searchText)
            );

            parent.Children.Clear(); // Очистка родительского контейнера перед добавлением результатов поиска

            // Добавление найденных аудиторий в родительский контейнер
            foreach (var item in result)
            {
                parent.Children.Add(new Item(item, this)); // Создание элемента Item для каждой найденной аудитории
            }
        }

        // Обработчик события нажатия кнопки "Назад"
        private void Back(object sender, RoutedEventArgs e)
        {
            // Переход на страницу меню
            MainWindow.init.OpenPages(new Menu());
        }

        // Обработчик события нажатия кнопки "Сортировать по возрастанию"
        private void SortUp(object sender, RoutedEventArgs e)
        {
            // Сортировка аудиторий по имени в порядке возрастания
            var sortUp = auditoriesContext.Auditories.OrderBy(x => x.Name);
            parent.Children.Clear(); // Очистка родительского контейнера перед добавлением отсортированных аудиторий

            // Добавление отсортированных аудиторий в родительский контейнер
            foreach (var auditories in sortUp)
            {
                parent.Children.Add(new Item(auditories, this)); // Создание элемента Item для каждой отсортированной аудитории
            }
        }

        // Обработчик события нажатия кнопки "Сортировать по убыванию"
        private void SortDown(object sender, RoutedEventArgs e)
        {
            // Сортировка аудиторий по имени в порядке убывания
            var sortDown = auditoriesContext.Auditories.OrderByDescending(x => x.Name);
            parent.Children.Clear(); // Очистка родительского контейнера перед добавлением отсортированных аудиторий

            // Добавление отсортированных аудиторий в родительский контейнер
            foreach (var auditories in sortDown)
            {
                parent.Children.Add(new Item(auditories, this)); // Создание элемента Item для каждой отсортированной аудитории
            }
        }

        // Обработчик события нажатия кнопки "Добавить"
        private void Add(object sender, RoutedEventArgs e)
        {
            // Переход на страницу добавления новой аудитории
            MainWindow.init.OpenPages(new Pages.Auditories.Add(this, null));
        }
    }
}
