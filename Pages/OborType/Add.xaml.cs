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

namespace YP02.Pages.OborType
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        // Поле для хранения ссылки на основной объект типа оборудования
        public OborType MainOborType;

        // Поле для хранения информации о конкретном типе оборудования
        public Models.OborType oborType;

        public Add(OborType MainOborType, Models.OborType oborType = null)
        {
            InitializeComponent();

            // Присваивание переданных параметров полям класса
            this.MainOborType = MainOborType;
            this.oborType = oborType;

            // Если объект типа оборудования не равен null, заполняем текстовое поле его именем
            if (oborType != null)
            {
                lb_title.Content = "Изменение типа оборудования";
                bt_click.Content = "Изменить";
                tb_Name.Text = oborType.Name;
            }
        }

        // Обработчик события нажатия кнопки "Редактировать"
        private void Click_Redact(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка, введено ли название типа оборудования
                if (string.IsNullOrEmpty(tb_Name.Text))
                {
                    // Если нет, выводим сообщение об ошибке
                    MessageBox.Show("Введите название типа оборудования");
                    return; // Прерываем выполнение метода
                }

                // Если объект типа оборудования равен null, создаем новый объект
                if (oborType == null)
                {
                    oborType = new Models.OborType();
                    oborType.Name = tb_Name.Text; // Устанавливаем имя типа оборудования
                    MainOborType.OborTypeContext.OborType.Add(oborType); // Добавляем новый тип оборудования в контекст
                }
                else
                {
                    // Если объект типа оборудования существует, обновляем его имя
                    oborType.Name = tb_Name.Text;
                }

                // Сохраняем изменения в контексте типов оборудования
                MainOborType.OborTypeContext.SaveChanges();

                // Открываем страницу со списком типов оборудования
                MainWindow.init.OpenPages(new Pages.OborType.OborType());
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
                // Открываем страницу со списком типов оборудования без сохранения изменений
                MainWindow.init.OpenPages(new Pages.OborType.OborType());
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
