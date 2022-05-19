using System.Windows;

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

        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
