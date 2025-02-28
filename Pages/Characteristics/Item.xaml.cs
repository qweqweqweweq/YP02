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

namespace YP02.Pages.Characteristics
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        Characteristics MainCharacteristics;
        Models.Characteristics characteristics;
        TypeCharacteristicsContext typeCharacteristicsContext = new TypeCharacteristicsContext();

        public Item(Models.Characteristics characteristics, Characteristics MainCharacteristics)
        {
            InitializeComponent();
            this.characteristics = characteristics;
            this.MainCharacteristics = MainCharacteristics;
            lb_Name.Content = characteristics.Name;
            lb_TypeChar.Content = typeCharacteristicsContext.TypeCharacteristics.Where(x => x.Id == characteristics.TypeCharacter).First().Name;
        }

        private void Click_redact(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Characteristics.Add(MainCharacteristics, characteristics));
        }

        private void Click_remove(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("При удалении характеристики все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MainCharacteristics.characteristicsContext.Characteristics.Remove(characteristics);
                MainCharacteristics.characteristicsContext.SaveChanges();
                (this.Parent as Panel).Children.Remove(this);
            }
            else MessageBox.Show("Действие отменено.");
        }
    }
}
