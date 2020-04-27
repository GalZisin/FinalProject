using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement.POCO.Views
{
    public class TicketView: Ticket
    {
        public TicketView()
        {
        }

        public TicketView(long iD, long fLIGHT_ID, long cUSTOMER_ID, string fIRST_NAME, string lAST_NAME, string o_COUNTRY_NAME, string d_COUNTRY_NAME, string aIRLINE_NAME, DateTime dEPARTURE_TIME, DateTime lANDING_TIME, string fLIGHT_NUMBER) : base(iD, fLIGHT_ID, cUSTOMER_ID)
        {
            ID = iD;
            FLIGHT_ID = fLIGHT_ID;
            CUSTOMER_ID = cUSTOMER_ID;
            FIRST_NAME = fIRST_NAME;
            LAST_NAME = lAST_NAME;
            O_COUNTRY_NAME = o_COUNTRY_NAME;
            D_COUNTRY_NAME = d_COUNTRY_NAME;
            AIRLINE_NAME = aIRLINE_NAME;
            DEPARTURE_TIME = dEPARTURE_TIME;
            LANDING_TIME = lANDING_TIME;
            FLIGHT_NUMBER = fLIGHT_NUMBER;
        }

        public string O_COUNTRY_NAME { get; set; }
        public string D_COUNTRY_NAME { get; set; }
        public string AIRLINE_NAME { get; set; }
        public DateTime DEPARTURE_TIME { get; set; }
        public DateTime LANDING_TIME { get; set; }
        public string FLIGHT_NUMBER { get; set; }
    }
}
