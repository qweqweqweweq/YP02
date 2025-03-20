using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using YP02.Context;
using System.Diagnostics;

namespace YP02.Pages.Oborudovanie
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        Oborudovanie MainOborudovanie;
        Models.Oborudovanie Oborudovanie;
        AuditoriesContext auditoriesContext = new();
        UsersContext usersContext = new();
        NapravlenieContext napravlenieContext = new();
        StatusContext statusContext = new();
        ViewModelContext viewModelContext = new();
        private Models.Users currentUser;

        public Item(Models.Oborudovanie Oborudovanie, Oborudovanie MainOborudovanie)
        {
            InitializeComponent();

            currentUser = MainWindow.init.CurrentUser;
            if (currentUser != null && currentUser.Role == "Администратор")
            {
                button1.Visibility = Visibility.Visible;
                button2.Visibility = Visibility.Visible;
            }

            this.Oborudovanie = Oborudovanie;
            this.MainOborudovanie = MainOborudovanie;
            lb_Name.Content = Oborudovanie.Name;
            lb_invNum.Content = Oborudovanie.InventNumber;
            lb_Audience.Content = auditoriesContext.Auditories.Where(x => x.Id == Oborudovanie.IdClassroom).FirstOrDefault().Name;
            lb_User.Content = usersContext.Users.Where(x => x.Id == Oborudovanie.IdResponUser).FirstOrDefault().FIO;
            lb_tempUser.Content = usersContext.Users.Where(x => x.Id == Oborudovanie.IdTimeResponUser).FirstOrDefault().FIO;
            lb_Price.Content = Oborudovanie.PriceObor;
            lb_Direct.Content = napravlenieContext.Napravlenie.Where(x => x.Id == Oborudovanie.IdNapravObor).FirstOrDefault().Name;
            lb_Status.Content = statusContext.Status.Where(x => x.Id == Oborudovanie.IdStatusObor).FirstOrDefault().Name;
            lb_Model.Content = viewModelContext.ViewModel.Where(x => x.Id == Oborudovanie.IdModelObor).FirstOrDefault().Name;
            lb_Comment.Content = Oborudovanie.Comments;
            DisplayImage(Oborudovanie.Photo);
        }

        private void Click_redact(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Oborudovanie.Add(MainOborudovanie, Oborudovanie));
        }

        private void Click_remove(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("При удалении оборудования все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MainOborudovanie.OborudovanieContext.Oborudovanie.Remove(Oborudovanie);
                MainOborudovanie.OborudovanieContext.SaveChanges();
                (this.Parent as Panel).Children.Remove(this);
            }
            else MessageBox.Show("Действие отменено.");
        }

        private void Click_history(object sender, RoutedEventArgs e)
        {
            // Переход на страницу истории, передавая Id оборудования
            MainWindow.init.OpenPages(new Pages.HistoryObor.HistoryObor(Oborudovanie.Id));
        }

        private void DisplayImage(byte[] imageData)
        {
            try
            {
                if (imageData != null && imageData.Length > 0)
                {
                    using (MemoryStream ms = new MemoryStream(imageData))
                    {
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.StreamSource = ms;
                        bitmap.EndInit();

                        imgObor.Source = bitmap;
                    }
                }
                else
                {
                    SetDefaultImage();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка загрузки изображения: {ex.Message}");
                SetDefaultImage();
            }
        }
        private void SetDefaultImage()
        {
            imgObor.Source = new BitmapImage(new Uri("pack://application:,,,/Images/NoneImage.png"));
        }
    }
}
