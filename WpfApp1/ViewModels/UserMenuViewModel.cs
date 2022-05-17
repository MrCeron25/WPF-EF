using System.Windows.Input;
using WpfApp1.Infrastructure.Commands;
using WpfApp1.Models;
using WpfApp1.ViewModels.Base;

namespace WpfApp1.ViewModels
{
    internal class UserMenuViewModel : ViewModel
    {
        #region Заголовок Логин Пользователя

        private string _UserLoginTitle = "Пользователь";
        public string UserLoginTitle
        {
            get => _UserLoginTitle;
            set => Set(ref _UserLoginTitle, value);
        }

        #endregion

        #region Логин пользователя

        private string _UserLogin = "";
        public string UserLogin
        {
            get => _UserLogin;
            set => Set(ref _UserLogin, value);
        }

        #endregion

        #region Пользователь

        private system _User = null;
        public system User
        {
            get => _User;
            set => Set(ref _User, value);
        }

        #endregion

        #region Покупка билетов

        private string _BuyingTicketButtonName = "Покупка билетов";
        public string BuyingTicketButtonName
        {
            get => _BuyingTicketButtonName;
            set => Set(ref _BuyingTicketButtonName, value);
        }

        #endregion

        #region Мои билеты

        private string _MyTicketsButtonName = "Мои билеты";
        public string MyTicketsButtonName
        {
            get => _MyTicketsButtonName;
            set => Set(ref _MyTicketsButtonName, value);
        }

        #endregion

        #region Выход

        private string _ExitButtonName = "Выход";
        public string ExitButtonName
        {
            get => _ExitButtonName;
            set => Set(ref _ExitButtonName, value);
        }

        #endregion

        #region Команды

        public ICommand BuyingTicketsCommand { get; }

        private bool CanBuyingTicketsCommandExecute(object parameters) => true;

        private void OnBuyingTicketsCommandExecuted(object parameters)
        {
            Manager.Instance.MainFrame.Navigate(new BuyingTicketsPage(User));
        }

        public ICommand MyTicketsCommand { get; }

        private bool CanMyTicketsCommandExecute(object parameters) => true;

        private void OnMyTicketsCommandExecuted(object parameters)
        {
            Manager.Instance.MainFrame.Navigate(new UserTicketPage(User));
        }

        public ICommand ExitCommand { get; }

        private bool CanExitCommandExecute(object parameters) => true;

        private void OnExitCommandExecuted(object parameters)
        {
            Manager.Instance.MainFrame.Navigate(new LoginPage());
            Manager.Instance.MenuFrame.Navigate(new MainMenuPage());
        }

        #endregion

        #region Конструктор

        public UserMenuViewModel()
        {
            BuyingTicketsCommand = new LambdaCommand(OnBuyingTicketsCommandExecuted, CanBuyingTicketsCommandExecute);
            MyTicketsCommand = new LambdaCommand(OnMyTicketsCommandExecuted, CanMyTicketsCommandExecute);
            ExitCommand = new LambdaCommand(OnExitCommandExecuted, CanExitCommandExecute);
        }

        #endregion
    }
}
