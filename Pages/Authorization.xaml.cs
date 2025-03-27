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

namespace YP02.Pages
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {

        public Authorization()
        {
            InitializeComponent();
        }

        private void AuthorizationClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string Login = login.Text;
                string Password = password.Password;

                if (string.IsNullOrWhiteSpace(Login))
                {
                    MessageBox.Show("Введите логин.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(Password))
                {
                    MessageBox.Show("Введите пароль.");
                    return;
                }

                using (var usersContext = new UsersContext())
                {
                    var user = usersContext.Users.FirstOrDefault(x => x.Login == Login && x.Password == Password);
                    if (user != null)
                    {
                        MainWindow.init.SetCurrentUser(user); // Сохраняем пользователя в MainWindow
                        MainWindow.init.OpenPages(new Menu());
                    }
                    else
                    {
                        MessageBox.Show("Некорректный ввод логина или пароля.");
                    }
                }
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
