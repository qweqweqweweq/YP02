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
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        Programs MainPrograms;
        Models.Programs Programs;
        DevelopersContext developersContext = new DevelopersContext();
        OborudovanieContext oborudovanieContext = new OborudovanieContext();
        public Item(Models.Programs Programs, Programs MainPrograms)
        {
            InitializeComponent();
            this.Programs = Programs;
            this.MainPrograms = MainPrograms;
            lb_Name.Content = Programs.Name;
            lb_VersionPO.Content = Programs.VersionPO;
            lb_Developer.Content = developersContext.Developers.Where(x => x.Id == Programs.Id).First().Name;
            lb_Obor.Content = oborudovanieContext.Oborudovanie.Where(x => x.Id == Programs.Id).First().Name;
        }

        private void Click_redact(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Programs.Add(MainPrograms, Programs));
        }

        private void Click_remove(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("При удалении все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MainPrograms.ProgramsContext.Programs.Remove(Programs);
                MainPrograms.ProgramsContext.SaveChanges();
                (this.Parent as Panel).Children.Remove(this);
            }
            else MessageBox.Show("Действие отменено.");
        }
    }
}
