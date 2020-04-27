using AirlineManagement.POCO.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement
{
    public class AnonymousUserFacade : FacadeBase, IAnonymousUserFacade, IFacade
    {
        /// <summary>
        /// Return list of airlinecompanies.
        /// </summary>
        /// <returns></returns>
        public IList<AirlineCompany> GetAllAirlineCompanies()
        {
            return _airlineDAO.GetAll();
        }
        /// <summary>
        /// Return list of airlinecompanies.
        /// </summary>
        /// <returns></returns>
        public IList<AirlineCompany> GetAllAirlineCompaniesByScheduledTime(string typeName)
        {
            return _airlineDAO.GetAllAirlineCompaniesByScheduledTime(typeName);
        }
        /// <summary>
        /// Return list of countries.
        /// </summary>
        /// <returns></returns>
        public IList<Country> GetAllCountries()
        {
            return _countryDAO.GetAll();
        }
        /// <summary>
        /// Return list of countries by scheduled time
        /// </summary>
        /// <returns></returns>
        public IList<Country> GetAllCountriesByScheduledTime(string typeName)
        {
            return _countryDAO.GetAllCountriesByScheduledTime(typeName);
        }
        /// <summary>
        /// Return list of all flights.
        /// </summary>
        /// <returns></returns>
        public IList<FlightView> GetAllFlights()
        {
            return _flightDAO.GetAll();
        }
        /// <summary>
        /// Get all flights by vacancy
        /// </summary>
        /// <returns></returns>
        public Dictionary<FlightView, int> GetAllFlightsByVacancy()
        {
            return _flightDAO.GetAllFlightsByVacancy();
        }
        /// <summary>
        /// Get all flights by vacancy and scheduled time
        /// </summary>
        /// <returns></returns>
        public IList<FlightView> GetAllFlightsByVacancyAndScheduledTime()
        {
            return _flightDAO.GetAllFlightsByVacancyAndScheduledTime();
        }
        /// <summary>
        /// Return flight by flight ID.
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns></returns>
        public FlightView GetFlightById(long flightId)
        {
            return _flightDAO.Get(flightId);
        }
        /// <summary>
        /// Return flight by flight ID.
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns></returns>
        public FlightView SearchFlightById(long flightId)
        {
            return _flightDAO.GetFlightById(flightId);
        }
        /// <summary>
        /// Return list of flights by depatrure date.
        /// </summary>
        /// <param name="departureDate"></param>
        /// <returns></returns>
        public IList<FlightView> GetFlightsByDepatrureDate(DateTime departureDate)
        {
            return _flightDAO.GetFlightsByDepartureDate(departureDate);
        }
        /// <summary>
        ///  Return list of flights by destination country.
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public IList<FlightView> GetFlightsByDestinationCountry(long countryCode)
        {
            return _flightDAO.GetFlightsByDestinationCountry(countryCode);
        }
        /// <summary>
        /// Return list of flights by landing date.
        /// </summary>
        /// <param name="landingDate"></param>
        /// <returns></returns>
        public IList<FlightView> GetFlightsByLandingDate(DateTime landingDate)
        {
            return _flightDAO.GetFlightsByLandingDate(landingDate);
        }
        /// <summary>
        /// Return list of flights by origin country code.
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public IList<FlightView> GetFlightsByOriginCountry(long countryCode)
        {
            return _flightDAO.GetFlightsByOriginCountry(countryCode);
        }

        /// <summary>
        /// Return list of filtered or all flights.
        /// </summary>
        /// <returns></returns>
        public IList<FlightView> GetAllFilteredFlights(string typeName, string flightId, string country, string company)
        {
            return _flightDAO.GetFilteredFlights(typeName, flightId, country, company);
        }
        /// <summary>
        /// Return list of filtered flights
        /// </summary>
        /// <param name="flightNumber"></param>
        /// <param name="originCountry"></param>
        /// <param name="destinationCountry"></param>
        /// <param name="company"></param>
        /// <param name="departureDate"></param>
        /// <param name="returnDate"></param>
        /// <returns></returns>
        public IList<FlightView> GetAllGoingFlightsByVacancyAndScheduledTime(string flightNumbers, string originCountry, string destinationCountry, string company, string departureDate)
        {
            return _flightDAO.GetAllGoingFlightsByVacancyAndScheduledTime(flightNumbers, originCountry, destinationCountry, company, departureDate);
        }
        public IList<FlightView> GetAllReturnFlightsByVacancyAndScheduledTime(string originCountry, string destinationCountry, string company, string returnDate)
        {
            return _flightDAO.GetAllReturnFlightsByVacancyAndScheduledTime(originCountry, destinationCountry, company, returnDate);
        }
        /// <summary>
        /// Return flights Ids.
        /// </summary>
        /// <returns></returns>
        public IList<long> GetAllFlightsIds()
        {
            return _flightDAO.GetFlightIds();
        }
        /// <summary>
        /// Return flights Ids.
        /// </summary>
        /// <returns></returns>
        public IList<long> GetFlightIdsByScheduledTime(string typeName)
        {
            return _flightDAO.GetFlightIdsByScheduledTime(typeName);
        }
        /// <summary>
        /// Update real depature time. Add delay time
        /// </summary>
        public void UpdateRealDepartureTime(long flightId, DateTime departureDateTime)
        {
            _flightDAO.UpdateRealDepartureTime(flightId, departureDateTime);
        }
        /// <summary>
        /// Update real arrival time. Add delay time
        /// </summary>
        public void UpdateRealArrivalTime(long flightId, DateTime arrivalDateTime)
        {
            _flightDAO.UpdateRealArrivalTime(flightId, arrivalDateTime);
        }
        public long CreateNewCustomerFromRedis(Customer customer)
        {

            string res = _customerDAO.CheckIfCustomerExist(customer);
            if (res == "0")
            {
                return _customerDAO.Add(customer);
            }
            else
            {
                throw new CustomerAlreadyExistException("Customer already exists");
            }

        }

    }
}
