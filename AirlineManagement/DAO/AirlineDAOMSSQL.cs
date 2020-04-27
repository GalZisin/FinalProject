using AirlineManagement.POCO.Views;
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
    public class AirlineDAOMSSQL : IAirlineDAO
    {
        private SqlDAO DL;  // A central class of database connections
        public AirlineDAOMSSQL()
        {
            DL = new SqlDAO(FlightCenterConfig.strConn);
        }
        public string CheckIfAirlineCompanyExist(AirlineCompany t)
        {
            StringBuilder sb = new StringBuilder();
            string SQL1 = $"SELECT COUNT(*) FROM AirlineCompanies WHERE USER_NAME = '{t.USER_NAME}' OR EXISTS (SELECT USER_NAME FROM Customers WHERE USER_NAME = '{t.USER_NAME}')";
            string res = DL.ExecuteSqlScalarStatement(SQL1);
            return res;
        }
        public string CheckIfAirlineCompanyExistById(AirlineCompany t)
        {
            StringBuilder sb = new StringBuilder();
            string SQL1 = $"SELECT COUNT(*) FROM AirlineCompanies WHERE USER_NAME = '{t.USER_NAME}' AND ID <> {t.ID}";
            string res = DL.ExecuteSqlScalarStatement(SQL1);
            return res;
        }
        public long Add(AirlineCompany t)
        {
            StringBuilder sb = new StringBuilder();
            sb = new StringBuilder();
            sb.Append($"INSERT INTO AirlineCompanies(AIRLINE_NAME, USER_NAME, PASSWORD, COUNTRY_CODE)");
            sb.Append($" values('{ t.AIRLINE_NAME}', '{ t.USER_NAME}', '{ t.PASSWORD}'");
            string SQL2 = sb.ToString();
            DL.ExecuteSqlNonQuery(SQL2);
            SQL2 = $"SELECT ID FROM AirlineCompanies WHERE USER_NAME = '{t.USER_NAME}'";
            return Int64.Parse(DL.ExecuteSqlScalarStatement(SQL2));
        }

        public long AddAirlineCompany(AirlineCompanyView t)
        {
            StringBuilder sb = new StringBuilder();
            sb = new StringBuilder();
            sb.Append($"INSERT INTO AirlineCompanies (AIRLINE_NAME, USER_NAME, PASSWORD, COUNTRY_CODE)");
            sb.Append($" values('{ t.AIRLINE_NAME}', '{ t.USER_NAME}', '{ t.PASSWORD}', (SELECT ID FROM Countries WHERE COUNTRY_NAME = '{t.COUNTRY_NAME}'))");
            string SQL2 = sb.ToString();
            DL.ExecuteSqlNonQuery(SQL2);
            SQL2 = $"SELECT ID FROM AirlineCompanies WHERE USER_NAME = '{t.USER_NAME}'";
            return Int64.Parse(DL.ExecuteSqlScalarStatement(SQL2));
        }

        public AirlineCompany Get(long id)
        {
            AirlineCompany a = null;
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM AirlineCompanies WHERE ID = {id}");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "AirlineCompanies");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                a = new AirlineCompany();
                a.ID = (long)dr["ID"];
                a.AIRLINE_NAME = (string)dr["AIRLINE_NAME"];
                a.USER_NAME = (string)dr["USER_NAME"];
                a.PASSWORD = (string)dr["PASSWORD"];
                a.COUNTRY_CODE = (long)dr["COUNTRY_CODE"];
            }
            if (a != null)
            {
                return a;
            }
            return null;
        }
        public AirlineCompanyView GetGetAirlineCompanyById(long id)
        {
            AirlineCompanyView a = null;
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT a.ID, a.AIRLINE_NAME, a.USER_NAME, a.PASSWORD, a.COUNTRY_CODE, (SELECT COUNTRY_NAME FROM Countries WHERE ID = a.COUNTRY_CODE) as countryName");
            sb.Append($" FROM AirlineCompanies a INNER JOIN Countries c ON c.ID = a.COUNTRY_CODE WHERE a.ID = {id}");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "AirlineCompanies");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                a = new AirlineCompanyView();
                a.ID = (long)dr["ID"];
                a.COUNTRY_NAME = (string)dr["countryName"];
                a.AIRLINE_NAME = (string)dr["AIRLINE_NAME"];
                a.USER_NAME = (string)dr["USER_NAME"];
                a.PASSWORD = (string)dr["PASSWORD"];
                a.COUNTRY_CODE = (long)dr["COUNTRY_CODE"];
            }
            if (a != null)
            {
                return a;
            }
            return null;
        }

        public AirlineCompany GetAirlineByUsername(string userName)
        {
            AirlineCompany a = null;
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM AirlineCompanies WHERE USER_NAME = '{userName}'");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "AirlineCompanies");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                a = new AirlineCompany();
                a.ID = (long)dr["ID"];
                a.AIRLINE_NAME = (string)dr["AIRLINE_NAME"];
                a.USER_NAME = (string)dr["USER_NAME"];
                a.PASSWORD = (string)dr["PASSWORD"];
                a.COUNTRY_CODE = (long)dr["COUNTRY_CODE"];
            }
            if (a != null)
            {
                return a;
            }
                return null;
        }
        public AirlineCompany GetAirlineCompanyByAirlineName(string AirlineName)
        {
            AirlineCompany a = null;
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM AirlineCompanies WHERE AIRLINE_NAME = '{AirlineName}'");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "AirlineCompanies");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                a = new AirlineCompany();
                a.ID = (long)dr["ID"];
                a.AIRLINE_NAME = (string)dr["AIRLINE_NAME"];
                a.USER_NAME = (string)dr["USER_NAME"];
                a.PASSWORD = (string)dr["PASSWORD"];
                a.COUNTRY_CODE = (long)dr["COUNTRY_CODE"];
            }
            if (a != null)
            {
                return a;
            }
                return null; 
        }
        public IList<AirlineCompany> GetAll()
        {
            IList<AirlineCompany> airlineCompanies = new List<AirlineCompany>();
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM AirlineCompanies");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "AirlineCompanies");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                AirlineCompany a = new AirlineCompany();
                a.ID = (long)dr["ID"];
                a.AIRLINE_NAME = (string)dr["AIRLINE_NAME"];
                a.USER_NAME = (string)dr["USER_NAME"];
                a.PASSWORD = (string)dr["PASSWORD"];
                a.COUNTRY_CODE = (long)dr["COUNTRY_CODE"];
                airlineCompanies.Add(a);
            }
            return airlineCompanies;
        }
        public IList<AirlineCompanyView> GetAllAirlineCompanies()
        {
            IList<AirlineCompanyView> airlineCompanies = new List<AirlineCompanyView>();
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT a.ID, a.AIRLINE_NAME, a.USER_NAME, a.PASSWORD, a.COUNTRY_CODE, (SELECT COUNTRY_NAME FROM Countries WHERE ID = a.COUNTRY_CODE) as countryName");
            sb.Append($" FROM AirlineCompanies a INNER JOIN Countries c ON c.ID = a.COUNTRY_CODE");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "AirlineCompanies");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                AirlineCompanyView a = new AirlineCompanyView();
                a.ID = (long)dr["ID"];
                a.COUNTRY_NAME = (string)dr["countryName"];
                a.AIRLINE_NAME = (string)dr["AIRLINE_NAME"];
                a.USER_NAME = (string)dr["USER_NAME"];
                a.PASSWORD = (string)dr["PASSWORD"];
                a.COUNTRY_CODE = (long)dr["COUNTRY_CODE"];
                airlineCompanies.Add(a);
            }
            return airlineCompanies;
        }
        public IList<AirlineCompany> GetAllAirlineCompaniesByScheduledTime(string typeName)
        {
            IList<AirlineCompany> airlineCompanies = new List<AirlineCompany>();
            StringBuilder sb = new StringBuilder();
            //string str1 = "";
            if (typeName == "Arrivals")
            {
                sb.Append($" SELECT a.AIRLINE_NAME");
                sb.Append($" FROM AirlineCompanies as a");
                sb.Append($" WHERE a.AIRLINE_NAME IN (SELECT a.AIRLINE_NAME");
                sb.Append($" FROM Flights as f");
                sb.Append($" INNER JOIN AirlineCompanies as a on f.AIRLINECOMPANY_ID = a.ID");
                sb.Append($" INNER JOIN Countries as c on f.ORIGIN_COUNTRY_CODE = c.ID");
                sb.Append($" INNER JOIN Countries as co on f.DESTINATION_COUNTRY_CODE = co.ID");
                sb.Append($" WHERE(REAL_LANDING_TIME BETWEEN GETDATE() AND DATEADD(hour, 12, GETDATE()) OR (REAL_LANDING_TIME BETWEEN DATEADD(hour, -4, GETDATE()) AND GETDATE())))");
            }
            else if (typeName == "Departures")
            {
                sb.Append($" SELECT a.AIRLINE_NAME");
                sb.Append($" FROM AirlineCompanies as a");
                sb.Append($" WHERE a.AIRLINE_NAME IN (SELECT a.AIRLINE_NAME");
                sb.Append($" FROM Flights as f");
                sb.Append($" INNER JOIN AirlineCompanies as a on f.AIRLINECOMPANY_ID = a.ID");
                sb.Append($" INNER JOIN Countries as c on f.ORIGIN_COUNTRY_CODE = c.ID");
                sb.Append($" INNER JOIN Countries as co on f.DESTINATION_COUNTRY_CODE = co.ID");
                sb.Append($" WHERE (REAL_DEPARTURE_TIME BETWEEN GETDATE() AND DATEADD(hour, 12, GETDATE())))");
            }
            else if (typeName == "" || typeName == null)
            {
                sb.Append($" SELECT DISTINCT AIRLINE_NAME");
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
            DataSet DS = DL.GetSqlQueryDS(SQL, "AirlineCompanies");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                AirlineCompany a = new AirlineCompany();
                //a.ID = (long)dr["ID"];
                a.AIRLINE_NAME = (string)dr["AIRLINE_NAME"];
                //a.USER_NAME = (string)dr["USER_NAME"];
                //a.PASSWORD = (string)dr["PASSWORD"];
                //a.COUNTRY_CODE = (long)dr["COUNTRY_CODE"];
                airlineCompanies.Add(a);
            }
            return airlineCompanies;
        }

        public IList<AirlineCompany> GetAllAirlinesByCountry(long countryId)
        {
            IList<AirlineCompany> airlineCompanies = new List<AirlineCompany>();
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM AirlineCompanies WHERE COUNTRY_CODE = '{countryId}'");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "AirlineCompanies");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                AirlineCompany a = new AirlineCompany();
                a.ID = (long)dr["ID"];
                a.AIRLINE_NAME = (string)dr["AIRLINE_NAME"];
                a.USER_NAME = (string)dr["USER_NAME"];
                a.PASSWORD = (string)dr["PASSWORD"];
                a.COUNTRY_CODE = (long)dr["COUNTRY_CODE"];
                airlineCompanies.Add(a);
            }
            return airlineCompanies;
        }

        public void Remove(AirlineCompany t)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"DELETE FROM AirlineCompanies WHERE ID = {t.ID}");
            string SQL = sb.ToString();
            string res = DL.ExecuteSqlNonQuery(SQL);
            if (res == "")
            {
                throw new AirlineCompanyDeleteErrorException("AirlineCompanie delete error");
            }
        }
        public void RemoveAirlineCompanyById(long airlinecompanyId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"DELETE FROM AirlineCompanies WHERE ID = {airlinecompanyId}");
            string SQL = sb.ToString();
            string res = DL.ExecuteSqlNonQuery(SQL);
            if (res == "")
            {
                throw new AirlineCompanyDeleteErrorException("AirlineCompanie delete error");
            }
        }
        public void RemoveAirlineCompanyByCountryCode(long countryCode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"DELETE FROM AirlineCompanies WHERE COUNTRY_CODE = {countryCode}");
            string SQL = sb.ToString();
            string res = DL.ExecuteSqlNonQuery(SQL);
            if (res == "")
            {
                throw new AirlineCompanyDeleteErrorException("AirlineCompanie delete error");
            }
        }
        //public long GetAirlineCompanyIdByCountryCode(long countryCode)
        //{
        //    AirlineCompany a = null;
        //    string SQL = $"SELECT ID FROM AirlineCompanies WHERE COUNTRY_CODE = {countryCode}";
        //    DataSet DS = DL.GetSqlQueryDS(SQL, "AirlineCompanies");
        //    DataTable dt = DS.Tables[0];
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        a = new AirlineCompany();
        //        a.ID = (long)dr["ID"];

        //    }
        //    if (a != null)
        //    {
        //        return a.ID;
        //    }
        //    else
        //    {
        //        throw new AirlineCompanyDoesNotExistException("Airline Company Doesn't Exist");
        //    }
        //}
        public void Update(AirlineCompany t)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"UPDATE AirlineCompanies SET AIRLINE_NAME = '{t.AIRLINE_NAME}', USER_NAME = '{t.USER_NAME}', PASSWORD = '{t.PASSWORD}', COUNTRY_CODE = {t.COUNTRY_CODE}");
            sb.Append($" WHERE ID = {t.ID}");
            string SQL = sb.ToString();
            string res = DL.ExecuteSqlNonQuery(SQL);
            if (res == "")
            {
                throw new AirlineCompanyUpdateErrorException("AirlineCompany update error");
            }
        }
        public void UpdateAirlineCompany(AirlineCompanyView t)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"UPDATE AirlineCompanies SET AIRLINE_NAME = '{t.AIRLINE_NAME}', USER_NAME = '{t.USER_NAME}', PASSWORD = '{t.PASSWORD}', COUNTRY_CODE = (SELECT ID FROM Countries WHERE COUNTRY_NAME = '{t.COUNTRY_NAME}')");
            sb.Append($" WHERE ID = {t.ID}");
            string SQL = sb.ToString();
            string res = DL.ExecuteSqlNonQuery(SQL);
            if (res == "")
            {
                throw new AirlineCompanyUpdateErrorException("AirlineCompany update error");
            }
        }

    }
}
