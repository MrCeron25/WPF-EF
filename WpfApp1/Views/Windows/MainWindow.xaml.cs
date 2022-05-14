using System.Windows;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Manager.Instance.Context = new course_work_WPF_EFEntities1();

            Manager.Instance.MainFrame = MainFrame;
            Manager.Instance.MainFrame.Navigate(new LoginPage());

            Manager.Instance.MenuFrame = MenuFrame;
            Manager.Instance.MenuFrame.Navigate(new MainMenuPage());
        }
    }
}
