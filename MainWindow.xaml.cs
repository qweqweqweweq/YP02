using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YP02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow init;
        public static Pages.Menu menu;
        public Models.Users CurrentUser { get; private set; }

        public MainWindow() : this("Default") { }

        public MainWindow(string role)
        {
            InitializeComponent();
            init = this;
            OpenPages(new Pages.Authorization());

        }
        public void OpenPages(Page page)
        {
            frame.Navigate(page);
        }
        public void SetCurrentUser(Models.Users user)
        {
            CurrentUser = user;
        }
    }
}