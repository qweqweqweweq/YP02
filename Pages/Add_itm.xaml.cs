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

namespace YP02.Pages
{
    /// <summary>
    /// Логика взаимодействия для Add_itm.xaml
    /// </summary>
    public partial class Add_itm : UserControl
    {
        Page page_str;
        public Add_itm(Page _page_str)
        {
            InitializeComponent();
            page_str = _page_str;
        }

        private void Click_add(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(page_str);
        }
    }
}
