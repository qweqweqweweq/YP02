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

namespace YP02.Pages.Programs
{
    /// <summary>
    /// Логика взаимодействия для Programs.xaml
    /// </summary>
    public partial class Programs : Page
    {
        public ProgramsContext ProgramsContext = new ProgramsContext();
        public Programs()
        {
            InitializeComponent();
            parent.Children.Clear();
            foreach (Models.Programs item in ProgramsContext.Programs)
            {
                parent.Children.Add(new Item(item, this));
            }
        }

        private void KeyDown_Search(object sender, KeyEventArgs e)
        {
            string searchText = search.Text.ToLower();
            var result = ProgramsContext.Programs.Where(x =>
                x.Name.ToLower().Contains(searchText)
            );
            parent.Children.Clear();
            foreach (var item in result)
            {
                parent.Children.Add(new Item(item, this));
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Menu());
        }

        private void SortUp(object sender, RoutedEventArgs e)
        {
            var sortUp = ProgramsContext.Programs.OrderBy(x => x.Name);
            parent.Children.Clear();
            foreach (var program in sortUp)
            {
                parent.Children.Add(new Item(program, this));
            }
        }

        private void SortDown(object sender, RoutedEventArgs e)
        {
            var sortDown = ProgramsContext.Programs.OrderByDescending(x => x.Name);
            parent.Children.Clear();
            foreach (var program in sortDown)
            {
                parent.Children.Add(new Item(program, this));
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Programs.Add(this, null));
        }
    }
}
