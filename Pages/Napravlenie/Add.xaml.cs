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
        // Поле для хранения ссылки на основной объект направления
        public Napravlenie MainNapravlenie;

        // Поле для хранения информации о конкретном направлении
        public Models.Napravlenie napravlenie;

        public Add(Napravlenie MainNapravlenie, Models.Napravlenie napravlenie = null)
        {
            InitializeComponent();

            // Присваивание переданных параметров полям класса
            this.MainNapravlenie = MainNapravlenie;
            this.napravlenie = napravlenie;

            // Если объект направления не равен null, заполняем текстовое поле его именем
            if (napravlenie != null)
            {
                tb_Name.Text = napravlenie.Name;
            }
        }

        // Обработчик события нажатия кнопки "Редактировать"
        private void Click_Redact(object sender, RoutedEventArgs e)
        {
            // Проверка, введено ли название направления
            if (string.IsNullOrEmpty(tb_Name.Text))
            {
                // Если нет, выводим сообщение об ошибке
                MessageBox.Show("Введите название направления");
                return; // Прерываем выполнение метода
            }

            // Если объект направления равен null, создаем новый объект
            if (napravlenie == null)
            {
                napravlenie = new Models.Napravlenie();
                napravlenie.Name = tb_Name.Text; // Устанавливаем имя направления
                MainNapravlenie.NapravlenieContext.Napravlenie.Add(napravlenie); // Добавляем новое направление в контекст
            }
            else
            {
                // Если объект направления существует, обновляем его имя
                napravlenie.Name = tb_Name.Text;
            }

            // Сохраняем изменения в контексте направлений
            MainNapravlenie.NapravlenieContext.SaveChanges();

            // Открываем страницу со списком направлений
            MainWindow.init.OpenPages(new Pages.Napravlenie.Napravlenie());
        }

        // Обработчик события нажатия кнопки "Отмена"
        private void Click_Cancel_Redact(object sender, RoutedEventArgs e)
        {
            // Открываем страницу со списком направлений без сохранения изменений
            MainWindow.init.OpenPages(new Pages.Napravlenie.Napravlenie());
        }
    }
}
