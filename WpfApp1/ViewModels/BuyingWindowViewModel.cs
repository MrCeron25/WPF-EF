using System.Windows.Input;
using WpfApp1.ViewModels.Base;
using WpfApp1.Infrastructure.Commands;
using System.Windows;
using System.Collections.ObjectModel;

namespace WpfApp1.ViewModels
{
    class BuyingWindowViewModel : ViewModel
    {
        #region Назваине окна

        private string _TitleName = "Окно покупки";
        public string TitleName
        {
            get => _TitleName;
            set => Set(ref _TitleName, value);
        }

        #endregion

        #region Заголовок Номер рейса

        private string _FlightNumberTitle = "Номер рейса";
        public string FlightNumberTitle
        {
            get => _FlightNumberTitle;
            set => Set(ref _FlightNumberTitle, value);
        }

        #endregion

        #region Номер рейса

        private string _FlightNumber = "";
        public string FlightNumber
        {
            get => _FlightNumber;
            set => Set(ref _FlightNumber, value);
        }

        #endregion

        #region Заголовок Рейс

        private string _FlightNameTitle = "Рейс";
        public string FlightNameTitle
        {
            get => _FlightNameTitle;
            set => Set(ref _FlightNameTitle, value);
        }

        #endregion

        #region Рейс

        private string _FlightName = "";
        public string FlightName
        {
            get => _FlightName;
            set => Set(ref _FlightName, value);
        }

        #endregion

        #region Заголовок Город вылета

        private string _DepartureCityTitle = "Город вылета";
        public string DepartureCityTitle
        {
            get => _DepartureCityTitle;
            set => Set(ref _DepartureCityTitle, value);
        }

        #endregion

        #region Город вылета

        private string _DepartureCity = "";
        public string DepartureCity
        {
            get => _DepartureCity;
            set => Set(ref _DepartureCity, value);
        }

        #endregion

        #region Заголовок Город прибытия

        private string _ArrivalCityTitle = "Город прибытия";
        public string ArrivalCityTitle
        {
            get => _ArrivalCityTitle;
            set => Set(ref _ArrivalCityTitle, value);
        }

        #endregion

        #region Город прибытия

        private string _ArrivalCity = "";
        public string ArrivalCity
        {
            get => _ArrivalCity;
            set => Set(ref _ArrivalCity, value);
        }

        #endregion

        #region Заголовок Время отправления

        private string _DepartureTimeTitle = "Время отправления";
        public string DepartureTimeTitle
        {
            get => _DepartureTimeTitle;
            set => Set(ref _DepartureTimeTitle, value);
        }

        #endregion

        #region Время отправления

        private string _DepartureTime = "";
        public string DepartureTime
        {
            get => _DepartureTime;
            set => Set(ref _DepartureTime, value);
        }

        #endregion


        #region Заголовок Время в пути

        private string _TravelTimeTitle = "Время в пути";
        public string TravelTimeTitle
        {
            get => _TravelTimeTitle;
            set => Set(ref _TravelTimeTitle, value);
        }

        #endregion

        #region Время в пути

        private string _TravelTime = "";
        public string TravelTime
        {
            get => _TravelTime;
            set => Set(ref _TravelTime, value);
        }

        #endregion


        #region Заголовок Время прибытия

        private string _ArrivalTimeTitle = "Время прибытия";
        public string ArrivalTimeTitle
        {
            get => _ArrivalTimeTitle;
            set => Set(ref _ArrivalTimeTitle, value);
        }

        #endregion

        #region Время прибытия

        private string _ArrivalTime = "";
        public string ArrivalTime
        {
            get => _ArrivalTime;
            set => Set(ref _ArrivalTime, value);
        }

        #endregion


        #region Заголовок прибытия

        private string _PlaceNumber = "№ места";
        public string PlaceNumber
        {
            get => _PlaceNumber;
            set => Set(ref _PlaceNumber, value);
        }

        #endregion



        #region Заголовок Цена

        private string _PriceTitle = "Цена";
        public string PriceTitle
        {
            get => _PriceTitle;
            set => Set(ref _PriceTitle, value);
        }

        #endregion

        #region Цена

        private double _Price = 0;
        public double Price
        {
            get => _Price;
            set => Set(ref _Price, value);
        }

        #endregion

        #region Номера мест

        private ObservableCollection<long> _SeatNumbers;
        public ObservableCollection<long> SeatNumbers
        {
            get => _SeatNumbers;
            set => Set(ref _SeatNumbers, value);
        }

        #endregion

        #region Выбранное место

        private long? _SelectedSeatNumber;
        public long? SelectedSeatNumber
        {
            get => _SelectedSeatNumber;
            set
            {
                Set(ref _SelectedSeatNumber, value);
                BuyIsEnabled = _SelectedSeatNumber != null;
            }
        }

        #endregion

        #region Покупка билета

        private bool _BuyIsEnabled = false;
        public bool BuyIsEnabled
        {
            get => _BuyIsEnabled;
            set => Set(ref _BuyIsEnabled, value);
        }

        #endregion

        #region Название кнопки

        private string _BuyButtonName = "Купить";
        public string BuyButtonName
        {
            get => _BuyButtonName;
            set => Set(ref _BuyButtonName, value);
        }

        #endregion

        #region Название кнопки

        private bool? _DialogResult = true;
        public bool? DialogResult
        {
            get => _DialogResult;
            set => Set(ref _DialogResult, value);
        }

        #endregion
    }
}
