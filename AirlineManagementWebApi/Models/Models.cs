﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace AirlineManagementWebApi.Models
{
    public class Models
    {

    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public LoginResponse()
        {

            this.Token = "";
            this.responseMsg = new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.Unauthorized };
        }

        public string Token { get; set; }
        public HttpResponseMessage responseMsg { get; set; }

    }
    public class CustomerDetailsAdmin
    {
        public int id { get; set; }
    }
    public class AirlineCompanyDetailsAdmin
    {
        public int id { get; set; }
    }
    public class AdministratorDetailsAdmin
    {
        public int id { get; set; }
    }
}
