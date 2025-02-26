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
        // Создание контекста направлений для работы с данными
        public NapravlenieContext NapravlenieContext = new NapravlenieContext();

        public Napravlenie()
        {
            InitializeComponent();

            // Очистка дочерних элементов родительского контейнера
            parent.Children.Clear();

            // Перебор всех направлений из контекста и добавление их в родительский контейнер
            foreach (Models.Napravlenie item in NapravlenieContext.Napravlenie)
            {
                parent.Children.Add(new Item(item, this)); // Создание нового элемента для каждого направления
            }
        }

        // Обработчик события нажатия клавиши в поле поиска
        private void KeyDown_Search(object sender, KeyEventArgs e)
        {
            // Получение текста поиска и преобразование его в нижний регистр
            string searchText = search.Text.ToLower();

            // Фильтрация направлений по имени, содержащему текст поиска
            var result = NapravlenieContext.Napravlenie.Where(x =>
                x.Name.ToLower().Contains(searchText)
            );

            // Очистка родительского контейнера перед добавлением результатов поиска
            parent.Children.Clear();

            // Добавление отфильтрованных направлений в родительский контейнер
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

        // Обработчик события нажатия кнопки "Добавить"
        private void Add(object sender, RoutedEventArgs e)
        {
            // Открытие страницы добавления нового направления
            MainWindow.init.OpenPages(new Pages.Napravlenie.Add(this, null));
        }
    }
}
