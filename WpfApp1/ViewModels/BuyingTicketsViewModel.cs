using System;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfApp1.Infrastructure.Commands;
using WpfApp1.ViewModels.Base;
using WpfApp1.Models;

namespace WpfApp1.ViewModels
{
    internal class BuyingTicketsViewModel : ViewModel
    {
        #region Заголовок Город вылета

        private string _DepartureCityTitle = "Город вылета";
        public string DepartureCityTitle
        {
            get => _DepartureCityTitle;
            set => Set(ref _DepartureCityTitle, value);
        }

        #endregion

        #region Город вылета

        private string _DepartureCity = "Москва";
        public string DepartureCity
        {
            get => _DepartureCity;
            set => Set(ref _DepartureCity, value);
        }

        #endregion

        #region Заголовок Город прилёта

        private string _ArrivalCityTitle = "Город прилёта";
        public string ArrivalCityTitle
        {
            get => _ArrivalCityTitle;
            set => Set(ref _ArrivalCityTitle, value);
        }

        #endregion

        #region Город прилёта

        private string _ArrivalCity = "Самара";
        public string ArrivalCity
        {
            get => _ArrivalCity;
            set => Set(ref _ArrivalCity, value);
        }

        #endregion

        #region Заголовок От

        private string _StartDateTitle = "От";
        public string StartDateTitle
        {
            get => _StartDateTitle;
            set => Set(ref _StartDateTitle, value);
        }

        #endregion

        #region От

        private DateTime? _StartDate = DateTime.Now;
        public DateTime? StartDate
        {
            get => _StartDate;
            set
            {
                Set(ref _StartDate, value);
                if (EndDate != null &&
                    StartDate != null &&
                    StartDate > EndDate)
                {
                    EndDate = StartDate;
                }
            }
        }

        #endregion

        #region Заголовок До

        private string _EndDateTitle = "До";
        public string EndDateTitle
        {
            get => _EndDateTitle;
            set => Set(ref _EndDateTitle, value);
        }

        #endregion

        #region До

        private DateTime? _EndDate = null;
        public DateTime? EndDate
        {
            get => _EndDate;
            set => Set(ref _EndDate, value);
        }

        #endregion

        #region Дата начала

        private DateTime _StartDateDisplayDateStart = DateTime.Now;
        public DateTime StartDateDisplayDateStart
        {
            get => _StartDateDisplayDateStart;
            set => Set(ref _StartDateDisplayDateStart, value);
        }

        #endregion

        #region Найти

        private string _SearchText = "Найти";
        public string SearchText
        {
            get => _SearchText;
            set => Set(ref _SearchText, value);
        }

        #endregion

        #region Просмотр

        private string _ViewingText = "Просмотр";
        public string ViewingText
        {
            get => _ViewingText;
            set => Set(ref _ViewingText, value);
        }

        #endregion

        #region Билеты

        private ObservableCollection<Flight> _Tickets;
        public ObservableCollection<Flight> Tickets
        {
            get => _Tickets;
            set => Set(ref _Tickets, value);
        }

        #endregion

        #region Выбранный билет

        private Flight _SelectedTicket = null;
        public Flight SelectedTicket
        {
            get => _SelectedTicket;
            set
            {
                Set(ref _SelectedTicket, value);
                if (SelectedTicket != null) ViewingIsEnabled = true;
            }
        }

        #endregion

        #region Просмотр билетов

        private bool _ViewingIsEnabled = false;
        public bool ViewingIsEnabled
        {
            get => _ViewingIsEnabled;
            set => Set(ref _ViewingIsEnabled, value);
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

        #region Команды

        public ICommand ReverseTextCommand { get; }

        private bool CanReverseTextCommandExecute(object parameters) => true;

        private void OnReverseTextCommandExecuted(object parameters)
        {
            string buf = ArrivalCity;
            ArrivalCity = DepartureCity;
            DepartureCity = buf;
        }

        public ICommand SearchTicketsCommand { get; }

        private bool CanSearchTicketsExecute(object parameters) => true;

        private void OnSearchTicketsExecuted(object parameters)
        {
            ViewingIsEnabled = false;
            if (!Tools.CheckStrings(string.IsNullOrEmpty,
                                    DepartureCity,
                                    ArrivalCity))
            {
                if (StartDate != null)
                {
                    Tickets = null;
                    ObservableCollection<Flight> data;
                    if (EndDate == null)
                    {
                        // пусто
                        data = Data.GetFlights(DepartureCity,
                                               ArrivalCity,
                                               (DateTime)StartDate);
                    }
                    else
                    {
                        data = Data.GetFlights(DepartureCity,
                                               ArrivalCity,
                                               (DateTime)StartDate,
                                               (DateTime)EndDate);
                    }
                    if (data.Count == 0)
                    {
                        MessageBox.Show("Рейсов не найдено.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        Tickets = null;
                    }
                    else
                    {
                        Tickets = data;
                    }
                    SelectedTicket = null;
                }
                else
                {
                    MessageBox.Show("Заполните дату отправления.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Заполните города вылета и прилёта.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public ICommand ViewingTicketCommand { get; }

        private bool CanViewingTicketCommandExecute(object parameters) => true;

        private void OnViewingTicketCommandExecuted(object parameters)
        {
            BuyingWindow window = new BuyingWindow(SelectedTicket);
            try
            {
                if ((bool)window.ShowDialog())
                {
                    tickets Newticket = new tickets
                    {
                        flight_id = long.Parse(window.FlightId.Text),
                        seat_number = (long)window.NumberOfSeats.SelectedItem,
                        user_id = User.user_id
                    };
                    Manager.Instance.Context.tickets.Add(Newticket);
                    Manager.Instance.Context.SaveChanges();
                    SearchTicketsCommand.Execute(null); // обновление данных
                    MessageBox.Show("Вы успешно купили билет.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Конструктор

        public BuyingTicketsViewModel()
        {
            ReverseTextCommand = new LambdaCommand(OnReverseTextCommandExecuted, CanReverseTextCommandExecute);
            SearchTicketsCommand = new LambdaCommand(OnSearchTicketsExecuted, CanSearchTicketsExecute);
            ViewingTicketCommand = new LambdaCommand(OnViewingTicketCommandExecuted, CanViewingTicketCommandExecute);
        }

        #endregion
    }
}
