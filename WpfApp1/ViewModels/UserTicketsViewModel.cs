using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Infrastructure.Commands;
using WpfApp1.Models;
using WpfApp1.ViewModels.Base;

namespace WpfApp1.ViewModels
{
    internal class UserTicketsViewModel : ViewModel
    {
        #region Текст кнопки Сохранить билеты

        private string _SaveTicketButtonName = "Сохранить билеты";
        public string SaveTicketButtonName
        {
            get => _SaveTicketButtonName;
            set => Set(ref _SaveTicketButtonName, value);
        }

        #endregion

        #region Билеты

        private ObservableCollection<Ticket> _Tickets;
        public ObservableCollection<Ticket> Tickets
        {
            get => _Tickets;
            set => Set(ref _Tickets, value);
        }

        #endregion

        #region Пользователь

        private system _User;
        public system User
        {
            get => _User;
            set
            {
                Set(ref _User, value);
                UpdateUserTickets();
            }
        }

        #endregion

        #region Методы

        private void UpdateUserTickets()
        {
            if (User != null)
            {
                Tickets = Data.GetUserTickets(User);
            }
        }

        private void SaveTickets()
        {
            try
            {
                if (Tickets != null &&
                    Tickets.Count > 0)
                {
                    string Folder = $"{Directory.GetCurrentDirectory()}\\Tickets";
                    if (!Directory.Exists(Folder))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(Folder);
                    }
                    foreach (Ticket ticket in Tickets)
                    {
                        Saver.Save(Folder,
                                   ticket.GetTicketName(),
                                   ticket.CreateTicket());
                    }
                    MessageBox.Show($"Билеты сохранены по пути:\n{Folder}.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Билетов нет.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show($"Ошибка {error.Message}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Команды

        public ICommand SaveTicketsCommand { get; }

        private bool CanSaveTicketsCommandExecute(object parameters) => true;

        private void OnSaveTicketsCommandExecuted(object parameters)
        {
            Thread NewThread = new Thread(SaveTickets);
            NewThread.Start();
        }

        #endregion

        #region Конструктор

        public UserTicketsViewModel()
        {
            SaveTicketsCommand = new LambdaCommand(OnSaveTicketsCommandExecuted, CanSaveTicketsCommandExecute);
        }

        #endregion
    }
}
