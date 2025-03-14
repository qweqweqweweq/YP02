using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
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

namespace YP02.Pages.RasxodMaterials
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        RasxodMaterials MainRasxodMaterials;
        Models.RasxodMaterials RasxodMaterials;
        UsersContext usersContext = new UsersContext();
        TypeCharacteristicsContext typeCharacteristicsContext = new TypeCharacteristicsContext();
        CharacteristicsContext characteristicsContext = new CharacteristicsContext();

        public Item(Models.RasxodMaterials RasxodMaterials, RasxodMaterials MainRasxodMaterials)
        {
            InitializeComponent();
            this.RasxodMaterials = RasxodMaterials;
            this.MainRasxodMaterials = MainRasxodMaterials;
            lb_Name.Content = RasxodMaterials.Name;
            lb_desc.Content = "Описание: " + RasxodMaterials.Description;
            lb_posDate.Content = "Дата поступления: " + RasxodMaterials.DatePostupleniya;
            lb_kolvo.Content = "Количество: " + RasxodMaterials.Quantity + " штук";
            lb_userResp.Content = "Ответственный: " + usersContext.Users.Where(x => x.Id == RasxodMaterials.UserRespon).FirstOrDefault().FIO;
            lb_userTimeResp.Content = "Временно-ответственный: " + usersContext.Users.Where(x => x.Id == RasxodMaterials.ResponUserTime).FirstOrDefault().FIO;
            lb_type.Content = "Тип: " + typeCharacteristicsContext.TypeCharacteristics.Where(x => x.Id == RasxodMaterials.CharacteristicsType).FirstOrDefault().Name;
            lb_charact.Content = "Характеристика: " + characteristicsContext.Characteristics.Where(x => x.Id == RasxodMaterials.Characteristics).FirstOrDefault().Name;
            DisplayImage(RasxodMaterials.Photo);
        }

        private void Click_redact(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.RasxodMaterials.Add(MainRasxodMaterials, RasxodMaterials));
        }

        private void Click_remove(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("При удалении расходного материала все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                MainRasxodMaterials.rasxodMaterialsContext.RasxodMaterials.Remove(RasxodMaterials);
                MainRasxodMaterials.rasxodMaterialsContext.SaveChanges();

                (this.Parent as Panel).Children.Remove(this);
            }
            else
            {
                MessageBox.Show("Действие отменено.");
            }
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
