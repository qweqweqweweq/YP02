using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Win32;
using YP02.Context;

namespace YP02.Pages.Oborudovanie
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        public Oborudovanie MainOborudovanie;
        public Models.Oborudovanie oborudovanie;
        AuditoriesContext auditoriesContext = new();
        UsersContext usersContext = new();
        NapravlenieContext napravlenieContext = new();
        StatusContext statusContext = new();
        ViewModelContext viewModelContext = new();

        //
        public Models.HistoryObor historyObor;
        public Add(Oborudovanie MainOborudovanie, Models.Oborudovanie oborudovanie = null)
        {
            InitializeComponent();
            this.MainOborudovanie = MainOborudovanie;
            this.oborudovanie = oborudovanie;
            if (oborudovanie != null)
            {
                text1.Content = "Изменение обороудования";
                text2.Content = "Изменить";
                tb_Name.Text = oborudovanie.Name;
                tb_invNum.Text = oborudovanie.InventNumber;
                tb_Audience.Text = auditoriesContext.Auditories.Where(x => x.Id == oborudovanie.IdClassroom).FirstOrDefault().Name;
                tb_User.SelectedItem = usersContext.Users.Where(x => x.Id == oborudovanie.IdResponUser).FirstOrDefault().FIO;
                tb_tempUser.SelectedItem = usersContext.Users.Where(x => x.Id == oborudovanie.IdTimeResponUser).FirstOrDefault().FIO;
                tb_Price.Text = oborudovanie.PriceObor;
                tb_Direction.SelectedItem = napravlenieContext.Napravlenie.Where(x => x.Id == oborudovanie.IdNapravObor).FirstOrDefault().Name;
                tb_Status.SelectedItem = statusContext.Status.Where(x => x.Id == oborudovanie.IdStatusObor).FirstOrDefault().Name;
                tb_Model.SelectedItem = viewModelContext.ViewModel.Where(x => x.Id == oborudovanie.IdModelObor).FirstOrDefault().Name;
                tb_Comment.Text = oborudovanie.Comments;
            }
            foreach (var item in auditoriesContext.Auditories)
            {
                tb_Audience.Items.Add(item.Name);
            }
            foreach (var item in usersContext.Users)
            {
                tb_User.Items.Add(item.FIO);
                tb_tempUser.Items.Add(item.FIO);
            }
            foreach (var item in napravlenieContext.Napravlenie)
            {
                tb_Direction.Items.Add(item.Name);
            }
            foreach (var item in statusContext.Status)
            {
                tb_Status.Items.Add(item.Name);
            }
            foreach (var item in viewModelContext.ViewModel)
            {
                tb_Model.Items.Add(item.Name);
            }
        }

        private void Click_Redact(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tb_Name.Text))
            {
                MessageBox.Show("Введите наименование оборудования");
                return;
            }
            if (string.IsNullOrEmpty(tb_invNum.Text))
            {
                MessageBox.Show("Введите инвентарный номер оборудования");
                return;
            }
            if (tb_Audience.SelectedItem == null)
            {
                MessageBox.Show("Выберите аудиторию");
                return;
            }
            if (tb_User.SelectedItem == null)
            {
                MessageBox.Show("Выберите ответственного пользователя");
                return;
            }
            if (tb_tempUser.SelectedItem == null)
            {
                MessageBox.Show("Выберите временно-ответственного пользователя");
                return;
            }
            if (string.IsNullOrEmpty(tb_Price.Text))
            {
                MessageBox.Show("Введите стоимость оборудования");
                return;
            }
            if (tb_Direction.SelectedItem == null)
            {
                MessageBox.Show("Выберите направление");
                return;
            }
            if (tb_Status.SelectedItem == null)
            {
                MessageBox.Show("Выберите статус");
                return;
            }
            if (tb_Model.SelectedItem == null)
            {
                MessageBox.Show("Выберите модель");
                return;
            }
            if (oborudovanie == null)
            {
                oborudovanie = new Models.Oborudovanie
                {
                    Name = tb_Name.Text,
                    InventNumber = tb_invNum.Text,
                    IdClassroom = auditoriesContext.Auditories.Where(x => x.Name == tb_Audience.SelectedItem).First().Id,
                    IdResponUser = usersContext.Users.Where(x => x.FIO == tb_User.SelectedItem).First().Id,
                    IdTimeResponUser = usersContext.Users.Where(x => x.FIO == tb_tempUser.SelectedItem).First().Id,
                    PriceObor = tb_Price.Text,
                    IdNapravObor = napravlenieContext.Napravlenie.Where(x => x.Name == tb_Direction.SelectedItem).First().Id,
                    IdStatusObor = statusContext.Status.Where(x => x.Name == tb_Status.SelectedItem).First().Id,
                    IdModelObor = viewModelContext.ViewModel.Where(x => x.Name == tb_Model.SelectedItem).First().Id,
                    Comments = tb_Comment.Text,
                    Photo = oborudovanie.Photo
                };
                //
                historyObor = new Models.HistoryObor();
                historyObor.IdUserr = usersContext.Users.Where(x => x.FIO == tb_tempUser.SelectedItem).First().Id;
                historyObor.IdObor = oborudovanie.Id;
                historyObor.Date = DateTime.Now;
                historyObor.Comment = tb_Comment.Text;
                //
                MainOborudovanie.OborudovanieContext.Oborudovanie.Add(oborudovanie);
            }
            else
            {
                oborudovanie.Name = tb_Name.Text;
                oborudovanie.InventNumber = tb_invNum.Text;
                oborudovanie.IdClassroom = auditoriesContext.Auditories.Where(x => x.Name == tb_Audience.SelectedItem).First().Id;
                oborudovanie.IdResponUser = usersContext.Users.Where(x => x.FIO == tb_User.SelectedItem).First().Id;
                oborudovanie.IdTimeResponUser = usersContext.Users.Where(x => x.FIO == tb_tempUser.SelectedItem).First().Id;
                oborudovanie.PriceObor = tb_Price.Text;
                oborudovanie.IdNapravObor = napravlenieContext.Napravlenie.Where(x => x.Name == tb_Direction.SelectedItem).First().Id;
                oborudovanie.IdStatusObor = statusContext.Status.Where(x => x.Name == tb_Status.SelectedItem).First().Id;
                oborudovanie.IdModelObor = viewModelContext.ViewModel.Where(x => x.Name == tb_Model.SelectedItem).First().Id;
                oborudovanie.Comments = tb_Comment.Text;
                oborudovanie.Photo = oborudovanie.Photo;
                //
                historyObor = new Models.HistoryObor();
                historyObor.IdUserr = usersContext.Users.Where(x => x.FIO == tb_tempUser.SelectedItem).First().Id;
                historyObor.IdObor = oborudovanie.Id;
                historyObor.Date = DateTime.Now;
                historyObor.Comment = tb_Comment.Text;
                //
            }
            MainOborudovanie.OborudovanieContext.SaveChanges();
            MainWindow.init.OpenPages(new Pages.Oborudovanie.Oborudovanie());
        }

        private void Click_Cancel_Redact(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Oborudovanie.Oborudovanie());
        }

        private void OpenPhoto(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Filter = "Image Files (*.jpg;*.jpeg;*.png;*.gif)|*.jpg;*.jpeg;*.png;*.gif"
            };
            if (ofd.ShowDialog() == true)
            {
                try
                {
                    oborudovanie = new Models.Oborudovanie();
                    using (var fileStream = File.OpenRead(ofd.FileName))
                    {
                        MemoryStream memoryStream = new MemoryStream();
                        fileStream.CopyTo(memoryStream);
                        oborudovanie.Photo = memoryStream.ToArray();
                    }
                    photobut.Content = "Фото выбрано";

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки фотографии: \n{ex.Message}");
                }
            }
        }
    }
}
