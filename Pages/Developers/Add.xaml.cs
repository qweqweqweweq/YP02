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

namespace YP02.Pages.Developers
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        public Developers MainDevelopers;
        public Models.Developers developers;
        public Add(Developers MainDevelopers, Models.Developers developers = null)
        {
            InitializeComponent();
            this.MainDevelopers = MainDevelopers;
            this.developers = developers;
            if (developers != null)
            {
                tb_Name.Text = developers.Name;
            }
        }

        private void Click_Redact(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tb_Name.Text))
            {
                MessageBox.Show("Введите название типа оборудования");
                return;
            }
            if (developers == null)
            {
                developers = new Models.Developers();
                developers.Name = tb_Name.Text;
                MainDevelopers.DevelopersContext.Developers.Add(developers);
            }
            else
            {
                developers.Name = tb_Name.Text;
            }
            MainDevelopers.DevelopersContext.SaveChanges();
            MainWindow.init.OpenPages(new Pages.Developers.Developers());
        }

        private void Click_Cancel_Redact(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Developers.Developers());
        }
    }
}
