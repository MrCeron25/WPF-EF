using System.Collections.Generic;
using System.Windows;
using System.Collections;
using System.Linq;
using System.Windows.Controls;
using System.Data.SqlClient;
using System;
using System.Data.Entity.Infrastructure;

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
                try
                {
                    List<system> FoundUsers = (
                        from SystemUser in Manager.Instance.Context.system
                        where SystemUser.login == Login.Text
                        select SystemUser
                    ).ToList();

                    //List<system> aaa = (
                    //    from SystemUser in Manager.Instance.Context.system
                    //    select SystemUser
                    //).ToList();
                    //foreach (system user in aaa)
                    //{
                    //    string pass = Tools.GetCrypt(user.password);
                    //    user.password = pass;
                    //}
                    //Manager.Instance.Context.SaveChanges();
                    //foreach (system user in aaa)
                    //{
                    //    Console.WriteLine($"({user.user_id}, '{user.login}', '{user.password.Replace("\'", "\'\'")}', {(user.is_admin ? '1' : '0')}),\n");
                    //}

                    string CryptPassword = Tools.GetCrypt(Password.Password);
                    bool UserIsFound = FoundUsers.Count == 1 &&
                                       FoundUsers[0].password == CryptPassword;
                    if (!UserIsFound)
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
                            Manager.Instance.MainFrame.Navigate(new CitiesAndCountriesPage());
                            Manager.Instance.MenuFrame.Navigate(new AdminMenuPage(user));
                        }
                        else
                        {
                            Manager.Instance.MainFrame.Navigate(new BuyingTicketsPage(user));
                            Manager.Instance.MenuFrame.Navigate(new UserMenuPage(user));
                        }
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show($"{error.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
