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

namespace YP02.Pages.HistoryObor
{
    /// <summary>
    /// Логика взаимодействия для HistoryObor.xaml
    /// </summary>
    public partial class HistoryObor : Page
    {
        private int _oborudovanieId;
        private Models.Users currentUser;

        public HistoryObor(int oborudovanieId)
        {
            InitializeComponent();

            currentUser = MainWindow.init.CurrentUser;
            
            _oborudovanieId = oborudovanieId;
            LoadHistory();
        }

        private void LoadHistory()
        {
            // Загрузка истории для данного оборудования
            var historyList = GetHistoryForOborudovanie(_oborudovanieId);
            foreach (var history in historyList)
            {
                // Создание пользовательского элемента для каждой записи истории
                var item = new Item(history, this);
                // Добавление элемента на страницу
                parent.Children.Add(item);
            }
        }

        private List<Models.HistoryObor> GetHistoryForOborudovanie(int oborudovanieId)
        {            
            using (var context = new HistoryOborContext())
            {
                return context.HistoryObor.Where(h => h.IdObor == oborudovanieId).ToList();
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Oborudovanie.Oborudovanie());
        }        
    }
}
