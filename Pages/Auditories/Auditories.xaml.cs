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

namespace YP02.Pages.Auditories
{
    /// <summary>
    /// Логика взаимодействия для Auditories.xaml
    /// </summary>
    public partial class Auditories : Page
    {
        public AuditoriesContext auditoriesContext = new AuditoriesContext();
        public Auditories()
        {
            InitializeComponent();
            parent.Children.Clear();
            foreach (Models.Auditories item in auditoriesContext.Auditories)
            {
                parent.Children.Add(new Item(item, this));
            }
        }

        private void KeyDown_Search(object sender, KeyEventArgs e)
        {
            string searchText = search.Text.ToLower();
            var result = auditoriesContext.Auditories.Where(x =>
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
            var sortUp = auditoriesContext.Auditories.OrderBy(x => x.Name);
            parent.Children.Clear();
            foreach (var auditories in sortUp)
            {
                parent.Children.Add(new Item(auditories, this));
            }
        }

        private void SortDown(object sender, RoutedEventArgs e)
        {
            var sortDown = auditoriesContext.Auditories.OrderByDescending(x => x.Name);
            parent.Children.Clear();
            foreach (var auditories in sortDown)
            {
                parent.Children.Add(new Item(auditories, this));
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Auditories.Add(this, null));
        }
    }
}
