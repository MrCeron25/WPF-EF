using System.Windows.Controls;
using WpfApp1.Models;
using WpfApp1.ViewModels;

namespace WpfApp1
{
    public partial class UserMenuPage : Page
    {
        public UserMenuPage(system User)
        {
            InitializeComponent();
            ((UserMenuViewModel)DataContext).User = User;
            ((UserMenuViewModel)DataContext).UserLogin = User.login;
        }
    }
}
