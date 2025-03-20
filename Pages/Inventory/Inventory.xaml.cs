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
        // Контекст для работы с инвентаризациями
        public InventoryContext InventoryContext = new InventoryContext();
        private Models.Users currentUser;

        public Inventory()
        {
            InitializeComponent();

            currentUser = MainWindow.init.CurrentUser;
            if (currentUser != null && currentUser.Role == "Администратор")
            {
                addBtn.Visibility = Visibility.Visible;
            }

            parent.Children.Clear(); // Очистка родительского контейнера перед добавлением новых элементов

            // Заполнение родительского контейнера элементами Item для каждой инвентаризации из контекста
            foreach (Models.Inventory item in InventoryContext.Inventory)
            {
                parent.Children.Add(new Item(item, this)); // Добавление нового элемента Item для каждой инвентаризации
            }
        }

        // Обработчик события нажатия клавиши в поле поиска
        private void KeyDown_Search(object sender, KeyEventArgs e)
        {
            string searchText = search.Text.ToLower(); // Получение текста поиска в нижнем регистре

            // Поиск инвентаризаций, имя которых содержит текст поиска
            var result = InventoryContext.Inventory.Where(x =>
                x.Name.ToLower().Contains(searchText)
            );

            parent.Children.Clear(); // Очистка родительского контейнера перед добавлением результатов поиска

            // Добавление найденных инвентаризаций в родительский контейнер
            foreach (var item in result)
            {
                parent.Children.Add(new Item(item, this)); // Создание элемента Item для каждой найденной инвентаризации
            }
        }

        // Обработчик события нажатия кнопки "Сортировать по возрастанию"
        private void SortUp(object sender, RoutedEventArgs e)
        {
            // Сортировка инвентаризаций по имени в порядке возрастания
            var sortUp = InventoryContext.Inventory.OrderBy(x => x.Name);
            parent.Children.Clear(); // Очистка родительского контейнера перед добавлением отсортированных инвентаризаций

            // Добавление отсортированных инвентаризаций в родительский контейнер
            foreach (var inventorys in sortUp)
            {
                parent.Children.Add(new Item(inventorys, this)); // Создание элемента Item для каждой отсортированной инвентаризации
            }
        }

        // Обработчик события нажатия кнопки "Сортировать по убыванию"
        private void SortDown(object sender, RoutedEventArgs e)
        {
            // Сортировка инвентаризаций по имени в порядке убывания
            var sortDown = InventoryContext.Inventory.OrderByDescending(x => x.Name);
            parent.Children.Clear(); // Очистка родительского контейнера перед добавлением отсортированных инвентаризаций

            // Добавление отсортированных инвентаризаций в родительский контейнер
            foreach (var inventorys in sortDown)
            {
                parent.Children.Add(new Item(inventorys, this)); // Создание элемента Item для каждой отсортированной инвентаризации
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
            // Переход на страницу добавления новой инвентаризации
            MainWindow.init.OpenPages(new Pages.Inventory.Add(this, null));
        }
    }
}
