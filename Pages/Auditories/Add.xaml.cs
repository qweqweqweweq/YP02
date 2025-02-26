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
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        public Auditories MainAuditories;
        public Models.Auditories auditories;
        UsersContext usersContext = new();
        public Add(Auditories MainAuditories, Models.Auditories auditories = null)
        {
            InitializeComponent();
            this.MainAuditories = MainAuditories;
            this.auditories = auditories;
            if (auditories != null)
            {
                text1.Content = "Изменение аудитории";
                text2.Content = "Изменить";
                tb_Name.Text = auditories.Name;
                tb_shortName.Text = auditories.ShortName;
                tb_User.SelectedItem = usersContext.Users.Where(x => x.Id == auditories.ResponUser).FirstOrDefault().FIO;
                tb_tempUser.SelectedItem = usersContext.Users.Where(x => x.Id == auditories.TimeResponUser).FirstOrDefault().FIO;
            }
            foreach (var item in usersContext.Users)
            {
                tb_User.Items.Add(item.FIO);
                tb_tempUser.Items.Add(item.FIO);
            }
        }

        private void Click_Redact(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tb_Name.Text))
            {
                MessageBox.Show("Введите наименование аудитории");
                return;
            }
            if (string.IsNullOrEmpty(tb_shortName.Text))
            {
                MessageBox.Show("Введите сокращённое наименование аудитории");
                return;
            }
            if (tb_User.SelectedItem == null)
            {
                MessageBox.Show("Выберите ответственного пользователя");
                return;
            }
            if (tb_tempUser.SelectedItem == null)
            {
                MessageBox.Show("Выберите временно-ответственного пользователя");
                return;
            }
            if (auditories == null)
            {
                auditories = new Models.Auditories
                {
                    Name = tb_Name.Text,
                    ShortName = tb_shortName.Text,
                    ResponUser = usersContext.Users.Where(x => x.FIO == tb_User.SelectedItem).First().Id,
                    TimeResponUser = usersContext.Users.Where(x => x.FIO == tb_tempUser.SelectedItem).First().Id
                };
                MainAuditories.AuditoriesContext.Auditories.Add(auditories);
            }
            else
            {
                auditories.Name = tb_Name.Text;
                auditories.ShortName = tb_shortName.Text;
                auditories.ResponUser = usersContext.Users.Where(x => x.FIO == tb_User.SelectedItem).First().Id;
                auditories.TimeResponUser = usersContext.Users.Where(x => x.FIO == tb_tempUser.SelectedItem).First().Id;
            }
            MainAuditories.AuditoriesContext.SaveChanges();
            MainWindow.init.OpenPages(new Pages.Auditories.Auditories());
        }

        private void Click_Cancel_Redact(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Auditories.Auditories());
        }
    }
}
