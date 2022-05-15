using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Windows.Input;
using WpfApp1.Infrastructure.Commands;
using WpfApp1.Models;
using WpfApp1.ViewModels.Base;

namespace WpfApp1.ViewModels
{
    internal class RegistrationViewModel : ViewModel
    {
        #region Имя кнопки Зарегистрироваться

        private string _RegistrationButtonName = "Зарегистрироваться";
        public string RegistrationButtonName
        {
            get => _RegistrationButtonName;
            set => Set(ref _RegistrationButtonName, value);
        }

        #endregion

        #region Заголовок Имени

        private string _NameTitleText = "Имя";
        public string NameTitleText
        {
            get => _NameTitleText;
            set => Set(ref _NameTitleText, value);
        }

        #endregion

        #region Текст имени

        private string _Name = "1";
        public string Name
        {
            get => _Name;
            set => Set(ref _Name, value);
        }

        #endregion

        #region Заголовок Фамилии

        private string _SurnameTitleText = "Фамилия";
        public string SurnameTitleText
        {
            get => _SurnameTitleText;
            set => Set(ref _SurnameTitleText, value);
        }

        #endregion

        #region Текст Фамилии

        private string _Surname = "1";
        public string Surname
        {
            get => _Surname;
            set => Set(ref _Surname, value);
        }

        #endregion

        #region Заголовок Пола

        private string _SexTitleText = "Пол";
        public string SexTitleText
        {
            get => _SexTitleText;
            set => Set(ref _SexTitleText, value);
        }

        #endregion

        #region Текст Жен Пола

        private string _FemaleText = "Женский";
        public string FemaleText
        {
            get => _FemaleText;
            set => Set(ref _FemaleText, value);
        }

        #endregion

        #region Текст Муж Пола

        private string _MaleText = "Мужской";
        public string MaleText
        {
            get => _MaleText;
            set => Set(ref _MaleText, value);
        }

        #endregion

        #region Заголовок Дата рождения

        private string _DateOfBirthTitleText = "Дата рождения";
        public string DateOfBirthTitleText
        {
            get => _DateOfBirthTitleText;
            set => Set(ref _DateOfBirthTitleText, value);
        }

        #endregion

        #region Дата рождения

        private DateTime _DateOfBirth = DateTime.Now;
        public DateTime DateOfBirth
        {
            get => _DateOfBirth;
            set => Set(ref _DateOfBirth, value);
        }

        #endregion

        #region Заголовок Номер паспорта

        private string _PassportIdTitleText = "Номер паспорта";
        public string PassportIdTitleText
        {
            get => _PassportIdTitleText;
            set => Set(ref _PassportIdTitleText, value);
        }

        #endregion

        #region Номер паспорта

        private string _PassportId = "1234";
        public string PassportId
        {
            get => _PassportId;
            set => Set(ref _PassportId, value);
        }

        #endregion

        #region Заголовок Серия паспорта

        private string _PassportSeriesTitleText = "Серия паспорта";
        public string PassportSeriesTitleText
        {
            get => _PassportSeriesTitleText;
            set => Set(ref _PassportSeriesTitleText, value);
        }

        #endregion

        #region Серия паспорта

        private string _PassportSeries = "123456";
        public string PassportSeries
        {
            get => _PassportSeries;
            set => Set(ref _PassportSeries, value);
        }

        #endregion

        #region Заголовок Логин

        private string _LoginTitleText = "Логин";
        public string LoginTitleText
        {
            get => _LoginTitleText;
            set => Set(ref _LoginTitleText, value);
        }

        #endregion

        #region Логин

        private string _Login = "11";
        public string Login
        {
            get => _Login;
            set => Set(ref _Login, value);
        }

        #endregion

        #region Заголовок Пароль

        private string _PasswordTitleText = "Пароль";
        public string PasswordTitleText
        {
            get => _PasswordTitleText;
            set => Set(ref _PasswordTitleText, value);
        }

        #endregion

        #region Ссылка на страницу входа

        private string _EntryLink = "Вход";
        public string EntryLink
        {
            get => _EntryLink;
            set => Set(ref _EntryLink, value);
        }

        #endregion

        #region Команды

        public ICommand RegistrationCommand { get; }

        private bool CanRegistrationCommandExecute(object parameters) => true;

        private void OnRegistrationCommandExecuted(object parameters)
        {
            var items = (object[])parameters;
            string Password = items[0] as string;
            string Sex = items[1] as string;
            if (!Tools.CheckStrings(string.IsNullOrEmpty,
                                    Name,
                                    Surname,
                                    PassportId,
                                    PassportSeries,
                                    Login,
                                    Password))
            {
                if (uint.TryParse(PassportId, out _) &&
                    uint.TryParse(PassportSeries, out _) &&
                    PassportId.Length == 4 &&
                    PassportSeries.Length == 6)
                {
                    if (Tools.CheckPassword(Password))
                    {
                        try
                        {
                            List<system> FoundUsers = (
                                from system in Manager.Instance.Context.system
                                where system.login == Login
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
                                        name = Name,
                                        surname = Surname,
                                        sex = Sex.Substring(0, 1),
                                        date_of_birth = DateOfBirth,
                                        passport_id = int.Parse(PassportId),
                                        passport_series = int.Parse(PassportSeries),
                                    };
                                    Manager.Instance.Context.users.Add(NewUser);
                                    Manager.Instance.Context.SaveChanges();

                                    system NewSystem = new system
                                    {
                                        user_id = NewUser.id,
                                        login = Login,
                                        password = Tools.GetCrypt(Password),
                                        is_admin = false
                                    };
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
                    MessageBox.Show("Серия паспорта и Номер паспорта должны иметь только числа (6 и 4 соответственно).", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля ввода.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public ICommand EntryLinkCommand { get; }

        private bool CanEntryLinkExecute(object parameters) => true;

        private void OnEntryLinkExecuted(object parameters)
        {
            Manager.Instance.MainFrame.Navigate(new LoginPage());
        }

        #endregion


        #region Конструктор

        public RegistrationViewModel()
        {
            RegistrationCommand = new LambdaCommand(OnRegistrationCommandExecuted, CanRegistrationCommandExecute);
            EntryLinkCommand = new LambdaCommand(OnEntryLinkExecuted, CanEntryLinkExecute);
        }

        #endregion
    }
}
