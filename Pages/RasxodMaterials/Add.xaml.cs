using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using YP02.Context;

namespace YP02.Pages.RasxodMaterials
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        public RasxodMaterials MainRasxodMaterials;
        public Models.RasxodMaterials rasxodMaterials;
        UsersContext usersContext = new UsersContext();
        CharacteristicsContext characteristicsContext = new CharacteristicsContext();
        TypeCharacteristicsContext typeCharacteristicsContext = new TypeCharacteristicsContext();
        ValueCharacteristicsContext valueCharacteristicsContext = new ValueCharacteristicsContext();

        private byte[] tempPhoto = null;
        public Models.HistoryRashod historyRashod;

        public Add(RasxodMaterials MainRasxodMaterials, Models.RasxodMaterials rasxodMaterials = null)
        {
            InitializeComponent();
            this.MainRasxodMaterials = MainRasxodMaterials;
            this.rasxodMaterials = rasxodMaterials;
            if (rasxodMaterials != null)
            {
                text1.Content = "Изменение расходного материала";
                text2.Content = "Изменить";
                tb_Name.Text = rasxodMaterials.Name;
                tb_Des.Text = rasxodMaterials.Description;
                tb_DatePost.Text = rasxodMaterials.DatePostupleniya.ToString("dd.MM.yyyy");
                tb_Quantity.Text = rasxodMaterials.Quantity.ToString();
                tb_responUser.SelectedItem = usersContext.Users.Where(x => x.Id == rasxodMaterials.UserRespon).FirstOrDefault().FIO;
                tb_timeResponUser.SelectedItem = usersContext.Users.Where(x => x.Id == rasxodMaterials.ResponUserTime).FirstOrDefault().FIO;
                tb_typeRasMat.SelectedItem = typeCharacteristicsContext.TypeCharacteristics.Where(x => x.Id == rasxodMaterials.CharacteristicsType).FirstOrDefault().Name;
                tb_characters.SelectedItem = characteristicsContext.Characteristics.Where(x => x.Id == rasxodMaterials.Characteristics).FirstOrDefault().Name;
                tb_valueChar.SelectedItem = valueCharacteristicsContext.ValueCharacteristics.Where(x => x.Id == rasxodMaterials.IdValue).FirstOrDefault().Znachenie;
            }
            foreach (var item in usersContext.Users)
            {
                tb_responUser.Items.Add(item.FIO);
                tb_timeResponUser.Items.Add(item.FIO);
            }
            foreach (var item in typeCharacteristicsContext.TypeCharacteristics)
            {
                tb_typeRasMat.Items.Add(item.Name);
            }
            foreach (var item in characteristicsContext.Characteristics)
            {
                tb_characters.Items.Add(item.Name);
            }
            foreach (var item in valueCharacteristicsContext.ValueCharacteristics)
            {
                tb_valueChar.Items.Add(item.Znachenie);
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
                        rasxodMaterials = new Models.RasxodMaterials();
                        using (var fileStream = File.OpenRead(ofd.FileName))
                        {
                            MemoryStream memoryStream = new MemoryStream();
                            fileStream.CopyTo(memoryStream);
                            rasxodMaterials.Photo = memoryStream.ToArray();
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
                LogError("Ошибка", ex).ConfigureAwait(false);
            }
        }

        private void Click_Redact(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tb_Name.Text))
                {
                    MessageBox.Show("Введите наименование расходного материала");
                    return;
                }
                if (string.IsNullOrEmpty(tb_Des.Text))
                {
                    MessageBox.Show("Введите описание расходного материала");
                    return;
                }
                if (tb_DatePost.SelectedDate == null)
                {
                    MessageBox.Show("Введите дату поступления расходного материала");
                    return;
                }
                if (string.IsNullOrEmpty(tb_Quantity.Text))
                {
                    MessageBox.Show("Введите количество расходного материала");
                    return;
                }
                // Валидация количества (только цифры)
                if (!Regex.IsMatch(tb_Quantity.Text, @"^\d*$"))
                {
                    MessageBox.Show("Поле количество должно содержать только цифры");
                    return;
                }
                if (tb_responUser.SelectedItem == null)
                {
                    MessageBox.Show("Выберите ответственного пользователя");
                    return;
                }
                if (tb_timeResponUser.SelectedItem == null)
                {
                    MessageBox.Show("Выберите временно-ответственного пользователя");
                    return;
                }
                if (tb_typeRasMat.SelectedItem == null)
                {
                    MessageBox.Show("Выберите тип материала");
                    return;
                }
                if (tb_characters.SelectedItem == null)
                {
                    MessageBox.Show("Выберите характеристику");
                    return;
                }
                if (tb_valueChar.SelectedItem == null)
                {
                    MessageBox.Show("Выберите значение характеристики");
                    return;
                }

                DateTime datePos = tb_DatePost.SelectedDate.Value;

                if (rasxodMaterials == null)
                {
                    rasxodMaterials = new Models.RasxodMaterials();
                }

                int oldIdResponUser = rasxodMaterials.UserRespon;

                rasxodMaterials.Name = tb_Name.Text;
                rasxodMaterials.Description = tb_Des.Text;
                rasxodMaterials.DatePostupleniya = datePos;
                rasxodMaterials.Quantity = double.Parse(tb_Quantity.Text);
                rasxodMaterials.UserRespon = usersContext.Users.Where(x => x.FIO == tb_responUser.SelectedItem).First().Id;
                rasxodMaterials.ResponUserTime = usersContext.Users.Where(x => x.FIO == tb_timeResponUser.SelectedItem).First().Id;
                rasxodMaterials.Characteristics = characteristicsContext.Characteristics.Where(x => x.Name == tb_characters.SelectedItem).First().Id;
                rasxodMaterials.CharacteristicsType = typeCharacteristicsContext.TypeCharacteristics.Where(x => x.Name == tb_typeRasMat.SelectedItem).First().Id;
                rasxodMaterials.Photo = rasxodMaterials.Photo;
                rasxodMaterials.IdValue = valueCharacteristicsContext.ValueCharacteristics.Where(x => x.Znachenie == tb_valueChar.SelectedItem).First().Id;



                // Если фотография не была загружена, оставляем старую
                if (tempPhoto != null)
                {
                    rasxodMaterials.Photo = tempPhoto;
                }

                if (rasxodMaterials.Id == 0)
                {
                    MainRasxodMaterials.rasxodMaterialsContext.RasxodMaterials.Add(rasxodMaterials);
                }

                MainRasxodMaterials.rasxodMaterialsContext.SaveChanges();

                // Проверяем, изменился ли IdTimeResponUser
                if (oldIdResponUser != rasxodMaterials.UserRespon)
                {
                    // Создаем запись в истории
                    var historyRashod = new Models.HistoryRashod
                    {
                        IdUser = usersContext.Users.First(x => x.FIO == tb_responUser.SelectedItem).Id,
                        IdRashod = rasxodMaterials.Id, // Используем Id оборудования, который был сгенерирован при сохранении
                        Date = DateTime.Now,
                    };

                    // Используем HistoryOborContext для сохранения истории
                    using (var historyContext = new HistoryRashodContext())
                    {
                        historyContext.HistoryRashod.Add(historyRashod);
                        historyContext.SaveChanges();
                    }
                }
                MainWindow.init.OpenPages(new Pages.RasxodMaterials.RasxodMaterials());
            }
            catch (Exception ex)
            {
                LogError("Ошибка", ex).ConfigureAwait(false);
            }
        }

        private void Click_Cancel_Redact(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow.init.OpenPages(new Pages.RasxodMaterials.RasxodMaterials());
            }
            catch (Exception ex)
            {
                LogError("Ошибка", ex).ConfigureAwait(false);
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
