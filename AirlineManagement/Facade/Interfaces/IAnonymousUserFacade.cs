using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement
{
    public interface IAnonymousUserFacade
    {
        IList<Flight> GetAllFlights();
        IList<AirlineCompany> GetAllAirlineCompanies();
        Dictionary<Flight, int> GetAllFlightsByVacancy();
        Flight GetFlightById(long flightId);
        IList<Flight> GetFlightsByOriginCountry(long countryCode);
        IList<Flight> GetFlightsByDestinationCountry(long countryCode);
        IList<Flight> GetFlightsByDepatrureDate(DateTime departureDate);
        IList<Flight> GetFlightsByLandingDate(DateTime landingDate);
        Flight SearchFlightById(long flightId);
        IList<Country> GetAllCountries();
        IList<long> GetAllFlightsIds();
        void UpdateRealDepartureTime(long flightId, DateTime departureDateTime);
        void UpdateRealArrivalTime(long flightId, DateTime arrivalDateTime);
        IList<Flight> GetAllFilteredFlights(string typeName, string flightId, string country, string company);
        IList<Country> GetAllCountriesByScheduledTime(string typeName);
        IList<long> GetFlightIdsByScheduledTime(string typeName);
        IList<AirlineCompany> GetAllAirlineCompaniesByScheduledTime(string typeName);
        IList<Flight> GetAllFlightsByVacancyAndScheduledTime();
        long CreateNewCustomerFromRedis(Customer customer);
        IList<Flight> GetAllGoingFlightsByVacancyAndScheduledTime(string flightId, string originCountry, string destinationCountry, string company, string departureDate);
        IList<Flight> GetAllReturnFlightsByVacancyAndScheduledTime(string originCountry, string destinationCountry, string company, string returnDate);
    }
}
