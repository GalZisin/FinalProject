using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineManagement;
using Newtonsoft.Json;

namespace AirlineTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //CustomerDAO cu = new CustomerDAOMSSQL();
            //AirlineDAOMSSQL company = new AirlineDAOMSSQL();
            Country c = new Country();
            c.COUNTRY_NAME = "Brazil";
            
            CountryDAOMSSQL countryDao = new CountryDAOMSSQL();
            countryDao.Add(c);
            //SqlConnection conn = new SqlConnection(@"Data Source=.;Initial Catalog=AirlineManagementDB;Integrated Security=True");
            //Console.WriteLine(conn.ToString());
            //Console.WriteLine(JsonConvert.SerializeObject(cu.Get(1)));

            Console.ReadLine();
        }
    }
}
