using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement
{
    public static class FlightCenterConfig
    {
        //public const string ADMIN_NAME = "admin";
        //public const string ADMIN_PASSWORD = "9999";

        //public static string strConn = @"Server=tcp:galzisindb.database.windows.net,1433;Initial Catalog = AirlineManagementGalZisinDB; Persist Security Info=False;User ID = galzisindb; Password=K28spq!1n; MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30";
        public static string strConn = @"Data Source=.;Initial Catalog=AirlineManagementDB;Integrated Security=True";
        public static int OneDayInterval = 24 * 60 * 60 * 1000; //24 hours in milliseconds
    }
}
