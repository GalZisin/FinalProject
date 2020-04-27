using AirlineManagement;
using AirlineManagement.POCO.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlineManagementWebApi.Models
{
    public class PassengersTickets
    {
        public MyTicket[] Tickets { get; set; }
        public MyFlight[] Flights { get; set; }
    }

    public partial class MyFlight
    {
        public long Id { get; set; }
        public string DepartureTime { get; set; }
    }
    public partial class MyTicket
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}