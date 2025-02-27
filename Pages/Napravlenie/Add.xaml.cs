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
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        public Napravlenie MainNapravlenie;
        public Models.Napravlenie napravlenie;
        public Add(Napravlenie MainNapravlenie, Models.Napravlenie napravlenie)
        {
            InitializeComponent();
            this.MainNapravlenie = MainNapravlenie;
            this.napravlenie = napravlenie;
            if (napravlenie != null )
            {
                text1.Content = "Изменение направления";
                text2.Content = "Изменить";
                tb_Name.Text = napravlenie.Name;
            }
        }

        private void Click_Redact(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tb_Name.Text))
            {
                MessageBox.Show("Введите наименование направления");
                return;
            }
            if (napravlenie == null)
            {
                napravlenie = new Models.Napravlenie
                {
                    Name = tb_Name.Text
                };
                MainNapravlenie.NapravlenieContext.Napravlenie.Add(napravlenie);
            }
            else
            {
                napravlenie.Name = tb_Name.Text;
            }
            MainNapravlenie.NapravlenieContext.SaveChanges();
            MainWindow.init.OpenPages(new Pages.Napravlenie.Napravlenie());
        }

        private void Click_Cancel_Redact(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Napravlenie.Napravlenie());
        }
    }
}
