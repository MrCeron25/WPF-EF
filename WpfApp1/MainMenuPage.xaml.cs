using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    public partial class MainMenuPage : Page
    {
        public MainMenuPage()
        {
            InitializeComponent();
        }

        private void RegistrationPage_Click(object sender, RoutedEventArgs e)
        {
            Manager.Instance.MainFrame.Navigate(new RegistrationPage());
        }

        private void LoginPage_Click(object sender, RoutedEventArgs e)
        {
            Manager.Instance.MainFrame.Navigate(new LoginPage());
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Manager.Instance.MainWindow.Close();
        }
    }
}
