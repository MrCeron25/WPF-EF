using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Models;

namespace WpfApp1
{
    public partial class UserMenuPage : Page
    {
        private system SystemUser { get; set; }
        public UserMenuPage(system systemUser)
        {
            InitializeComponent();
            SystemUser = systemUser;
            UserLogin.Text = SystemUser.login;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Manager.Instance.MainFrame.Navigate(new LoginPage());
            Manager.Instance.MenuFrame.Navigate(new MainMenuPage());
        }

        private void MyTickets_Click(object sender, RoutedEventArgs e)
        {
            Manager.Instance.MainFrame.Navigate(new UserTicketPage(SystemUser));
        }

        private void BuyingTickets_Click(object sender, RoutedEventArgs e)
        {
            Manager.Instance.MainFrame.Navigate(new BuyingTicketsPage(SystemUser));
        }
    }
}
