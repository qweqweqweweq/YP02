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

namespace YP02.Pages.Users
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        // Основная страница пользователей, с которой работает текущий элемент
        Users MainUsers;

        // Модель пользователя, которую представляет данный элемент
        Models.Users Users;

        // Конструктор класса, который принимает модель пользователя и основную страницу пользователей
        public Item(Models.Users Users, Users MainUsers)
        {
            InitializeComponent(); 

            this.Users = Users; // Сохранение ссылки на модель пользователя
            this.MainUsers = MainUsers; // Сохранение ссылки на основную страницу пользователей

            // Заполнение элементов управления данными пользователя
            lb_FIO.Content = Users.FIO; // Установка ФИО пользователя
            lb_Role.Content = Users.Role; // Установка роли пользователя
            lb_Number.Content = Users.Number; // Установка номера телефона пользователя
            lb_Email.Content = Users.Email; // Установка электронной почты пользователя
            lb_Address.Content = Users.Address; // Установка адреса пользователя
        }

        // Обработчик события нажатия кнопки "Редактировать"
        private void Click_redact(object sender, RoutedEventArgs e)
        {
            // Переход на страницу редактирования пользователя, передавая основную страницу и текущего пользователя
            MainWindow.init.OpenPages(new Pages.Users.Add(MainUsers, Users));
        }

        // Обработчик события нажатия кнопки "Удалить"
        private void Click_remove(object sender, RoutedEventArgs e)
        {
            // Запрос подтверждения на удаление
            MessageBoxResult result = MessageBox.Show("При удалении пользователя все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Если пользователь подтвердил удаление
            if (result == MessageBoxResult.Yes)
            {
                // Удаление пользователя из контекста
                MainUsers.UsersContext.Users.Remove(Users);
                MainUsers.UsersContext.SaveChanges(); // Сохранение изменений в базе данных

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
