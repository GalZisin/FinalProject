using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlineManagementWebApi.Models
{
    public class FlightLists
    {
        public IList<string> ids = null;
        public IList<string> companies = null;
        public IList<string> originCountries = null;
        public IList<string> destinationCountries = null;

    }
    public class KMessage
    {
        public string Keyst { get; set; }

    }

}