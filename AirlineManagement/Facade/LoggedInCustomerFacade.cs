using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement
{
    public class LoggedInCustomerFacade : AnonymousUserFacade, ILoggedInCustomerFacade, IFacade
    {
        private void CheckTokenValidity(LoginToken<Customer> token, out bool isTokenValid)
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
        /// Cancel Ticket by using given ticket.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ticket"></param>
        public void CancelTicket(LoginToken<Customer> token, Ticket ticket)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                Flight f = _flightDAO.Get(ticket.FLIGHT_ID);
                f.REMANING_TICKETS++;
                _flightDAO.Update(f);
                _ticketDAO.Remove(ticket);
            }
        }
        /// <summary>
        /// Get all flights
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IList<Flight> GetAllFlights(LoginToken<Customer> token)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                return _flightDAO.GetAll();
            }
            return null;
        }
        /// <summary>
        /// Return list of all customer flights.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IList<Flight> GetAllMyFlights(LoginToken<Customer> token, string customerUserName)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                return _flightDAO.GetFlightsByCustomerUserName(customerUserName);
            }
            return null;
        }
        /// <summary>
        /// Get ticket by customer ID
        /// </summary>
        /// <param name="token"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public Ticket GetTicketByCustomerId(LoginToken<Customer> token, long customerId)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                return _ticketDAO.GetTicketByCustomerId(customerId);
            }
            return null;
        }
        /// <summary>
        /// Get ticket by customer user name
        /// </summary>
        /// <param name="token"></param>
        /// <param name="customerUserName"></param>
        /// <returns></returns>
        public Ticket GetTicketByCustomerUserName(LoginToken<Customer> token, string customerUserName)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                return _ticketDAO.GetTicketByCustomerUserName(customerUserName);
            }
            return null;
        }
        /// <summary>
        /// Get flight by flight ID
        /// </summary>
        /// <param name="token"></param>
        /// <param name="flightId"></param>
        /// <returns></returns>
        public Flight GetFlightByFlightId(LoginToken<Customer> token, long flightId)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                return _flightDAO.Get(flightId);
            }
            return null;
        }
        /// <summary>
        /// Return ticket that customer had purchase by using given flight.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="flight"></param>
        /// <returns></returns>
        public Ticket PurchaseTicket(LoginToken<Customer> token, Flight flight)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                //int value = DateTime.Compare(flight.DEPARTURE_TIME, DateTime.Now);
                if (_flightDAO.GetReminingTickets(flight.ID) <= 0 || flight.DEPARTURE_TIME > DateTime.Now)
                {
                    throw new NoTicketsException("There is no tickets");
                }
                else
                {
                    Ticket ticket = new Ticket()
                    {
                        FLIGHT_ID = flight.ID,
                        CUSTOMER_ID = token.User.ID
                    };
                    string res = _ticketDAO.CheckIfTicketExist(ticket);
                    if(res == "0")
                    {
                        long ID = _ticketDAO.Add(ticket);
                        ticket.ID = ID;
                        _flightDAO.UpdateRemainingTickets(flight.ID);
                        return ticket;
                    }
                    else
                    {
                        throw new TicketAlreadyExistException("Ticket already exists");
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// Get ticket by tiket id
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        public Ticket GetTicketByTicketId(LoginToken<Customer> token, long ticketId)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                return _ticketDAO.Get(ticketId);
            }
            return null;
        }
    }
}
