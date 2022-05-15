using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
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

        private string _NameText = "1";
        public string NameText
        {
            get => _NameText;
            set => Set(ref _NameText, value);
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

        private string _SurnameText = "1";
        public string SurnameText
        {
            get => _SurnameText;
            set => Set(ref _SurnameText, value);
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

        #region Команды

        public ICommand RegistrationCommand { get; }

        private bool CanRegistrationCommandExecute(object parameters) => true;

        private void OnRegistrationCommandExecuted(object parameters)
        {
            string Password = parameters as string;
            
        }

        #endregion

        #region Конструктор
        public RegistrationViewModel()
        {
            RegistrationCommand = new LambdaCommand(OnRegistrationCommandExecuted, CanRegistrationCommandExecute);
        }
        #endregion
    }
}
