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

namespace YP02.Pages.ViewModel
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        public ViewModel MainViewModel;
        public Models.ViewModel viewModel;
        OborTypeContext oborTypeContext = new OborTypeContext();
        public Add(ViewModel MainViewModel, Models.ViewModel viewModel = null)
        {
            InitializeComponent();
            this.MainViewModel = MainViewModel;
            this.viewModel = viewModel;
            if (viewModel != null)
            {
                tb_Name.Text = viewModel.Name;
                cm_OborType.SelectedItem = oborTypeContext.OborType.Where(x => x.Id == viewModel.Id).First().Name;
            }
            foreach(var item in oborTypeContext.OborType)
            {
                cm_OborType.Items.Add(item.Name);
            }
        }

        private void Click_Redact(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tb_Name.Text))
            {
                MessageBox.Show("Введите название типа оборудования");
                return;
            }
            if (cm_OborType.SelectedItem == null)
            {
                MessageBox.Show("Выберите тип оборудвания");
                return;
            }
            if (viewModel == null)
            {
                viewModel = new Models.ViewModel();
                viewModel.Name = tb_Name.Text;
                viewModel.OborType = oborTypeContext.OborType.Where(x => x.Name == cm_OborType.SelectedItem).First().Id;
                MainViewModel.ViewModelContext.ViewModel.Add(viewModel);
            }
            else
            {
                viewModel.Name = tb_Name.Text;
                viewModel.OborType = oborTypeContext.OborType.Where(x => x.Name == cm_OborType.SelectedItem).First().Id;
            }
            MainViewModel.ViewModelContext.SaveChanges();
            MainWindow.init.OpenPages(new Pages.ViewModel.ViewModel());
        }

        private void Click_Cancel_Redact(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.ViewModel.ViewModel());
        }
    }
}
