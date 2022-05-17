using System.Windows.Controls;
using WpfApp1.Models;
using WpfApp1.ViewModels;

namespace WpfApp1
{
    public partial class UserTicketPage : Page
    {

        public UserTicketPage(system User)
        {
            InitializeComponent();
            ((UserTicketsViewModel)DataContext).User = User;
        }
    }
}
