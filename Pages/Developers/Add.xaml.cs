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

namespace YP02.Pages.Developers
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        // Поле для хранения ссылки на основной объект разработчиков
        public Developers MainDevelopers;

        // Поле для хранения информации о конкретном разработчике
        public Models.Developers developers;

        public Add(Developers MainDevelopers, Models.Developers developers = null)
        {            
            InitializeComponent();

            // Присваивание переданных параметров полям класса
            this.MainDevelopers = MainDevelopers;
            this.developers = developers;

            // Если объект разработчика не равен null, заполняем текстовое поле его именем
            if (developers != null)
            {
                lb_title.Content = "Изменение разработчика";
                bt_click.Content = "Изменить";
                tb_Name.Text = developers.Name;
            }
        }

        // Обработчик события нажатия кнопки "Редактировать"
        private void Click_Redact(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка, введено ли название разработчика
                if (string.IsNullOrEmpty(tb_Name.Text))
                {
                    // Если нет, выводим сообщение об ошибке
                    MessageBox.Show("Введите название типа оборудования");
                    return; // Прерываем выполнение метода
                }

                // Если объект разработчика равен null, создаем новый объект
                if (developers == null)
                {
                    developers = new Models.Developers();
                    developers.Name = tb_Name.Text; // Устанавливаем имя разработчика
                    MainDevelopers.DevelopersContext.Developers.Add(developers); // Добавляем нового разработчика в контекст
                }
                else
                {
                    // Если объект разработчика существует, обновляем его имя
                    developers.Name = tb_Name.Text;
                }

                // Сохраняем изменения в контексте разработчиков
                MainDevelopers.DevelopersContext.SaveChanges();

                // Открываем страницу со списком разработчиков
                MainWindow.init.OpenPages(new Pages.Developers.Developers());
            }
            catch (Exception ex)
            {
                try
                {
                    using (var errorsContext = new ErrorsContext())
                    {
                        var error = new Models.Errors
                        {
                            Message = ex.Message
                        };
                        errorsContext.Errors.Add(error);
                        errorsContext.SaveChanges(); // Сохраняем ошибку в базе данных
                    }

                    // Логирование ошибки в файл log.txt
                    string logPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "log.txt");
                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(logPath)); // Создаем папку bin, если ее нет
                    System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: {ex.Message}\n{ex.StackTrace}\n\n");
                }
                catch (Exception logEx)
                {
                    MessageBox.Show("Ошибка при записи в лог-файл: " + logEx.Message);
                }

                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        // Обработчик события нажатия кнопки "Отмена"
        private void Click_Cancel_Redact(object sender, RoutedEventArgs e)
        {
            try
            {
                // Открываем страницу со списком разработчиков без сохранения изменений
                MainWindow.init.OpenPages(new Pages.Developers.Developers());
            }
            catch (Exception ex)
            {
                try
                {
                    using (var errorsContext = new ErrorsContext())
                    {
                        var error = new Models.Errors
                        {
                            Message = ex.Message
                        };
                        errorsContext.Errors.Add(error);
                        errorsContext.SaveChanges(); // Сохраняем ошибку в базе данных
                    }

                    // Логирование ошибки в файл log.txt
                    string logPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "log.txt");
                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(logPath)); // Создаем папку bin, если ее нет
                    System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: {ex.Message}\n{ex.StackTrace}\n\n");
                }
                catch (Exception logEx)
                {
                    MessageBox.Show("Ошибка при записи в лог-файл: " + logEx.Message);
                }

                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
    }
}
