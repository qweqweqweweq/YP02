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

namespace YP02.Pages.Oborudovanie
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        public Oborudovanie MainOborudovanie;
        public Models.Oborudovanie oborudovanie;
        AudiencesContext audiencesContext = new AudiencesContext();
        UsersContext usersContext = new UsersContext();
        NapravlenieContext napravlenieContext = new NapravlenieContext();
        StatusContext statusContext = new StatusContext();
        ViewModelContext viewModelContext = new ViewModelContext();
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
                tb_Audience.Text = audiencesContext.Auditories.Where(x => x.id == oborudovanie.Id).FirstOrDefault().Name;
                tb_User.SelectedItem = usersContext.Users.Where(x => x.Id == oborudovanie.Id).FirstOrDefault().FIO;
                tb_tempUser.SelectedItem = usersContext.Users.Where(x => x.Id == oborudovanie.Id).FirstOrDefault().FIO;
                tb_Price.Text = oborudovanie.PriceObor;
                tb_Direction.SelectedItem = napravlenieContext.Napravlenie.Where(x => x.Id == oborudovanie.Id).FirstOrDefault().Name;
                tb_Status.SelectedItem = statusContext.Status.Where(x => x.Id == oborudovanie.Id).FirstOrDefault().Name;
                tb_Model.SelectedItem = viewModelContext.ViewModel.Where(x => x.Id == oborudovanie.Id).FirstOrDefault().Name;
                tb_Comment.Text = oborudovanie.Comments;
            }
            foreach (var item in audiencesContext.Auditories)
            {
                tb_Audience.Items.Add(item.Name);
            }
            foreach (var item in usersContext.Users)
            {
                tb_User.Items.Add(item.FIO);
                tb_tempUser.Items.Add(item.FIO);
            }
            foreach (var item in audiencesContext.Auditories)
            {
                tb_Direction.Items.Add(item.Name);
            }
            foreach (var item in audiencesContext.Auditories)
            {
                tb_Status.Items.Add(item.Name);
            }
            foreach (var item in audiencesContext.Auditories)
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
                oborudovanie = new Models.Oborudovanie();
                oborudovanie.Name = tb_Name.Text;
                oborudovanie.InventNumber = tb_invNum.Text;
                oborudovanie.IdClassroom = audiencesContext.Auditories.Where(x => x.Name == tb_Audience.SelectedItem).First().id;
                oborudovanie.IdResponUser = usersContext.Users.Where(x => x.FIO == tb_User.SelectedItem).First().Id;
                oborudovanie.IdTimeResponUser = usersContext.Users.Where(x => x.FIO == tb_tempUser.SelectedItem).First().Id;
                oborudovanie.PriceObor = tb_Price.Text;
                oborudovanie.IdNapravObor = napravlenieContext.Napravlenie.Where(x => x.Name == tb_Direction.SelectedItem).First().Id;
                oborudovanie.IdStatusObor = statusContext.Status.Where(x => x.Name == tb_Status.SelectedItem).First().Id;
                oborudovanie.IdModelObor = viewModelContext.ViewModel.Where(x => x.Name == tb_Model.SelectedItem).First().Id;
                oborudovanie.Comments = tb_Comment.Text;
                MainOborudovanie.OborudovanieContext.Oborudovanie.Add(oborudovanie);
            }
            else
            {
                oborudovanie.Name = tb_Name.Text;
                oborudovanie.InventNumber = tb_invNum.Text;
                oborudovanie.IdClassroom = audiencesContext.Auditories.Where(x => x.Name == tb_Audience.SelectedItem).First().id;
                oborudovanie.IdResponUser = usersContext.Users.Where(x => x.FIO == tb_User.SelectedItem).First().Id;
                oborudovanie.IdTimeResponUser = usersContext.Users.Where(x => x.FIO == tb_tempUser.SelectedItem).First().Id;
                oborudovanie.PriceObor = tb_Price.Text;
                oborudovanie.IdNapravObor = napravlenieContext.Napravlenie.Where(x => x.Name == tb_Direction.SelectedItem).First().Id;
                oborudovanie.IdStatusObor = statusContext.Status.Where(x => x.Name == tb_Status.SelectedItem).First().Id;
                oborudovanie.IdModelObor = viewModelContext.ViewModel.Where(x => x.Name == tb_Model.SelectedItem).First().Id;
                oborudovanie.Comments = tb_Comment.Text;
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

        }
    }
}
