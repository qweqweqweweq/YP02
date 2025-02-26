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

namespace YP02.Pages.Users
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        public Users MainUsers;
        public Models.Users users;
        public Add(Users MainUsers, Models.Users users = null)
        {
            InitializeComponent();
            this.MainUsers = MainUsers;
            this.users = users;
            if (users != null)
            {
                text1.Content = "Изменение пользователя";
                text2.Content = "Изменить";
                tb_FIO.Text = users.FIO;
                tb_Login.Text = users.Login;
                tb_Password.Text = users.Password;
                tb_Email.Text = users.Email;
                tb_Phone.Text = users.Number;
                tb_Role.Text = users.Role;
                tb_Address.Text = users.Address;
            }
            tb_Role.Items.Add("Администратор");
            tb_Role.Items.Add("Сотрудник");
            tb_Role.Items.Add("Преподаватель");
        }

        private void Click_Redact(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tb_FIO.Text))
            {
                MessageBox.Show("Введите ФИО пользователя");
                return;
            }
            if (string.IsNullOrEmpty(tb_Login.Text))
            {
                MessageBox.Show("Введите логин");
                return;
            }
            if (string.IsNullOrEmpty(tb_Password.Text))
            {
                MessageBox.Show("Введите пароль");
                return;
            }
            if (string.IsNullOrEmpty(tb_Phone.Text))
            {
                MessageBox.Show("Введите номер телефона");
                return;
            }
            if (string.IsNullOrEmpty(tb_Email.Text))
            {
                MessageBox.Show("Введите эл. почту");
                return;
            }
            if (string.IsNullOrEmpty(tb_Address.Text))
            {
                MessageBox.Show("Введите адрес");
                return;
            }
            if (tb_Role.SelectedItem == null)
            {
                MessageBox.Show("Выберите роль");
                return;
            }
            if (users == null)
            {
                users = new Models.Users
                {
                    FIO = tb_FIO.Text,
                    Login = tb_Login.Text,
                    Password = tb_Password.Text,
                    Number = tb_Phone.Text,
                    Email = tb_Email.Text,
                    Address = tb_Address.Text,
                    Role = tb_Role.Text
                };
                MainUsers.UsersContext.Users.Add(users);
            }
            else
            {
                users.FIO = tb_FIO.Text;
                users.Login = tb_Login.Text;
                users.Password = tb_Password.Text;
                users.Email = tb_Email.Text;
                users.Address = tb_Address.Text;
                users.Role = tb_Role.Text;
                users.Number = tb_Phone.Text;
            }
            MainUsers.UsersContext.SaveChanges();
            MainWindow.init.OpenPages(new Pages.Users.Users());
        }

        private void Click_Cancel_Redact(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Users.Users());
        }
    }
}
