using AirlineManagement.POCO.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement
{
    public interface IAnonymousUserFacade
    {
        IList<FlightView> GetAllFlights();
        IList<AirlineCompany> GetAllAirlineCompanies();
        Dictionary<FlightView, int> GetAllFlightsByVacancy();
        FlightView GetFlightByFlightId(long flightId);
        IList<FlightView> GetFlightsByOriginCountry(long countryCode);
        IList<FlightView> GetFlightsByDestinationCountry(long countryCode);
        IList<FlightView> GetFlightsByDepatrureDate(DateTime departureDate);
        IList<FlightView> GetFlightsByLandingDate(DateTime landingDate);
        FlightView SearchFlightById(long flightId);
        IList<Country> GetAllCountries();
        IList<long> GetAllFlightsIds();
        void UpdateRealDepartureTime(long flightId, DateTime departureDateTime);
        void UpdateRealArrivalTime(long flightId, DateTime arrivalDateTime);
        IList<FlightView> GetAllFilteredFlights(string typeName, string flightId, string country, string company);
        IList<Country> GetAllCountriesByScheduledTime(string typeName);
        IList<long> GetFlightIdsByScheduledTime(string typeName);
        IList<AirlineCompany> GetAllAirlineCompaniesByScheduledTime(string typeName);
        IList<FlightView> GetAllFlightsByVacancyAndScheduledTime();
        IList<FlightView> GetAllFlightsByVacancyAndScheduledTime(string localCountryName);
        long CreateNewCustomerFromRedis(Customer customer);
        IList<FlightView> GetAllGoingFlightsByVacancyAndScheduledTime(string flightNumber, string originCountry, string destinationCountry, string company, string departureDate);
        IList<FlightView> GetAllReturnFlightsByVacancyAndScheduledTime(string originCountry, string destinationCountry, string company, string returnDate);
        void AddNewCompanyToStandbyTable(AirlineCompanyView newCompany);
        IList<Flight> GetFlightsToFillCalendar(string o_countryName, string d_countryName, string companyName, string destinationDate, int monthsToAdd, int hoursToAdd);
    }
}
