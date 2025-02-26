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

namespace YP02.Pages.Users
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        Users MainUsers;
        Models.Users Users;
        public Item(Models.Users Users, Users MainUsers)
        {
            InitializeComponent();
            this.Users = Users;
            this.MainUsers = MainUsers;
            lb_FIO.Content = Users.FIO;
            lb_Role.Content = Users.Role;
            lb_Number.Content = Users.Number;
            lb_Email.Content = Users.Email;
            lb_Address.Content = Users.Address;
        }

        private void Click_redact(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Users.Add(MainUsers, Users));
        }

        private void Click_remove(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("При удалении пользователя все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MainUsers.UsersContext.Users.Remove(Users);
                MainUsers.UsersContext.SaveChanges();
                (this.Parent as Panel).Children.Remove(this);
            }
            else MessageBox.Show("Действие отменено.");
        }
    }
}
