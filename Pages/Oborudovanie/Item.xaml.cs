using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using YP02.Context;
using System.Drawing.Imaging;

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

        public Item(Models.Oborudovanie Oborudovanie, Oborudovanie MainOborudovanie)
        {
            InitializeComponent();
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
            MainWindow.init.OpenPages(new Pages.HistoryObor.HistoryObor());
        }

        private void DisplayImage(byte[] imageData)
        {
            if (imageData != null && imageData.Length > 0)
            {
                try
                {
                    using (var ms = new MemoryStream(imageData))
                    {
                        var image = System.Drawing.Image.FromStream(ms);
                        var bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        MemoryStream memoryStream = new MemoryStream();
                        image.Save(memoryStream, ImageFormat.Png);
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        bitmapImage.StreamSource = memoryStream;
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.EndInit();

                        imgObor.Source = bitmapImage;
                    }
                }
                catch (Exception ex)
                {
                    Uri uri = new Uri("/Images/NoneImage.png", UriKind.Relative);
                    imgObor.Source = new BitmapImage(uri);

                    Debug.WriteLine(ex.Message);
                }
            }
            else
            {
                Uri uri = new Uri("/Images/NoneImage.png", UriKind.Relative);
                imgObor.Source = new BitmapImage(uri);
            }
        }
    }
}
