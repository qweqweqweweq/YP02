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

namespace YP02.Pages.Programs
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        public Programs MainPrograms;
        public Models.Programs programs;
        DevelopersContext developersContext = new DevelopersContext();
        OborudovanieContext oborudovanieContext = new OborudovanieContext();
        public Add(Programs MainPrograms, Models.Programs programs = null)
        {
            InitializeComponent();
            this.MainPrograms = MainPrograms;
            this.programs = programs;
            if (programs != null)
            {
                tb_Name.Text = programs.Name;
                tb_VersionPO.Text = programs.VersionPO;
                cm_DeveloperId.SelectedItem = developersContext.Developers.Where(x => x.Id == programs.Id).FirstOrDefault().Name;
                cm_OborId.SelectedItem = oborudovanieContext.Oborudovanie.Where(x => x.Id == programs.Id).FirstOrDefault().Name;
            }
            foreach (var item in developersContext.Developers)
            {
                cm_DeveloperId.Items.Add(item.Name);
            }
        }

        private void Click_Redact(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tb_Name.Text))
            {
                MessageBox.Show("Введите название программы");
                return;
            }
            if (string.IsNullOrEmpty(tb_VersionPO.Text))
            {
                MessageBox.Show("Введите версию ПО");
                return;
            }
            if (cm_DeveloperId.SelectedItem == null)
            {
                MessageBox.Show("Выберите разработчика");
                return;
            }
            if (programs == null)
            {
                programs = new Models.Programs();
                programs.Name = tb_Name.Text;
                programs.VersionPO = tb_VersionPO.Text;
                programs.DeveloperId = developersContext.Developers.Where(x => x.Name == cm_DeveloperId.SelectedItem).First().Id;
                programs.OborId = oborudovanieContext.Oborudovanie.Where(x => x.Name == cm_OborId.SelectedItem).First().Id;
                MainPrograms.ProgramsContext.Programs.Add(programs);
            }
            else
            {
                programs.Name = tb_Name.Text;
                programs.VersionPO = tb_VersionPO.Text;
                programs.DeveloperId = developersContext.Developers.Where(x => x.Name == cm_DeveloperId.SelectedItem).First().Id;
                programs.OborId = oborudovanieContext.Oborudovanie.Where(x => x.Name == cm_OborId.SelectedItem).First().Id;
            }
            MainPrograms.ProgramsContext.SaveChanges();
            MainWindow.init.OpenPages(new Pages.Programs.Programs());
        }

        private void Click_Cancel_Redact(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Programs.Programs());
        }
    }
}
