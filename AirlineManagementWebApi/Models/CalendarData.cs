using AirlineManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlineManagementWebApi.Models
{
    public class CalendarData
    {
        public IList<Flight> departureDates;
        public IList<Flight> returnDates;
        public DateTime DefaultDepartureDate { get; set; }
        public string CompanyName { get; set; }
        public string DestinationCountry { get; set; }
    }
}