using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement
{
    class LoggedInAdministratorFacade : AnonymousUserFacade, ILoggedInAdministratorFacade, IFacade
    {
        private void CheckTokenValidity(LoginToken<Administrator> token, out bool isTokenValid)
        {
            isTokenValid = false;
            if (token != null)
            {
                isTokenValid = true;
            }
            else
            {
                throw new InvalidTokenException("Token can't be null");
            }
        }
        /// <summary>
        /// Create new administrator by using given administrator.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="administrator"></param>
        /// <returns></returns>
        public long CreateNewAdministrator(LoginToken<Administrator> token, Administrator administrator)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                string res = _administratorDAO.CheckIfAdministratorExist(administrator);
                if (res == "0")
                {
                    return _administratorDAO.Add(administrator);
                }
                else
                {
                    throw new AdministratorAlreadyExistException("Administrator already exists");
                }
            }
            return 0;
        }
        /// <summary>
        ///  Create new Country by using given Country.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="country"></param>
        /// <returns></returns>
        public long CreateNewCountry(LoginToken<Administrator> token, Country country)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                string res = _countryDAO.CheckIfCountryExist(country);
                if(res == "0")
                {
                    return _countryDAO.Add(country);
                }
                else
                {
                    throw new CountryAlreadyExistException("Country already exists");
                }
            }
           return 0;
        }
        /// <summary>
        /// Create new airline company by using given airline company.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="airline"></param>
        public long CreateNewAirline(LoginToken<Administrator> token, AirlineCompany airline)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                string res = _airlineDAO.CheckIfAirlineCompanyExist(airline);
                if (res == "0")
                {
                    return _airlineDAO.Add(airline);
                }
                else
                {
                    throw new AirlineCompanyAlreadyExistException("AirlineCompany already exists");
                }
            }
            return 0;
        }
        /// <summary>
        /// Create new Customer by using given customer.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="customer"></param>
        public long CreateNewCustomer(LoginToken<Administrator> token, Customer customer)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                string res = _customerDAO.CheckIfCustomerExist(customer);
                if(res == "0")
                {
                    return _customerDAO.Add(customer);
                }
                else
                {
                    throw new CustomerAlreadyExistException("Customer already exists");
                }
              
            }
            return 0;
        }
        /// <summary>
        /// Create new flight
        /// </summary>
        /// <param name="token"></param>
        /// <param name="flight"></param>
        /// <returns></returns>
        public long CreateNewFlight(LoginToken<Administrator> token, Flight flight)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                string res = _flightDAO.CheckIfFlightExist(flight);
                if(res == "0")
                {
                    return _flightDAO.Add(flight);
                }
                else
                {
                    throw new FlightAlreadyExistException("Flight already exists");
                }
            }
            return 0;
        }
        /// <summary>
        /// Remove Country By country ID
        /// </summary>
        /// <param name="token"></param>
        /// <param name="country"></param>
        public void RemoveCountry(LoginToken<Administrator> token, Country country)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                _ticketDAO.RemoveTicketsByCountryCode(country.ID);
                _flightDAO.RemoveFlightsByCountryCode(country.ID);
                _airlineDAO.RemoveAirlineCompanyByCountryCode(country.ID);
                _countryDAO.Remove(country);
            }
        }
        /// <summary>
        /// Remove airline company by using given airline company.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="airline"></param>
        public void RemoveAirline(LoginToken<Administrator> token, AirlineCompany airline)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                _ticketDAO.RemoveTicketsByAirlineCompanyId(airline.ID);
                _flightDAO.RemoveFlightsByAirlineCompanyId(airline.ID);
                _airlineDAO.Remove(airline);
            }
        }
        /// <summary>
        /// Remove customer by using given customer.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="customer"></param>
        public void RemoveCustomer(LoginToken<Administrator> token, Customer customer)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                _ticketDAO.RemoveTicketsByCustomerId(customer.ID);
                _customerDAO.Remove(customer);
            }
        }
        /// <summary>
        /// Remove administrator by using given administrator.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="administrator"></param>
        public void RemoveAdministrator(LoginToken<Administrator> token, Administrator administrator)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                _administratorDAO.Remove(administrator);
            }
        }
        /// <summary>
        /// Update airline company details by using given airline data.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="customer"></param>
        public void UpdateAirlineDetails(LoginToken<Administrator> token, AirlineCompany airline)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                string res = _airlineDAO.CheckIfAirlineCompanyExistById(airline);
                if (res == "0")
                {
                    _airlineDAO.Update(airline);
                }
                else
                {
                    throw new AirlineCompanyAlreadyExistException("Flight already exists");
                }

            }
        }
        /// <summary>
        /// Update customer details by using given customer data.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="customer"></param>
        public void UpdateCustomerDetails(LoginToken<Administrator> token, Customer customer)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                string res = _customerDAO.CheckIfCustomerExistById(customer);
                if (res == "0")
                {
                    _customerDAO.Update(customer);
                }
                else
                {
                    throw new FlightAlreadyExistException("Flight already exists");
                }  
            }
        }
        /// <summary>
        /// Update administrator details by using given admin data.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="customer"></param>
        public void UpdateAdministratorDetails(LoginToken<Administrator> token, Administrator administrator)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                string res = _administratorDAO.CheckIfAdministratorExistById(administrator);
                if (res == "0")
                {
                    _administratorDAO.Update(administrator);
                }
                else
                {
                    throw new AdministratorAlreadyExistException("Administrator already exists");
                }
            }
        }
        /// <summary>
        /// Get Customer By customer ID
        /// </summary>
        /// <param name="token"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public Customer GetCustomerById(LoginToken<Administrator> token, long customerId)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                return _customerDAO.Get(customerId);
            }
            return null;
        }
        /// <summary>
        /// Get Airline Company By airline company ID
        /// </summary>
        /// <param name="token"></param>
        /// <param name="airlineCompanyId"></param>
        /// <returns></returns>
        public AirlineCompany GetAirlineCompanyById(LoginToken<Administrator> token, long airlineCompanyId)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                return _airlineDAO.Get(airlineCompanyId);
            }
            return null;
        }
        /// <summary>
        /// Get Administrator By Admin ID
        /// </summary>
        /// <param name="token"></param>
        /// <param name="adminId"></param>
        /// <returns></returns>
        public Administrator GetAdministratorById(LoginToken<Administrator> token, long adminId)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                return _administratorDAO.Get(adminId);
            }
            return null;
        }
        /// <summary>
        /// Get country By country code
        /// </summary>
        /// <param name="token"></param>
        /// <param name="CountryCode"></param>
        /// <returns></returns>
        public Country GetCountryByCode(LoginToken<Administrator> token, long CountryCode)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                return _countryDAO.Get(CountryCode);
            }
            return null;
        }
        /// <summary>
        /// Get all customers
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IList<Customer> GetAllCustomers(LoginToken<Administrator> token)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                return _customerDAO.GetAll();
            }
            return null;
        }
        /// <summary>
        /// Get all administrators
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IList<Administrator> GetAllAdministrators(LoginToken<Administrator> token)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                return _administratorDAO.GetAll();
            }
            return null;
        }
        /// <summary>
        /// Get all countries
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IList<Country> GetAllCountries(LoginToken<Administrator> token)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                return _countryDAO.GetAll();
            }
            return null;
        }
        /// <summary>
        /// Get all airline companies
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IList<AirlineCompany> GetAllAirlineCompanies(LoginToken<Administrator> token)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                return _airlineDAO.GetAll();
            }
            return null;
        }
        /// <summary>
        /// Get all flights
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IList<Flight> GetAllFlights(LoginToken<Administrator> token)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                return _flightDAO.GetAll();
            }
            return null;
        }
        /// <summary>
        /// Get customer by user name
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Customer GetCustomerByUserName(LoginToken<Administrator> token, string userName)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                return _customerDAO.GetCustomerByUsername(userName);
            }
            return null;
        }
        /// <summary>
        /// Get country by country name
        /// </summary>
        /// <param name="token"></param>
        /// <param name="countryName"></param>
        /// <returns></returns>
        public Country GetCountryByName(LoginToken<Administrator> token, string countryName)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                return _countryDAO.GetCountryByName(countryName);
            }
            return null;
        }
        /// <summary>
        /// Get airline company by airline company name
        /// </summary>
        /// <param name="token"></param>
        /// <param name="AirlineName"></param>
        /// <returns></returns>
        public AirlineCompany GetAirlineCompanyByAirlineName(LoginToken<Administrator> token, string AirlineName)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                return _airlineDAO.GetAirlineCompanyByAirlineName(AirlineName);
            }
            return null;
        }
        /// <summary>
        /// Get administrator by administrator user name.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="administratorUserName"></param>
        /// <returns></returns>
        public Administrator GetAdministratorByUserName(LoginToken<Administrator> token, string administratorUserName)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                return _administratorDAO.GetAdministratorByUserName(administratorUserName);
            }
            return null;
        }
        /// <summary>
        /// Get administrator by administrator id.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public Ticket GetTicketByCustomerId(LoginToken<Administrator> token, long customerId)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                return _ticketDAO.GetTicketByCustomerId(customerId);
            }
            return null;
        }
        /// <summary>
        /// Add ticket by using customer id and flight id.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="customerId"></param>
        /// <param name="flightId"></param>
        public void AddTicket(LoginToken<Administrator> token, long customerId, long flightId)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                Ticket ticket = new Ticket()
                {
                    FLIGHT_ID = flightId,
                    CUSTOMER_ID = customerId
                };
                long ID = _ticketDAO.Add(ticket);
                ticket.ID = ID;
                _flightDAO.UpdateRemainingTickets(flightId);
            }
        }
    }
}
