using AirlineManagement.POCO.Views;
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
        IList<FlightView> GetAllFlights(LoginToken<AirlineCompany> token);
        IList<FlightView> GetAllFlightsByCompanyId(LoginToken<AirlineCompany> token, long companyId);
        FlightView GetFlightByFlightId(LoginToken<AirlineCompany> token, long flightId);
        void CancelFlight(LoginToken<AirlineCompany> token, FlightView flight);
        long CreateFlight(LoginToken<AirlineCompany> token, FlightView flight);
        void UpdateFlight(LoginToken<AirlineCompany> token, FlightView flight);
        void ChangeMyPassword(LoginToken<AirlineCompany> token, string oldPassword, string newPassword);
        void UpdateAirlineDetails(LoginToken<AirlineCompany> token, AirlineCompany airline);
        Customer GetCustomerByName(LoginToken<AirlineCompany> token, string countryName);
        IList<TicketView> GetTicketByCustomerId(LoginToken<AirlineCompany> token, long customerId);
        Country GetCountryByName(LoginToken<AirlineCompany> token, string countryName);
        Customer GetCustomerByUserName(LoginToken<AirlineCompany> token, string customerUserName);
        void UpdateRemainingTickets(LoginToken<AirlineCompany> token, FlightView flight);
        AirlineCompany GetAirlineCompanyByAirlineName(LoginToken<AirlineCompany> token, string companyName);
        AirlineCompany GetAirlineCompanyByAirlineCompanyId(LoginToken<AirlineCompany> token, long companyId);
        void DeleteAirlineCompany(LoginToken<AirlineCompany> token, AirlineCompany airlineCompany);
        IList<Country> GetAllCountries(LoginToken<AirlineCompany> token);


    }
}
