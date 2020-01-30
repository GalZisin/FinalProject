using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement
{
    public interface ICountryDAO : IBasicDB<Country>
    {
        Country GetCountryByName(string countryName);
        string CheckIfCountryExist(Country t);
        IList<Country> GetAllCountriesByScheduledTime(string typeName);
    }
}
