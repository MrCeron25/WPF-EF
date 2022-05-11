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
using System.Windows.Shapes;

namespace WpfApp1
{
    public partial class PassengerWindow : Window
    {
        private long FlightId { get; set; }
        private void UpdatePassengers()
        {
            List<PassengersOnFlight> PassengersOnFlights = Data.GetPassengersOnFlights(FlightId);
            if (PassengersOnFlights.Count > 0)
            {
                Passengers.ItemsSource = PassengersOnFlights;
            }
            else
            {
                MessageBox.Show($"На данный рейс пассажиров не найдено.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
        }

        public PassengerWindow(long flightId)
        {
            InitializeComponent();
            FlightId = flightId;
            UpdatePassengers();
        }
    }
}
