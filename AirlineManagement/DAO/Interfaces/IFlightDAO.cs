using AirlineManagement.POCO.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement
{
    public interface IFlightDAO : IBasicDB<FlightView>
    {
        Dictionary<FlightView, int> GetAllFlightsByVacancy();
        //Flight GetFlightById(int id);
        IList<FlightView> GetFlightsByOriginCountry(long countryCode);
        IList<FlightView> GetFlightsByDestinationCountry(long countryCode);
        IList<FlightView> GetFlightsByDepartureDate(DateTime departureDate);
        IList<FlightView> GetFlightsByLandingDate(DateTime landingDate);
        IList<FlightView> GetFlightsByAirlineCompanyId(long airlineCompanyId);
        IList<FlightView> GetFlightsByCustomerId(long CustomerId);
        void UpdateRemainingTickets(long flightId);
        int GetReminingTickets(long flightId);
        void RemoveFlightsByAirlineCompanyId(long airlineCompamyId);
        void RemoveFlightsByCountryCode(long countryCode);
        IList<FlightView> GetFlightsByCustomerUserName(string customerUserName);
        string CheckIfFlightExist(FlightView t);
        FlightView GetFlightById(long flightId);
        IList<long> GetFlightIds();
        void UpdateRealDepartureTime(long flightID, DateTime departureDateTime);
        void UpdateRealArrivalTime(long flightID, DateTime arrivalDateTime);
        IList<FlightView> GetFilteredFlights(string typeName, string flightId, string country, string company);
        IList<long> GetFlightIdsByScheduledTime(string typeName);
        IList<FlightView> GetAllFlightsByVacancyAndScheduledTime();
        IList<FlightView> GetAllGoingFlightsByVacancyAndScheduledTime(string flightNumber, string originCountry, string destinationCountry, string company, string departureDate);
        IList<FlightView> GetAllReturnFlightsByVacancyAndScheduledTime(string originCountry, string destinationCountry, string company, string returnDate);
    }
}
