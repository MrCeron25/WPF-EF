using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    public partial class ReportsPage : Page
    {
        public ReportsPage()
        {
            InitializeComponent();
            GetStatistic();
        }

        private void GetStatistic()
        {
            Reports.ItemsSource = Data.GetStatistic();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (Reports.Items.Count > 0)
            {
                string Folder = $"{Directory.GetCurrentDirectory()}\\Statistics";
                try
                {
                    if (!Directory.Exists(Folder))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(Folder);
                    }
                    string data = "";
                    foreach (StatisticOnTickets statistic in Reports.Items)
                    {
                        data += $"{statistic}\n";
                    }
                    Saver.Save(Folder,
                               "statistics",
                               data);
                    MessageBox.Show($"Билеты сохранены по пути:\n{Folder}.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception error)
                {
                    MessageBox.Show($"Ошибка {error.Message}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show($"Записей нет.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
