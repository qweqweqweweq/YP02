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

namespace YP02.Pages.Inventory
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        public Inventory MainInventory;
        public Models.Inventory inventory;
        UsersContext usersContext = new UsersContext();

        public Add(Inventory MainInventory, Models.Inventory inventory = null)
        {
            InitializeComponent();
            this.MainInventory = MainInventory;
            this.inventory = inventory;
            if (inventory != null)
            {
                text1.Content = "Изменение инвентаризации";
                text2.Content = "Изменить";
                tb_Name.Text = inventory.Name;
                tb_DateStart.Text = inventory.StartDate;
                tb_DateEnd.Text = inventory.EndDate;
                cb_IdUser.SelectedItem = usersContext.Users.Where(x => x.Id == inventory.Id).FirstOrDefault().FIO;
            }
            foreach (var item in usersContext.Users)
            {
                cb_IdUser.Items.Add(item.FIO);
            }
        }

        private void Click_Redact(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tb_Name.Text))
            {
                MessageBox.Show("Введите наименование инвентаризации");
                return;
            }
            if (string.IsNullOrEmpty(tb_DateStart.Text))
            {
                MessageBox.Show("Введите дату начала инвентаризации");
                return;
            }
            if (string.IsNullOrEmpty(tb_DateEnd.Text))
            {
                MessageBox.Show("Выберите дату окончания инвентаризации");
                return;
            }
            if (cb_IdUser.SelectedItem == null)
            {
                MessageBox.Show("Выберите пользователя");
                return;
            }
            if (inventory == null)
            {
                inventory = new Models.Inventory();
                inventory.Name = tb_Name.Text;
                inventory.StartDate = tb_DateStart.Text;
                inventory.EndDate = tb_DateEnd.Text;
                inventory.UserId = usersContext.Users.Where(x => x.FIO == cb_IdUser.SelectedItem).First().Id;
                MainInventory.InventoryContext.Inventory.Add(inventory);
            }
            else
            {
                inventory.Name = tb_Name.Text;
                inventory.StartDate = tb_DateStart.Text;
                inventory.EndDate = tb_DateEnd.Text;
                inventory.UserId = usersContext.Users.Where(x => x.FIO == cb_IdUser.SelectedItem).First().Id;
            }
            MainInventory.InventoryContext.SaveChanges();
            MainWindow.init.OpenPages(new Pages.Inventory.Inventory());
        }

        private void Click_Cancel_Redact(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Inventory.Inventory());
        }
    }
}
