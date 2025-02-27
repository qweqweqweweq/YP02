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

namespace YP02.Pages.Auditories
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        Auditories MainAuditories;
        Models.Auditories Auditories;
        UsersContext usersContext = new();
        public Item(Models.Auditories Auditories, Auditories MainAuditories)
        {
            InitializeComponent();
            this.Auditories = Auditories;
            this.MainAuditories = MainAuditories;
            lb_Name.Content = Auditories.Name;
            lb_sokrName.Content = Auditories.ShortName;
            lb_User.Content = "Ответственный: " + usersContext.Users.Where(x => x.Id == Auditories.ResponUser).First().FIO;
            lb_tempUser.Content = "Временно-ответственный: " + usersContext.Users.Where(x => x.Id == Auditories.TimeResponUser).First().FIO;
        }

        private void Click_redact(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Auditories.Add(MainAuditories, Auditories));
        }

        private void Click_remove(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("При удалении аудитории все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MainAuditories.auditoriesContext.Auditories.Remove(Auditories);
                MainAuditories.auditoriesContext.SaveChanges();
                (this.Parent as Panel).Children.Remove(this);
            }
            else MessageBox.Show("Действие отменено.");
        }
    }
}
