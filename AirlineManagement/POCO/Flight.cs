using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement
{
    public class Flight : IPoco
    {
        public Flight()
        {
        }

        public Flight(long iD, long aIRLINECOMPANY_ID, long oRIGIN_COUNTRY_CODE, long dESTINATION_COUNTRY_CODE, DateTime dEPARTURE_TIME, DateTime lANDING_TIME, int rEMANING_TICKETS, int tOTAL_TICKETS, string o_cOUNTRY_NAME, string d_cOUNTRY_NAME, string aIRLINE_NAME)
        {
            ID = iD;
            AIRLINECOMPANY_ID = aIRLINECOMPANY_ID;
            ORIGIN_COUNTRY_CODE = oRIGIN_COUNTRY_CODE;
            DESTINATION_COUNTRY_CODE = dESTINATION_COUNTRY_CODE;
            DEPARTURE_TIME = dEPARTURE_TIME;
            LANDING_TIME = lANDING_TIME;
            REMANING_TICKETS = rEMANING_TICKETS;
            TOTAL_TICKETS = tOTAL_TICKETS;

            O_COUNTRY_NAME = o_cOUNTRY_NAME;
            D_COUNTRY_NAME = d_cOUNTRY_NAME;
            AIRLINE_NAME = aIRLINE_NAME;
        }

        public long ID { get; set; }
        public long AIRLINECOMPANY_ID { get; set; }
        public long ORIGIN_COUNTRY_CODE { get; set; }
        public long DESTINATION_COUNTRY_CODE { get; set; }
        public DateTime DEPARTURE_TIME { get; set; }
        public DateTime LANDING_TIME { get; set; }
        public int REMANING_TICKETS { get; set; }
        public int TOTAL_TICKETS { get; set; }
   
        /// <summary>
        /// property for flight departures and landings
        /// </summary>
        public string O_COUNTRY_NAME { get; set; }
        public string D_COUNTRY_NAME { get; set; }
        public string AIRLINE_NAME { get; set; }
        public DateTime REAL_DEPARTURE_TIME { get; set; }
        public DateTime REAL_LANDING_TIME { get; set; }
        public TimeSpan TIME_DIFF { get; set; }
        //public TimeSpan LANDING_TIME_DIFF { get; set; }
        //public string Status { get; set; }
        //public string LandingStatus { get; set; }
        public static bool operator ==(Flight thisFlight, Flight otherFlight)
        {
            if (ReferenceEquals(thisFlight, null) && ReferenceEquals(otherFlight, null))
                return true;
            if (ReferenceEquals(thisFlight, null) || ReferenceEquals(otherFlight, null))
                return false;
            return thisFlight.ID == otherFlight.ID;
        }
        public static bool operator !=(Flight thisFlight, Flight otherFlight)
        {
            if (thisFlight == null && otherFlight == null)
                return false;
            if (thisFlight == null || otherFlight == null)
                return true;
            return !(thisFlight.ID == otherFlight.ID);
        }

        public override bool Equals(object obj)
        {
            var flight = obj as Flight;
            if (ReferenceEquals(flight, null))
                return false;

            return this.ID == flight.ID;
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }
    }
}
