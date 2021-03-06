using System.Windows.Input;
using WpfApp1.ViewModels.Base;
using System.Linq;
using WpfApp1.Infrastructure.Commands;
using System.Windows;
using System.Threading;

namespace WpfApp1.ViewModels
{
    class MainMenuViewModel : ViewModel
    {
        #region Вход

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

        #region Регистрация
        private string _RegistrationButtonName = "Регистрация";

        /// <summary>
        /// Регистрация
        /// </summary>
        public string RegistrationButtonName
        {
            get => _RegistrationButtonName;
            set => Set(ref _RegistrationButtonName, value);
        }

        #endregion

        #region Выход
        private string _ExitButtonName = "Выход";

        /// <summary>
        /// Выход
        /// </summary>
        public string ExitButtonName
        {
            get => _ExitButtonName;
            set => Set(ref _ExitButtonName, value);
        }

        #endregion

        #region Команды

        public ICommand CloseApplicationCommand { get; }

        private bool CanCloseApplicationCommandExecute(object parameters) => true;

        private void OnCloseApplicationCommandExecuted(object parameters)
        {
            Application.Current.Shutdown();
        }



        public ICommand EntryCommand { get; }

        private bool CanEntryCommandExecute(object parameters) => true;

        private void OnEntryCommandExecuted(object parameters)
        {
            Manager.Instance.MainFrame.Navigate(new LoginPage());
        }



        public ICommand RegistrationCommand { get; }

        private bool CanRegistrationCommandExecute(object p) => true;

        private void OnRegistrationCommandExecuted(object p)
        {
            Manager.Instance.MainFrame.Navigate(new RegistrationPage());
        }

        #endregion

        #region Test Connect

        /// <summary>
        /// Для ускорения загрузки данных для EF
        /// </summary>
        private void TestConnect()
        {
            var data = from system in Manager.Instance.Context.system
                       select system;
        }

        #endregion

        #region Конструктор

        public MainMenuViewModel()
        {
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            EntryCommand = new LambdaCommand(OnEntryCommandExecuted, CanEntryCommandExecute);
            RegistrationCommand = new LambdaCommand(OnRegistrationCommandExecuted, CanRegistrationCommandExecute);
            Thread testConnect = new Thread(TestConnect);
            testConnect.Start();
        }

        #endregion
    }
}
