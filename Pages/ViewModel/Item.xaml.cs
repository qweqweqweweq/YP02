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
using YP02.Context;

namespace YP02.Pages.ViewModel
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        // Основная модель представления, с которой работает текущий элемент
        ViewModel MainViewModel;

        // Модель представления, которую представляет данный элемент
        Models.ViewModel ViewModel;

        // Контекст для работы с типами оборудования
        OborTypeContext oborTypeContext = new OborTypeContext();
        private Models.Users currentUser;

        // Конструктор класса, который принимает модель представления и основную модель представления
        public Item(Models.ViewModel ViewModel, ViewModel MainViewModel)
        {
            InitializeComponent();

            currentUser = MainWindow.init.CurrentUser;
            if (currentUser != null && currentUser.Role == "Администратор")
            {
                buttons.Visibility = Visibility.Visible;
            }

            this.ViewModel = ViewModel; // Сохранение ссылки на модель представления
            this.MainViewModel = MainViewModel; // Сохранение ссылки на основную модель представления

            // Заполнение элементов управления данными модели представления
            lb_Name.Content = ViewModel.Name; // Установка имени модели
            lb_OborType.Content = oborTypeContext.OborType.Where(x => x.Id == ViewModel.Id).FirstOrDefault()?.Name; // Установка типа оборудования по ID
        }

        // Обработчик события нажатия кнопки "Редактировать"
        private void Click_redact(object sender, RoutedEventArgs e)
        {
            // Переход на страницу редактирования модели представления, передавая основную модель и текущую модель
            MainWindow.init.OpenPages(new Pages.ViewModel.Add(MainViewModel, ViewModel));
        }

        // Обработчик события нажатия кнопки "Удалить"
        private void Click_remove(object sender, RoutedEventArgs e)
        {
            // Запрос подтверждения на удаление
            MessageBoxResult result = MessageBox.Show("При удалении все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Если пользователь подтвердил удаление
            if (result == MessageBoxResult.Yes)
            {
                // Удаление модели представления из контекста
                MainViewModel.ViewModelContext.ViewModel.Remove(ViewModel);
                MainViewModel.ViewModelContext.SaveChanges(); // Сохранение изменений в базе данных

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
