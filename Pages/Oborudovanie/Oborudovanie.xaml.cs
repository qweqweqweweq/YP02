using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Input;
using YP02.Context;
using Page = System.Windows.Controls.Page;
using Xceed.Document.NET;
using Xceed.Words.NET;
using YP02.Models;
using ExcelDataReader;
using System.Diagnostics;

namespace YP02.Pages.Oborudovanie
{
    public partial class Oborudovanie : Page
    {
        public OborudovanieContext OborudovanieContext = new OborudovanieContext();
        public UsersContext UsersContext = new UsersContext();
        private Models.Users currentUser;
        public Models.Users Users;
        public Models.Oborudovanie Oborud;
        public UsersContext usContext = new UsersContext();
        public Models.ViewModel ViewModel;
        public ViewModelContext ViewModelContext = new ViewModelContext();

        private Item _selectedItem;

        public Oborudovanie()
        {
            InitializeComponent();

            currentUser = MainWindow.init.CurrentUser;
            if (currentUser != null && currentUser.Role == "Администратор")
            {
                addBtn.Visibility = Visibility.Visible;
                exportDoc.Visibility = Visibility.Visible;
                exportDoc1.Visibility = Visibility.Visible;
                import.Visibility = Visibility.Visible;
            }

            LoadEquipment();
        }

        private void LoadEquipment()
        {
            parent.Children.Clear();
            foreach (Models.Oborudovanie item in OborudovanieContext.Oborudovanie)
            {
                var itemControl = new Item(item, this);
                itemControl.SelectionChanged += ItemControl_SelectionChanged;
                parent.Children.Add(itemControl);
            }
        }

        private void ItemControl_SelectionChanged(object sender, EventArgs e)
        {
            var item = (Item)sender;

            if (item.IsSelected)
            {
                // Снимаем выделение с предыдущего выбранного элемента
                if (_selectedItem != null && _selectedItem != item)
                {
                    _selectedItem.IsSelected = false;
                }
                _selectedItem = item;
            }
            else if (_selectedItem == item)
            {
                _selectedItem = null;
            }
        }

        private Models.Oborudovanie GetSelectedEquipment()
        {
            return _selectedItem?.Oborudovanie;
        }

        private void KeyDown_Search(object sender, KeyEventArgs e)
        {
            try
            {
                string searchText = search.Text?.ToLowerInvariant();
                var result = OborudovanieContext.Oborudovanie.ToList().Where(x => x.Name != null && x.Name.ToLowerInvariant().Contains(searchText)).ToList();

                parent.Children.Clear();
                foreach (var item in result)
                {
                    parent.Children.Add(new Item(item, this));
                }
            }
            catch (Exception ex)
            {
                LogError("Ошибка поиска", ex);
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow.init.OpenPages(new Menu());
            }
            catch (Exception ex)
            {
                LogError("Ошибка при возврате в меню", ex);
            }
        }
        private void Sort(bool ascending)
        {
            try
            {
                var sortedItems = ascending
                    ? OborudovanieContext.Oborudovanie
                        .Where(x => x.Name != null) // Фильтрация null
                        .OrderBy(x => x.Name)
                        .ToList()
                    : OborudovanieContext.Oborudovanie
                        .Where(x => x.Name != null) // Фильтрация null
                        .OrderByDescending(x => x.Name)
                        .ToList();

                parent.Children.Clear();
                foreach (var oborudovanie in sortedItems)
                {
                    parent.Children.Add(new Item(oborudovanie, this));
                }
            }
            catch (Exception ex)
            {
                LogError("Ошибка", ex);
            }
        }

        private void SortUp(object sender, RoutedEventArgs e)
        {
            Sort(true);
        }

        private void SortDown(object sender, RoutedEventArgs e)
        {
            Sort(false);
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow.init.OpenPages(new Pages.Oborudovanie.Add(this, null));
            }
            catch (Exception ex)
            {
                LogError("Ошибка", ex);
            }
        }

        private void ExportObor(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedEquipment = GetSelectedEquipment();
                if (selectedEquipment == null)
                {
                    MessageBox.Show("Пожалуйста, выберите оборудование для генерации отчета.");
                    return;
                }

                // Получаем текущую дату
                string currentDate = DateTime.Now.ToString("dd.MM.yyyy");

                using (var obContext = new OborudovanieContext())
                {
                    var oborudovanie = obContext.Oborudovanie
                        .FirstOrDefault(x => x.Id == selectedEquipment.Id);

                    if (oborudovanie == null)
                    {
                        MessageBox.Show("Оборудование не найдено в базе данных.");
                        return;
                    }

                    var currentUser = usContext.Users.FirstOrDefault(x => x.Role == "Сотрудник");

                    using (DocX document = DocX.Create("Akt_Priema_Peredachi.docx"))
                    {
                        // Добавляем заголовок
                        document.InsertParagraph("АКТ\nприема-передачи оборудования\n\n")
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
                            string lastName = fioParts[0];
                            string initials = $"{fioParts[1][0]}.{fioParts[2][0]}.";
                            var mainText = document.InsertParagraph($"КГАПОУ Пермский Авиационный техникум им. А.Д. Швецова в целях\nобеспечения необходимым оборудованием для исполнения должностных обязанностей\nпередаёт сотруднику {lastName} {initials}, а сотрудник принимает от учебного учреждения\nследующее оборудование:\n\n");
                            mainText.Font("Times New Roman");
                            mainText.FontSize(12);
                            mainText.IndentationFirstLine = 26;
                            mainText.Alignment = Alignment.both;
                        }

                        using (var viewContext = new ViewModelContext())
                        {
                            var model = viewContext.ViewModel
                                .FirstOrDefault(x => x.Id == selectedEquipment.IdModelObor);

                            var equipmentInfo = document.InsertParagraph($" {oborudovanie.Name} {model?.Name}, серийный номер {oborudovanie.InventNumber}, стоимостью {oborudovanie.PriceObor} руб. \n\n\n")
                                .Font("Times New Roman")
                                .FontSize(12)
                                .Alignment = Alignment.center;
                        }

                        if (currentUser != null)
                        {
                            var fioParts = currentUser.FIO.Split(' ');
                            string lastName = fioParts[0];
                            string initials = $"{fioParts[1][0]}.{fioParts[2][0]}.";
                            var paragraph = document.InsertParagraph($"{lastName} {initials}       ____________________     ________________")
                                .Font("Times New Roman")
                                .FontSize(12)
                                .Alignment = Alignment.left;
                        }

                        document.Save();
                    }

                    MessageBox.Show("Документ успешно сгенерирован по пути: Desktop\\YP02\\bin\\Debug\\net6.0-windows");
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

        private void ExportObor1(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedEquipment = GetSelectedEquipment();
                if (selectedEquipment == null)
                {
                    MessageBox.Show("Пожалуйста, выберите оборудование для генерации отчета.");
                    return;
                }

                string currentDate = DateTime.Now.ToString("dd.MM.yyyy");

                using (var obContext = new OborudovanieContext())
                {
                    var oborudovanie = obContext.Oborudovanie
                        .FirstOrDefault(x => x.Id == selectedEquipment.Id);

                    if (oborudovanie == null)
                    {
                        MessageBox.Show("Оборудование не найдено в базе данных.");
                        return;
                    }

                    var currentUser = usContext.Users.FirstOrDefault(x => x.Role == "Сотрудник");

                    using (DocX document = DocX.Create("Akt_Priema_Peredachi_Vrem_Polz.docx"))
                    {
                        document.InsertParagraph("АКТ\nприема-передачи оборудования на временное пользование\n\n")
                            .Font("Times New Roman")
                            .FontSize(12)
                            .Alignment = Alignment.center;

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
                            string lastName = fioParts[0];
                            string initials = $"{fioParts[1][0]}.{fioParts[2][0]}.";
                            var mainText = document.InsertParagraph($"КГАПОУ Пермский Авиационный техникум им. А.Д. Швецова в целях\nобеспечения необходимым оборудованием для исполнения должностных обязанностей\nпередаёт сотруднику {lastName} {initials}, а сотрудник принимает от учебного учреждения\nследующее оборудование:\n\n");
                            mainText.Font("Times New Roman");
                            mainText.FontSize(12);
                            mainText.IndentationFirstLine = 26;
                            mainText.Alignment = Alignment.both;
                        }

                        using (var viewContext = new ViewModelContext())
                        {
                            var model = viewContext.ViewModel
                                .FirstOrDefault(x => x.Id == selectedEquipment.IdModelObor);

                            var equipmentInfo = document.InsertParagraph($" {oborudovanie.Name} {model?.Name}, серийный номер {oborudovanie.InventNumber}, стоимостью {oborudovanie.PriceObor} руб. \n\n")
                                .Font("Times New Roman")
                                .FontSize(12)
                                .Alignment = Alignment.center;
                        }

                        var lastText = document.InsertParagraph($"По окончанию должностных работ  «__»  ____________  20___  года, работник\nобязуется вернуть полученное оборудование.\n\n");
                        lastText.Font("Times New Roman");
                        lastText.FontSize(12);
                        lastText.IndentationFirstLine = 26;
                        lastText.Alignment = Alignment.both;

                        if (currentUser != null)
                        {
                            var fioParts = currentUser.FIO.Split(' ');
                            string lastName = fioParts[0];
                            string initials = $"{fioParts[1][0]}.{fioParts[2][0]}.";
                            var paragraph = document.InsertParagraph($"{lastName} {initials}       ____________________     ________________")
                                .Font("Times New Roman")
                                .FontSize(12)
                                .Alignment = Alignment.left;
                        }

                        document.Save();
                    }

                    MessageBox.Show("Документ успешно сгенерирован по пути: Desktop\\YP02\\bin\\Debug\\net6.0-windows");
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

        private List<Models.Oborudovanie> ReadExcelFile(string filePath)
        {
            List<Models.Oborudovanie> equipmentList = new List<Models.Oborudovanie>();

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();
                    var table = result.Tables[0];

                    using (var context = new UsersContext())
                    {
                        for (int i = 1; i < table.Rows.Count; i++)
                        {
                            var row = table.Rows[i];
                            string userFIO = row[0].ToString().Trim();
                            var user = context.Users.FirstOrDefault(u => u.FIO == userFIO);

                            if (user == null)
                            {
                                MessageBox.Show($"Ошибка: Пользователь '{userFIO}' не найден в базе!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                continue;
                            }

                            equipmentList.Add(new Models.Oborudovanie
                            {
                                Name = row[1].ToString(),
                                InventNumber = row[2].ToString(),
                                PriceObor = "Не указано",
                                IdResponUser = user.Id,
                                IdTimeResponUser = user.Id,
                                IdClassroom = 8,
                                IdNapravObor = 7,
                                IdStatusObor = 9,
                                IdModelObor = 6,
                                Comments = "Импортировано из Excel"
                            });
                        }
                    }
                }
            }
            return equipmentList;
        }

        private void SaveToDatabase(List<Models.Oborudovanie> equipmentList)
        {
            try
            {
                using (var context = new OborudovanieContext()) // Ваш DbContext
                {
                    context.Oborudovanie.AddRange(equipmentList);
                    context.SaveChanges();
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

        private void GoImport(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Excel Files|*.xls;*.xlsx",
                    Title = "Выберите файл Excel"
                };
                if (openFileDialog.ShowDialog() == true)
                {
                    string filePath = openFileDialog.FileName;
                    List<Models.Oborudovanie> equipmentList = ReadExcelFile(filePath);

                    // Сохраняем данные в базу
                    SaveToDatabase(equipmentList);

                    MessageBox.Show("Импорт завершён успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    parent.Children.Clear();
                    foreach (Models.Oborudovanie item in OborudovanieContext.Oborudovanie)
                    {
                        parent.Children.Add(new Item(item, this));
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
        private void LogError(string message, Exception ex)
        {
            Debug.WriteLine($"{message}: {ex.Message}");

            try
            {
                using var errorsContext = new ErrorsContext();
                errorsContext.Errors.Add(new Models.Errors { Message = ex.Message });
                errorsContext.SaveChanges();

                string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt");
                File.AppendAllText(logPath, $"{DateTime.Now}: {ex.Message}\n{ex.StackTrace}\n\n");
            }
            catch (Exception logEx)
            {
                MessageBox.Show($"Ошибка при записи в лог-файл: {logEx.Message}", "Логирование ошибки", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            MessageBox.Show($"{message}: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}