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
            try
            {
                MainWindow.init.OpenPages(new Pages.Oborudovanie.Add(MainOborudovanie, Oborudovanie));
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
                MessageBoxResult result = MessageBox.Show("При удалении оборудования все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    MainOborudovanie.OborudovanieContext.Oborudovanie.Remove(Oborudovanie);
                    MainOborudovanie.OborudovanieContext.SaveChanges();
                    (this.Parent as Panel).Children.Remove(this);
                }
                else MessageBox.Show("Действие отменено.");
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

        private void Click_history(object sender, RoutedEventArgs e)
        {
            try
            {
                // Переход на страницу истории, передавая Id оборудования
                MainWindow.init.OpenPages(new Pages.HistoryObor.HistoryObor(Oborudovanie.Id));
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

        private async void DisplayImage(byte[] imageData)
        {
            try
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
                            bitmap.Freeze();

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
        private void SetDefaultImage()
        {
            try
            {
                try
                {
                    BitmapImage defaultImage = new BitmapImage();
                    defaultImage.BeginInit();
                    defaultImage.UriSource = new Uri("pack://application:,,,/Images/NoneImage.png", UriKind.Absolute);
                    defaultImage.EndInit();
                    defaultImage.Freeze();
                    imgObor.Source = defaultImage;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Ошибка загрузки стандартного изображения: {ex.Message}");
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
