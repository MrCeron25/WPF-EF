using System;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp1.Infrastructure.Commands;
using WpfApp1.ViewModels.Base;

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

        private DateTime _StartDate = DateTime.Now;
        public DateTime StartDate
        {
            get => _StartDate;
            set
            {
                Set(ref _StartDate, value);
                if (EndDate != null &&
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



        #region Команды

        public ICommand ReverseTextCommand { get; }

        private bool CanReverseTextCommandExecute(object parameters) => true;

        private void OnReverseTextCommandExecuted(object parameters)
        {
            string buf = ArrivalCity;
            ArrivalCity = DepartureCity;
            DepartureCity = buf;
        }

        public ICommand StartSelectedDateChangedCommand { get; }

        private bool CanStartSelectedDateChangedExecute(object parameters) => true;

        private void OnStartSelectedDateChangedExecuted(object parameters)
        {
            //var DatePicker = parameters as DatePicker;
            //StartDate.DisplayDateStart = start.SelectedDate;
            // DisplayDateStart="{Binding Mode=OneWay, Source={x:Static System:DateTime.Today}}"
        }

        #endregion

        #region Конструктор

        public BuyingTicketsViewModel()
        {
            ReverseTextCommand = new LambdaCommand(OnReverseTextCommandExecuted, CanReverseTextCommandExecute);
            StartSelectedDateChangedCommand = new LambdaCommand(OnStartSelectedDateChangedExecuted, CanStartSelectedDateChangedExecute);
        }

        #endregion
    }
}
