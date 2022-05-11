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
            FlightWindow window = new FlightWindow();
            window.Button.Content = "Изменить";
            window.Button.IsEnabled = true;
            FlightInfo SelectedFlightInfo = Flights.SelectedItem as FlightInfo;
            window.FlightName.Text = SelectedFlightInfo.FlightName;

            List<cities> Cities = Data.GetCities();
            //window.DepartureCity.Text = SelectedFlightInfo.DepartureCity;
            window.DepartureCity.SelectedItem = Tools.BinarySearch(Cities,
                                                                   SelectedFlightInfo.DepartureCity,
                                                                   0,
                                                                   Cities.Count,
                                                                   item => item.name);

            //window.ArrivalCity.Text = SelectedFlightInfo.ArrivalCity;
            window.ArrivalCity.SelectedItem = Tools.BinarySearch(Cities,
                                                                 SelectedFlightInfo.ArrivalCity,
                                                                 0,
                                                                 Cities.Count,
                                                                 item => item.name);

            List<airplane> Airplanes = Data.GetAirplanes();
            //window.Airplane.Text = SelectedFlightInfo.Model;
            window.Airplane.SelectedItem = Tools.BinarySearch(Airplanes,
                                                              SelectedFlightInfo.Model,
                                                              0,
                                                              Airplanes.Count,
                                                              item => item.model);

            window.DepartureDate.Value = SelectedFlightInfo.DepartureDate;
            window.TravelTime.Value = new DateTime().AddSeconds(SelectedFlightInfo.TravelTime.TotalSeconds);
            window.Price.Text = SelectedFlightInfo.Price.ToString("n0");
            if ((bool)window.ShowDialog() && (
                window.FlightName.Text != SelectedFlightInfo.FlightName ||
                (window.DepartureCity.SelectedItem as cities).name != SelectedFlightInfo.DepartureCity ||
                (window.ArrivalCity.SelectedItem as cities).name != SelectedFlightInfo.ArrivalCity ||
                window.DepartureDate.Value.Value != SelectedFlightInfo.DepartureDate ||
                window.TravelTime.Value.Value.TimeOfDay != SelectedFlightInfo.TravelTime ||
                double.Parse(window.Price.Text) != SelectedFlightInfo.Price ||
                (window.Airplane.SelectedItem as airplane).model != SelectedFlightInfo.Model
                ))
            {
                try
                {
                    flights flight = (
                        from fl in Manager.Instance.Context.flights
                        where fl.id == SelectedFlightInfo.FlightId &&
                              fl.is_archive == false
                        select fl
                    ).ToList()[0];
                    flight.flight_name = window.FlightName.Text;
                    cities SelectedDepartureCity = window.DepartureCity.SelectedItem as cities;
                    flight.departure_city = SelectedDepartureCity.id;
                    cities SelectedArrivalCity = window.ArrivalCity.SelectedItem as cities;
                    flight.arrival_city = SelectedArrivalCity.id;
                    airplane SelectedAirplane = window.Airplane.SelectedItem as airplane;
                    flight.airplane_id = SelectedAirplane.id;
                    flight.departure_date = window.DepartureDate.Value.Value;
                    flight.travel_time = window.TravelTime.Value.Value.TimeOfDay;
                    flight.price = double.Parse(window.Price.Text);
                    Manager.Instance.Context.SaveChanges();
                    UpdateFlights();
                }
                catch (Exception error)
                {
                    MessageBox.Show($"{error.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void DeleteFlight_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить рейс?\nБудут удалены все связанные записи (билеты).", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    FlightInfo flightInfo = Flights.SelectedItem as FlightInfo;
                    flights SelectedFlight = (
                        from fl in Manager.Instance.Context.flights
                        where fl.id == flightInfo.FlightId
                        select fl
                    ).ToList()[0];
                    List<tickets> Tickets = (
                        from ticket in Manager.Instance.Context.tickets
                        where ticket.flight_id == SelectedFlight.id
                        select ticket
                    ).ToList();
                    Manager.Instance.Context.tickets.RemoveRange(Tickets);
                    Manager.Instance.Context.flights.Remove(SelectedFlight);
                    Manager.Instance.Context.SaveChanges();
                    UpdateFlights();
                }
                catch (Exception error)
                {
                    MessageBox.Show($"{error.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Flights_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Flights.SelectedItem == null)
            {
                ViewPassengers.IsEnabled = false;
                ChangeFlight.IsEnabled = false;
                DeleteFlight.IsEnabled = false;
                InArchive.IsEnabled = false;
            }
            else
            {
                ViewPassengers.IsEnabled = true;
                ChangeFlight.IsEnabled = true;
                DeleteFlight.IsEnabled = true;
                InArchive.IsEnabled = true;
            }
        }

        private void ViewPassengers_Click(object sender, RoutedEventArgs e)
        {
            FlightInfo flightInfo = Flights.SelectedItem as FlightInfo;
            PassengerWindow window = new PassengerWindow(flightInfo.FlightId);
            try
            {
                window.ShowDialog();
            }
            catch (InvalidOperationException)
            {
            }
        }

        private void InArchive_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите перенести рейс в архив?\nЭтот рейс будет скрыт от пользователей.", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                FlightInfo SelectedFlightInfo = Flights.SelectedItem as FlightInfo;
                flights SelectedFlight = (
                    from fl in Manager.Instance.Context.flights
                    where fl.id == SelectedFlightInfo.FlightId
                    select fl
                ).ToList()[0];
                SelectedFlight.is_archive = true;
                Manager.Instance.Context.SaveChanges();
                UpdateFlights();
            }
        }
    }
}
