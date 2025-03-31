using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using YP02.Context;
using System.Diagnostics;

namespace YP02.Pages.Oborudovanie
{
    public partial class Item : UserControl
    {
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(Item),
                new PropertyMetadata(false, OnIsSelectedChanged));

        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        Oborudovanie MainOborudovanie;
        public Models.Oborudovanie Oborudovanie;
        AuditoriesContext auditoriesContext = new();
        UsersContext usersContext = new();
        NapravlenieContext napravlenieContext = new();
        StatusContext statusContext = new();
        ViewModelContext viewModelContext = new();
        private Models.Users currentUser;

        public Item(Models.Oborudovanie Oborudovanie, Oborudovanie MainOborudovanie)
        {
            InitializeComponent();
            this.Oborudovanie = Oborudovanie;
            this.MainOborudovanie = MainOborudovanie;

            currentUser = MainWindow.init.CurrentUser;
            if (currentUser != null && currentUser.Role == "Администратор")
            {
                button1.Visibility = Visibility.Visible;
                button2.Visibility = Visibility.Visible;
            }

            lb_Name.Content = Oborudovanie.Name;
            lb_invNum.Content = Oborudovanie.InventNumber;
            lb_Audience.Content = auditoriesContext.Auditories.Where(x => x.Id == Oborudovanie.IdClassroom).FirstOrDefault()?.Name;
            lb_User.Content = usersContext.Users.Where(x => x.Id == Oborudovanie.IdResponUser).FirstOrDefault()?.FIO;
            lb_tempUser.Content = usersContext.Users.Where(x => x.Id == Oborudovanie.IdTimeResponUser).FirstOrDefault()?.FIO;
            lb_Price.Content = Oborudovanie.PriceObor;
            lb_Direct.Content = napravlenieContext.Napravlenie.Where(x => x.Id == Oborudovanie.IdNapravObor).FirstOrDefault()?.Name;
            lb_Status.Content = statusContext.Status.Where(x => x.Id == Oborudovanie.IdStatusObor).FirstOrDefault()?.Name;
            lb_Model.Content = viewModelContext.ViewModel.Where(x => x.Id == Oborudovanie.IdModelObor).FirstOrDefault()?.Name;
            lb_Comment.Content = Oborudovanie.Comments;
            DisplayImage(Oborudovanie.Photo);

            // Добавляем обработчик клика по всему элементу
            this.MouseLeftButtonDown += Item_MouseLeftButtonDown;
        }

        private static void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var item = (Item)d;
            item.RaiseSelectionChanged();
        }

        public event EventHandler SelectionChanged;

        private void RaiseSelectionChanged()
        {
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }

        private void Item_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            IsSelected = !IsSelected;
            e.Handled = true;
        }

        private void Click_redact(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow.init.OpenPages(new Pages.Oborudovanie.Add(MainOborudovanie, Oborudovanie));
            }
            catch (Exception ex)
            {
                LogError("Ошибка редактирования оборудования", ex);
            }
        }

        private void Click_remove(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show(
                    "При удалении оборудования все связанные данные также будут удалены!",
                    "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning
                );

                if (result != MessageBoxResult.Yes)
                {
                    MessageBox.Show("Действие отменено.");
                    return;
                }

                using var context = MainOborudovanie.OborudovanieContext;
                context.Oborudovanie.Remove(Oborudovanie);
                context.SaveChanges();

                Dispatcher.Invoke(() =>
                {
                    if (Parent is Panel parentPanel)
                    {
                        parentPanel.Children.Remove(this);
                    }
                });
            }
            catch (Exception ex)
            {
                LogError("Ошибка удаления оборудования", ex);
            }
        }

        private void Click_history(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow.init.OpenPages(new Pages.HistoryObor.HistoryObor(Oborudovanie.Id));
            }
            catch (Exception ex)
            {
                LogError("Ошибка открытия истории оборудования", ex);
            }
        }

        private async void DisplayImage(byte[] imageData)
        {
            if (imageData is { Length: > 0 })
            {
                try
                {
                    var bitmap = new BitmapImage();
                    using (var ms = new MemoryStream(imageData))
                    {
                        bitmap.BeginInit();
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.StreamSource = ms;
                        bitmap.EndInit();
                        bitmap.Freeze();
                    }

                    imgObor.Source = bitmap;
                    return;
                }
                catch (Exception ex)
                {
                    LogError($"Ошибка загрузки изображения: {ex.Message}", ex);
                }
            }

            SetDefaultImage();
        }

        private void SetDefaultImage()
        {
            try
            {
                var defaultImage = new BitmapImage();
                defaultImage.BeginInit();
                defaultImage.UriSource = new Uri("pack://application:,,,/Images/NoneImage.png", UriKind.Absolute);
                defaultImage.EndInit();
                defaultImage.Freeze();

                imgObor.Source = defaultImage;
            }
            catch (Exception ex)
            {
                LogError($"Ошибка загрузки стандартного изображения: {ex.Message}", ex);
            }
        }
        private void LogError(string message, Exception ex)
        {
            Debug.WriteLine(message);

            try
            {
                using var errorsContext = new ErrorsContext();
                errorsContext.Errors.Add(new Models.Errors { Message = ex.Message });
                errorsContext.SaveChanges();

                string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "log.txt");
                Directory.CreateDirectory(Path.GetDirectoryName(logPath) ?? string.Empty);
                File.AppendAllText(logPath, $"{DateTime.Now}: {ex.Message}\n{ex.StackTrace}\n\n");
            }
            catch (Exception logEx)
            {
                Debug.WriteLine($"Ошибка при записи в лог-файл: {logEx.Message}");
            }
        }
    }
}