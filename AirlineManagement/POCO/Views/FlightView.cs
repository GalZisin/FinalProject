using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement.POCO.Views
{
    public class FlightView: Flight
    {
        public FlightView()
        {
        }

        public FlightView(long iD, long aIRLINECOMPANY_ID, long oRIGIN_COUNTRY_CODE, long dESTINATION_COUNTRY_CODE, DateTime dEPARTURE_TIME, DateTime lANDING_TIME, DateTime rEAL_DEPARTURE_TIME, DateTime rEAL_LANDING_TIME, int rEMANING_TICKETS, int tOTAL_TICKETS, string fLIGHT_NUMBER, string o_COUNTRY_NAME, string d_COUNTRY_NAME, string aIRLINE_NAME, TimeSpan tIME_DIFF) : base(iD, aIRLINECOMPANY_ID, oRIGIN_COUNTRY_CODE, dESTINATION_COUNTRY_CODE, dEPARTURE_TIME, lANDING_TIME, rEAL_DEPARTURE_TIME, rEAL_LANDING_TIME, rEMANING_TICKETS, tOTAL_TICKETS, fLIGHT_NUMBER)
        {
            O_COUNTRY_NAME = o_COUNTRY_NAME;
            D_COUNTRY_NAME = d_COUNTRY_NAME;
            AIRLINE_NAME = aIRLINE_NAME;
            TIME_DIFF = tIME_DIFF;
        }

        public string O_COUNTRY_NAME { get; set; }
        public string D_COUNTRY_NAME { get; set; }
        public string AIRLINE_NAME { get; set; }
        public TimeSpan TIME_DIFF { get; set; }
     
   
    }
}
