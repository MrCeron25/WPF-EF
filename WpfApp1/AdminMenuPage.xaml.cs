using System.Windows;
using System.Windows.Controls;
using WpfApp1.Models;

namespace WpfApp1
{
    public partial class AdminMenuPage : Page
    {
        private system SystemUser { get; set; }
        public AdminMenuPage(system SystemUser)
        {
            InitializeComponent();
            this.SystemUser = SystemUser;
            AdminLogin.Text = SystemUser.login;
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

        private void Reports_Click(object sender, RoutedEventArgs e)
        {
            Manager.Instance.MainFrame.Navigate(new ReportsPage());
        }

        private void Flights_Click(object sender, RoutedEventArgs e)
        {
            Manager.Instance.MainFrame.Navigate(new FlightsPage());
        }
    }
}
