using System;
using System.Collections;
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
    public partial class BuyingTicketsPage : Page
    {
        private system SystemUser { get; set; }
        public BuyingTicketsPage(system SystemUser)
        {
            InitializeComponent();
            this.SystemUser = SystemUser;
            end.DisplayDateStart = start.SelectedDate;
            //Tickets.ItemsSource = Data.GetTickets(SystemUser);
        }

        private void Reverse_MouseDown(object sender, MouseEventArgs e)
        {
            string buf = to.Text;
            to.Text = from.Text;
            from.Text = buf;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
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
                        // 2 дата пуста
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

        private void start_SelectedDateChanged
                (object sender, SelectionChangedEventArgs e)
        {
            if (end != null)
            {
                end.DisplayDateStart = start.SelectedDate;
            }
        }
    }
}
