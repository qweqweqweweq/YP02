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

namespace YP02.Pages.Napravlenie
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        // Основная страница направлений, с которой работает текущая страница
        public Napravlenie MainNapravlenie;

        // Модель направления, которую мы редактируем или добавляем
        public Models.Napravlenie napravlenie;

        // Конструктор класса, который принимает основную страницу направлений и (опционально) направление для редактирования
        public Add(Napravlenie MainNapravlenie, Models.Napravlenie napravlenie)
        {
            InitializeComponent(); 

            this.MainNapravlenie = MainNapravlenie; // Сохранение ссылки на основную страницу направлений
            this.napravlenie = napravlenie; // Сохранение направления для редактирования (если передано)

            // Если направление для редактирования не равно null, заполняем поля данными направления
            if (napravlenie != null)
            {
                text1.Content = "Изменение направления"; // Установка заголовка
                text2.Content = "Изменить"; // Изменение текста кнопки
                tb_Name.Text = napravlenie.Name; // Заполнение поля имени направления
            }
        }

        // Обработчик события нажатия кнопки "Сохранить" (или "Изменить")
        private void Click_Redact(object sender, RoutedEventArgs e)
        {
            // Проверка на заполненность поля имени направления
            if (string.IsNullOrEmpty(tb_Name.Text))
            {
                MessageBox.Show("Введите наименование направления"); // Сообщение об ошибке, если поле пустое
                return; // Прерывание выполнения метода
            }

            // Если направление не было передано (т.е. мы добавляем новое)
            if (napravlenie == null)
            {
                napravlenie = new Models.Napravlenie // Создание новой модели направления
                {
                    Name = tb_Name.Text // Установка имени направления
                };
                MainNapravlenie.NapravlenieContext.Napravlenie.Add(napravlenie); // Добавление новой модели в контекст
            }
            else // Если направление уже существует (редактируем)
            {
                napravlenie.Name = tb_Name.Text; // Обновление имени направления
            }

            // Сохранение изменений в базе данных
            MainNapravlenie.NapravlenieContext.SaveChanges();
            // Переход на страницу со списком направлений
            MainWindow.init.OpenPages(new Pages.Napravlenie.Napravlenie());
        }

        // Обработчик события нажатия кнопки "Отмена"
        private void Click_Cancel_Redact(object sender, RoutedEventArgs e)
        {
            // Переход на страницу со списком направлений без сохранения изменений
            MainWindow.init.OpenPages(new Pages.Napravlenie.Napravlenie());
        }
    }
}
