using System.Windows;
using WpfApp1.Models;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Manager.Instance.Context = new course_work_WPF_EFEntities2();

            Manager.Instance.MainFrame = MainFrame;
            Manager.Instance.MainFrame.Navigate(new LoginPage());

            Manager.Instance.MenuFrame = MenuFrame;
            Manager.Instance.MenuFrame.Navigate(new MainMenuPage());
        }
    }
}
