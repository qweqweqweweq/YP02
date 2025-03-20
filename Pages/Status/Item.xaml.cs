﻿using System;
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

namespace YP02.Pages.Status
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        // Основная страница статусов, с которой работает текущий элемент
        Status MainStatus;

        // Модель статуса, которую представляет данный элемент
        Models.Status Status;
        private Models.Users currentUser;

        // Конструктор класса, который принимает модель статуса и основную страницу статусов
        public Item(Models.Status Status, Status MainStatus)
        {
            InitializeComponent();

            currentUser = MainWindow.init.CurrentUser;
            if (currentUser != null && currentUser.Role == "Администратор")
            {
                buttons.Visibility = Visibility.Visible;
            }

            this.MainStatus = MainStatus; // Сохранение ссылки на основную страницу статусов
            this.Status = Status; // Сохранение ссылки на модель статуса

            // Заполнение элемента управления данными статуса
            lb_Name.Content = Status.Name; // Установка имени статуса
        }

        // Обработчик события нажатия кнопки "Редактировать"
        private void Click_redact(object sender, RoutedEventArgs e)
        {
            // Переход на страницу редактирования статуса, передавая основную страницу и текущий статус
            MainWindow.init.OpenPages(new Pages.Status.Add(MainStatus, Status));
        }

        // Обработчик события нажатия кнопки "Удалить"
        private void Click_remove(object sender, RoutedEventArgs e)
        {
            // Запрос подтверждения на удаление
            MessageBoxResult result = MessageBox.Show("При удалении все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Если пользователь подтвердил удаление
            if (result == MessageBoxResult.Yes)
            {
                // Удаление статуса из контекста
                MainStatus.StatusContext.Status.Remove(Status);
                MainStatus.StatusContext.SaveChanges(); // Сохранение изменений в базе данных

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
