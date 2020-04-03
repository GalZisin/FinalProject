using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement
{
    public abstract class FacadeBase
    {
        protected IAirlineDAO _airlineDAO;
        protected ICountryDAO _countryDAO;
        protected ICustomerDAO _customerDAO;
        protected IFlightDAO _flightDAO;
        protected ITicketDAO _ticketDAO;
        protected IAdministratorDAO _administratorDAO;

        //protected AirlineDAOMSSQL _airlineDAO { get; set; }
        //protected CountryDAOMSSQL _countryDAO { get; set; }
        //protected CustomerDAOMSSQL _customerDAO { get; set; }
        //protected FlightDAOMSSQL _flightDAO { get; set; }
        //protected TicketDAOMSSQL _ticketDAO { get; set; }

        public FacadeBase()
        {
            _airlineDAO = new AirlineDAOMSSQL();
            _countryDAO = new CountryDAOMSSQL();
            _customerDAO = new CustomerDAOMSSQL();
            _flightDAO = new FlightDAOMSSQL();
            _ticketDAO = new TicketDAOMSSQL();
            _administratorDAO = new AdministratorDAOMSSQL();
        }
    }
}
