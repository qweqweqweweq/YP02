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

namespace YP02.Pages.TypeRasxod
{
    /// <summary>
    /// Логика взаимодействия для TypeRasxod.xaml
    /// </summary>
    public partial class TypeRasxod : Page
    {
        public TypeRasxodContext TypeRasxodContext = new TypeRasxodContext();
        public TypeRasxod()
        {
            InitializeComponent();
            parent.Children.Clear();
            foreach (Models.TypeRasxod item in TypeRasxodContext.TypeRasxod)
            {
                parent.Children.Add(new Item(item, this));
            }
            parent.Children.Add(new Add_itm(new Add(this, null)));
        }

        private void KeyDown_Search(object sender, KeyEventArgs e)
        {
            
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Menu());
        }
    }
}
