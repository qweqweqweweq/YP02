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
        Models.OborType OborType;
        public Item(Models.OborType OborType)
        {
            InitializeComponent();
            this.OborType = OborType;
            lb_Name.Content = OborType.Name;
        }

        private void Click_redact(object sender, RoutedEventArgs e)
        {

        }

        private void Click_remove(object sender, RoutedEventArgs e)
        {

        }
    }
}
