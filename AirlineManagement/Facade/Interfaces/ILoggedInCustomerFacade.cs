using AirlineManagement.POCO.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement
{
    public interface ILoggedInCustomerFacade
    {
        IList<FlightView> GetAllMyFlights(LoginToken<Customer> token, string customerUserName);
        void CancelTicket(LoginToken<Customer> token, Ticket ticket);
        FlightView GetFlightByFlightId(LoginToken<Customer> token, long flightId);
        IList<TicketView> GetAllTicketsByCustomerId(LoginToken<Customer> token, long customerId);
        IList<FlightView> GetAllFlights(LoginToken<Customer> token);
        Ticket GetTicketByCustomerUserName(LoginToken<Customer> token, string customerUserName);
        TicketView GetTicketByTicketId(LoginToken<Customer> token, long ticketId);
        Customer GetCustomerByCustomerId(LoginToken<Customer> token, long customerId);
        void UpdateCustomerDetails(LoginToken<Customer> token, Customer customer);
        void DeleteCustomer(LoginToken<Customer> token, Customer customer);
        Ticket PurchaseTickets(LoginToken<Customer> token, Ticket[] tickets);
        void UpdateTicketDetails(LoginToken<Customer> token, Ticket ticket);
    }
}
