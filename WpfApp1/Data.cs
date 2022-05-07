using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{

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

    public static class Data
    {
        //public static List<T> GetData<T>(this DbContext contetx) where T : class
        //{
        //    var data = (
        //        from ticket in
        //        select ticket
        //    ).ToList();

        //    Manager.Instance.Context.airplane.AsEnumerable();

        //    return data;
        //}




        public static List<Flight> GetFlights(string FromCity,
                                              string ToCity,
                                              DateTime StartDate,
                                              DateTime EndDate)
        {
            List<Flight> data = (
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
                                              select ti).Count() > 0)
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
            ).ToList();
            return data;
        }

        public static List<Flight> GetFlights(string FromCity,
                                              string ToCity,
                                              DateTime StartDate)
        {
            List<Flight> data = (
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
                                              select ti).Count() > 0)
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
            ).ToList();
            return data;
        }

        public static List<Ticket> GetUserTickets(system SystemUser)
        {
            List<Ticket> data = (
                from ti in Manager.Instance.Context.tickets
                join user in Manager.Instance.Context.users on ti.user_id equals user.id
                join fl in Manager.Instance.Context.flights on ti.flight_id equals fl.id
                join air in Manager.Instance.Context.airplane on fl.airplane_id equals air.id
                where (ti.user_id == SystemUser.user_id)
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
            ).ToList();
            return data;
        }
    }
}
