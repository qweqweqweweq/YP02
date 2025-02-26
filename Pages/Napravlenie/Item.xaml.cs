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
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        // Поле для хранения ссылки на основной объект направления
        Napravlenie MainNapravlenie;

        // Поле для хранения информации о конкретном направлении
        Models.Napravlenie Napravlenie;

        public Item(Models.Napravlenie Napravlenie, Napravlenie MainNapravlenie)
        {
            InitializeComponent();

            // Присваивание переданных параметров полям класса
            this.Napravlenie = Napravlenie;
            this.MainNapravlenie = MainNapravlenie;

            // Установка имени направления в элемент управления (например, Label)
            lb_Name.Content = Napravlenie.Name;
        }

        // Обработчик события нажатия кнопки "Редактировать"
        private void Click_redact(object sender, RoutedEventArgs e)
        {
            // Открытие страницы редактирования выбранного направления
            MainWindow.init.OpenPages(new Pages.Napravlenie.Add(MainNapravlenie, Napravlenie));
        }

        // Обработчик события нажатия кнопки "Удалить"
        private void Click_remove(object sender, RoutedEventArgs e)
        {
            // Вывод сообщения с подтверждением удаления
            MessageBoxResult result = MessageBox.Show("При удалении все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Если пользователь подтвердил удаление
            if (result == MessageBoxResult.Yes)
            {
                // Удаление направления из контекста
                MainNapravlenie.NapravlenieContext.Napravlenie.Remove(Napravlenie);
                // Сохранение изменений в контексте
                MainNapravlenie.NapravlenieContext.SaveChanges();
                // Удаление текущего элемента из родительского контейнера
                (this.Parent as Panel).Children.Remove(this);
            }
            else
            {
                // Если действие отменено, выводим сообщение
                MessageBox.Show("Действие отменено.");
            }
        }
    }
}
