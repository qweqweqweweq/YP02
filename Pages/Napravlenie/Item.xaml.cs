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

namespace YP02.Pages.Napravlenie
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        Napravlenie MainNapravlenie;
        Models.Napravlenie Napravlenie;
        public Item(Models.Napravlenie Napravlenie, Napravlenie MainNapravlenie)
        {
            InitializeComponent();
            this.Napravlenie = Napravlenie;
            this.MainNapravlenie = MainNapravlenie;
            lb_Name.Content = Napravlenie.Name;
        }

        private void Click_redact(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Napravlenie.Add(MainNapravlenie, Napravlenie));
        }

        private void Click_remove(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("При удалении отдела все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MainNapravlenie.NapravlenieContext.Napravlenie.Remove(Napravlenie);
                MainNapravlenie.NapravlenieContext.SaveChanges();
                (this.Parent as Panel).Children.Remove(this);
            }
            else MessageBox.Show("Действие отменено.");
        }
    }
}
