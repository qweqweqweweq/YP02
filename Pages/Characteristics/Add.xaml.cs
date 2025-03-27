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

namespace YP02.Pages.Characteristics
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        public Characteristics MainCharacteristics;
        public Models.Characteristics characteristics;
        TypeCharacteristicsContext typeCharacteristicsContext = new TypeCharacteristicsContext();

        public Add(Characteristics MainCharacteristics, Models.Characteristics characteristics = null)
        {
            InitializeComponent();
            this.MainCharacteristics = MainCharacteristics;
            this.characteristics = characteristics;
            if (characteristics != null)
            {
                text1.Content = "Изменение характеристики";
                text2.Content = "Изменить";
                tb_Name.Text = characteristics.Name;
                cb_TypeCharac.SelectedItem = typeCharacteristicsContext.TypeCharacteristics.Where(x => x.Id == characteristics.TypeCharacter).FirstOrDefault().Name;
            }
            foreach (var item in typeCharacteristicsContext.TypeCharacteristics)
            {
                cb_TypeCharac.Items.Add(item.Name);
            }
        }

        private void Click_Redact(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tb_Name.Text))
                {
                    MessageBox.Show("Введите наименование характеристики");
                    return;
                }
                if (cb_TypeCharac.SelectedItem == null)
                {
                    MessageBox.Show("Выберите тип характеристики");
                    return;
                }
                if (characteristics == null)
                {
                    characteristics = new Models.Characteristics();
                    characteristics.Name = tb_Name.Text;
                    characteristics.TypeCharacter = typeCharacteristicsContext.TypeCharacteristics.Where(x => x.Name == cb_TypeCharac.SelectedItem).First().Id;
                    MainCharacteristics.characteristicsContext.Characteristics.Add(characteristics);
                }
                else
                {
                    characteristics.Name = tb_Name.Text;
                    characteristics.TypeCharacter = typeCharacteristicsContext.TypeCharacteristics.Where(x => x.Name == cb_TypeCharac.SelectedItem).First().Id;
                }
                MainCharacteristics.characteristicsContext.SaveChanges();
                MainWindow.init.OpenPages(new Pages.Characteristics.Characteristics());
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
                MainWindow.init.OpenPages(new Pages.Characteristics.Characteristics());
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
