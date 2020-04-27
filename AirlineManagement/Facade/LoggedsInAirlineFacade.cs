using AirlineManagement.POCO.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement
{
    class LoggedInAirlineFacade : AnonymousUserFacade, ILoggedInAirlineFacade, IFacade
    {
        private void CheckTokenValidity(LoginToken<AirlineCompany> token, out bool isTokenValid)
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
     /// Delete Airline Comompany
     /// </summary>
     /// <param name="token"></param>
     /// <param name="airlineCompany"></param>
        public void DeleteAirlineCompany(LoginToken<AirlineCompany> token, AirlineCompany airlineCompany)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                _ticketDAO.RemoveTicketsByAirlineCompanyId(airlineCompany.ID);
                _flightDAO.RemoveFlightsByAirlineCompanyId(airlineCompany.ID);
                _airlineDAO.Remove(airlineCompany);
            }
        }
        /// <summary>
        /// Cancel flight by using a given flight.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="flight"></param>
        public void CancelFlight(LoginToken<AirlineCompany> token, FlightView flight)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                _ticketDAO.RemoveTicketsByFlightId(flight.ID);
                _flightDAO.Remove(flight);
            }
        }
        /// <summary>
        /// Change to new password by using old password. 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        public void ChangeMyPassword(LoginToken<AirlineCompany> token, string oldPassword, string newPassword)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                if (token.User.PASSWORD == oldPassword)
                {
                    token.User.PASSWORD = newPassword;
                    _airlineDAO.Update(token.User);
                }
                else
                {
                    throw new NotValidPasswordException("Wrong Password");
                }
            }
        }
        /// <summary>
        /// Create new flight with given flight data.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="flight"></param>
        public long CreateFlight(LoginToken<AirlineCompany> token, FlightView flight)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                string res = _flightDAO.CheckIfFlightExist(flight);
                if (res == "0")
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
        /// Return list of all flights by Company ID.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IList<FlightView> GetAllFlightsByCompanyId(LoginToken<AirlineCompany> token, long companyId)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                return _flightDAO.GetFlightsByAirlineCompanyId(companyId);
            }
            return null;
        }
        /// <summary>
        /// Return list of all flights.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IList<FlightView> GetAllFlights(LoginToken<AirlineCompany> token)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                return _flightDAO.GetAll();
            }
            return null;
        }
        /// <summary>
        /// Return list of all tickets.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IList<Ticket> GetAllTickets(LoginToken<AirlineCompany> token)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                return _ticketDAO.GetAll();
            }
            return null;
        }
        /// <summary>
        /// Return list of all airline company ID.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IList<Ticket> GetAllTicketsByAirlineCompanyId(LoginToken<AirlineCompany> token)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                return _ticketDAO.GetTicketsByAirlineCompanyId(token.User.ID);
            }
            return null;
        }
        /// <summary>
        /// Update airline company details by using modified airline data.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="airline"></param>
        public void UpdateAirlineDetails(LoginToken<AirlineCompany> token, AirlineCompany airline)
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
                    throw new AirlineCompanyAlreadyExistException("Airline company already exists");
                }

            }
        }
        /// <summary>
        /// Update flight by using modified or new flight data.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="flight"></param>
        /// 
        public void UpdateFlight(LoginToken<AirlineCompany> token, FlightView flight)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                _flightDAO.Update(flight);
            }
        }
        /// <summary>
        /// Update remaining tickets
        /// </summary>
        /// <param name="token"></param>
        /// <param name="flight"></param>
        public void UpdateRemainingTickets(LoginToken<AirlineCompany> token, FlightView flight)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                _flightDAO.Update(flight);
            }
        }
        /// <summary>
        /// Return flight by flight ID.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="flightId"></param>
        /// <returns></returns>
        public FlightView GetFlightByFlightId(LoginToken<AirlineCompany> token, long flightId)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                return _flightDAO.Get(flightId);
            }
            return null;
        }
        /// <summary>
        /// Get all countries
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IList<Country> GetAllCountries(LoginToken<AirlineCompany> token)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                return _countryDAO.GetAll();
            }
            return null;
        }
        /// <summary>
        /// Get country by country name
        /// </summary>
        /// <param name="token"></param>
        /// <param name="countryName"></param>
        /// <returns></returns>
        public Country GetCountryByName(LoginToken<AirlineCompany> token, string countryName)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                return _countryDAO.GetCountryByName(countryName);
            }
            return null;
        }
        /// <summary>
        /// Get customer by customer user name
        /// </summary>
        /// <param name="token"></param>
        /// <param name="customerUserName"></param>
        /// <returns></returns>
        public Customer GetCustomerByUserName(LoginToken<AirlineCompany> token, string customerUserName)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                return _customerDAO.GetCustomerByUsername(customerUserName);
            }
            return null;
        }
        /// <summary>
        /// Get customer by custome first name
        /// </summary>
        /// <param name="token"></param>
        /// <param name="customerName"></param>
        /// <returns></returns>
        public Customer GetCustomerByName(LoginToken<AirlineCompany> token, string customerName)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                return _customerDAO.GetCustomerByName(customerName);
            }
            return null;
        }
        /// <summary>
        /// Get ticket by customer ID
        /// </summary>
        /// <param name="token"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public IList<TicketView> GetTicketByCustomerId(LoginToken<AirlineCompany> token, long customerId)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                return _ticketDAO.GetAllTicketsByCustomerId(customerId);
            }
            return null;
        }
        public AirlineCompany GetAirlineCompanyByAirlineName(LoginToken<AirlineCompany> token, string companyName)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                return _airlineDAO.GetAirlineCompanyByAirlineName(companyName);
            }
            return null;
        }
        public AirlineCompany GetAirlineCompanyByAirlineCompanyId(LoginToken<AirlineCompany> token, long companyId)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                return _airlineDAO.Get(companyId);
            }
            return null;
        }

    }
}
