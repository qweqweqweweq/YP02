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

namespace YP02.Pages.Auditories
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        // Основная страница аудиторий, с которой работает текущий элемент
        Auditories MainAuditories;

        // Модель аудитории, которую представляет данный элемент
        Models.Auditories Auditories;

        // Контекст для работы с пользователями
        UsersContext usersContext = new();

        // Конструктор класса, который принимает модель аудитории и основную страницу аудиторий
        public Item(Models.Auditories Auditories, Auditories MainAuditories)
        {
            InitializeComponent(); 

            this.Auditories = Auditories; // Сохранение ссылки на модель аудитории
            this.MainAuditories = MainAuditories; // Сохранение ссылки на основную страницу аудиторий

            // Заполнение элементов управления данными аудитории
            lb_Name.Content = Auditories.Name; // Установка имени аудитории
            lb_sokrName.Content = Auditories.ShortName; // Установка сокращённого имени аудитории
                                                        // Установка ответственного пользователя по ID
            lb_User.Content = "Ответственный: " + usersContext.Users.Where(x => x.Id == Auditories.ResponUser).FirstOrDefault()?.FIO;
            // Установка временно-ответственного пользователя по ID
            lb_tempUser.Content = "Временно-ответственный: " + usersContext.Users.Where(x => x.Id == Auditories.TimeResponUser).FirstOrDefault()?.FIO;
        }

        // Обработчик события нажатия кнопки "Редактировать"
        private void Click_redact(object sender, RoutedEventArgs e)
        {
            // Переход на страницу редактирования аудитории, передавая основную страницу и текущую аудиторию
            MainWindow.init.OpenPages(new Pages.Auditories.Add(MainAuditories, Auditories));
        }

        // Обработчик события нажатия кнопки "Удалить"
        private void Click_remove(object sender, RoutedEventArgs e)
        {
            // Запрос подтверждения на удаление
            MessageBoxResult result = MessageBox.Show("При удалении аудитории все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Если пользователь подтвердил удаление
            if (result == MessageBoxResult.Yes)
            {
                // Удаление аудитории из контекста
                MainAuditories.auditoriesContext.Auditories.Remove(Auditories);
                MainAuditories.auditoriesContext.SaveChanges(); // Сохранение изменений в базе данных

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
