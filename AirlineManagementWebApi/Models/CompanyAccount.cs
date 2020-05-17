using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlineManagementWebApi.Models
{
    public class CompanyAccount
    {
        public string companyName { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string country { get; set; }
        public string isApproved { get; set; }
    }
}