using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Windows.Controls;

namespace WpfApp1
{
    public partial class AirplanesPage : Page
    {
        public AirplanesPage()
        {
            InitializeComponent();
            UpdateAirplanes();
        }

        private void UpdateAirplanes()
        {
            Airplanes.ItemsSource = Data.GetAirplanes();
        }

        private void Airplanes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Airplanes.SelectedItem == null)
            {
                ChangeAirplane.IsEnabled = false;
                DeleteAirplane.IsEnabled = false;
            }
            else
            {
                ChangeAirplane.IsEnabled = true;
                DeleteAirplane.IsEnabled = true;
            }
        }

        private void AddAirplane_Click(object sender, RoutedEventArgs e)
        {
            AirplaneWindow window = new AirplaneWindow();
            window.Button.Content = "Добавить";
            if ((bool)window.ShowDialog())
            {
                try
                {
                    airplane NewAirplane = new airplane
                    {
                        model = window.AirplaneModelName.Text,
                        number_of_seats = int.Parse(window.NumberOfSeats.Text)
                    };
                    Manager.Instance.Context.airplane.Add(NewAirplane);
                    Manager.Instance.Context.SaveChanges();
                    UpdateAirplanes();
                }
                catch (Exception error)
                {
                    MessageBox.Show($"{error.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ChangeAirplane_Click(object sender, RoutedEventArgs e)
        {
            airplane SelectedAirplane = Airplanes.SelectedItem as airplane;
            AirplaneWindow window = new AirplaneWindow();
            window.Button.Content = "Изменить";
            window.Button.IsEnabled = true;

            string AirplaneModelName = SelectedAirplane.model;
            window.AirplaneModelName.Text = AirplaneModelName;
            int NumberOfSeats = SelectedAirplane.number_of_seats;
            window.NumberOfSeats.Text = NumberOfSeats.ToString();

            if ((bool)window.ShowDialog() &&
                (AirplaneModelName != window.AirplaneModelName.Text ||
                NumberOfSeats != int.Parse(window.NumberOfSeats.Text)))
            {
                try
                {
                    SelectedAirplane.model = window.AirplaneModelName.Text;
                    SelectedAirplane.number_of_seats = int.Parse(window.NumberOfSeats.Text);
                    Manager.Instance.Context.SaveChanges();
                    UpdateAirplanes();
                }
                catch (Exception error)
                {
                    MessageBox.Show($"{error.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void DeleteAirplane_Click(object sender, RoutedEventArgs e)
        {
            airplane SelectedAirplane = Airplanes.SelectedItem as airplane;
            if (MessageBox.Show("Вы действительно хотите удалить самолёт?\nБудут удалены все связанные записи (рейсы, билеты).", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    List<flights> flights = (
                        from flight in Manager.Instance.Context.flights
                        where flight.airplane_id == SelectedAirplane.id
                        select flight
                    ).ToList();
                    foreach (flights flight in flights)
                    {
                        List<tickets> tickets = (
                            from ticket in Manager.Instance.Context.tickets
                            where ticket.flight_id == flight.id
                            select ticket
                        ).ToList();
                        Manager.Instance.Context.tickets.RemoveRange(tickets);
                    }
                    Manager.Instance.Context.flights.RemoveRange(flights);

                    Manager.Instance.Context.airplane.Remove(SelectedAirplane);
                    Manager.Instance.Context.SaveChanges();
                    UpdateAirplanes();
                }
                catch (Exception error)
                {
                    MessageBox.Show($"{error.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
