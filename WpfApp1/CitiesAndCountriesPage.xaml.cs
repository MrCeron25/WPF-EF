using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    public partial class CitiesAndCountriesPage : Page
    {
        private void UpdateCities()
        {
            Cities.ItemsSource = Data.GetCities();
        }

        private void UpdateCountries()
        {
            Countries.ItemsSource = Data.GetCountries();
        }

        public CitiesAndCountriesPage()
        {
            InitializeComponent();
            UpdateCities();
            UpdateCountries();
        }

        private void AddCountry_Click(object sender, RoutedEventArgs e)
        {
            CountryWindow window = new CountryWindow();
            window.Button.Content = "Добавить";
            if ((bool)window.ShowDialog())
            {
                try
                {
                    country NewCountry = new country
                    {
                        name = window.CountryName.Text
                    };
                    Manager.Instance.Context.country.Add(NewCountry);
                    Manager.Instance.Context.SaveChanges();
                }
                catch (Exception error)
                {
                    MessageBox.Show($"{error.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                UpdateCountries();
            }
        }

        private void Countries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Countries.SelectedItem == null)
            {
                ChangeCountry.IsEnabled = false;
                DeleteCountry.IsEnabled = false;
            }
            else
            {
                ChangeCountry.IsEnabled = true;
                DeleteCountry.IsEnabled = true;
            }
        }

        private void ChangeCountry_Click(object sender, RoutedEventArgs e)
        {
            CountryWindow window = new CountryWindow();
            country SelectedCountry = Countries.SelectedItem as country;
            string CountryName = SelectedCountry.name;
            window.CountryName.Text = CountryName;
            window.Button.IsEnabled = true;
            window.Button.Content = "Изменить";
            if ((bool)window.ShowDialog())
            {
                if (CountryName != window.CountryName.Text)
                {
                    try
                    {
                        SelectedCountry.name = window.CountryName.Text;
                        Manager.Instance.Context.SaveChanges();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show($"{error.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    UpdateCountries();
                    UpdateCities();
                    Countries.SelectedItem = null;
                }
            }
        }

        private void DeleteCountry_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Вы действительно хотите удалить страну?\nБудут удалены все связанные записи (страны, города, рейсы, архивные рейсы, билеты).", "Warning", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning);
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    country SelectedCountry = Countries.SelectedItem as country;
                    List<cities> cities = (
                        from city in Manager.Instance.Context.cities
                        where city.country_id == SelectedCountry.id
                        select city
                    ).ToList();
                    foreach (cities city in cities)
                    {
                        List<flights> flights = (
                            from flight in Manager.Instance.Context.flights
                            where (flight.departure_city == city.id) || (flight.arrival_city == city.id)
                            select flight
                        ).ToList();
                        foreach (flights flight in flights)
                        {
                            List<tickets> tickets = (
                                from ticket in Manager.Instance.Context.tickets
                                where ticket.flight_id == flight.id
                                select ticket
                            ).ToList();
                            Manager.Instance.Context.tickets.RemoveRange(tickets);
                        }
                        Manager.Instance.Context.flights.RemoveRange(flights);

                        List<archive_flights> archive_flights = (
                            from archive_flight in Manager.Instance.Context.archive_flights
                            where (archive_flight.departure_city == city.id) || (archive_flight.arrival_city == city.id)
                            select archive_flight
                        ).ToList();
                        Manager.Instance.Context.archive_flights.RemoveRange(archive_flights);
                    }
                    Manager.Instance.Context.cities.RemoveRange(cities);
                    Manager.Instance.Context.country.Remove(SelectedCountry);
                    Manager.Instance.Context.SaveChanges();
                }
                catch (Exception error)
                {
                    MessageBox.Show($"{error.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                UpdateCountries();
                UpdateCities();
            }
        }

        private void Cities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Cities.SelectedItem == null)
            {
                ChangeCity.IsEnabled = false;
                DeleteCity.IsEnabled = false;
            }
            else
            {
                ChangeCity.IsEnabled = true;
                DeleteCity.IsEnabled = true;
            }
        }

        private void ChangeCity_Click(object sender, RoutedEventArgs e)
        {
            CityWindow window = new CityWindow();

            CountriesWithCities SelectedItem = Cities.SelectedItem as CountriesWithCities;
            string CityName = SelectedItem.CityName;
            window.CityName.Text = CityName;
            string CountryName = SelectedItem.CountryName;
            window.Countries.SelectedItem = CountryName;

            window.Button.Content = "Изменить";
            window.Button.IsEnabled = true;
            if ((bool)window.ShowDialog())
            {
                string NewCountryName = window.Countries.SelectedItem as string;
                if (CityName != window.CityName.Text ||
                    CountryName != NewCountryName)
                {
                    try
                    {
                        country FoundCountry = (
                            from country_ in Manager.Instance.Context.country
                            where country_.name == NewCountryName
                            select country_
                        ).ToList()[0];

                        cities FoundCity = (
                            from city in Manager.Instance.Context.cities
                            where city.name == CityName &&
                                  city.country_id == SelectedItem.CountryId
                            select city
                        ).ToList()[0];

                        FoundCity.name = window.CityName.Text;
                        FoundCity.country_id = FoundCountry.id;

                        Manager.Instance.Context.SaveChanges();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show($"{error.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    UpdateCities();
                }
            }
        }

        private void DeleteCity_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Вы действительно хотите удалить город?\nБудут удалены все связанные записи (города, рейсы, архивные рейсы, билеты).", "Warning", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning);
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    CountriesWithCities SelectedItem = Cities.SelectedItem as CountriesWithCities;
                    cities city = (
                        from city_ in Manager.Instance.Context.cities
                        where (city_.name == SelectedItem.CityName) && (city_.country_id == SelectedItem.CountryId)
                        select city_
                    ).ToList()[0];

                    List<flights> flights = (
                        from flight in Manager.Instance.Context.flights
                        where (flight.departure_city == city.id) || (flight.arrival_city == city.id)
                        select flight
                    ).ToList();
                    foreach (flights flight in flights)
                    {
                        List<tickets> tickets = (
                            from ticket in Manager.Instance.Context.tickets
                            where ticket.flight_id == flight.id
                            select ticket
                        ).ToList();
                        Manager.Instance.Context.tickets.RemoveRange(tickets);
                    }
                    Manager.Instance.Context.flights.RemoveRange(flights);

                    List<archive_flights> archive_flights = (
                        from archive_flight in Manager.Instance.Context.archive_flights
                        where (archive_flight.departure_city == city.id) || (archive_flight.arrival_city == city.id)
                        select archive_flight
                    ).ToList();
                    Manager.Instance.Context.archive_flights.RemoveRange(archive_flights);

                    Manager.Instance.Context.cities.Remove(city);
                    Manager.Instance.Context.SaveChanges();
                }
                catch (Exception error)
                {
                    MessageBox.Show($"{error.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                UpdateCities();
            }
        }

        private void AddCity_Click(object sender, RoutedEventArgs e)
        {
            CityWindow window = new CityWindow();
            window.Button.Content = "Добавить";
            if ((bool)window.ShowDialog())
            {
                try
                {
                    string CountryName = window.Countries.SelectedItem as string;
                    country FoundCountry = (
                        from country_ in Manager.Instance.Context.country
                        where country_.name == CountryName
                        select country_
                    ).ToList()[0];
                    cities NewCity = new cities
                    {
                        name = window.CityName.Text,
                        country_id = FoundCountry.id
                    };
                    Manager.Instance.Context.cities.Add(NewCity);
                    Manager.Instance.Context.SaveChanges();
                }
                catch (Exception error)
                {
                    MessageBox.Show($"{error.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                UpdateCities();
            }
        }
    }
}
