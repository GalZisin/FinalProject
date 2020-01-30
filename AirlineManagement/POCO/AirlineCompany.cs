using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement
{
    public class AirlineCompany: IPoco, IUser
    {
        public AirlineCompany()
        {
        }

        public AirlineCompany(long iD, string aIRLINE_NAME, string uSER_NAME, string pASSWORD, long cOUNTRY_CODE)
        {
            ID = iD;
            AIRLINE_NAME = aIRLINE_NAME;
            USER_NAME = uSER_NAME;
            PASSWORD = pASSWORD;
            COUNTRY_CODE = cOUNTRY_CODE;
        }

        public long ID { get; set; }
        public string AIRLINE_NAME { get; set; }
        public string USER_NAME { get; set; }
        public string PASSWORD { get; set; }
        public long COUNTRY_CODE { get; set; }

        public static bool operator ==(AirlineCompany thisAirlineCompany, AirlineCompany otherAirlineCompany)
        {
            
            if (ReferenceEquals(thisAirlineCompany, null) && ReferenceEquals(otherAirlineCompany, null))
                return true;
            if (ReferenceEquals(thisAirlineCompany, null) || ReferenceEquals(otherAirlineCompany, null))
                return false;
            return thisAirlineCompany.ID == otherAirlineCompany.ID;
        }
        public static bool operator !=(AirlineCompany thisAirlineCompany, AirlineCompany otherAirlineCompany)
        {
            if (ReferenceEquals(thisAirlineCompany, null) && ReferenceEquals(otherAirlineCompany, null))
                return false;
            if (ReferenceEquals(thisAirlineCompany, null) || ReferenceEquals(otherAirlineCompany, null))
                return true;
            return !(thisAirlineCompany.ID == otherAirlineCompany.ID);
        }

        public override bool Equals(object obj)
        {
            var airlineCompany = obj as AirlineCompany;
            if (ReferenceEquals(airlineCompany, null))
                return false;

            return this.ID == airlineCompany.ID;
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }
    }
}
