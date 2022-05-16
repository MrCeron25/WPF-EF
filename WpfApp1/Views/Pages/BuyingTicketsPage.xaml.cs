using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp1.Models;

namespace WpfApp1
{
    public partial class BuyingTicketsPage : Page
    {
        private system SystemUser { get; set; }
        public BuyingTicketsPage(system SystemUser)
        {
            InitializeComponent();
            this.SystemUser = SystemUser;
            end.DisplayDateStart = start.SelectedDate;
        }

        private void Reverse_MouseDown(object sender, MouseEventArgs e)
        {
            string buf = to.Text;
            to.Text = from.Text;
            from.Text = buf;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            Viewing.IsEnabled = false;
            if (!Tools.CheckStrings(string.IsNullOrEmpty,
                                    from.Text,
                                    to.Text))
            {
                if (!string.IsNullOrEmpty(start.Text))
                {
                    Tickets.ItemsSource = null;
                    List<Flight> data;
                    if (string.IsNullOrEmpty(end.Text))
                    {
                        // пуста
                        data = Data.GetFlights(from.Text,
                                               to.Text,
                                               start.SelectedDate.Value);
                    }
                    else
                    {
                        data = Data.GetFlights(from.Text,
                                               to.Text,
                                               start.SelectedDate.Value,
                                               end.SelectedDate.Value);
                    }
                    Tickets.ItemsSource = data;
                    if (data.Count == 0)
                    {
                        MessageBox.Show("Рейсов не найдено.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    Tickets.ItemsSource = data;
                    Tickets.SelectedItem = null;
                }
                else
                {
                    MessageBox.Show("Заполните дату отправления.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Заполните города вылета и прилёта.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void start_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (end != null)
            {
                end.DisplayDateStart = start.SelectedDate;
            }
        }

        private void Tickets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Tickets.SelectedItem != null)
            {
                Viewing.IsEnabled = true;
            }
        }

        private void Viewing_Click(object sender, RoutedEventArgs e)
        {
            BuyingWindow window = new BuyingWindow(Tickets.SelectedItem as Flight);
            if ((bool)window.ShowDialog())
            {
                try
                {
                    tickets Newticket = new tickets
                    {
                        flight_id = long.Parse(window.FlightId.Text),
                        seat_number = (long)window.NumberOfSeats.SelectedItem,
                        user_id = SystemUser.user_id
                    };
                    Manager.Instance.Context.tickets.Add(Newticket);
                    Manager.Instance.Context.SaveChanges();
                    Search_Click(null, null); // обновление данных
                    MessageBox.Show("Вы успешно купили билет.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
