using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    public partial class AdminMenuPage : Page
    {
        private system SystemUser { get; set; }
        public AdminMenuPage(system SystemUser)
        {
            InitializeComponent();
            this.SystemUser = SystemUser;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Manager.Instance.MainFrame.Navigate(new LoginPage());
            Manager.Instance.MenuFrame.Navigate(new MainMenuPage());
        }

        private void CitiesAndCountries_Click(object sender, RoutedEventArgs e)
        {
            Manager.Instance.MainFrame.Navigate(new CitiesAndCountriesPage());
        }

        private void Airplanes_Click(object sender, RoutedEventArgs e)
        {
            Manager.Instance.MainFrame.Navigate(new AirplanesPage());
        }
    }
}
