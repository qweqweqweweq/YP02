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
using YP02.Models;

namespace YP02.Pages.NetworkSettings
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        public NetworkSettings MainNetworkSettings;

        public Models.NetworkSettings networkSettings;

        OborudovanieContext obContext = new OborudovanieContext();

        public Add(NetworkSettings MainNetworkSettings, Models.NetworkSettings networkSettings = null)
        {
            InitializeComponent();
            this.MainNetworkSettings = MainNetworkSettings;
            this.networkSettings = networkSettings;

            if (networkSettings != null)
            {
                text1.Content = "Изменение сетевых настроек"; // Установка заголовка
                text2.Content = "Изменить"; // Изменение текста кнопки
                tb_IpAddress.Text = networkSettings.IpAddress; 
                tb_Mask.Text = networkSettings.SubnetMask; 
                tb_MainShluz.Text = networkSettings.MainGateway; 
                tb_DNSServers.Text = networkSettings.DNSServer1;
                tb_DNSServers1.Text = networkSettings.DNSServer2;
                tb_DNSServers2.Text = networkSettings.DNSServer3;
                tb_DNSServers3.Text = networkSettings.DNSServer4;
                cb_IdOb.SelectedItem = obContext.Oborudovanie.Where(x => x.Id == networkSettings.OborudovanieId).FirstOrDefault()?.Name;
            }

            foreach (var item in obContext.Oborudovanie)
            {
                cb_IdOb.Items.Add(item.Name);
            }
        }

        private void Click_Redact(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tb_IpAddress.Text))
                {
                    MessageBox.Show("Введите IP-адрес"); // Сообщение об ошибке, если поле пустое
                    return; // Прерывание выполнения метода
                }
                if (!IsValidIpAddress(tb_IpAddress.Text))
                {
                    MessageBox.Show("Введите корректный IP-адрес в формате XXX.XXX.XXX.XXX, где XXX от 0 до 255.");
                    return; // Прерывание выполнения метода
                }
                // Проверка уникальности IP-адреса
                if (MainNetworkSettings.NetworkSettingsContext.NetworkSettings.Any(ns => ns.IpAddress == tb_IpAddress.Text && (networkSettings == null || ns.Id != networkSettings.Id)))
                {
                    MessageBox.Show("Этот IP-адрес уже используется другим устройством.");
                    return;
                }
                if (string.IsNullOrEmpty(tb_Mask.Text))
                {
                    MessageBox.Show("Введите маску подсети"); // Сообщение об ошибке, если поле пустое
                    return; // Прерывание выполнения метода
                }
                if (!IsValidIpAddress(tb_Mask.Text))
                {
                    MessageBox.Show("Введите корректную маску подсети в формате XXX.XXX.XXX.XXX, где XXX от 0 до 255.");
                    return; // Прерывание выполнения метода
                }
                if (string.IsNullOrEmpty(tb_MainShluz.Text))
                {
                    MessageBox.Show("Введите главный шлюз"); // Сообщение об ошибке, если поле пустое
                    return; // Прерывание выполнения метода
                }
                if (!IsValidIpAddress(tb_MainShluz.Text))
                {
                    MessageBox.Show("Введите корректный главный шлюз в формате XXX.XXX.XXX.XXX, где XXX от 0 до 255.");
                    return; // Прерывание выполнения метода
                }
                if (string.IsNullOrEmpty(tb_DNSServers.Text))
                {
                    MessageBox.Show("Введите DNS-Server 1");
                    return; // Прерывание выполнения метода
                }
                if (!IsValidIpAddress(tb_DNSServers.Text))
                {
                    MessageBox.Show("Введите корректный DNS-Server 1 в формате XXX.XXX.XXX.XXX, где XXX от 0 до 255.");
                    return; // Прерывание выполнения метода
                }
                if (string.IsNullOrEmpty(tb_DNSServers1.Text))
                {
                    MessageBox.Show("Введите DNS-Server 2");
                    return; // Прерывание выполнения метода
                }
                if (!IsValidIpAddress(tb_DNSServers1.Text))
                {
                    MessageBox.Show("Введите корректный DNS-Server 2 в формате XXX.XXX.XXX.XXX, где XXX от 0 до 255.");
                    return; // Прерывание выполнения метода
                }
                if (string.IsNullOrEmpty(tb_DNSServers2.Text))
                {
                    MessageBox.Show("Введите DNS-Server 3 в формате XXX.XXX.XXX.XXX, где XXX от 0 до 255.\nЕсли он отсутствует, поставьте: -");
                    return; // Прерывание выполнения метода
                }
                if (string.IsNullOrEmpty(tb_DNSServers3.Text))
                {
                    MessageBox.Show("Введите DNS-Server 4 в формате XXX.XXX.XXX.XXX, где XXX от 0 до 255.\nЕсли он отсутствует, поставьте: -");
                    return; // Прерывание выполнения метода
                }
                if (cb_IdOb.SelectedItem == null)
                {
                    MessageBox.Show("Выберите оборудование");
                    return; // Прерывание выполнения метода
                }

                if (networkSettings == null)
                {
                    networkSettings = new Models.NetworkSettings();
                    networkSettings.IpAddress = tb_IpAddress.Text;
                    networkSettings.SubnetMask = tb_Mask.Text;
                    networkSettings.MainGateway = tb_MainShluz.Text;
                    networkSettings.DNSServer1 = tb_DNSServers.Text;
                    networkSettings.DNSServer2 = tb_DNSServers1.Text;
                    networkSettings.DNSServer3 = tb_DNSServers2.Text;
                    networkSettings.DNSServer4 = tb_DNSServers3.Text;
                    networkSettings.OborudovanieId = obContext.Oborudovanie.Where(x => x.Name == cb_IdOb.SelectedItem.ToString()).First().Id;
                    MainNetworkSettings.NetworkSettingsContext.NetworkSettings.Add(networkSettings);
                }
                else
                {
                    networkSettings.IpAddress = tb_IpAddress.Text;
                    networkSettings.SubnetMask = tb_Mask.Text;
                    networkSettings.MainGateway = tb_MainShluz.Text;
                    networkSettings.DNSServer1 = tb_DNSServers.Text;
                    networkSettings.DNSServer2 = tb_DNSServers1.Text;
                    networkSettings.DNSServer3 = tb_DNSServers2.Text;
                    networkSettings.DNSServer4 = tb_DNSServers3.Text;
                    networkSettings.OborudovanieId = obContext.Oborudovanie.Where(x => x.Name == cb_IdOb.SelectedItem.ToString()).First().Id;
                }

                MainNetworkSettings.NetworkSettingsContext.SaveChanges();
                MainWindow.init.OpenPages(new Pages.NetworkSettings.NetworkSettings());
            }
            catch (Exception ex)
            {
                try
                {
                    using (var errorsContext = new ErrorsContext())
                    {
                        var error = new Models.Errors
                        {
                            Message = ex.Message
                        };
                        errorsContext.Errors.Add(error);
                        errorsContext.SaveChanges(); // Сохраняем ошибку в базе данных
                    }

                    // Логирование ошибки в файл log.txt
                    string logPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "log.txt");
                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(logPath)); // Создаем папку bin, если ее нет
                    System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: {ex.Message}\n{ex.StackTrace}\n\n");
                }
                catch (Exception logEx)
                {
                    MessageBox.Show("Ошибка при записи в лог-файл: " + logEx.Message);
                }

                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void Click_Cancel_Redact(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow.init.OpenPages(new Pages.NetworkSettings.NetworkSettings());
            }
            catch (Exception ex)
            {
                try
                {
                    using (var errorsContext = new ErrorsContext())
                    {
                        var error = new Models.Errors
                        {
                            Message = ex.Message
                        };
                        errorsContext.Errors.Add(error);
                        errorsContext.SaveChanges(); // Сохраняем ошибку в базе данных
                    }

                    // Логирование ошибки в файл log.txt
                    string logPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "log.txt");
                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(logPath)); // Создаем папку bin, если ее нет
                    System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: {ex.Message}\n{ex.StackTrace}\n\n");
                }
                catch (Exception logEx)
                {
                    MessageBox.Show("Ошибка при записи в лог-файл: " + logEx.Message);
                }

                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private bool IsValidIpAddress(string ip)
        {
            if (string.IsNullOrEmpty(ip))
                return false;

            var parts = ip.Split('.');
            if (parts.Length != 4)
                return false;

            foreach (var part in parts)
            {
                if (!int.TryParse(part, out int number))
                    return false;

                if (number < 0 || number > 255)
                    return false;
            }

            return true;
        }
    }
}
