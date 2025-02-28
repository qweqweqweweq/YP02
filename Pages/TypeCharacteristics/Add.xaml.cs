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

namespace YP02.Pages.TypeCharacteristics
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        public TypeCharacteristics MainTypeCharacteristics;
        public Models.TypeCharacteristics typeCharacteristics;

        public Add(TypeCharacteristics MainTypeCharacteristics, Models.TypeCharacteristics typeCharacteristics = null)
        {
            InitializeComponent();
            this.MainTypeCharacteristics = MainTypeCharacteristics;
            this.typeCharacteristics = typeCharacteristics;
            if (typeCharacteristics != null)
            {
                text1.Content = "Изменение типа характеристики";
                text2.Content = "Изменить";
                tb_Name.Text = typeCharacteristics.Name;
            }
        }

        private void Click_Redact(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tb_Name.Text))
            {
                MessageBox.Show("Введите наименование типа характеристики");
                return;
            }
            if (typeCharacteristics == null)
            {
                typeCharacteristics = new Models.TypeCharacteristics();
                typeCharacteristics.Name = tb_Name.Text;
                //MainTypeCharacteristics..Characteristics.Add(typeCharacteristics);
            }
            else
            {
                typeCharacteristics.Name = tb_Name.Text;
            }
            //MainTypeCharacteristics.characteristicsContext.SaveChanges();
            MainWindow.init.OpenPages(new Pages.TypeCharacteristics.TypeCharacteristics());
        }

        private void Click_Cancel_Redact(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.TypeCharacteristics.TypeCharacteristics());
        }
    }
}
