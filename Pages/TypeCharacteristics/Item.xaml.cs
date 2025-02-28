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

namespace YP02.Pages.TypeCharacteristics
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        TypeCharacteristics MainTypeCharacteristics;
        Models.TypeCharacteristics typeCharacteristics;

        public Item(Models.TypeCharacteristics typeCharacteristics, TypeCharacteristics MainTypeCharacteristics)
        {
            InitializeComponent();
            this.typeCharacteristics = typeCharacteristics;
            this.MainTypeCharacteristics = MainTypeCharacteristics;
            lb_Name.Content = typeCharacteristics.Name;
        }

        private void Click_redact(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.TypeCharacteristics.Add(MainTypeCharacteristics, typeCharacteristics));
        }

        private void Click_remove(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("При удалении типа характеристики все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                //MainTypeCharacteristics..Characteristics.Remove(typeCharacteristics);
                //MainTypeCharacteristics.characteristicsContext.SaveChanges();
                (this.Parent as Panel).Children.Remove(this);
            }
            else MessageBox.Show("Действие отменено.");
        }
    }
}
