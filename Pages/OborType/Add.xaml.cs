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
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        public OborType MainOborType;
        public Models.OborType oborType;
        public Add(OborType MainOborType, Models.OborType oborType = null)
        {
            InitializeComponent();
            this.MainOborType = MainOborType;
            this.oborType = oborType;
            if(oborType != null)
            {
                tb_Name.Text = oborType.Name;
            }
        }

        private void Click_Redact(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tb_Name.Text))
            {
                MessageBox.Show("Введите название типа оборудования");
                return;
            }
            if(oborType == null)
            {
                oborType = new Models.OborType();
                oborType.Name = tb_Name.Text;
                MainOborType.OborTypeContext.OborType.Add(oborType);
            }
            else
            {
                oborType.Name = tb_Name.Text;
            }
            MainOborType.OborTypeContext.SaveChanges();
            MainWindow.init.OpenPages(new Pages.OborType.OborType());
        }

        private void Click_Cancel_Redact(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.OborType.OborType());
        }
    }
}
