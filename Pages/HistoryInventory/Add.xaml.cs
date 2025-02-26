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

namespace YP02.Pages.HistoryInventory
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        public HistoryInventory MainHistoryInventory;
        public Models.HistoryInventory historyInventory;
        public Models.Oborudovanie oborudovanie;
        OborudovanieContext oborudovanieContext = new OborudovanieContext();
        UsersContext usersContext = new UsersContext();

        public Add(HistoryInventory MainHistoryInventory, Models.HistoryInventory historyInventory = null)
        {
            InitializeComponent();
            this.MainHistoryInventory = MainHistoryInventory;
            this.historyInventory = historyInventory;
            if (historyInventory != null )
            {
                text1.Content = "Изменение истории инвентаризации";
                text2.Content = "Изменить";
                cb_Oborud.SelectedItem = oborudovanieContext.Oborudovanie.Where(x => x.Id == historyInventory.Id).FirstOrDefault().Name;
                cb_User.SelectedItem = usersContext.Users.Where(x => x.Id == historyInventory.Id).FirstOrDefault().FIO;
            }
            foreach (var item in usersContext.Users)
            {
                cb_User.Items.Add(item.FIO);
            }
            foreach (var item in oborudovanieContext.Oborudovanie)
            {
                cb_Oborud.Items.Add(item.Name);
            }
        }

        private void Click_Redact(object sender, RoutedEventArgs e)
        {
            if (cb_Oborud.SelectedItem == null)
            {
                MessageBox.Show("Выберите оборудование");
                return;
            }
            if (cb_User.SelectedItem == null)
            {
                MessageBox.Show("Выберите пользователя");
                return;
            }
            if (historyInventory == null)
            {
                historyInventory = new Models.HistoryInventory();
                historyInventory.OborId = oborudovanieContext.Oborudovanie.Where(x => x.Name == cb_Oborud.SelectedItem).First().Id;
                historyInventory.IdUser = usersContext.Users.Where(x => x.FIO == cb_User.SelectedItem).First().Id;
                MainHistoryInventory.HistoryInventoryContext.HistoryInventory.Add(historyInventory);
            }
            else
            {
                historyInventory.OborId = oborudovanieContext.Oborudovanie.Where(x => x.Name == cb_Oborud.SelectedItem).First().Id;
                historyInventory.IdUser = usersContext.Users.Where(x => x.FIO == cb_User.SelectedItem).First().Id;
            }
            MainHistoryInventory.HistoryInventoryContext.SaveChanges();
            MainWindow.init.OpenPages(new Pages.HistoryInventory.HistoryInventory());
        }

        private void Click_Cancel_Redact(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.HistoryInventory.HistoryInventory());
        }
    }
}
