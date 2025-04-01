using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
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
                Models.Users? user;

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
                    user = usersContext.Users.FirstOrDefault(x => x.Login == Login && x.Password == Password);
                }
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
            catch (Exception ex)
            {
                LogError("Ошибка", ex).ConfigureAwait(false);
            }
        }

        private async Task LogError(string message, Exception ex)
        {
            Debug.WriteLine($"{message}: {ex.Message}");

            try
            {
                await using (var errorsContext = new ErrorsContext())
                {
                    errorsContext.Errors.Add(new Models.Errors { Message = ex.Message });
                    await errorsContext.SaveChangesAsync();
                }
                string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "log.txt");
                Directory.CreateDirectory(Path.GetDirectoryName(logPath) ?? string.Empty);

                await File.AppendAllTextAsync(logPath, $"{DateTime.Now}: {ex.Message}\n{ex.StackTrace}\n\n");
            }
            catch (Exception logEx)
            {
                Debug.WriteLine($"Ошибка при записи в лог-файл: {logEx.Message}");
            }
        }
    }
}
