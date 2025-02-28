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
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        // Основная страница инвентаризации, с которой работает текущий элемент
        Inventory MainInventory;

        // Модель инвентаризации, которую представляет данный элемент
        Models.Inventory inventory;

        // Контекст для работы с пользователями
        UsersContext usersContext = new UsersContext();

        // Конструктор класса, который принимает модель инвентаризации и основную страницу инвентаризации
        public Item(Models.Inventory inventory, Inventory MainInventory)
        {
            InitializeComponent(); 

            this.inventory = inventory; // Сохранение ссылки на модель инвентаризации
            this.MainInventory = MainInventory; // Сохранение ссылки на основную страницу инвентаризации

            // Заполнение элементов управления данными инвентаризации
            lb_Name.Content = inventory.Name; // Установка имени инвентаризации
            lb_DateStart.Content = inventory.StartDate; // Установка даты начала инвентаризации
            lb_DateEnd.Content = inventory.EndDate; // Установка даты окончания инвентаризации
                                                    // Установка ответственного пользователя по ID
            lb_UserId.Content = usersContext.Users.Where(x => x.Id == inventory.UserId).FirstOrDefault()?.FIO; // Исправлено на inventory.UserId
        }

        // Обработчик события нажатия кнопки "Редактировать"
        private void Click_redact(object sender, RoutedEventArgs e)
        {
            // Переход на страницу редактирования инвентаризации, передавая основную страницу и текущую инвентаризацию
            MainWindow.init.OpenPages(new Pages.Inventory.Add(MainInventory, inventory));
        }

        // Обработчик события нажатия кнопки "Удалить"
        private void Click_remove(object sender, RoutedEventArgs e)
        {
            // Запрос подтверждения на удаление
            MessageBoxResult result = MessageBox.Show("При удалении инвентаризации все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Если пользователь подтвердил удаление
            if (result == MessageBoxResult.Yes)
            {
                // Удаление инвентаризации из контекста
                MainInventory.InventoryContext.Inventory.Remove(inventory);
                MainInventory.InventoryContext.SaveChanges(); // Сохранение изменений в базе данных

                // Удаление текущего элемента из родительского контейнера
                (this.Parent as Panel).Children.Remove(this);
            }
            else
            {
                // Сообщение о том, что действие отменено
                MessageBox.Show("Действие отменено.");
            }
        }
    }
}
