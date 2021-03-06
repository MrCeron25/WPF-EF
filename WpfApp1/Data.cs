using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Windows;
using WpfApp1.Models;

namespace WpfApp1
{
    public class Ticket
    {
        public long Id { get; set; }
        public long FlightId { get; set; }
        public long SeatNumber { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string FlightName { get; set; }
        public TimeSpan TravelTime { get; set; }
        public double Price { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public string GetTicketName()
        {
            return $@"{Id} {Surname} {Name} {FlightName}";
        }

        public string CreateTicket()
        {
            string res = $"Номер билета : {Id}\nРейс : {FlightName}\nМесто : {SeatNumber}\nГород вылета : {DepartureCity}\nГород прилёта : {ArrivalCity}\nФамилия : {Surname}\nИмя : {Name}\nВремя отправления : {DepartureDate}\nВремя прилёта : {ArrivalDate}\nВремя в пути : {TravelTime}\nЦена : {Price}";
            return res;
        }
    }
    public class CountriesWithCities
    {
        public long CountryId { get; set; }
        public string CountryName { get; set; }
        public string CityName { get; set; }
    }

    public class Flight
    {
        public long FlightId { get; set; }
        public string FlightName { get; set; }
        public DateTime DepartureDate { get; set; }
        public TimeSpan TravelTime { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public long NumberOfSeat { get; set; }
        public double Price { get; set; }
    }

    public class FlightInfo
    {
        public long FlightId { get; set; }
        public string FlightName { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public DateTime DepartureDate { get; set; }
        public TimeSpan TravelTime { get; set; }
        public DateTime ArrivalDate { get; set; }
        public double Price { get; set; }
        public string Model { get; set; }
    }

    public class StatisticOnTickets
    {
        public int Year { get; set; }
        public string Month { get; set; }
        public double Sum { get; set; }

        public override string ToString()
        {
            return $"{Year,8} {Month,8} {Sum,20:n0}";
        }
    }

    public class PassengersOnFlight
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int PassportSeries { get; set; }
        public int PassportId { get; set; }
        public long SeatNumber { get; set; }
    }

    public static class Data
    {
        public static List<cities> GetCities()
        {
            try
            {
                List<cities> data = (
                    from city in Manager.Instance.Context.cities
                    orderby city.name
                    select city
                ).ToList();
                return data;
            }
            catch (Exception error)
            {
                MessageBox.Show($"{error.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public static List<StatisticOnTickets> GetStatistic()
        {
            try
            {
                List<StatisticOnTickets> data = (
                     from ti in Manager.Instance.Context.tickets
                     join fl in Manager.Instance.Context.flights on ti.flight_id equals fl.id
                     orderby fl.departure_date
                     group fl by fl.departure_date into g
                     select new StatisticOnTickets
                     {
                         Year = g.Key.Year,
                         Month = SqlFunctions.DateName("MM", g.Key),
                         Sum = g.Sum(fl => fl.price)
                     }
                 ).ToList();
                return data;
            }
            catch (Exception error)
            {
                MessageBox.Show($"{error.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public static ObservableCollection<Flight> GetFlights(string FromCity,
                                                              string ToCity,
                                                              DateTime StartDate,
                                                              DateTime EndDate)
        {
            try
            {
                return new ObservableCollection<Flight>(
                    from fl in Manager.Instance.Context.flights
                    join air in Manager.Instance.Context.airplane on fl.airplane_id equals air.id
                    where (from city in Manager.Instance.Context.cities
                           where city.id == fl.departure_city
                           select city.name).FirstOrDefault().ToLower().Contains(FromCity.ToLower()) &&
                          (from city in Manager.Instance.Context.cities
                           where city.id == fl.arrival_city
                           select city.name).FirstOrDefault().ToLower().Contains(ToCity.ToLower()) &&
                          (fl.departure_date >= StartDate) &&
                          (fl.arrival_date <= EndDate) &&
                          (air.number_of_seats - (from ti in Manager.Instance.Context.tickets
                                                  where fl.id == ti.flight_id
                                                  select ti).Count() > 0) &&
                          fl.is_archive == false
                    select new Flight
                    {
                        FlightId = fl.id,
                        FlightName = fl.flight_name,
                        DepartureDate = fl.departure_date,
                        TravelTime = fl.travel_time,
                        ArrivalDate = (DateTime)fl.arrival_date,
                        DepartureCity = (from ci in Manager.Instance.Context.cities
                                         where fl.departure_city == ci.id &&
                                               ci.name.ToLower().Contains(FromCity.ToLower())
                                         select ci.name).FirstOrDefault(),
                        ArrivalCity = (from ci in Manager.Instance.Context.cities
                                       where fl.arrival_city == ci.id &&
                                             ci.name.ToLower().Contains(ToCity.ToLower())
                                       select ci.name).FirstOrDefault(),
                        NumberOfSeat = air.number_of_seats - (from ti in Manager.Instance.Context.tickets
                                                              where fl.id == ti.flight_id
                                                              select ti).Count(),
                        Price = fl.price,
                    }
                );
            }
            catch (Exception error)
            {
                MessageBox.Show($"{error.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public static ObservableCollection<Flight> GetFlights(string FromCity,
                                                              string ToCity,
                                                              DateTime StartDate)
        {
            try
            {
                return new ObservableCollection<Flight>(
                    from fl in Manager.Instance.Context.flights
                    join air in Manager.Instance.Context.airplane on fl.airplane_id equals air.id
                    where (from city in Manager.Instance.Context.cities
                           where city.id == fl.departure_city
                           select city.name).FirstOrDefault().ToLower().Contains(FromCity.ToLower()) &&
                          (from city in Manager.Instance.Context.cities
                           where city.id == fl.arrival_city
                           select city.name).FirstOrDefault().ToLower().Contains(ToCity.ToLower()) &&
                          (fl.departure_date >= StartDate) &&
                          (air.number_of_seats - (from ti in Manager.Instance.Context.tickets
                                                  where fl.id == ti.flight_id
                                                  select ti).Count() > 0) &&
                          fl.is_archive == false
                    select new Flight
                    {
                        FlightId = fl.id,
                        FlightName = fl.flight_name,
                        DepartureDate = fl.departure_date,
                        TravelTime = fl.travel_time,
                        ArrivalDate = (DateTime)fl.arrival_date,
                        DepartureCity = (from ci in Manager.Instance.Context.cities
                                         where fl.departure_city == ci.id &&
                                               ci.name.ToLower().Contains(FromCity.ToLower())
                                         select ci.name).FirstOrDefault(),
                        ArrivalCity = (from ci in Manager.Instance.Context.cities
                                       where fl.arrival_city == ci.id &&
                                             ci.name.ToLower().Contains(ToCity.ToLower())
                                       select ci.name).FirstOrDefault(),
                        NumberOfSeat = air.number_of_seats - (from ti in Manager.Instance.Context.tickets
                                                              where fl.id == ti.flight_id
                                                              select ti).Count(),
                        Price = fl.price,
                    }
                );
            }
            catch (Exception error)
            {
                MessageBox.Show($"{error.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public static ObservableCollection<Ticket> GetUserTickets(system SystemUser)
        {
            try
            {
                return new ObservableCollection<Ticket>(
                    from ti in Manager.Instance.Context.tickets
                    join user in Manager.Instance.Context.users on ti.user_id equals user.id
                    join fl in Manager.Instance.Context.flights on ti.flight_id equals fl.id
                    join air in Manager.Instance.Context.airplane on fl.airplane_id equals air.id
                    where ti.user_id == SystemUser.user_id &&
                          fl.is_archive == false
                    select new Ticket
                    {
                        Id = ti.id,
                        FlightId = fl.id,
                        SeatNumber = ti.seat_number,
                        DepartureDate = fl.departure_date,
                        ArrivalDate = (DateTime)fl.arrival_date,
                        FlightName = fl.flight_name,
                        TravelTime = fl.travel_time,
                        Price = fl.price,
                        DepartureCity = (from ci in Manager.Instance.Context.cities
                                         where fl.departure_city == ci.id
                                         select ci.name).FirstOrDefault(),
                        ArrivalCity = (from ci in Manager.Instance.Context.cities
                                       where fl.arrival_city == ci.id
                                       select ci.name).FirstOrDefault(),
                        Name = user.name,
                        Surname = user.surname
                    }
                );
            }
            catch (Exception error)
            {
                MessageBox.Show($"{error.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public static List<FlightInfo> GetFlights()
        {
            try
            {
                List<FlightInfo> data = (
                    from fl in Manager.Instance.Context.flights
                    join air in Manager.Instance.Context.airplane on fl.airplane_id equals air.id
                    where fl.is_archive == false
                    select new FlightInfo
                    {
                        FlightId = fl.id,
                        FlightName = fl.flight_name,
                        DepartureCity = (from ci in Manager.Instance.Context.cities
                                         where fl.departure_city == ci.id
                                         select ci.name).FirstOrDefault(),
                        ArrivalCity = (from ci in Manager.Instance.Context.cities
                                       where fl.arrival_city == ci.id
                                       select ci.name).FirstOrDefault(),
                        DepartureDate = fl.departure_date,
                        TravelTime = fl.travel_time,
                        ArrivalDate = (DateTime)fl.arrival_date,
                        Price = fl.price,
                        Model = air.model,
                    }
                ).ToList();
                return data;
            }
            catch (Exception error)
            {
                MessageBox.Show($"{error.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public static List<PassengersOnFlight> GetPassengersOnFlights(long FlightId)
        {
            try
            {
                List<PassengersOnFlight> data = (
                    from fl in Manager.Instance.Context.flights
                    join ti in Manager.Instance.Context.tickets on fl.id equals ti.flight_id
                    join user in Manager.Instance.Context.users on ti.user_id equals user.id
                    where fl.is_archive == false &&
                          fl.id == FlightId
                    select new PassengersOnFlight
                    {
                        UserId = user.id,
                        Name = user.name,
                        Surname = user.surname,
                        Sex = user.sex,
                        DateOfBirth = user.date_of_birth,
                        PassportSeries = user.passport_series,
                        PassportId = user.passport_id,
                        SeatNumber = ti.seat_number
                    }
                ).ToList();
                return data;
            }
            catch (Exception error)
            {
                MessageBox.Show($"{error.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public static List<country> GetCountries()
        {
            try
            {
                List<country> data = (
                    from country_ in Manager.Instance.Context.country
                    select country_
                ).ToList();
                return data;
            }
            catch (Exception error)
            {
                MessageBox.Show($"{error.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public static List<airplane> GetAirplanes()
        {
            try
            {
                List<airplane> data = (
                    from airplane_ in Manager.Instance.Context.airplane
                    orderby airplane_.model
                    select airplane_
                ).ToList();
                return data;
            }
            catch (Exception error)
            {
                MessageBox.Show($"{error.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public static List<string> GetCountriesNames()
        {
            try
            {
                List<string> data = (
                    from country_ in Manager.Instance.Context.country
                    select country_.name
                ).ToList();
                return data;
            }
            catch (Exception error)
            {
                MessageBox.Show($"{error.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public static List<CountriesWithCities> GetCountriesWithCities()
        {
            try
            {
                List<CountriesWithCities> data = (
                    from country_ in Manager.Instance.Context.country
                    join city in Manager.Instance.Context.cities on country_.id equals city.country_id
                    select new CountriesWithCities
                    {
                        CountryId = country_.id,
                        CountryName = country_.name,
                        CityName = city.name
                    }
                ).ToList();
                return data;
            }
            catch (Exception error)
            {
                MessageBox.Show($"{error.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        private static List<long> GetOccupiedSeats(long FlightId)
        {
            try
            {
                return (
                    from fl in Manager.Instance.Context.flights
                    join ti in Manager.Instance.Context.tickets on fl.id equals ti.flight_id
                    where fl.id == FlightId &&
                          fl.is_archive == false
                    select ti.seat_number
                 ).ToList();
            }
            catch (Exception error)
            {
                MessageBox.Show($"{error.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public static List<long> GetFreeSeats(long FlightId, long numberOfSeat)
        {
            var occupiedPlaces = GetOccupiedSeats(FlightId);
            if (occupiedPlaces != null)
            {
                List<long> res = new List<long>();
                for (long i = 1; i <= numberOfSeat; i++)
                {
                    if (!occupiedPlaces.Contains(i))
                    {
                        res.Add(i);
                    }
                }
                return res;
            }
            else
            {
                return null;
            }
        }
    }
}
