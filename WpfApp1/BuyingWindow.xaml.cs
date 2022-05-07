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
    public partial class BuyingWindow : Window
    {

        public BuyingWindow(Flight flight)
        {
            InitializeComponent();
            FlightId.Text = flight.FlightId.ToString();
            FlightName.Text = flight.FlightName;
            DepartureCity.Text = flight.DepartureCity.ToString();
            ArrivalCity.Text = flight.ArrivalCity.ToString();
            DepartureDate.Text = flight.DepartureDate.ToString();
            TravelTime.Text = flight.TravelTime.ToString();
            ArrivalDate.Text = flight.ArrivalDate.ToString();
            Price.Text = flight.Price.ToString();
            NumberOfSeats.ItemsSource = Data.GetFreeSeats(flight.FlightId, flight.NumberOfSeat);
        }

        private void NumberOfSeats_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NumberOfSeats.SelectedItem != null)
            {
                Buy.IsEnabled = true;
            }
            else
            {
                Buy.IsEnabled = false;
            }
        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
