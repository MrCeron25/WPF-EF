using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Manager.Instance.MainFrame.Navigate(new LoginPage());
        }

        private bool CheckPassword(string password)
        {
            char[] specialCharactersArray = "%!@#$%^&*()?/>.<,:;'\\|}]{[_~`+=-\"".ToCharArray();
            if (password.Length < 8 || !password.Any(char.IsUpper) || !password.Any(char.IsLower))
            {
                return false;
            }
            else if (password.Any(specialCharactersArray.Contains))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            if (!Tools.CheckStrings(string.IsNullOrEmpty,
                                    Name.Text,
                                    Surname.Text,
                                    Sex.Text,
                                    DateOfBirth.Text,
                                    PassportId.Text,
                                    PassportSeries.Text,
                                    Login.Text,
                                    Password.Password))
            {
                if (CheckPassword(Password.Password))
                {
                    try
                    {
                        List<system> FoundUsers = (
                            from system in Manager.Instance.Context.system
                            where system.login == Login.Text
                            select system
                        ).ToList();
                        if (FoundUsers.Count > 0)
                        {
                            MessageBox.Show("Логин занят.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        {
                            try
                            {
                                users NewUser = new users
                                {
                                    name = Name.Text,
                                    surname = Surname.Text,
                                    sex = Sex.Text.Substring(0, 1),
                                    date_of_birth = (DateTime)DateOfBirth.SelectedDate,
                                    passport_id = int.Parse(PassportId.Text),
                                    passport_series = int.Parse(PassportSeries.Text),
                                };
                                Manager.Instance.Context.users.Add(NewUser);
                                Manager.Instance.Context.SaveChanges();

                                system NewSystem = new system
                                {
                                    user_id = NewUser.id,
                                    login = Login.Text,
                                    password = Tools.GetCrypt(Password.Password),
                                    is_admin = false
                                };

                                //Console.WriteLine($"{NewSystem.login} {NewSystem.password}\nХЕШ = '{Tools.GetCrypt(NewSystem.password)}'");

                                Manager.Instance.Context.system.Add(NewSystem);
                                Manager.Instance.Context.SaveChanges();

                                MessageBox.Show("Вы зарегистрированы.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                                Manager.Instance.MainFrame.Navigate(new LoginPage());
                            }
                            catch (Exception error)
                            {
                                MessageBox.Show(error.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    MessageBox.Show("Слишком лёгкий пароль.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля ввода.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CheckRegistrationButton(object sender, TextChangedEventArgs e)
        {
            if (PassportId != null &&
                PassportSeries != null &&
                Registration != null)
            {
                bool IsNumberPassportId = uint.TryParse(PassportId.Text, out _);
                bool IsNumberPassportSeries = uint.TryParse(PassportSeries.Text, out _);
                Registration.IsEnabled = IsNumberPassportId &&
                    IsNumberPassportSeries &&
                    PassportId.Text.Length == 4 &&
                    PassportSeries.Text.Length == 6;
            }
        }
    }
}
