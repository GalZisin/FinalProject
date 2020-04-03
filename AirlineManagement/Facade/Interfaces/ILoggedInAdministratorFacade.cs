using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement
{
    public interface ILoggedInAdministratorFacade
    {
        long CreateNewAirline(LoginToken<Administrator> token, AirlineCompany airline);
        void UpdateAirlineDetails(LoginToken<Administrator> token, AirlineCompany customer);
        void RemoveAirline(LoginToken<Administrator> token, AirlineCompany airline);
        long CreateNewCustomer(LoginToken<Administrator> token, Customer customer);
        void UpdateCustomerDetails(LoginToken<Administrator> token, Customer customer);
        void RemoveCustomer(LoginToken<Administrator> token, Customer customer);
        Customer GetCustomerById(LoginToken<Administrator> token, long customerId);
        AirlineCompany GetAirlineCompanyById(LoginToken<Administrator> token, long airlineCompanyId);
        Administrator GetAdministratorById(LoginToken<Administrator> token, long adminId);
        long CreateNewCountry(LoginToken<Administrator> token, Country country);
        Country GetCountryByCode(LoginToken<Administrator> token, long CountryCode);
        void RemoveCountry(LoginToken<Administrator> token, Country country);
        IList<Customer> GetAllCustomers(LoginToken<Administrator> token);
        Customer GetCustomerByUserName(LoginToken<Administrator> token, string userName);
        Country GetCountryByName(LoginToken<Administrator> token, string countryName);
        AirlineCompany GetAirlineCompanyByAirlineName(LoginToken<Administrator> token, string AirlineName);
        IList<Country> GetAllCountries(LoginToken<Administrator> token);
        IList<AirlineCompany> GetAllAirlineCompanies(LoginToken<Administrator> token);
        long CreateNewFlight(LoginToken<Administrator> token, Flight flight);
        void AddTicket(LoginToken<Administrator> token, long customerId, long flightId);
        IList<Flight> GetAllFlights(LoginToken<Administrator> token);
        Ticket GetTicketByCustomerId(LoginToken<Administrator> token, long customerId);
        Administrator GetAdministratorByUserName(LoginToken<Administrator> token, string administratorUserName);
        long CreateNewAdministrator(LoginToken<Administrator> token, Administrator administrator);
        void UpdateAdministratorDetails(LoginToken<Administrator> token, Administrator administrator);
        IList<Administrator> GetAllAdministrators(LoginToken<Administrator> token);
        void RemoveAdministrator(LoginToken<Administrator> token, Administrator administrator);




    }
}
