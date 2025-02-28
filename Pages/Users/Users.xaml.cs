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

namespace YP02.Pages.Users
{
    /// <summary>
    /// Логика взаимодействия для Users.xaml
    /// </summary>
    public partial class Users : Page
    {
        // Контекст для работы с пользователями
        public UsersContext UsersContext = new();

        public Users()
        {
            InitializeComponent(); 
            parent.Children.Clear(); // Очистка родительского контейнера перед добавлением новых элементов

            // Заполнение родительского контейнера элементами Item для каждого пользователя из контекста
            foreach (Models.Users item in UsersContext.Users)
            {
                parent.Children.Add(new Item(item, this)); // Добавление нового элемента Item для каждого пользователя
            }
        }

        // Обработчик события нажатия клавиши в поле поиска
        private void KeyDown_Search(object sender, KeyEventArgs e)
        {
            string searchText = search.Text.ToLower(); // Получение текста поиска в нижнем регистре

            // Поиск пользователей, ФИО которых содержит текст поиска
            var result = UsersContext.Users.Where(x =>
                x.FIO.ToLower().Contains(searchText)
            );

            parent.Children.Clear(); // Очистка родительского контейнера перед добавлением результатов поиска

            // Добавление найденных пользователей в родительский контейнер
            foreach (var item in result)
            {
                parent.Children.Add(new Item(item, this)); // Создание элемента Item для каждого найденного пользователя
            }
        }

        // Обработчик события нажатия кнопки "Сортировать по возрастанию"
        private void SortUp(object sender, RoutedEventArgs e)
        {
            // Сортировка пользователей по ФИО в порядке возрастания
            var sortUp = UsersContext.Users.OrderBy(x => x.FIO);
            parent.Children.Clear(); // Очистка родительского контейнера перед добавлением отсортированных пользователей

            // Добавление отсортированных пользователей в родительский контейнер
            foreach (var users in sortUp)
            {
                parent.Children.Add(new Item(users, this)); // Создание элемента Item для каждого отсортированного пользователя
            }
        }

        // Обработчик события нажатия кнопки "Сортировать по убыванию"
        private void SortDown(object sender, RoutedEventArgs e)
        {
            // Сортировка пользователей по ФИО в порядке убывания
            var sortDown = UsersContext.Users.OrderByDescending(x => x.FIO);
            parent.Children.Clear(); // Очистка родительского контейнера перед добавлением отсортированных пользователей

            // Добавление отсортированных пользователей в родительский контейнер
            foreach (var users in sortDown)
            {
                parent.Children.Add(new Item(users, this)); // Создание элемента Item для каждого отсортированного пользователя
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
            // Переход на страницу добавления нового пользователя
            MainWindow.init.OpenPages(new Pages.Users.Add(this, null));
        }
    }
}
