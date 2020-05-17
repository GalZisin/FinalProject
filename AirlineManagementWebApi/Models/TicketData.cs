using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlineManagementWebApi.Models
{
    public class TicketData
    {
        public string ID { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string FLIGHT_ID { get; set; }
        public string CUSTOMER_ID { get; set; }

    }
}