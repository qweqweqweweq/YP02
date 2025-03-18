using Microsoft.EntityFrameworkCore;
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

namespace YP02.Pages.ValueCharacteristics
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        public ValueCharacteristics MainValueCharacteristics;
        public Models.ValueCharacteristics valueCharacteristics;
        RasxodMaterialsContext rasxodMaterialsContext = new RasxodMaterialsContext();
        CharacteristicsContext characteristicsContext = new CharacteristicsContext();

        public Add(ValueCharacteristics MainValueCharacteristics, Models.ValueCharacteristics valueCharacteristics = null)
        {
            InitializeComponent();
            this.MainValueCharacteristics = MainValueCharacteristics;
            this.valueCharacteristics = valueCharacteristics;

            if (valueCharacteristics != null)
            {
                text1.Content = "Изменение значения характеристики"; // Установка заголовка
                text2.Content = "Изменить"; // Изменение текста кнопки
                tb_Value.Text = valueCharacteristics.Znachenie; 
                cb_RasxMat.SelectedItem = rasxodMaterialsContext.RasxodMaterials.Where(x => x.Id == valueCharacteristics.IdRasxod).FirstOrDefault()?.Name;
                cb_Characteristic.SelectedItem = characteristicsContext.Characteristics.Where(x => x.Id == valueCharacteristics.IdCharacter).FirstOrDefault()?.Name;
            }

            foreach (var item in rasxodMaterialsContext.RasxodMaterials)
            {
                cb_RasxMat.Items.Add(item.Name);
            }

            foreach (var item in characteristicsContext.Characteristics)
            {
                cb_Characteristic.Items.Add(item.Name);
            }
        }

        private void Click_Redact(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tb_Value.Text))
            {
                MessageBox.Show("Введите значение характеристики"); // Сообщение об ошибке, если поле пустое
                return; // Прерывание выполнения метода
            }
            if (cb_RasxMat.SelectedItem == null)
            {
                MessageBox.Show("Выберите расходный материал"); // Сообщение об ошибке, если поле пустое
                return; // Прерывание выполнения метода
            }
            if (cb_Characteristic.SelectedItem == null)
            {
                MessageBox.Show("Выберите характеристику"); // Сообщение об ошибке, если поле пустое
                return; // Прерывание выполнения метода
            }

            if (valueCharacteristics == null)
            {
                valueCharacteristics = new Models.ValueCharacteristics();
                valueCharacteristics.Znachenie = tb_Value.Text;
                valueCharacteristics.IdRasxod = rasxodMaterialsContext.RasxodMaterials.Where(x => x.Name == cb_RasxMat.SelectedItem.ToString()).First().Id;
                valueCharacteristics.IdCharacter = characteristicsContext.Characteristics.Where(x => x.Name == cb_Characteristic.SelectedItem.ToString()).First().Id;
                MainValueCharacteristics.ValueCharacteristicsContext.ValueCharacteristics.Add(valueCharacteristics);
            }
            else 
            {
                valueCharacteristics.Znachenie = tb_Value.Text;
                valueCharacteristics.IdRasxod = rasxodMaterialsContext.RasxodMaterials.Where(x => x.Name == cb_RasxMat.SelectedItem.ToString()).First().Id;
                valueCharacteristics.IdCharacter = characteristicsContext.Characteristics.Where(x => x.Name == cb_Characteristic.SelectedItem.ToString()).First().Id;
            }

            // Сохранение изменений в базе данных
            MainValueCharacteristics.ValueCharacteristicsContext.SaveChanges();
            MainWindow.init.OpenPages(new Pages.ValueCharacteristics.ValueCharacteristics());
        }

        private void Click_Cancel_Redact(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.ValueCharacteristics.ValueCharacteristics());
        }
    }
}
