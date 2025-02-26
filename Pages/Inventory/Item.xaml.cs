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
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        Inventory MainInventory;
        Models.Inventory inventory;
        UsersContext usersContext = new UsersContext();

        public Item(Models.Inventory inventory, Inventory MainInventory)
        {
            InitializeComponent();
            this.inventory = inventory;
            this.MainInventory = MainInventory;
            lb_Name.Content = inventory.Name;
            lb_DateStart.Content = inventory.StartDate;
            lb_DateEnd.Content = inventory.EndDate;
            lb_UserId.Content = usersContext.Users.Where(x => x.Id == inventory.Id).First().FIO;
        }

        private void Click_redact(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Inventory.Add(MainInventory, inventory));
        }

        private void Click_remove(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("При удалении инвентаризации все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MainInventory.InventoryContext.Inventory.Remove(inventory);
                MainInventory.InventoryContext.SaveChanges();
                (this.Parent as Panel).Children.Remove(this);
            }
            else MessageBox.Show("Действие отменено.");
        }
    }
}
