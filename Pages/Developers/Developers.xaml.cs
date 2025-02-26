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

namespace YP02.Pages.Developers
{
    /// <summary>
    /// Логика взаимодействия для Developers.xaml
    /// </summary>    
    public partial class Developers : Page
    {
        // Создание контекста разработчиков для работы с данными
        public DevelopersContext DevelopersContext = new DevelopersContext();

        public Developers()
        {
            InitializeComponent();

            // Очистка дочерних элементов родительского контейнера
            parent.Children.Clear();

            // Перебор всех разработчиков из контекста и добавление их в родительский контейнер
            foreach (Models.Developers item in DevelopersContext.Developers)
            {
                parent.Children.Add(new Item(item, this)); // Создание нового элемента для каждого разработчика
            }
        }

        // Обработчик события нажатия клавиши в поле поиска
        private void KeyDown_Search(object sender, KeyEventArgs e)
        {
            // Получение текста поиска и преобразование его в нижний регистр
            string searchText = search.Text.ToLower();

            // Фильтрация разработчиков по имени, содержащему текст поиска
            var result = DevelopersContext.Developers.Where(x =>
                x.Name.ToLower().Contains(searchText)
            );

            // Очистка родительского контейнера перед добавлением результатов поиска
            parent.Children.Clear();

            // Добавление отфильтрованных разработчиков в родительский контейнер
            foreach (var item in result)
            {
                parent.Children.Add(new Item(item, this));
            }
        }

        // Обработчик события нажатия кнопки "Назад"
        private void Back(object sender, RoutedEventArgs e)
        {
            // Открытие главного меню
            MainWindow.init.OpenPages(new Menu());
        }

        // Обработчик события нажатия кнопки "Сортировать по возрастанию"
        private void SortUp(object sender, RoutedEventArgs e)
        {
            // Сортировка разработчиков по имени в порядке возрастания
            var sortUp = DevelopersContext.Developers.OrderBy(x => x.Name);

            // Очистка родительского контейнера перед добавлением отсортированных разработчиков
            parent.Children.Clear();

            // Добавление отсортированных разработчиков в родительский контейнер
            foreach (var developers in sortUp)
            {
                parent.Children.Add(new Item(developers, this));
            }
        }

        // Обработчик события нажатия кнопки "Сортировать по убыванию"
        private void SortDown(object sender, RoutedEventArgs e)
        {
            // Сортировка разработчиков по имени в порядке убывания
            var sortDown = DevelopersContext.Developers.OrderByDescending(x => x.Name);

            // Очистка родительского контейнера перед добавлением отсортированных разработчиков
            parent.Children.Clear();

            // Добавление отсортированных разработчиков в родительский контейнер
            foreach (var developers in sortDown)
            {
                parent.Children.Add(new Item(developers, this));
            }
        }

        // Обработчик события нажатия кнопки "Добавить"
        private void Add(object sender, RoutedEventArgs e)
        {
            // Открытие страницы добавления нового разработчика
            MainWindow.init.OpenPages(new Pages.Developers.Add(this, null));
        }
    }
}
