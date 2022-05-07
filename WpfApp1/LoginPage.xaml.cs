using System.Collections.Generic;
using System.Windows;
using System.Collections;
using System.Linq;
using System.Windows.Controls;

namespace WpfApp1
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!Tools.CheckStrings(string.IsNullOrEmpty, Login.Text, Password.Password))
            {
                List<system> FoundUsers = (
                    from SystemUser in Manager.Instance.Context.system
                    where (SystemUser.login == Login.Text) && (SystemUser.password == Password.Password)
                    select SystemUser
                ).ToList();
                if (FoundUsers.Count == 0)
                {
                    MessageBox.Show("Неверный логин или пароль.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    system user = FoundUsers[0];
                    Login.Text = "";
                    Password.Password = "";
                    if (user.is_admin)
                    {
                        //Manager.Instance.MainFrame.Navigate(new BuyingTicketsPage());
                        //Manager.Instance.MenuFrame.Navigate(new UserMenuPage());
                    }
                    else
                    {
                        Manager.Instance.MainFrame.Navigate(new BuyingTicketsPage(user));
                        Manager.Instance.MenuFrame.Navigate(new UserMenuPage(user));
                    }
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RegistrationLink_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Manager.Instance.MainFrame.Navigate(new RegistrationPage());
        }
    }
}
