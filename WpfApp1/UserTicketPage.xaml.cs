using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
using System.Diagnostics;

namespace WpfApp1
{
    public class Ticket
    {
        public long Id { get; set; }
        public long FlightId { get; set; }
        public long SeatNumber { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string FlightName { get; set; }
        public TimeSpan TravelTime { get; set; }
        public double Price { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public string GetTicketName()
        {
            return $@"{Id} {Surname} {Name} {FlightName}";
        }

        public string CreateTicket()
        {
            string res = $"Номер билета : {Id}\nРейс : {FlightName}\nМесто : {SeatNumber}\nГород вылета : {DepartureCity}\nГород прилёта : {ArrivalCity}\nФамилия : {Surname}\nИмя : {Name}\nВремя отправления : {DepartureDate}\nВремя прилёта : {ArrivalDate}\nВремя в пути : {TravelTime}\nЦена : {Price}";
            return res;
        }
    }

    public partial class UserTicketPage : Page
    {
        private system SystemUser { get; set; }
        private void UpdateUserTickets()
        {
            Tickets.ItemsSource = Data.GetUserTickets(SystemUser);
        }

        public UserTicketPage(system SystemUser)
        {
            InitializeComponent();
            this.SystemUser = SystemUser;
            UpdateUserTickets();
        }

        private void SaveTickets_Click(object sender, RoutedEventArgs e)
        {
            if (Tickets.Items.Count > 0)
            {
                string Folder = $"{Directory.GetCurrentDirectory()}\\Tickets";
                try
                {
                    if (!Directory.Exists(Folder))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(Folder);
                    }
                    foreach (Ticket ticket in Tickets.Items)
                    {
                        Saver.Save(Folder,
                                   ticket.GetTicketName(),
                                   ticket.CreateTicket());
                    }
                    MessageBox.Show($"Билеты сохранены по пути:\n{Folder}.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception error)
                {
                    MessageBox.Show($"Ошибка {error.Message}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show($"Записей нет.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
