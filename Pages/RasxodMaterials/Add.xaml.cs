﻿using System;
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

namespace YP02.Pages.RasxodMaterials
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        public RasxodMaterials MainRasxodMaterials;
        public Models.RasxodMaterials rasxodMaterials;
        UsersContext usersContext = new UsersContext();
        TypeRasxodContext typeRasxodContext = new TypeRasxodContext();
        MainCharacMaterialsContext mainCharacMaterialsContext = new MainCharacMaterialsContext();

        public Add(RasxodMaterials MainRasxodMaterials, Models.RasxodMaterials rasxodMaterials = null)
        {
            InitializeComponent();
            this.MainRasxodMaterials = MainRasxodMaterials;
            this.rasxodMaterials = rasxodMaterials;
            if (rasxodMaterials != null)
            {
                text1.Content = "Изменение расходного материала";
                text2.Content = "Изменить";
                tb_Name.Text = rasxodMaterials.Name;
                tb_Des.Text = rasxodMaterials.Description;
                tb_DatePost.Text = rasxodMaterials.DatePostupleniya.ToString("dd.MM.yyyy");
                tb_Quantity.Text = rasxodMaterials.Quantity.ToString();
                tb_responUser.SelectedItem = usersContext.Users.Where(x => x.Id == rasxodMaterials.Id).FirstOrDefault().FIO;
                tb_timeResponUser.SelectedItem = usersContext.Users.Where(x => x.Id == rasxodMaterials.Id).FirstOrDefault().FIO;
                tb_typeRasMat.SelectedItem = typeRasxodContext.TypeRasxod.Where(x => x.Id == rasxodMaterials.Id).FirstOrDefault().Name;
                tb_characters.SelectedItem = mainCharacMaterialsContext.MainCharacMaterials.Where(x => x.Id == rasxodMaterials.Id).FirstOrDefault().Id;
            }
            foreach (var item in usersContext.Users)
            {
                tb_responUser.Items.Add(item.FIO);
                tb_timeResponUser.Items.Add(item.FIO);
            }
            foreach (var item in typeRasxodContext.TypeRasxod)
            {
                tb_typeRasMat.Items.Add(item.Name);
            }
            foreach (var item in mainCharacMaterialsContext.MainCharacMaterials)
            {
                tb_characters.Items.Add(item.Id);
            }
        }

        private void OpenPhoto(object sender, RoutedEventArgs e)
        {

        }

        private void Click_Redact(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tb_Name.Text))
            {
                MessageBox.Show("Введите наименование расходного материала");
                return;
            }
            if (string.IsNullOrEmpty(tb_Des.Text))
            {
                MessageBox.Show("Введите описание расходного материала");
                return;
            }
            if (string.IsNullOrEmpty(tb_Quantity.Text))
            {
                MessageBox.Show("Выберите количество расходного материала");
                return;
            }
            if (tb_responUser.SelectedItem == null)
            {
                MessageBox.Show("Выберите ответственного пользователя");
                return;
            }
            if (tb_timeResponUser.SelectedItem == null)
            {
                MessageBox.Show("Выберите временно-ответственного пользователя");
                return;
            }
            if (tb_typeRasMat.SelectedItem == null)
            {
                MessageBox.Show("Выберите тип расходного материала");
                return;
            }
            if (tb_characters.SelectedItem == null)
            {
                MessageBox.Show("Выберите характеристики расходного материала");
                return;
            }
            if (rasxodMaterials == null)
            {
                rasxodMaterials = new Models.RasxodMaterials();
                rasxodMaterials.Name = tb_Name.Text;
                rasxodMaterials.Description = tb_Des.Text;
                rasxodMaterials.DatePostupleniya = tb_DatePost.Text;
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

        }

        private void Click_Cancel_Redact(object sender, RoutedEventArgs e)
        {

        }

        private void Click_Redact(object sender, RoutedEventArgs e)
        {

        }

        private void OpenPhoto(object sender, RoutedEventArgs e)
        {

        }
    }
}
