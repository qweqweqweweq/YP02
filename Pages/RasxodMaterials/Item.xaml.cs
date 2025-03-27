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
        ValueCharacteristicsContext valueCharacteristicsContext = new ValueCharacteristicsContext();
        private Models.Users currentUser;

        public Item(Models.RasxodMaterials RasxodMaterials, RasxodMaterials MainRasxodMaterials)
        {
            InitializeComponent();

            currentUser = MainWindow.init.CurrentUser;
            if (currentUser != null && currentUser.Role == "Администратор")
            {
                buttons.Visibility = Visibility.Visible;
            }

            this.RasxodMaterials = RasxodMaterials;
            this.MainRasxodMaterials = MainRasxodMaterials;
            lb_Name.Content = RasxodMaterials.Name;
            lb_desc.Content = "Описание: " + RasxodMaterials.Description;
            lb_posDate.Content = "Дата поступления: " + RasxodMaterials.DatePostupleniya.ToString("dd.MM.yyyy");
            lb_kolvo.Content = "Количество: " + RasxodMaterials.Quantity;
            lb_userResp.Content = "Ответственный: " + usersContext.Users.Where(x => x.Id == RasxodMaterials.UserRespon).FirstOrDefault().FIO;
            lb_userTimeResp.Content = "Временно-ответственный: " + usersContext.Users.Where(x => x.Id == RasxodMaterials.ResponUserTime).FirstOrDefault().FIO;
            lb_type.Content = "Тип расходника: " + typeCharacteristicsContext.TypeCharacteristics.Where(x => x.Id == RasxodMaterials.CharacteristicsType).FirstOrDefault().Name;
            lb_charact.Content = "Характеристика: " + characteristicsContext.Characteristics.Where(x => x.Id == RasxodMaterials.Characteristics).FirstOrDefault().Name;
            lb_valueCharact.Content = "Значение характеристики: " + valueCharacteristicsContext.ValueCharacteristics.Where(x => x.Id == RasxodMaterials.IdValue).FirstOrDefault().Znachenie;
            DisplayImage(RasxodMaterials.Photo);
        }

        private void Click_redact(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow.init.OpenPages(new Pages.RasxodMaterials.Add(MainRasxodMaterials, RasxodMaterials));
            }
            catch (Exception ex)
            {
                try
                {
                    using (var errorsContext = new ErrorsContext())
                    {
                        var error = new Models.Errors
                        {
                            Message = ex.Message
                        };
                        errorsContext.Errors.Add(error);
                        errorsContext.SaveChanges(); // Сохраняем ошибку в базе данных
                    }

                    // Логирование ошибки в файл log.txt
                    string logPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "log.txt");
                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(logPath)); // Создаем папку bin, если ее нет
                    System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: {ex.Message}\n{ex.StackTrace}\n\n");
                }
                catch (Exception logEx)
                {
                    MessageBox.Show("Ошибка при записи в лог-файл: " + logEx.Message);
                }

                MessageBox.Show("Ошибка: " + ex.Message);
            }            
        }

        private void Click_remove(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                try
                {
                    using (var errorsContext = new ErrorsContext())
                    {
                        var error = new Models.Errors
                        {
                            Message = ex.Message
                        };
                        errorsContext.Errors.Add(error);
                        errorsContext.SaveChanges(); // Сохраняем ошибку в базе данных
                    }

                    // Логирование ошибки в файл log.txt
                    string logPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "log.txt");
                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(logPath)); // Создаем папку bin, если ее нет
                    System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: {ex.Message}\n{ex.StackTrace}\n\n");
                }
                catch (Exception logEx)
                {
                    MessageBox.Show("Ошибка при записи в лог-файл: " + logEx.Message);
                }

                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void DisplayImage(byte[] imageData)
        {
            try
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
            catch (Exception ex)
            {
                try
                {
                    using (var errorsContext = new ErrorsContext())
                    {
                        var error = new Models.Errors
                        {
                            Message = ex.Message
                        };
                        errorsContext.Errors.Add(error);
                        errorsContext.SaveChanges(); // Сохраняем ошибку в базе данных
                    }

                    // Логирование ошибки в файл log.txt
                    string logPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "log.txt");
                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(logPath)); // Создаем папку bin, если ее нет
                    System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: {ex.Message}\n{ex.StackTrace}\n\n");
                }
                catch (Exception logEx)
                {
                    MessageBox.Show("Ошибка при записи в лог-файл: " + logEx.Message);
                }

                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
    }
}
