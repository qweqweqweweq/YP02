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
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        public TypeRasxod MainTypeRasxod;
        public Models.TypeCharacteristics typeRasxod;
        public Add(TypeRasxod MainTypeRasxod, Models.TypeCharacteristics typeRasxod = null)
        {
            InitializeComponent();
            this.MainTypeRasxod = MainTypeRasxod;
            this.typeRasxod = typeRasxod;
            if (typeRasxod != null)
            {
                lb_title.Content = "Изменение типа расходов";
                bt_click.Content = "Изменить";
                tb_Name.Text = typeRasxod.Name;
            }
        }

        private void Click_Redact(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tb_Name.Text))
            {
                MessageBox.Show("Введите название типа оборудования");
                return;
            }
            if (typeRasxod == null)
            {
                typeRasxod = new Models.TypeCharacteristics();
                typeRasxod.Name = tb_Name.Text;
                MainTypeRasxod.TypeRasxodContext.TypeRasxod.Add(typeRasxod);
            }
            else
            {
                typeRasxod.Name = tb_Name.Text;
            }
            MainTypeRasxod.TypeRasxodContext.SaveChanges();
            MainWindow.init.OpenPages(new Pages.TypeRasxod.TypeRasxod());
        }

        private void Click_Cancel_Redact(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.TypeRasxod.TypeRasxod());
        }
    }
}
