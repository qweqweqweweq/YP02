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

namespace YP02.Pages.TypeRasxod
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        TypeRasxod MainTypeRasxod;
        Models.TypeRasxod TypeRasxod;
        public Item(Models.TypeRasxod TypeRasxod, TypeRasxod MainTypeRasxod)
        {
            InitializeComponent();
            this.TypeRasxod = TypeRasxod;
            this.MainTypeRasxod = MainTypeRasxod;
            lb_Name.Content = TypeRasxod.Name;
        }

        private void Click_redact(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.TypeRasxod.Add(MainTypeRasxod, TypeRasxod));
        }

        private void Click_remove(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("При удалении все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MainTypeRasxod.TypeRasxodContext.TypeRasxod.Remove(TypeRasxod);
                MainTypeRasxod.TypeRasxodContext.SaveChanges();
                (this.Parent as Panel).Children.Remove(this);
            }
            else MessageBox.Show("Действие отменено.");
        }
    }
}
