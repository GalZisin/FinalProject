using AirlineManagement.POCO.Views;
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
                FlightView f = _flightDAO.Get(ticket.FLIGHT_ID);
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
        public IList<FlightView> GetAllFlights(LoginToken<Customer> token)
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
        public IList<FlightView> GetAllMyFlights(LoginToken<Customer> token, string customerUserName)
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
        public IList<TicketView> GetAllTicketsByCustomerId(LoginToken<Customer> token, long customerId)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                return _ticketDAO.GetAllTicketsByCustomerId(customerId);
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
        public FlightView GetFlightByFlightId(LoginToken<Customer> token, long flightId)
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
        //public Ticket PurchaseTicket(LoginToken<Customer> token, Ticket ticket, DateTime departureDate)
        //{
        //    CheckTokenValidity(token, out bool isTokenValid);
        //    if (isTokenValid)
        //    {
        //        FlightView flight = _flightDAO.Get(ticket.FLIGHT_ID);
        //        if (flight == null)
        //        {
        //            throw new NoFlightException("No flight");
        //        }
        //        //int value = DateTime.Compare(flight.DEPARTURE_TIME, DateTime.Now);
        //        if (_flightDAO.GetReminingTickets(ticket.FLIGHT_ID) <= 0 || departureDate > DateTime.Now)
        //        {
        //            throw new NoTicketsException("There is no tickets");
        //        }
        //        else
        //        {
        //            //Ticket ticket1 = new Ticket()
        //            //{
        //            //    FLIGHT_ID = ticket.FLIGHT_ID,
        //            //    CUSTOMER_ID = token.User.ID
        //            //};
        //            string res = _ticketDAO.CheckIfTicketExist(ticket);
        //            if (res == "0")
        //            {
        //                long ID = _ticketDAO.Add(ticket);
        //                ticket.ID = ID;
        //                _flightDAO.UpdateRemainingTickets(ticket.FLIGHT_ID);
        //                return ticket;
        //            }
        //            else
        //            {
        //                throw new TicketAlreadyExistException("Ticket already exists");
        //            }
        //        }
        //    }
        //    return null;
        //}
        public Ticket PurchaseTicket(Ticket ticket)
        {
            long ID = _ticketDAO.Add(ticket);
            ticket.ID = ID;
            _flightDAO.UpdateRemainingTickets(ticket.FLIGHT_ID);
            return ticket;
        }


        public Ticket PurchaseTickets(LoginToken<Customer> token, Ticket[] tickets)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                bool ticketsOk = true;
                if (tickets.Length % 2 != 0)
                {
                    ticketsOk = false;
                    throw new NoFlightException("No flight");
                }

                for (int i = 0; i < tickets.Length; i++)
                {
                    FlightView flight = _flightDAO.Get(tickets[i].FLIGHT_ID);
                    if (flight == null)
                    {
                        ticketsOk = false;
                        throw new NoFlightException("No flight");
                    }
                }
                for (int i = 0; i < tickets.Length; i++)
                {
                    if (_flightDAO.GetReminingTickets(tickets[i].FLIGHT_ID) <= 0)
                    {
                        ticketsOk = false;
                        throw new NoTicketsException("There is no tickets");
                    }
                }
                for (int i = 0; i < tickets.Length; i++)
                {
                    string res = _ticketDAO.CheckIfTicketExist(tickets[i]);
                    if (res != "0")
                    {
                        ticketsOk = false;
                        throw new TicketAlreadyExistException("Ticket already exists");
                    }
                }
                if (ticketsOk)
                {
                    for (int i = 0; i < tickets.Length; i++)
                    {
                        PurchaseTicket(tickets[i]);
                    }
                }

            }
            return null;
        }
        /// <summary>
        /// Updatye customer details
        /// </summary>
        /// <param name="token"></param>
        /// <param name="customer"></param>
        public void UpdateCustomerDetails(LoginToken<Customer> token, Customer customer)
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
                    throw new CustomerAlreadyExistException("Customer already exists");
                }

            }
        }
        /// <summary>
        /// Delete customer
        /// </summary>
        /// <param name="token"></param>
        /// <param name="customer"></param>
        public void DeleteCustomer(LoginToken<Customer> token, Customer customer)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                _ticketDAO.RemoveTicketsByCustomerId(customer.ID);
                _customerDAO.Remove(customer);
            }
        }
        /// <summary>
        /// Get customer by customer id
        /// </summary>
        /// <param name="token"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public Customer GetCustomerByCustomerId(LoginToken<Customer> token, long customerId)
        {
            CheckTokenValidity(token, out bool isTokenValid);
            if (isTokenValid)
            {
                return _customerDAO.Get(customerId);
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
