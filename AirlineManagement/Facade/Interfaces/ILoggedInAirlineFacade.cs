using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement
{
    public interface ILoggedInAirlineFacade
    {
        IList<Ticket> GetAllTickets(LoginToken<AirlineCompany> token);
        IList<Flight> GetAllFlights(LoginToken<AirlineCompany> token);
        IList<Flight> GetAllFlightsByCompanyId(LoginToken<AirlineCompany> token);
        Flight GetFlightByFlightId(LoginToken<AirlineCompany> token, long flightId);
        void CancelFlight(LoginToken<AirlineCompany> token, Flight flight);
        long CreateFlight(LoginToken<AirlineCompany> token, Flight flight);
        void UpdateFlight(LoginToken<AirlineCompany> token, Flight flight);
        void ChangeMyPassword(LoginToken<AirlineCompany> token, string oldPassword, string newPassword);
        void ModifyAirlineDetails(LoginToken<AirlineCompany> token, AirlineCompany airline);
        Customer GetCustomerByName(LoginToken<AirlineCompany> token, string countryName);
        Ticket GetTicketByCustomerId(LoginToken<AirlineCompany> token, long customerId);
        Country GetCountryByName(LoginToken<AirlineCompany> token, string countryName);
        Customer GetCustomerByUserName(LoginToken<AirlineCompany> token, string customerUserName);
        void UpdateRemainingTickets(LoginToken<AirlineCompany> token, Flight flight);


    }
}
