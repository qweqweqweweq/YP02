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
            string Login = login.Text;
            string Password = password.Password;
            if (Login != "")
            {
                if (Password != "")
                {
                    using (var usersContext = new UsersContext())
                    {
                        var user = usersContext.Users.FirstOrDefault(x => x.Login == Login && x.Password == Password);
                        if (user != null)
                        {
                            MainWindow.init.OpenPages(new Menu());
                        }
                        else
                        {
                            MessageBox.Show("Некорректный ввод логина или пароля.");
                        }
                    }
                }
                else MessageBox.Show("Введите пароль.");
            }
            else MessageBox.Show("Введите логин.");
        }
    }
}
