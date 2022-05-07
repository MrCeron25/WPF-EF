using System.Windows;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Manager.Instance.Context = new course_work_WPF_EFEntities();

            Manager.Instance.MainWindow = this;

            Manager.Instance.MainFrame = MainFrame;
            Manager.Instance.MainFrame.Content = new LoginPage();

            Manager.Instance.MenuFrame = MenuFrame;
            Manager.Instance.MenuFrame.Content = new MainMenuPage();
        }
    }
}
