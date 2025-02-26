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

namespace YP02.Pages.Developers
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        // Поле для хранения ссылки на основной объект разработчиков
        Developers MainDevelopers;

        // Поле для хранения информации о конкретном разработчике
        Models.Developers Developers;

        public Item(Models.Developers Developers, Developers MainDevelopers)
        {
            InitializeComponent();

            // Присваивание переданных параметров полям класса
            this.Developers = Developers;
            this.MainDevelopers = MainDevelopers;

            // Установка имени разработчика в элемент управления 
            lb_Name.Content = Developers.Name;
        }

        // Обработчик события нажатия кнопки "Редактировать"
        private void Click_redact(object sender, RoutedEventArgs e)
        {
            // Открытие страницы редактирования выбранного разработчика
            MainWindow.init.OpenPages(new Pages.Developers.Add(MainDevelopers, Developers));
        }

        // Обработчик события нажатия кнопки "Удалить"
        private void Click_remove(object sender, RoutedEventArgs e)
        {
            // Вывод сообщения с подтверждением удаления
            MessageBoxResult result = MessageBox.Show("При удалении все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Если пользователь подтвердил удаление
            if (result == MessageBoxResult.Yes)
            {
                // Удаление разработчика из контекста
                MainDevelopers.DevelopersContext.Developers.Remove(Developers);
                // Сохранение изменений в контексте
                MainDevelopers.DevelopersContext.SaveChanges();
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
