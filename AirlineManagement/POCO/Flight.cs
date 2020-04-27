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

        public Flight(long iD, long aIRLINECOMPANY_ID, long oRIGIN_COUNTRY_CODE, long dESTINATION_COUNTRY_CODE, DateTime dEPARTURE_TIME, DateTime lANDING_TIME, DateTime rEAL_DEPARTURE_TIME, DateTime rEAL_LANDING_TIME, int rEMANING_TICKETS, int tOTAL_TICKETS, string fLIGHT_NUMBER)
        {
            ID = iD;
            AIRLINECOMPANY_ID = aIRLINECOMPANY_ID;
            ORIGIN_COUNTRY_CODE = oRIGIN_COUNTRY_CODE;
            DESTINATION_COUNTRY_CODE = dESTINATION_COUNTRY_CODE;
            DEPARTURE_TIME = dEPARTURE_TIME;
            LANDING_TIME = lANDING_TIME;
            REAL_DEPARTURE_TIME = rEAL_DEPARTURE_TIME;
            REAL_LANDING_TIME = rEAL_LANDING_TIME;
            REMANING_TICKETS = rEMANING_TICKETS;
            TOTAL_TICKETS = tOTAL_TICKETS;
            FLIGHT_NUMBER = fLIGHT_NUMBER;
        }

        public long ID { get; set; }
        public long AIRLINECOMPANY_ID { get; set; }
        public long ORIGIN_COUNTRY_CODE { get; set; }
        public long DESTINATION_COUNTRY_CODE { get; set; }
        public DateTime DEPARTURE_TIME { get; set; }
        public DateTime LANDING_TIME { get; set; }
        public DateTime REAL_DEPARTURE_TIME { get; set; }
        public DateTime REAL_LANDING_TIME { get; set; }
        public int REMANING_TICKETS { get; set; }
        public int TOTAL_TICKETS { get; set; }
        public string FLIGHT_NUMBER { get; set; }
     
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
