using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement
{
    public interface IFlightDAO : IBasicDB<Flight>
    {
        Dictionary<Flight, int> GetAllFlightsByVacancy();
        //Flight GetFlightById(int id);
        IList<Flight> GetFlightsByOriginCountry(long countryCode);
        IList<Flight> GetFlightsByDestinationCountry(long countryCode);
        IList<Flight> GetFlightsByDepartureDate(DateTime departureDate);
        IList<Flight> GetFlightsByLandingDate(DateTime landingDate);
        IList<Flight> GetFlightsByAirlineCompanyId(long airlineCompanyId);
        IList<Flight> GetFlightsByCustomerId(long CustomerId);
        void UpdateRemainingTickets(long flightId);
        int GetReminingTickets(long flightId);
        void RemoveFlightsByAirlineCompanyId(long airlineCompamyId);
        void RemoveFlightsByCountryCode(long countryCode);
        IList<Flight> GetFlightsByCustomerUserName(string customerUserName);
        string CheckIfFlightExist(Flight t);
        Flight GetFlightById(long flightId);
        IList<long> GetFlightIds();
        void UpdateRealDepartureTime(long flightID, DateTime departureDateTime);
        void UpdateRealArrivalTime(long flightID, DateTime arrivalDateTime);
        IList<Flight> GetFilteredFlights(string typeName, string flightId, string country, string company);
        IList<long> GetFlightIdsByScheduledTime(string typeName);
        IList<Flight> GetAllFlightsByVacancyAndScheduledTime();
        IList<Flight> GetAllGoingFlightsByVacancyAndScheduledTime(string flightId, string originCountry, string destinationCountry, string company, string departureDate);
        IList<Flight> GetAllReturnFlightsByVacancyAndScheduledTime(string flightId, string originCountry, string destinationCountry, string company, string returnDate);
    }
}
