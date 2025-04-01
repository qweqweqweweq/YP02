﻿using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using YP02.Context;

namespace YP02.Pages.RasxodMaterials
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

        RasxodMaterials MainRasxodMaterials;
        public Models.RasxodMaterials RasxodMaterials;
        UsersContext usersContext = new UsersContext();
        TypeCharacteristicsContext typeCharacteristicsContext = new TypeCharacteristicsContext();
        CharacteristicsContext characteristicsContext = new CharacteristicsContext();
        ValueCharacteristicsContext valueCharacteristicsContext = new ValueCharacteristicsContext();
        private Models.Users currentUser;

        public Item(Models.RasxodMaterials RasxodMaterials, RasxodMaterials MainRasxodMaterials)
        {
            InitializeComponent();
            this.RasxodMaterials = RasxodMaterials;
            this.MainRasxodMaterials = MainRasxodMaterials;

            currentUser = MainWindow.init.CurrentUser;
            if (currentUser != null && currentUser.Role == "Администратор")
            {
                button1.Visibility = Visibility.Visible;
                button2.Visibility = Visibility.Visible;
            }

            lb_Name.Content = RasxodMaterials.Name;
            lb_desc.Content = "Описание: " + RasxodMaterials.Description;
            lb_posDate.Content = "Дата поступления: " + RasxodMaterials.DatePostupleniya.ToString("dd.MM.yyyy");
            lb_kolvo.Content = "Количество: " + RasxodMaterials.Quantity;
            lb_userResp.Content = "Ответственный: " + usersContext.Users.Where(x => x.Id == RasxodMaterials.UserRespon).FirstOrDefault()?.FIO;
            lb_userTimeResp.Content = "Временно-ответственный: " + usersContext.Users.Where(x => x.Id == RasxodMaterials.ResponUserTime).FirstOrDefault()?.FIO;
            lb_type.Content = "Тип расходника: " + typeCharacteristicsContext.TypeCharacteristics.Where(x => x.Id == RasxodMaterials.CharacteristicsType).FirstOrDefault()?.Name;
            lb_charact.Content = "Характеристика: " + characteristicsContext.Characteristics.Where(x => x.Id == RasxodMaterials.Characteristics).FirstOrDefault()?.Name;
            lb_valueCharact.Content = "Значение характеристики: " + valueCharacteristicsContext.ValueCharacteristics.Where(x => x.Id == RasxodMaterials.IdValue).FirstOrDefault()?.Znachenie;
            DisplayImage(RasxodMaterials.Photo);

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
                MainWindow.init.OpenPages(new Pages.RasxodMaterials.Add(MainRasxodMaterials, RasxodMaterials));
            }
            catch (Exception ex)
            {
                LogError("Ошибка редактирования оборудования", ex).ConfigureAwait(false);
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
                LogError("Ошибка удаления оборудования", ex).ConfigureAwait(false);
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
                Debug.WriteLine($"Ошибка загрузки изображения: {ex.Message}");
            }
        }

        private void Click_history(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow.init.OpenPages(new Pages.HistoryRashod.HistoryRashod(RasxodMaterials.Id));
            }
            catch (Exception ex)
            {
                LogError("Ошибка открытия истории оборудования", ex).ConfigureAwait(false);
            }
        }

        private async Task LogError(string message, Exception ex)
        {
            Debug.WriteLine($"{message}: {ex.Message}");

            try
            {
                await using (var errorsContext = new ErrorsContext())
                {
                    errorsContext.Errors.Add(new Models.Errors { Message = ex.Message });
                    await errorsContext.SaveChangesAsync();
                }
                string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "log.txt");
                Directory.CreateDirectory(Path.GetDirectoryName(logPath) ?? string.Empty);

                await File.AppendAllTextAsync(logPath, $"{DateTime.Now}: {ex.Message}\n{ex.StackTrace}\n\n");
            }
            catch (Exception logEx)
            {
                Debug.WriteLine($"Ошибка при записи в лог-файл: {logEx.Message}");
            }
        }
    }    
}