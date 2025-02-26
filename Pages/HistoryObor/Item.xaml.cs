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
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        HistoryObor MainHistoryObor;
        Models.HistoryObor HistoryObor;
        AudiencesContext auditoriesContext = new AudiencesContext();
        public Item(Models.HistoryObor HistoryObor, HistoryObor MainHistoryObor)
        {
            InitializeComponent();
            this.HistoryObor = HistoryObor;
            this.MainHistoryObor = MainHistoryObor;
            lb_IdAuditories.Content = auditoriesContext.Auditories.Where(x => x.Id == HistoryObor.Id).First().Name;
            lb_Date.Content = HistoryObor.Date.ToString();
        }
    }
}
