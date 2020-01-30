using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlineManagementWebApi.Models
{
    public class SearchParameters
    {
        public string flightId { get; set; }
        public string originCountry { get; set; }
        public string airlineCompany { get; set; }
        public string arrivalsDepartures { get; set; }
    }
}