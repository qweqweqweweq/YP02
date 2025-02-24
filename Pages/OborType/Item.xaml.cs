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

namespace YP02.Pages.OborType
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        OborType MainOborType;
        Models.OborType OborType;
        public Item(Models.OborType OborType, OborType MainOborType)
        {
            InitializeComponent();
            this.OborType = OborType;
            this.MainOborType = MainOborType;
            lb_Name.Content = OborType.Name;
        }

        private void Click_redact(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.OborType.Add(MainOborType, OborType));
        }

        private void Click_remove(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("При удалении отдела все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MainOborType.OborTypeContext.OborType.Remove(OborType);
                MainOborType.OborTypeContext.SaveChanges();
                (this.Parent as Panel).Children.Remove(this);
            }
            else MessageBox.Show("Действие отменено.");
        }
    }
}
