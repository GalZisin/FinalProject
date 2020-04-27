using AirlineManagement.POCO.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement
{
    public interface ITicketDAO: IBasicDB<Ticket>
    {
        void RemoveTicketsByFlightId(long flightId);
        IList<Ticket> GetTicketsByAirlineCompanyId(long airlineCompanyId);
        void RemoveTicketsByAirlineCompanyId(long airlineCompanyId);
        void RemoveTicketsByCustomerId(long customerId);
        void RemoveTicketsByCountryCode(long countryCode);
        IList<TicketView> GetAllTicketsByCustomerId(long customerId);
        Ticket GetTicketByCustomerId(long customerId);
        Ticket GetTicketByCustomerUserName(string customerUserName);
        string CheckIfTicketExist(Ticket t);
    }
}
