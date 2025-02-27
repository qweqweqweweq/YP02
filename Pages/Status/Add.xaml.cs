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

namespace YP02.Pages.Status
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        public Status MainStatus;
        public Models.Status status;
        public Add(Status MainStatus, Models.Status status)
        {
            InitializeComponent();
            this.MainStatus = MainStatus;
            this.status = status;
            if (status != null)
            {
                text1.Content = "Изменение статуса";
                text2.Content = "Изменить";
                tb_Name.Text = status.Name;
            }
        }

        private void Click_Redact(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tb_Name.Text))
            {
                MessageBox.Show("Введите наименование статуса");
                return;
            }
            if (status == null)
            {
                status = new Models.Status
                {
                    Name = tb_Name.Text
                };
                MainStatus.StatusContext.Status.Add(status);
            }
            else
            {
                status.Name = tb_Name.Text;
            }
            MainStatus.StatusContext.SaveChanges();
            MainWindow.init.OpenPages(new Pages.Status.Status());
        }

        private void Click_Cancel_Redact(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Status.Status());
        }
    }
}
