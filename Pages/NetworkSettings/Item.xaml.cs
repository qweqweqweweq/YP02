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

namespace YP02.Pages.NetworkSettings
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        NetworkSettings MainNetworkSettings;
        Models.NetworkSettings networkSettings;

        OborudovanieContext obContext = new OborudovanieContext();

        public Item(Models.NetworkSettings networkSettings, NetworkSettings MainNetworkSettings)
        {
            InitializeComponent();
            this.networkSettings = networkSettings;
            this.MainNetworkSettings = MainNetworkSettings;

            lb_IpAddress.Content = "IP-адрес: " + networkSettings.IpAddress;
            lb_SubnetMask.Content = "Маска подсети: " + networkSettings.SubnetMask;
            lb_MainGateway.Content = "Главный шлюз: " + networkSettings.MainGateway;
            lb_DNSServer1.Content = "DNS-Server №1: " + networkSettings.DNSServer1;
            lb_DNSServer2.Content = "DNS-Server №2: " + networkSettings.DNSServer2;
            lb_DNSServer3.Content = "DNS-Server №3: " + networkSettings.DNSServer3;
            lb_DNSServer4.Content = "DNS-Server №4: " + networkSettings.DNSServer4;

            lb_OborudovanieId.Content = obContext.Oborudovanie.Where(x => x.Id == networkSettings.OborudovanieId).FirstOrDefault()?.Name;
        }

        private void Click_redact(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.NetworkSettings.Add(MainNetworkSettings, networkSettings));
        }

        private void Click_remove(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("При удалении сетевых настроек все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                MainNetworkSettings.NetworkSettingsContext.NetworkSettings.Remove(networkSettings);
                MainNetworkSettings.NetworkSettingsContext.SaveChanges(); // Сохранение изменений в базе данных

                // Удаление текущего элемента из родительского контейнера
                (this.Parent as Panel).Children.Remove(this);
            }
            else
            {
                // Сообщение о том, что действие отменено
                MessageBox.Show("Действие отменено.");
            }
        }
    }
}
