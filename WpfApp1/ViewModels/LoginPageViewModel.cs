﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Infrastructure.Commands;
using WpfApp1.ViewModels.Base;

namespace WpfApp1.ViewModels
{
    class LoginPageViewModel : ViewModel
    {
        #region Заголовок Вход в систему

        private string _LoginTitle = "Вход в систему";

        /// <summary>
        /// Заголовок страницы
        /// </summary>
        public string LoginTitle
        {
            get => _LoginTitle;
            set => Set(ref _LoginTitle, value);
        }

        #endregion

        #region Заголовок Логин

        private string _LoginName = "Логин";

        /// <summary>
        /// Логин
        /// </summary>
        public string LoginName
        {
            get => _LoginName;
            set => Set(ref _LoginName, value);
        }

        #endregion

        #region Заголовок Пароль

        private string _PasswordName = "Пароль";

        /// <summary>
        /// Пароль
        /// </summary>
        public string PasswordName
        {
            get => _PasswordName;
            set => Set(ref _PasswordName, value);
        }

        #endregion

        #region Заголовок Регистрация

        private string _RegistrationLinkName = "Регистрация";

        /// <summary>
        /// Регистрация
        /// </summary>
        public string RegistrationLink
        {
            get => _RegistrationLinkName;
            set => Set(ref _RegistrationLinkName, value);
        }

        #endregion

        #region Имя кнопки Вход

        private string _EntryButtonName = "Вход";

        /// <summary>
        /// Вход
        /// </summary>
        public string EntryButtonName
        {
            get => _EntryButtonName;
            set => Set(ref _EntryButtonName, value);
        }

        #endregion

        #region Текст Логина

        private string _Login = "2";

        /// <summary>
        /// Текст Логина
        /// </summary>
        public string Login
        {
            get => _Login;
            set => Set(ref _Login, value);
        }

        #endregion

        #region Команды

        public ICommand RegistrationCommand { get; }

        private bool CanRegistrationCommandExecute(object parameters) => true;

        private void OnRegistrationCommandExecuted(object parameters)
        {
            Manager.Instance.MainFrame.Navigate(new RegistrationPage());
        }



        public ICommand EntryCommand { get; }

        private bool CanEntryCommandExecute(object parameters) => true;

        private void OnEntryCommandExecuted(object parameters)
        {
            string Password = parameters as string;
            if (!Tools.CheckStrings(string.IsNullOrEmpty, Login, Password))
            {
                try
                {
                    List<system> FoundUsers = (
                        from SystemUser in Manager.Instance.Context.system
                        where SystemUser.login == Login
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

                    bool UserIsFound = FoundUsers.Count == 1 &&
                                       FoundUsers[0].password == Tools.GetCrypt(Password);
                    if (!UserIsFound)
                    {
                        MessageBox.Show("Неверный логин или пароль.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        system user = FoundUsers[0];
                        Login = "";
                        Password = "";
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

        #endregion

        #region Конструктор
        public LoginPageViewModel()
        {
            RegistrationCommand = new LambdaCommand(OnRegistrationCommandExecuted, CanRegistrationCommandExecute);
            EntryCommand = new LambdaCommand(OnEntryCommandExecuted, CanEntryCommandExecute);
        }
        #endregion
    }
}
