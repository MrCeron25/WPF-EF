using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using WpfApp1.Models;

namespace WpfApp1
{
    public partial class FlightWindow : Window
    {
        private void UpdateDepartureCity()
        {
            DepartureCity.ItemsSource = Data.GetCities();
        }

        private void UpdateAirplanes()
        {
            Airplane.ItemsSource = Data.GetAirplanes();
        }

        private void UpdateArrivalCity()
        {
            foreach (cities city in DepartureCity.Items)
            {
                if (city.name != (DepartureCity.SelectedItem as cities).name)
                {
                    ArrivalCity.Items.Add(city);
                }
            }
            ArrivalCity.IsEnabled = ArrivalCity.Items.Count > 0;
        }

        public FlightWindow()
        {
            InitializeComponent();
            UpdateDepartureCity();
            UpdateAirplanes();
        }

        private void CheckButton()
        {
            double number = -1;
            bool IsNumber = double.TryParse(Price.Text, out number) && number > 0;
            Regex regex = new Regex(@"^[A-Z]{2} \d{4}$");
            bool IsFlightName = regex.IsMatch(FlightName.Text);
            Button.IsEnabled = IsFlightName &&
                               DepartureCity.SelectedItem != null &&
                               ArrivalCity.SelectedItem != null &&
                               !string.IsNullOrEmpty(DepartureDate.Text) &&
                               !string.IsNullOrEmpty(TravelTime.Text) &&
                               IsNumber &&
                               Airplane.SelectedItem != null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void FlightName_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckButton();
        }

        private void DepartureDate_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            CheckButton();
        }

        private void TravelTime_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            CheckButton();
        }

        private void Price_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckButton();
        }

        private void ArrivalCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckButton();
        }

        private void DepartureCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ArrivalCity.SelectedItem = null;
            ArrivalCity.Items.Clear();
            UpdateArrivalCity();
        }

        private void Airplane_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckButton();
        }
    }
}
