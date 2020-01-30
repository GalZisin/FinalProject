using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement
{
    public class Country : IPoco
    {
        public Country()
        {
        }

        public Country(long iD, string cOUNTRY_NAME)
        {
            ID = iD;
            COUNTRY_NAME = cOUNTRY_NAME;
        }

        public long ID { get; set; }
        public string COUNTRY_NAME { get; set; }
        public static bool operator ==(Country thisCountry, Country otherCountry)
        {
            if (ReferenceEquals(thisCountry, null) && ReferenceEquals(otherCountry, null))
                return true;
            if (ReferenceEquals(thisCountry, null) || ReferenceEquals(otherCountry, null))
                return false;
            return thisCountry.ID == otherCountry.ID;
        }
        public static bool operator !=(Country thisCountry, Country otherCountry)
        {
            if (thisCountry == null && otherCountry == null)
                return false;
            if (thisCountry == null || otherCountry == null)
                return true;
            return !(thisCountry.ID == otherCountry.ID);
        }

        public override bool Equals(object obj)
        {
            var country = obj as Country;
            if (ReferenceEquals(country, null))
                return false;

            return this.ID == country.ID;
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }
    }
}
