using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement
{
    public class CountryDAOMSSQL : ICountryDAO
    {
        private SqlDAO DL;  // A central class of database connections
        public CountryDAOMSSQL()
        {
            DL = new SqlDAO(FlightCenterConfig.strConn);
        }
        public string CheckIfCountryExist(Country t)
        {
            StringBuilder sb = new StringBuilder();
            string SQL1 = $"SELECT COUNT(*) FROM Countries WHERE COUNTRY_NAME = '{t.COUNTRY_NAME}'";
            string res = DL.ExecuteSqlScalarStatement(SQL1);
            return res;
        }
        public long Add(Country t)
        {
            StringBuilder sb = new StringBuilder();
            sb = new StringBuilder();
            sb.Append($"INSERT INTO Countries(COUNTRY_NAME)");
            sb.Append($" values('{ t.COUNTRY_NAME}')");
            string SQL = sb.ToString();
            DL.ExecuteSqlNonQuery(SQL);
            SQL = $"SELECT ID FROM Countries WHERE COUNTRY_NAME ='{t.COUNTRY_NAME}'";

            return Int64.Parse(DL.ExecuteSqlScalarStatement(SQL));
        }
        public Country Get(long id)
        {
            Country country = null;
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM Countries WHERE ID = {id}");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Countries");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                country = new Country();
                country.ID = Convert.ToInt64(dr["ID"]);
                country.COUNTRY_NAME = (string)dr["COUNTRY_NAME"];
            }
            if (country != null)
            {
                return country;
            }
            return null;
        }
        public Country GetCountryByName(string countryName)
        {
            Country country = null;
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM Countries WHERE COUNTRY_NAME = '{countryName}'");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Countries");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                country = new Country();
                country.ID = Convert.ToInt64(dr["ID"]);
                country.COUNTRY_NAME = (string)dr["COUNTRY_NAME"];
            }
            if (country != null)
            {
                return country;
            }
            return null;
        }

        public IList<Country> GetAll()
        {
            IList<Country> countries = new List<Country>();
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM Countries");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Countries");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                Country country = new Country();
                country.ID = (long)dr["ID"];
                country.COUNTRY_NAME = (string)dr["COUNTRY_NAME"];
                countries.Add(country);
            }
            return countries;
        }

        public IList<Country> GetAllCountriesByScheduledTime(string typeName)
        {
            IList<Country> countries = new List<Country>();
            StringBuilder sb = new StringBuilder();
            //string str1 = "";
            if (typeName == "Arrivals")
            {
                sb.Append($" SELECT c.COUNTRY_NAME as 'Coming from'");
                sb.Append($" FROM Countries as c");
                sb.Append($" WHERE c.COUNTRY_NAME IN (SELECT c.COUNTRY_NAME as 'Coming from'");
                sb.Append($" FROM Flights as f");
                sb.Append($" INNER JOIN AirlineCompanies as a on f.AIRLINECOMPANY_ID = a.ID");
                sb.Append($" INNER JOIN Countries as c on f.ORIGIN_COUNTRY_CODE = c.ID");
                sb.Append($" INNER JOIN Countries as co on f.DESTINATION_COUNTRY_CODE = co.ID");
                sb.Append($" WHERE(REAL_LANDING_TIME BETWEEN GETDATE() AND DATEADD(hour, 12, GETDATE()) OR (REAL_LANDING_TIME BETWEEN DATEADD(hour, -4, GETDATE()) AND GETDATE())))");
            }
            else if (typeName == "Departures")
            {
                sb.Append($" SELECT c.COUNTRY_NAME as 'Coming from'");
                sb.Append($" FROM Countries as c");
                sb.Append($" WHERE c.COUNTRY_NAME IN (SELECT c.COUNTRY_NAME as 'Coming from'");
                sb.Append($" FROM Flights as f");
                sb.Append($" INNER JOIN AirlineCompanies as a on f.AIRLINECOMPANY_ID = a.ID");
                sb.Append($" INNER JOIN Countries as c on f.ORIGIN_COUNTRY_CODE = c.ID");
                sb.Append($" INNER JOIN Countries as co on f.DESTINATION_COUNTRY_CODE = co.ID");
                sb.Append($" WHERE (REAL_DEPARTURE_TIME BETWEEN GETDATE() AND DATEADD(hour, 12, GETDATE())))");
            }
            else if (typeName == "" || typeName == null)
            {
                sb.Append($" SELECT DISTINCT ComingFrom as 'Coming from'");
                sb.Append($" FROM (SELECT f.ID, a.AIRLINE_NAME, c.COUNTRY_NAME as ComingFrom, co.COUNTRY_NAME as Destination, f.DEPARTURE_TIME as DepartureTime, REAL_DEPARTURE_TIME, f.LANDING_TIME, REAL_LANDING_TIME");
                sb.Append($" FROM Flights as f");
                sb.Append($" INNER JOIN AirlineCompanies as a on f.AIRLINECOMPANY_ID = a.ID INNER JOIN Countries as c on f.ORIGIN_COUNTRY_CODE = c.ID");
                sb.Append($" INNER JOIN Countries as co on f.DESTINATION_COUNTRY_CODE = co.ID");
                sb.Append($" WHERE (REAL_LANDING_TIME BETWEEN GETDATE() AND DATEADD(hour, 12, GETDATE()) OR (REAL_LANDING_TIME BETWEEN DATEADD(hour, -4, GETDATE()) AND GETDATE()))");
                sb.Append($" UNION");
                sb.Append($" SELECT f.ID, a.AIRLINE_NAME, c.COUNTRY_NAME as ComingFrom, co.COUNTRY_NAME as Destination, f.DEPARTURE_TIME as DepartureTime, REAL_DEPARTURE_TIME, f.LANDING_TIME, REAL_LANDING_TIME");
                sb.Append($" FROM Flights as f");
                sb.Append($" INNER JOIN AirlineCompanies as a on f.AIRLINECOMPANY_ID = a.ID INNER JOIN Countries as c on f.ORIGIN_COUNTRY_CODE = c.ID");
                sb.Append($" INNER JOIN Countries as co on f.DESTINATION_COUNTRY_CODE = co.ID WHERE(REAL_DEPARTURE_TIME BETWEEN GETDATE() AND DATEADD(hour, 12, GETDATE()))) as ss");
            }
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Countries");

            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                Country country = new Country();
                country.COUNTRY_NAME = (string)dr["Coming from"];

                countries.Add(country);
            }
            return countries;
        }

        public void Remove(Country t)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"DELETE FROM Countries WHERE ID = {t.ID}");
            string SQL = sb.ToString();
            string res = DL.ExecuteSqlNonQuery(SQL);
            if (res == "")
            {
                throw new CountryDeleteErrorException("Country delete error");
            }
        }

        public void Update(Country t)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"UPDATE Countries SET COUNTRY_NAME = '{t.COUNTRY_NAME}'");
            sb.Append($" WHERE ID = {t.ID}");
            string SQL = sb.ToString();
            string res = DL.ExecuteSqlNonQuery(SQL);
            if (res == "")
            {
                throw new CountryUpdateErrorException("Country update error");
            }
        }
    }
}
