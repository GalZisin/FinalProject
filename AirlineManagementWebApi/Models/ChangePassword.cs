using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlineManagementWebApi.Models
{
    public class ChangePassword
    {
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
}