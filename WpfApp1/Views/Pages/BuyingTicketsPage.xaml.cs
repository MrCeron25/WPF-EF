using System.Windows.Controls;
using WpfApp1.Models;
using WpfApp1.ViewModels;

namespace WpfApp1
{
    public partial class BuyingTicketsPage : Page
    {
        public BuyingTicketsPage(system User)
        {
            InitializeComponent();
            ((BuyingTicketsViewModel)DataContext).User = User;
        }
    }
}
