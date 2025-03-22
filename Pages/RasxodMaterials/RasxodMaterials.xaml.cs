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
using Xceed.Document.NET;
using Xceed.Words.NET;
using YP02.Context;

namespace YP02.Pages.RasxodMaterials
{
    /// <summary>
    /// Логика взаимодействия для RasxodMaterials.xaml
    /// </summary>
    public partial class RasxodMaterials : Page
    {
        public RasxodMaterialsContext rasxodMaterialsContext = new RasxodMaterialsContext();
        private Models.Users currentUser;
        public UsersContext usContext = new UsersContext();

        public RasxodMaterials()
        {
            InitializeComponent();

            currentUser = MainWindow.init.CurrentUser;
            if (currentUser != null && currentUser.Role == "Администратор")
            {
                addBtn.Visibility = Visibility.Visible;
                exportDoc.Visibility = Visibility.Visible;
            }

            parent.Children.Clear();
            foreach (Models.RasxodMaterials item in rasxodMaterialsContext.RasxodMaterials)
            {
                parent.Children.Add(new Item(item, this));
            }
        }

        private void KeyDown_Search(object sender, KeyEventArgs e)
        {
            string searchText = search.Text.ToLower();
            var result = rasxodMaterialsContext.RasxodMaterials.Where(x =>
                x.Name.ToLower().Contains(searchText)
            );
            parent.Children.Clear();
            foreach (var item in result)
            {
                parent.Children.Add(new Item(item, this));
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Menu());
        }

        private void SortUp(object sender, RoutedEventArgs e)
        {
            var sortUp = rasxodMaterialsContext.RasxodMaterials.OrderBy(x => x.Name);
            parent.Children.Clear();

            foreach (var auditories in sortUp)
            {
                parent.Children.Add(new Item(auditories, this)); 
            }
        }

        private void SortDown(object sender, RoutedEventArgs e)
        {
            var sortDown = rasxodMaterialsContext.RasxodMaterials.OrderByDescending(x => x.Name);
            parent.Children.Clear(); 

            foreach (var auditories in sortDown)
            {
                parent.Children.Add(new Item(auditories, this)); 
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.RasxodMaterials.Add(this, null));
        }

        private void ExportRasxodMaterials(object sender, RoutedEventArgs e)
        {
            // Получаем текущую дату
            string currentDate = DateTime.Now.ToString("dd.MM.yyyy");

            // Создаем экземпляр контекста
            using (var rasxMatContext = new RasxodMaterialsContext())
            {
                // Получаем ID расходника
                int selectedEquipmentId = GetSelectedEquipmentId();

                // Получаем данные об расходных материалах из базы данных по ID
                var rasMat = rasxMatContext.RasxodMaterials
                    .FirstOrDefault(x => x.Id == selectedEquipmentId);

                if (rasMat == null)
                {
                    MessageBox.Show("Расходные материалы не найдены в базе данных.");
                    return;
                }

                // Получаем текущего пользователя
                var currentUser = usContext.Users.FirstOrDefault(x => x.Role == "Сотрудник");

                // Создаем новый документ
                using (DocX document = DocX.Create("Akt_Priema_Peredachi_Rasxodnyx_Materialov.docx"))
                {
                    // Добавляем заголовок
                    document.InsertParagraph("АКТ\nприема-передачи расходных материалов\n\n")
                        .Font("Times New Roman")
                        .FontSize(12)
                        .Alignment = Alignment.center;

                    // Добавляем информацию о месте и дате
                    var locationAndDate = document.InsertParagraph($"г. Пермь")
                        .Font("Times New Roman")
                        .FontSize(12)
                        .Alignment = Alignment.left;

                    var date = document.InsertParagraph($"{currentDate}\n")
                        .Font("Times New Roman")
                        .FontSize(12)
                        .Alignment = Alignment.right;

                    if (currentUser != null)
                    {
                        var fioParts = currentUser.FIO.Split(' ');
                        string lastName = fioParts[0]; // Фамилия
                        string initials = $"{fioParts[1][0]}.{fioParts[2][0]}."; // Инициалы (Имя и Отчество)
                        // Добавляем основной текст с отступом
                        var mainText = document.InsertParagraph($"КГАПОУ Пермский Авиационный техникум им. А.Д. Швецова в целях\nобеспечения необходимым оборудованием для исполнения должностных обязанностей\nпередаёт сотруднику {lastName} {initials}, а сотрудник принимает от учебного учреждения\nследующие расходные материалы:\n\n");
                        mainText.Font("Times New Roman");
                        mainText.FontSize(12);
                        mainText.IndentationFirstLine = 26;
                        mainText.Alignment = Alignment.both;
                    }

                    using (var typeContext = new TypeCharacteristicsContext())
                    {
                        int selectEquipmentId = GetSelectEquipmentId();
                        // Получаем тип расходника
                        var typeChar = typeContext.TypeCharacteristics
                            .FirstOrDefault(x => x.Id == selectEquipmentId);

                        using (var valueContext = new ValueCharacteristicsContext())
                        {
                            int selctEquipmentId = GetSelectEquipmentId();
                            var valueChar = valueContext.ValueCharacteristics
                            .FirstOrDefault(x => x.Id == selctEquipmentId);

                            // Добавляем информацию об расходных материалах в одной строке и по центру
                            var equipmentInfo = document.InsertParagraph($" {typeChar.Name} {rasMat.Name}, в количестве {rasMat.Quantity} шт, стоимостью {valueChar.Znachenie} руб. \n\n\n")
                                .Font("Times New Roman")
                                .FontSize(12)
                                .Alignment = Alignment.center;
                        }
                        
                    }
                    // Извлекаем фамилию и инициалы
                    if (currentUser != null)
                    {
                        var fioParts = currentUser.FIO.Split(' ');
                        string lastName = fioParts[0]; // Фамилия
                        string initials = $"{fioParts[1][0]}.{fioParts[2][0]}."; // Инициалы (Имя и Отчество)
                        var paragraph = document.InsertParagraph($"{lastName} {initials}       ____________________     ________________")
                            .Font("Times New Roman")
                            .FontSize(12)
                            .Alignment = Alignment.left;
                    }

                    // Сохраняем документ
                    document.Save();
                }

                MessageBox.Show("Документ успешно сгенерирован по пути: Desktop\\YP02\\bin\\Debug\\net6.0-windows");
            }
        }

        private int GetSelectedEquipmentId()
        {
            return 1;
        }

        private int GetSelectEquipmentId()
        {
            return 1;
        }
    }
}
