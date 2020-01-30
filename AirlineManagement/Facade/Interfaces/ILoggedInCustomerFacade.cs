using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement
{
    public interface ILoggedInCustomerFacade
    {
        IList<Flight> GetAllMyFlights(LoginToken<Customer> token, string customerUserName);
        Ticket PurchaseTicket(LoginToken<Customer> token, Flight flight);
        void CancelTicket(LoginToken<Customer> token, Ticket ticket);
        Flight GetFlightByFlightId(LoginToken<Customer> token, long flightId);
        Ticket GetTicketByCustomerId(LoginToken<Customer> token, long customerId);
        IList<Flight> GetAllFlights(LoginToken<Customer> token);
        Ticket GetTicketByCustomerUserName(LoginToken<Customer> token, string customerUserName);
        Ticket GetTicketByTicketId(LoginToken<Customer> token, long ticketId);


    }
}
