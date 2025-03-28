﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
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

        private byte[] tempPhoto = null;
        public Models.HistoryObor historyObor;

        public Add(Oborudovanie MainOborudovanie, Models.Oborudovanie oborudovanie = null)
        {
            InitializeComponent();
            this.MainOborudovanie = MainOborudovanie;
            this.oborudovanie = oborudovanie;

            if (oborudovanie != null)
            {
                text1.Content = "Изменение оборудования";
                text2.Content = "Изменить";
                tb_Name.Text = oborudovanie.Name;
                tb_invNum.Text = oborudovanie.InventNumber;
                tb_Audience.SelectedItem = auditoriesContext.Auditories.FirstOrDefault(x => x.Id == oborudovanie.IdClassroom)?.Name;
                tb_User.SelectedItem = usersContext.Users.FirstOrDefault(x => x.Id == oborudovanie.IdResponUser)?.FIO;
                tb_tempUser.SelectedItem = usersContext.Users.FirstOrDefault(x => x.Id == oborudovanie.IdTimeResponUser)?.FIO;
                tb_Price.Text = oborudovanie.PriceObor;
                tb_Direction.SelectedItem = napravlenieContext.Napravlenie.FirstOrDefault(x => x.Id == oborudovanie.IdNapravObor)?.Name;
                tb_Status.SelectedItem = statusContext.Status.FirstOrDefault(x => x.Id == oborudovanie.IdStatusObor)?.Name;
                tb_Model.SelectedItem = viewModelContext.ViewModel.FirstOrDefault(x => x.Id == oborudovanie.IdModelObor)?.Name;
                tb_Comment.Text = oborudovanie.Comments;

            }

            foreach (var item in auditoriesContext.Auditories) tb_Audience.Items.Add(item.Name);
            foreach (var item in usersContext.Users)
            {
                tb_User.Items.Add(item.FIO);
                tb_tempUser.Items.Add(item.FIO);
            }
            foreach (var item in napravlenieContext.Napravlenie) tb_Direction.Items.Add(item.Name);
            foreach (var item in statusContext.Status) tb_Status.Items.Add(item.Name);
            foreach (var item in viewModelContext.ViewModel) tb_Model.Items.Add(item.Name);
        }

        private void Click_Redact(object sender, RoutedEventArgs e)
        {
            try
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
                }

                int oldIdTimeResponUser = oborudovanie.IdTimeResponUser;

                oborudovanie.Name = tb_Name.Text;
                oborudovanie.InventNumber = tb_invNum.Text;
                oborudovanie.IdClassroom = auditoriesContext.Auditories.First(x => x.Name == tb_Audience.SelectedItem).Id;
                oborudovanie.IdResponUser = usersContext.Users.First(x => x.FIO == tb_User.SelectedItem).Id;
                oborudovanie.IdTimeResponUser = usersContext.Users.First(x => x.FIO == tb_tempUser.SelectedItem).Id;
                oborudovanie.PriceObor = tb_Price.Text;
                oborudovanie.IdNapravObor = napravlenieContext.Napravlenie.First(x => x.Name == tb_Direction.SelectedItem).Id;
                oborudovanie.IdStatusObor = statusContext.Status.First(x => x.Name == tb_Status.SelectedItem).Id;
                oborudovanie.IdModelObor = viewModelContext.ViewModel.First(x => x.Name == tb_Model.SelectedItem).Id;
                oborudovanie.Comments = tb_Comment.Text;

                // Если фотография не была загружена, оставляем старую
                if (tempPhoto != null)
                {
                    oborudovanie.Photo = tempPhoto;
                }

                if (oborudovanie.Id == 0)
                {
                    MainOborudovanie.OborudovanieContext.Oborudovanie.Add(oborudovanie);
                }

                MainOborudovanie.OborudovanieContext.SaveChanges();

                // Проверяем, изменился ли IdTimeResponUser
                if (oldIdTimeResponUser != oborudovanie.IdTimeResponUser)
                {
                    // Создаем запись в истории
                    var historyObor = new Models.HistoryObor
                    {
                        IdUserr = usersContext.Users.First(x => x.FIO == tb_tempUser.SelectedItem).Id,
                        IdObor = oborudovanie.Id, // Используем Id оборудования, который был сгенерирован при сохранении
                        Date = DateTime.Now,
                        Comment = tb_Comment.Text
                    };

                    // Используем HistoryOborContext для сохранения истории
                    using (var historyContext = new HistoryOborContext())
                    {
                        historyContext.HistoryObor.Add(historyObor);
                        historyContext.SaveChanges();
                    }
                }

                MainWindow.init.OpenPages(new Pages.Oborudovanie.Oborudovanie());
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

        private void Click_Cancel_Redact(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow.init.OpenPages(new Pages.Oborudovanie.Oborudovanie());
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

        private void OpenPhoto(object sender, RoutedEventArgs e)
        {
            try
            {
                var ofd = new OpenFileDialog
                {
                    Filter = "Image Files (*.jpg;*.jpeg;*.png;*.gif)|*.jpg;*.jpeg;*.png;*.gif"
                };

                if (ofd.ShowDialog() == true)
                {
                    try
                    {
                        using (var fileStream = File.OpenRead(ofd.FileName))
                        {
                            MemoryStream memoryStream = new MemoryStream();
                            fileStream.CopyTo(memoryStream);
                            tempPhoto = memoryStream.ToArray();
                        }
                        photobut.Content = "Фото выбрано";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка загрузки фотографии: \n{ex.Message}");
                    }
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
