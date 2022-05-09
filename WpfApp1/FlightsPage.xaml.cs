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

namespace WpfApp1
{
    public partial class FlightsPage : Page
    {
        private void UpdateFlights()
        {
            Flights.ItemsSource = Data.GetFlights();
        }

        public FlightsPage()
        {
            InitializeComponent();
            UpdateFlights();
        }

        private void ViewFlights_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddFlight_Click(object sender, RoutedEventArgs e)
        {
            FlightWindow window = new FlightWindow();
            window.Button.Content = "Добавить";
            if ((bool)window.ShowDialog())
            {
                try
                {
                    flights NewFlight = new flights
                    {
                        flight_name = window.FlightName.Text,
                        departure_city = (window.DepartureCity.SelectedItem as cities).id,
                        arrival_city = (window.ArrivalCity.SelectedItem as cities).id,
                        airplane_id = (window.Airplane.SelectedItem as airplane).id,
                        departure_date = window.DepartureDate.Value.Value,
                        travel_time = window.TravelTime.Value.Value.TimeOfDay,
                        price = double.Parse(window.Price.Text)
                    };
                    Manager.Instance.Context.flights.Add(NewFlight);
                    Manager.Instance.Context.SaveChanges();
                    UpdateFlights();
                }
                catch (Exception error)
                {
                    MessageBox.Show($"{error.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ChangeFlight_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteFlight_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Flights_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Flights.SelectedItem == null)
            {
                ViewFlights.IsEnabled = false;
                AddFlight.IsEnabled = false;
                ChangeFlight.IsEnabled = false;
                DeleteFlight.IsEnabled = false;
                InArchive.IsEnabled = false;
            }
            else
            {
                ViewFlights.IsEnabled = true;
                AddFlight.IsEnabled = true;
                ChangeFlight.IsEnabled = true;
                DeleteFlight.IsEnabled = true;
                InArchive.IsEnabled = true;
            }
        }
    }
}
