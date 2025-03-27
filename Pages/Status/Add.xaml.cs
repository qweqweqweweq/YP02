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

namespace YP02.Pages.Status
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        // Основная страница статусов, с которой работает текущая страница
        public Status MainStatus;

        // Модель статуса, которую мы редактируем или добавляем
        public Models.Status status;

        // Конструктор класса, который принимает основную страницу статусов и (опционально) статус для редактирования
        public Add(Status MainStatus, Models.Status status)
        {
            InitializeComponent(); 
            this.MainStatus = MainStatus; // Сохранение ссылки на основную страницу статусов
            this.status = status; // Сохранение статуса для редактирования (если передан)

            // Если статус для редактирования не равен null, заполняем поля данными статуса
            if (status != null)
            {
                text1.Content = "Изменение статуса"; // Установка заголовка
                text2.Content = "Изменить"; // Изменение текста кнопки
                tb_Name.Text = status.Name; // Заполнение поля имени статуса
            }
        }

        // Обработчик события нажатия кнопки "Сохранить" (или "Изменить")
        private void Click_Redact(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка на заполненность поля имени статуса
                if (string.IsNullOrEmpty(tb_Name.Text))
                {
                    MessageBox.Show("Введите наименование статуса"); // Сообщение об ошибке, если поле пустое
                    return; // Прерывание выполнения метода
                }

                // Если статус не был передан (т.е. мы добавляем новый)
                if (status == null)
                {
                    status = new Models.Status // Создание новой модели статуса
                    {
                        Name = tb_Name.Text // Установка имени статуса
                    };
                    MainStatus.StatusContext.Status.Add(status); // Добавление новой модели в контекст
                }
                else // Если статус уже существует (редактируем)
                {
                    status.Name = tb_Name.Text; // Обновление имени статуса
                }

                // Сохранение изменений в базе данных
                MainStatus.StatusContext.SaveChanges();
                // Переход на страницу со списком статусов
                MainWindow.init.OpenPages(new Pages.Status.Status());
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
                // Переход на страницу со списком статусов без сохранения изменений
                MainWindow.init.OpenPages(new Pages.Status.Status());
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
