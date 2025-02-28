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
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        // Основная страница инвентаризации, с которой работает текущая страница
        public Inventory MainInventory;

        // Модель инвентаризации, которую мы редактируем или добавляем
        public Models.Inventory inventory;

        // Контекст для работы с пользователями
        UsersContext usersContext = new UsersContext();

        // Конструктор класса, который принимает основную страницу инвентаризации и (опционально) инвентаризацию для редактирования
        public Add(Inventory MainInventory, Models.Inventory inventory = null)
        {
            InitializeComponent(); 

            this.MainInventory = MainInventory; // Сохранение ссылки на основную страницу инвентаризации
            this.inventory = inventory; // Сохранение инвентаризации для редактирования (если передана)

            // Если инвентаризация для редактирования не равна null, заполняем поля данными инвентаризации
            if (inventory != null)
            {
                text1.Content = "Изменение инвентаризации"; // Установка заголовка
                text2.Content = "Изменить"; // Изменение текста кнопки
                tb_Name.Text = inventory.Name; // Заполнение поля имени инвентаризации
                tb_DateStart.Text = inventory.StartDate; // Заполнение поля даты начала инвентаризации
                tb_DateEnd.Text = inventory.EndDate; // Заполнение поля даты окончания инвентаризации
                cb_IdUser.SelectedItem = usersContext.Users.Where(x => x.Id == inventory.UserId).FirstOrDefault()?.FIO; // Установка выбранного пользователя по ID
            }

            // Заполнение выпадающего списка пользователей
            foreach (var item in usersContext.Users)
            {
                cb_IdUser.Items.Add(item.FIO); // Добавление каждого пользователя в список
            }
        }

        // Обработчик события нажатия кнопки "Сохранить" (или "Изменить")
        private void Click_Redact(object sender, RoutedEventArgs e)
        {
            // Проверка на заполненность полей
            if (string.IsNullOrEmpty(tb_Name.Text))
            {
                MessageBox.Show("Введите наименование инвентаризации"); // Сообщение об ошибке, если поле пустое
                return; // Прерывание выполнения метода
            }
            if (string.IsNullOrEmpty(tb_DateStart.Text))
            {
                MessageBox.Show("Введите дату начала инвентаризации"); // Сообщение об ошибке, если поле пустое
                return; // Прерывание выполнения метода
            }
            if (string.IsNullOrEmpty(tb_DateEnd.Text))
            {
                MessageBox.Show("Выберите дату окончания инвентаризации"); // Сообщение об ошибке, если поле пустое
                return; // Прерывание выполнения метода
            }
            if (cb_IdUser.SelectedItem == null)
            {
                MessageBox.Show("Выберите пользователя"); // Сообщение об ошибке, если пользователь не выбран
                return; // Прерывание выполнения метода
            }

            // Если инвентаризация не была передана (т.е. мы добавляем новую)
            if (inventory == null)
            {
                inventory = new Models.Inventory(); // Создание новой модели инвентаризации
                inventory.Name = tb_Name.Text; // Установка имени инвентаризации
                inventory.StartDate = tb_DateStart.Text; // Установка даты начала инвентаризации
                inventory.EndDate = tb_DateEnd.Text; // Установка даты окончания инвентаризации
                inventory.UserId = usersContext.Users.Where(x => x.FIO == cb_IdUser.SelectedItem.ToString()).First().Id; // Получение ID пользователя по имени
                MainInventory.InventoryContext.Inventory.Add(inventory); // Добавление новой инвентаризации в контекст
            }
            else // Если инвентаризация уже существует (редактируем)
            {
                // Обновление данных существующей инвентаризации
                inventory.Name = tb_Name.Text; // Обновление имени инвентаризации
                inventory.StartDate = tb_DateStart.Text; // Обновление даты начала инвентаризации
                inventory.EndDate = tb_DateEnd.Text; // Обновление даты окончания инвентаризации
                inventory.UserId = usersContext.Users.Where(x => x.FIO == cb_IdUser.SelectedItem.ToString()).First().Id; // Обновление ID пользователя
            }

            // Сохранение изменений в базе данных
            MainInventory.InventoryContext.SaveChanges();
            // Переход на страницу со списком инвентаризаций
            MainWindow.init.OpenPages(new Pages.Inventory.Inventory());
        }

        // Обработчик события нажатия кнопки " Отмена" 
        private void Click_Cancel_Redact(object sender, RoutedEventArgs e)
        {
            // Переход на страницу со списком инвентаризаций без сохранения изменений
            MainWindow.init.OpenPages(new Pages.Inventory.Inventory());
        }
    }
}
