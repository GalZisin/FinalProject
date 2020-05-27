using AirlineManagement.POCO.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement
{
    public class FlightDAOMSSQL : IFlightDAO
    {
        private SqlDAO DL;  // A central class of database connections
        public FlightDAOMSSQL()
        {
            DL = new SqlDAO(FlightCenterConfig.strConn);
        }
        public string CheckIfFlightExist(FlightView t)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT COUNT(*) FROM Flights WHERE ID = {t.ID } AND AIRLINECOMPANY_ID = {t.AIRLINECOMPANY_ID}");
            string SQL1 = sb.ToString();
            string res = DL.ExecuteSqlScalarStatement(SQL1);
            return res;
        }
        public long Add(FlightView t)
        {
            StringBuilder sb = new StringBuilder();

            sb = new StringBuilder();
            sb.Append($"INSERT INTO Flights(AIRLINECOMPANY_ID, ORIGIN_COUNTRY_CODE, DESTINATION_COUNTRY_CODE, DEPARTURE_TIME, REAL_DEPARTURE_TIME, LANDING_TIME, REAL_LANDING_TIME, REMANING_TICKETS, TOTAL_TICKETS, FLIGHT_NUMBER)");
            sb.Append($" values((SELECT ID FROM AirlineCompanies WHERE AIRLINE_NAME = '{t.AIRLINE_NAME}'),");
            sb.Append($" (SELECT ID FROM Countries WHERE COUNTRY_NAME ='{t.O_COUNTRY_NAME}'),");
            sb.Append($" (SELECT ID FROM Countries WHERE COUNTRY_NAME ='{t.D_COUNTRY_NAME}'),");
            sb.Append($" '{ t.REAL_DEPARTURE_TIME.ToString("yyyy-MM-dd HH:mm:ss")}',");
            sb.Append($" '{ t.REAL_DEPARTURE_TIME.ToString("yyyy-MM-dd HH:mm:ss")}',");
            sb.Append($" '{ t.REAL_LANDING_TIME.ToString("yyyy-MM-dd HH:mm:ss")}',");
            sb.Append($" '{ t.REAL_LANDING_TIME.ToString("yyyy-MM-dd HH:mm:ss")}',");
            sb.Append($" {t.REMANING_TICKETS}, {t.TOTAL_TICKETS}, '{t.FLIGHT_NUMBER}')");
            string SQL2 = sb.ToString();
            DL.ExecuteSqlNonQuery(SQL2);
            sb = new StringBuilder();
            sb.Append($"SELECT ID FROM Flights WHERE AIRLINECOMPANY_ID = (SELECT ID FROM AirlineCompanies WHERE AIRLINE_NAME = '{t.AIRLINE_NAME}')");
            sb.Append($" AND ORIGIN_COUNTRY_CODE = (SELECT ID FROM Countries WHERE COUNTRY_NAME ='{t.O_COUNTRY_NAME}')");
            sb.Append($" AND DESTINATION_COUNTRY_CODE = (SELECT ID FROM Countries WHERE COUNTRY_NAME ='{t.D_COUNTRY_NAME}')");
            sb.Append($" AND CONVERT(char(16),REAL_DEPARTURE_TIME,120) = '{t.REAL_DEPARTURE_TIME.ToString("yyyy-MM-dd HH:mm")}'");
            sb.Append($" AND CONVERT(char(16),REAL_LANDING_TIME,120) = '{t.REAL_LANDING_TIME.ToString("yyyy-MM-dd HH:mm")}'");
            sb.Append($" AND REMANING_TICKETS = {t.REMANING_TICKETS}");
            sb.Append($" AND TOTAL_TICKETS = {t.TOTAL_TICKETS}");
            sb.Append($" AND FLIGHT_NUMBER = '{ t.FLIGHT_NUMBER}'");
            string SQL3 = sb.ToString();
            return Int64.Parse(DL.ExecuteSqlScalarStatement(SQL3));
        }
        public FlightView Get(long flightId)
        {
            FlightView flightView = null;
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT f.ID, a.AIRLINE_NAME, f.ORIGIN_COUNTRY_CODE, f.DESTINATION_COUNTRY_CODE, c.COUNTRY_NAME as 'Coming from', co.COUNTRY_NAME as Destination, f.DEPARTURE_TIME as 'Departure time', REAL_DEPARTURE_TIME, f.LANDING_TIME as 'Landing time', REAL_LANDING_TIME, REMANING_TICKETS, TOTAL_TICKETS, FLIGHT_NUMBER");
            sb.Append($" FROM Flights as f");
            sb.Append($" INNER JOIN AirlineCompanies as a on f.AIRLINECOMPANY_ID = a.ID");
            sb.Append($" INNER JOIN Countries as c on f.ORIGIN_COUNTRY_CODE = c.ID");
            sb.Append($" INNER JOIN Countries as co on f.DESTINATION_COUNTRY_CODE = co.ID");
            sb.Append($" WHERE f.ID = {flightId}");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Flights");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                flightView = new FlightView();
                flightView.ID = Convert.ToInt64(dr["ID"]);
                flightView.FLIGHT_NUMBER= (string)dr["FLIGHT_NUMBER"];
                flightView.AIRLINE_NAME = (string)dr["AIRLINE_NAME"];
                flightView.ORIGIN_COUNTRY_CODE = (long)dr["ORIGIN_COUNTRY_CODE"];
                flightView.DESTINATION_COUNTRY_CODE = (long)dr["DESTINATION_COUNTRY_CODE"];
                flightView.O_COUNTRY_NAME = (string)dr["Coming from"];
                flightView.D_COUNTRY_NAME = (string)dr["Destination"];
                flightView.DEPARTURE_TIME = (DateTime)dr["Departure time"];
                flightView.REAL_DEPARTURE_TIME = (DateTime)dr["REAL_DEPARTURE_TIME"];
                flightView.LANDING_TIME = (DateTime)dr["Landing time"];
                flightView.REAL_LANDING_TIME = (DateTime)dr["REAL_LANDING_TIME"];
                flightView.REMANING_TICKETS = (int)dr["REMANING_TICKETS"];
                flightView.TOTAL_TICKETS = (int)dr["TOTAL_TICKETS"];
            }
            if (flightView != null)
            {
                return flightView;
            }
            return null;
        }
        public IList<FlightView> GetAll()
        {
            IList<FlightView> flights = new List<FlightView>();
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM Flights");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Flights");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                FlightView flight = new FlightView();
                flight.ID = Convert.ToInt64(dr["ID"]);
                flight.AIRLINECOMPANY_ID = (long)dr["AIRLINECOMPANY_ID"];
                flight.ORIGIN_COUNTRY_CODE = (long)dr["ORIGIN_COUNTRY_CODE"];
                flight.DESTINATION_COUNTRY_CODE = (long)dr["DESTINATION_COUNTRY_CODE"];
                flight.DEPARTURE_TIME = Convert.ToDateTime(dr["DEPARTURE_TIME"]);
                flight.LANDING_TIME = Convert.ToDateTime(dr["LANDING_TIME"]);
                flight.REMANING_TICKETS = (int)dr["REMANING_TICKETS"];
                flight.TOTAL_TICKETS = (int)dr["TOTAL_TICKETS"];
                flights.Add(flight);
            }
            return flights;
        }
        public IList<FlightView> GetFlightsByCustomerId(long CustomerId)
        {
            IList<FlightView> flights = new List<FlightView>();
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM Flights WHERE ID IN (SELECT FLIGHT_ID FROM Tickets WHERE CUSTOMER_ID = {CustomerId})");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Flights");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                FlightView flight = new FlightView();
                flight.ID = Convert.ToInt64(dr["ID"]);
                flight.AIRLINECOMPANY_ID = (long)dr["AIRLINECOMPANY_ID"];
                flight.ORIGIN_COUNTRY_CODE = (long)dr["ORIGIN_COUNTRY_CODE"];
                flight.DESTINATION_COUNTRY_CODE = (long)dr["DESTINATION_COUNTRY_CODE"];
                flight.DEPARTURE_TIME = Convert.ToDateTime(dr["DEPARTURE_TIME"]);
                flight.LANDING_TIME = Convert.ToDateTime(dr["LANDING_TIME"]);
                flight.REMANING_TICKETS = (int)dr["REMANING_TICKETS"];
                flights.Add(flight);
            }
            return flights;
        }
        public IList<FlightView> GetFlightsByCustomerUserName(string customerUserName)
        {
            IList<FlightView> flights = new List<FlightView>();
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM Flights WHERE ID IN (SELECT FLIGHT_ID FROM Tickets WHERE CUSTOMER_ID = (SELECT ID FROM Customers WHERE USER_NAME = '{customerUserName}'))");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Flights");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                FlightView flight = new FlightView();
                flight.ID = Convert.ToInt64(dr["ID"]);
                flight.AIRLINECOMPANY_ID = (long)dr["AIRLINECOMPANY_ID"];
                flight.ORIGIN_COUNTRY_CODE = (long)dr["ORIGIN_COUNTRY_CODE"];
                flight.DESTINATION_COUNTRY_CODE = (long)dr["DESTINATION_COUNTRY_CODE"];
                flight.DEPARTURE_TIME = Convert.ToDateTime(dr["DEPARTURE_TIME"]);
                flight.LANDING_TIME = Convert.ToDateTime(dr["LANDING_TIME"]);
                flight.REMANING_TICKETS = (int)dr["REMANING_TICKETS"];
                flight.TOTAL_TICKETS = (int)dr["TOTAL_TICKETS"];
                flights.Add(flight);
            }
            return flights;
        }
        public Dictionary<FlightView, int> GetAllFlightsByVacancy()
        {
            Dictionary<FlightView, int> vacancyByFlight = new Dictionary<FlightView, int>();
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM Flights");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Flights");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                FlightView flight = new FlightView();
                flight.ID = (long)dr["ID"];
                flight.AIRLINECOMPANY_ID = (long)dr["AIRLINECOMPANY_ID"];
                flight.ORIGIN_COUNTRY_CODE = (long)dr["ORIGIN_COUNTRY_CODE"];
                flight.DESTINATION_COUNTRY_CODE = (long)dr["DESTINATION_COUNTRY_CODE"];
                flight.DEPARTURE_TIME = (DateTime)dr["DEPARTURE_TIME"];
                flight.LANDING_TIME = (DateTime)dr["LANDING_TIME"];
                flight.REMANING_TICKETS = (int)dr["REMANING_TICKETS"];
                flight.TOTAL_TICKETS = (int)dr["TOTAL_TICKETS"];
                if (!vacancyByFlight.ContainsKey(flight))
                {
                    vacancyByFlight.Add(flight, flight.REMANING_TICKETS);
                }
            }
            return vacancyByFlight;
        }
        public IList<FlightView> GetAllFlightsByVacancyAndScheduledTime()
        {
            IList<FlightView> flights = new List<FlightView>();
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT f.ID, a.AIRLINE_NAME, c.COUNTRY_NAME as 'Coming from', co.COUNTRY_NAME as Destination, f.DEPARTURE_TIME as 'Departure time', REAL_DEPARTURE_TIME, f.LANDING_TIME as 'Landing time', REAL_LANDING_TIME, REMANING_TICKETS, TOTAL_TICKETS, FLIGHT_NUMBER");
            sb.Append($" FROM Flights as f");
            sb.Append($" INNER JOIN AirlineCompanies as a on f.AIRLINECOMPANY_ID = a.ID");
            sb.Append($" INNER JOIN Countries as c on f.ORIGIN_COUNTRY_CODE = c.ID");
            sb.Append($" INNER JOIN Countries as co on f.DESTINATION_COUNTRY_CODE = co.ID");
            sb.Append($" WHERE(REAL_DEPARTURE_TIME BETWEEN DATEADD(hour, 12, GETDATE()) AND CONVERT(datetime, '2021-01-25 23:59:59'))");
            sb.Append($" AND REMANING_TICKETS > 0 AND REMANING_TICKETS < TOTAL_TICKETS");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Flights");
            DL.ErrorMessage = "";
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                FlightView flight = new FlightView();
                flight.ID = (long)dr["ID"];
                flight.FLIGHT_NUMBER = (string)dr["FLIGHT_NUMBER"];
                flight.AIRLINE_NAME = (string)dr["AIRLINE_NAME"];
                flight.O_COUNTRY_NAME = (string)dr["Coming from"];
                flight.D_COUNTRY_NAME = (string)dr["Destination"];
                flight.DEPARTURE_TIME = (DateTime)dr["Departure time"];
                flight.REAL_DEPARTURE_TIME = (DateTime)dr["REAL_DEPARTURE_TIME"];
                flight.LANDING_TIME = (DateTime)dr["Landing time"];
                flight.REAL_LANDING_TIME = (DateTime)dr["REAL_LANDING_TIME"];
                flight.REMANING_TICKETS = (int)dr["REMANING_TICKETS"];
                flight.TOTAL_TICKETS = (int)dr["TOTAL_TICKETS"];
                flights.Add(flight);
            }
            return flights;
        }
        public IList<FlightView> GetAllFlightsByVacancyAndScheduledTime(string LocalCountryName)
        {
            IList<FlightView> flights = new List<FlightView>();
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT f.ID, a.AIRLINE_NAME, c.COUNTRY_NAME as 'Coming from', co.COUNTRY_NAME as Destination, f.DEPARTURE_TIME as 'Departure time', REAL_DEPARTURE_TIME, f.LANDING_TIME as 'Landing time', REAL_LANDING_TIME, REMANING_TICKETS, TOTAL_TICKETS, FLIGHT_NUMBER");
            sb.Append($" FROM Flights as f");
            sb.Append($" INNER JOIN AirlineCompanies as a on f.AIRLINECOMPANY_ID = a.ID");
            sb.Append($" INNER JOIN Countries as c on f.ORIGIN_COUNTRY_CODE = c.ID");
            sb.Append($" INNER JOIN Countries as co on f.DESTINATION_COUNTRY_CODE = co.ID");
            sb.Append($" WHERE(REAL_DEPARTURE_TIME BETWEEN DATEADD(hour, 12, GETDATE()) AND CONVERT(datetime, '2021-01-25 23:59:59'))");
            sb.Append($" AND REMANING_TICKETS > 0 AND REMANING_TICKETS < TOTAL_TICKETS  AND c.COUNTRY_NAME = '{LocalCountryName}'");
            sb.Append($" AND EXISTS(SELECT f1.AIRLINECOMPANY_ID FROM Flights as f1 WHERE f1.ORIGIN_COUNTRY_CODE = f.DESTINATION_COUNTRY_CODE AND f1.DESTINATION_COUNTRY_CODE=f.ORIGIN_COUNTRY_CODE and f1.AIRLINECOMPANY_ID=f.AIRLINECOMPANY_ID ");
            sb.Append($" AND f1.REAL_DEPARTURE_TIME > DATEADD(hour, 24, f.REAL_LANDING_TIME)) Order by f.REAL_DEPARTURE_TIME");

            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Flights");
            DL.ErrorMessage = "";
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                FlightView flight = new FlightView();
                flight.ID = (long)dr["ID"];
                flight.FLIGHT_NUMBER = (string)dr["FLIGHT_NUMBER"];
                flight.AIRLINE_NAME = (string)dr["AIRLINE_NAME"];
                flight.O_COUNTRY_NAME = (string)dr["Coming from"];
                flight.D_COUNTRY_NAME = (string)dr["Destination"];
                flight.DEPARTURE_TIME = (DateTime)dr["Departure time"];
                flight.REAL_DEPARTURE_TIME = (DateTime)dr["REAL_DEPARTURE_TIME"];
                flight.LANDING_TIME = (DateTime)dr["Landing time"];
                flight.REAL_LANDING_TIME = (DateTime)dr["REAL_LANDING_TIME"];
                flight.REMANING_TICKETS = (int)dr["REMANING_TICKETS"];
                flight.TOTAL_TICKETS = (int)dr["TOTAL_TICKETS"];
                flights.Add(flight);
            }
            return flights;
        }
        public IList<FlightView> GetAllGoingFlightsByVacancyAndScheduledTime(string flightNumber, string originCountry, string destinationCountry, string company, string departureDate)
        {
            string str1 = "";
            IList<FlightView> flights = new List<FlightView>();
            StringBuilder sb = new StringBuilder();
            //if (flightNumber == null || flightNumber == "" || flightNumber == "Select Flight Number")
            //{
            //    str1 = $" AND AIRLINE_NAME LIKE '{company}%' AND c.COUNTRY_NAME LIKE '{originCountry}%' AND co.COUNTRY_NAME LIKE '{destinationCountry}%' AND CONVERT(date, REAL_DEPARTURE_TIME) = '{departureDate}'";
            //}
            //else
            //{
            //    str1 = $" AND f.FLIGHT_NUMBER LIKE '{flightNumber}%'";
            //}
                sb.Append($"SELECT f.ID, a.AIRLINE_NAME, c.COUNTRY_NAME as 'Coming from', co.COUNTRY_NAME as Destination, f.DEPARTURE_TIME as 'Departure time', REAL_DEPARTURE_TIME, f.LANDING_TIME as 'Landing time', REAL_LANDING_TIME, REMANING_TICKETS, TOTAL_TICKETS, FLIGHT_NUMBER");
            sb.Append($" FROM Flights as f");
            sb.Append($" INNER JOIN AirlineCompanies as a on f.AIRLINECOMPANY_ID = a.ID");
            sb.Append($" INNER JOIN Countries as c on f.ORIGIN_COUNTRY_CODE = c.ID");
            sb.Append($" INNER JOIN Countries as co on f.DESTINATION_COUNTRY_CODE = co.ID");
            sb.Append($" WHERE (REAL_DEPARTURE_TIME BETWEEN DATEADD(hour, 12, GETDATE()) AND CONVERT(datetime, '2021-01-25 23:59:59'))");
            sb.Append($" AND REMANING_TICKETS > 0 AND REMANING_TICKETS < TOTAL_TICKETS");
            sb.Append($" AND AIRLINE_NAME LIKE '{company}%' AND c.COUNTRY_NAME LIKE '{originCountry}%' AND co.COUNTRY_NAME LIKE '{destinationCountry}%' AND CONVERT(date, REAL_DEPARTURE_TIME) = '{departureDate}'");
            sb.Append($" AND f.FLIGHT_NUMBER LIKE '{flightNumber}%'");
           //sb.Append(str1);
          string SQL = sb.ToString();
            //AddToLogFile("GetAllGoingFlightsByVacancyAndScheduledTime: SQL: " + SQL);
            DataSet DS = DL.GetSqlQueryDS(SQL, "Flights");
            DL.ErrorMessage = "";
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                FlightView flight = new FlightView();
                flight.ID = (long)dr["ID"];
                flight.FLIGHT_NUMBER = (string)dr["FLIGHT_NUMBER"];
                flight.AIRLINE_NAME = (string)dr["AIRLINE_NAME"];
                flight.O_COUNTRY_NAME = (string)dr["Coming from"];
                flight.D_COUNTRY_NAME = (string)dr["Destination"];
                flight.DEPARTURE_TIME = (DateTime)dr["Departure time"];
                flight.REAL_DEPARTURE_TIME = (DateTime)dr["REAL_DEPARTURE_TIME"];
                flight.LANDING_TIME = (DateTime)dr["Landing time"];
                flight.REAL_LANDING_TIME = (DateTime)dr["REAL_LANDING_TIME"];
                flight.REMANING_TICKETS = (int)dr["REMANING_TICKETS"];
                flight.TOTAL_TICKETS = (int)dr["TOTAL_TICKETS"];
                flights.Add(flight);
            }
            return flights;
        }
        public IList<FlightView> GetAllReturnFlightsByVacancyAndScheduledTime(string originCountry, string destinationCountry, string company, string returnDate)
        {
            string str1 = "";
            IList<FlightView> flights = new List<FlightView>();
            StringBuilder sb = new StringBuilder();
            //if (flightId == null || flightId == "")
            //{
            //    str1 = $" AND AIRLINE_NAME LIKE '{company}%' AND c.COUNTRY_NAME LIKE '{destinationCountry}%' AND co.COUNTRY_NAME LIKE '{originCountry}%' AND CONVERT(date, REAL_DEPARTURE_TIME) = '{returnDate}'";
            //}
            //else
            //{
            //    str1 = $" AND AIRLINE_NAME LIKE '{company}%' AND c.COUNTRY_NAME LIKE '{destinationCountry}%' AND co.COUNTRY_NAME LIKE '{originCountry}%' AND CONVERT(date, REAL_DEPARTURE_TIME) = '{returnDate}' AND f.ID = '{flightId}'";
            //}
            sb.Append($"SELECT f.ID, a.AIRLINE_NAME, c.COUNTRY_NAME as 'Coming from', co.COUNTRY_NAME as Destination, f.DEPARTURE_TIME as 'Departure time', REAL_DEPARTURE_TIME, f.LANDING_TIME as 'Landing time', REAL_LANDING_TIME, REMANING_TICKETS, TOTAL_TICKETS, FLIGHT_NUMBER");
            sb.Append($" FROM Flights as f");
            sb.Append($" INNER JOIN AirlineCompanies as a on f.AIRLINECOMPANY_ID = a.ID");
            sb.Append($" INNER JOIN Countries as c on f.ORIGIN_COUNTRY_CODE = c.ID");
            sb.Append($" INNER JOIN Countries as co on f.DESTINATION_COUNTRY_CODE = co.ID");
            sb.Append($" WHERE (REAL_DEPARTURE_TIME BETWEEN DATEADD(hour, 12, GETDATE()) AND CONVERT(datetime, '2021-01-25 23:59:59'))");
            sb.Append($" AND REMANING_TICKETS > 0 AND REMANING_TICKETS < TOTAL_TICKETS");
            sb.Append($" AND AIRLINE_NAME LIKE '{company}%' AND c.COUNTRY_NAME LIKE '{destinationCountry}%' AND co.COUNTRY_NAME LIKE '{originCountry}%' AND CONVERT(date, REAL_DEPARTURE_TIME) = '{returnDate}'");
           //sb.Append(str1);
           string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Flights");
            DL.ErrorMessage = "";
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                FlightView flight = new FlightView();
                flight.ID = (long)dr["ID"];
                flight.FLIGHT_NUMBER = (string)dr["FLIGHT_NUMBER"];
                flight.AIRLINE_NAME = (string)dr["AIRLINE_NAME"];
                flight.O_COUNTRY_NAME = (string)dr["Coming from"];
                flight.D_COUNTRY_NAME = (string)dr["Destination"];
                flight.DEPARTURE_TIME = (DateTime)dr["Departure time"];
                flight.REAL_DEPARTURE_TIME = (DateTime)dr["REAL_DEPARTURE_TIME"];
                flight.LANDING_TIME = (DateTime)dr["Landing time"];
                flight.REAL_LANDING_TIME = (DateTime)dr["REAL_LANDING_TIME"];
                flight.REMANING_TICKETS = (int)dr["REMANING_TICKETS"];
                flight.TOTAL_TICKETS = (int)dr["TOTAL_TICKETS"];
                flights.Add(flight);
            }
            return flights;
        }
        public IList<FlightView> GetFlightsByAirlineCompanyId(long airlineCompanyId)
        {
            IList<FlightView> flights = new List<FlightView>();
            StringBuilder sb = new StringBuilder();

            sb.Append($"SELECT f.ID, a.AIRLINE_NAME, c.COUNTRY_NAME as 'Coming from', co.COUNTRY_NAME as Destination, f.DEPARTURE_TIME as 'Departure time', REAL_DEPARTURE_TIME, f.LANDING_TIME as 'Landing time', REAL_LANDING_TIME, REMANING_TICKETS, TOTAL_TICKETS, FLIGHT_NUMBER");
            sb.Append($" FROM Flights as f");
            sb.Append($" INNER JOIN AirlineCompanies as a on f.AIRLINECOMPANY_ID = a.ID");
            sb.Append($" INNER JOIN Countries as c on f.ORIGIN_COUNTRY_CODE = c.ID");
            sb.Append($" INNER JOIN Countries as co on f.DESTINATION_COUNTRY_CODE = co.ID");
            sb.Append($" WHERE AIRLINECOMPANY_ID = {airlineCompanyId}");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Flights");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                FlightView flight = new FlightView();
                flight.ID = (long)dr["ID"];
                flight.FLIGHT_NUMBER = (string)dr["FLIGHT_NUMBER"];
                flight.AIRLINE_NAME = (string)dr["AIRLINE_NAME"];
                flight.O_COUNTRY_NAME = (string)dr["Coming from"];
                flight.D_COUNTRY_NAME = (string)dr["Destination"];
                flight.DEPARTURE_TIME = (DateTime)dr["Departure time"];
                flight.REAL_DEPARTURE_TIME = (DateTime)dr["REAL_DEPARTURE_TIME"];
                flight.LANDING_TIME = (DateTime)dr["Landing time"];
                flight.REAL_LANDING_TIME = (DateTime)dr["REAL_LANDING_TIME"];
                flight.REMANING_TICKETS = (int)dr["REMANING_TICKETS"];
                flight.TOTAL_TICKETS = (int)dr["TOTAL_TICKETS"];
                flights.Add(flight);
            }
            return flights;
        }
        public IList<FlightView> GetFlightsByDepartureDate(DateTime departureDate)
        {
            IList<FlightView> flights = new List<FlightView>();
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM Flights WHERE DEPARTURE_TIME = '{departureDate.ToString("yyyy-MM-dd HH:mm:ss")}'");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Flights");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                FlightView flight = new FlightView();
                flight.ID = (long)dr["ID"];
                flight.AIRLINECOMPANY_ID = (long)dr["AIRLINECOMPANY_ID"];
                flight.ORIGIN_COUNTRY_CODE = (long)dr["ORIGIN_COUNTRY_CODE"];
                flight.DESTINATION_COUNTRY_CODE = (long)dr["DESTINATION_COUNTRY_CODE"];
                flight.DEPARTURE_TIME = Convert.ToDateTime(dr["DEPARTURE_TIME"]);
                flight.LANDING_TIME = Convert.ToDateTime(dr["LANDING_TIME"]);
                flight.REMANING_TICKETS = (int)dr["REMANING_TICKETS"];
                flight.TOTAL_TICKETS = (int)dr["TOTAL_TICKETS"];
                flights.Add(flight);
            }
            return flights;
        }
        public IList<FlightView> GetFlightsByDestinationCountry(long countryCode)
        {
            IList<FlightView> flights = new List<FlightView>();
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM Flights WHERE DESTINATION_COUNTRY_CODE = {countryCode}");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Flights");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                FlightView flight = new FlightView();
                flight.ID = (long)dr["ID"];
                flight.AIRLINECOMPANY_ID = (long)dr["AIRLINECOMPANY_ID"];
                flight.ORIGIN_COUNTRY_CODE = (long)dr["ORIGIN_COUNTRY_CODE"];
                flight.DESTINATION_COUNTRY_CODE = (long)dr["DESTINATION_COUNTRY_CODE"];
                flight.DEPARTURE_TIME = (DateTime)dr["DEPARTURE_TIME"];
                flight.LANDING_TIME = (DateTime)dr["LANDING_TIME"];
                flight.REMANING_TICKETS = (int)dr["REMANING_TICKETS"];
                flight.TOTAL_TICKETS = (int)dr["TOTAL_TICKETS"];
                flights.Add(flight);
            }
            return flights;
        }
        public IList<FlightView> GetFlightsByLandingDate(DateTime landingDate)
        {
            IList<FlightView> flights = new List<FlightView>();
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM Flights WHERE LANDING_TIME = '{landingDate.ToString("yyyy-MM-dd HH:mm:ss")}'");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Flights");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                FlightView flight = new FlightView();
                flight.ID = (long)dr["ID"];
                flight.AIRLINECOMPANY_ID = (long)dr["AIRLINECOMPANY_ID"];
                flight.ORIGIN_COUNTRY_CODE = (long)dr["ORIGIN_COUNTRY_CODE"];
                flight.DESTINATION_COUNTRY_CODE = (long)dr["DESTINATION_COUNTRY_CODE"];
                flight.DEPARTURE_TIME = Convert.ToDateTime(dr["DEPARTURE_TIME"]);
                flight.LANDING_TIME = Convert.ToDateTime(dr["LANDING_TIME"]);
                flight.REMANING_TICKETS = (int)dr["REMANING_TICKETS"];
                flight.TOTAL_TICKETS = (int)dr["TOTAL_TICKETS"];
                flights.Add(flight);
            }
            return flights;
        }
        public IList<FlightView> GetFlightsByOriginCountry(long countryCode)
        {
            IList<FlightView> flights = new List<FlightView>();
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM Flights WHERE ORIGIN_COUNTRY_CODE = {countryCode}");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Flights");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                FlightView flight = new FlightView();
                flight.ID = (long)dr["ID"];
                flight.AIRLINECOMPANY_ID = (long)dr["AIRLINECOMPANY_ID"];
                flight.ORIGIN_COUNTRY_CODE = (long)dr["ORIGIN_COUNTRY_CODE"];
                flight.DESTINATION_COUNTRY_CODE = (long)dr["DESTINATION_COUNTRY_CODE"];
                flight.DEPARTURE_TIME = (DateTime)dr["DEPARTURE_TIME"];
                flight.LANDING_TIME = (DateTime)dr["LANDING_TIME"];
                flight.REMANING_TICKETS = (int)dr["REMANING_TICKETS"];
                flight.TOTAL_TICKETS = (int)dr["TOTAL_TICKETS"];
                flights.Add(flight);
            }
            return flights;
        }
        public void Remove(FlightView t)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"DELETE FROM Flights WHERE ID = {t.ID}");
            string SQL = sb.ToString();
            string res = DL.ExecuteSqlNonQuery(SQL);
            if (res == "")
            {
                throw new FlightDeleteErrorException("Flight delete error");
            }
        }
        public void RemoveFlightsByAirlineCompanyId(long  airlineCompamyId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"DELETE FROM Flights WHERE AIRLINECOMPANY_ID IN (SELECT ID FROM AirlineCompanies WHERE ID = {airlineCompamyId})");
            string SQL = sb.ToString();
            string res = DL.ExecuteSqlNonQuery(SQL);
            if (res == "")
            {
                throw new FlightDeleteErrorException("Flight delete error");
            }
        }
        public void RemoveFlightsByCountryCode(long countryCode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"DELETE FROM Flights WHERE ID IN (SELECT ID FROM AirlineCompanies WHERE COUNTRY_CODE = {countryCode})");
            string SQL = sb.ToString();
            string res = DL.ExecuteSqlNonQuery(SQL);
            if (res == "")
            {
                throw new FlightDeleteErrorException("Flight delete error");
            }
        }
        public void Update(FlightView t)
        {
          
            StringBuilder sb = new StringBuilder();
            sb.Append($"UPDATE Flights SET AIRLINECOMPANY_ID = (SELECT ID FROM AirlineCompanies WHERE AIRLINE_NAME = '{t.AIRLINE_NAME}'),");
            sb.Append($" ORIGIN_COUNTRY_CODE = (SELECT ID FROM Countries WHERE COUNTRY_NAME = '{t.O_COUNTRY_NAME}'),");
            sb.Append($" DESTINATION_COUNTRY_CODE = (SELECT ID FROM Countries WHERE COUNTRY_NAME = '{t.D_COUNTRY_NAME}'),");
            sb.Append($" LANDING_TIME = '{t.REAL_LANDING_TIME.ToString("yyyy-MM-dd HH:mm:ss")}',");
            sb.Append($" DEPARTURE_TIME = '{t.REAL_DEPARTURE_TIME.ToString("yyyy-MM-dd HH:mm:ss")}',");
            sb.Append($" REAL_LANDING_TIME = '{t.REAL_LANDING_TIME.ToString("yyyy-MM-dd HH:mm:ss")}',");
            sb.Append($" REAL_DEPARTURE_TIME = '{t.REAL_DEPARTURE_TIME.ToString("yyyy-MM-dd HH:mm:ss")}',");
            sb.Append($" REMANING_TICKETS = {t.REMANING_TICKETS}, TOTAL_TICKETS = {t.TOTAL_TICKETS}, FLIGHT_NUMBER = '{t.FLIGHT_NUMBER}'");
            sb.Append($" WHERE ID = {t.ID}");
            string SQL = sb.ToString();
            string res = DL.ExecuteSqlNonQuery(SQL);
            if (res == "")
            {
                throw new FlightUpdateErrorException("Flight update error:" + DL.ErrorMessage);
            }
        }
        public void UpdateRemainingTickets(long flightId)
        {
            string SQL = $"UPDATE Flights SET REMANING_TICKETS = TOTAL_TICKETS-(SELECT COUNT(*) FROM Tickets WHERE FLIGHT_ID = {flightId}) WHERE ID = {flightId}";
            string res = DL.ExecuteSqlNonQuery(SQL);
            if (res == "")
            {
                throw new FlightUpdateErrorException("Flight update error:" + DL.ErrorMessage);
            }
        }
        public void UpdateRealDepartureTime(long flightID, DateTime departureDateTime)
        {
            string SQL = $"UPDATE Flights SET REAL_DEPARTURE_TIME = '{departureDateTime.ToString("yyyy-MM-dd HH:mm:ss")}' WHERE ID = {flightID}";
            string res = DL.ExecuteSqlNonQuery(SQL);
            if (res == "")
            {
                throw new FlightUpdateErrorException("Flight update error:" + DL.ErrorMessage);
            }
        }
        public void UpdateRealArrivalTime(long flightID, DateTime arrivalDateTime)
        {
            string SQL = $"UPDATE Flights SET REAL_LANDING_TIME = '{arrivalDateTime.ToString("yyyy-MM-dd HH:mm:ss")}' WHERE ID = {flightID}";
            string res = DL.ExecuteSqlNonQuery(SQL);
            if (res == "")
            {
                throw new FlightUpdateErrorException("Flight update error:" + DL.ErrorMessage);
            }
        }
        public int GetReminingTickets(long flightId)
        {
            Flight flight = null;
            string SQL = $"SELECT REMANING_TICKETS FROM Flights WHERE ID = {flightId}";
            DataSet DS = DL.GetSqlQueryDS(SQL, "Flights");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                flight = new Flight();
                flight.REMANING_TICKETS = (int)dr["REMANING_TICKETS"];  
            }
            return flight.REMANING_TICKETS;
        }
        public IList<FlightView> GetFilteredFlights(string typeName, string flightId, string country, string company)
        {
            string str1 = "", str2 ="", str3="";
            IList<FlightView> flights = new List<FlightView>();
            StringBuilder sb = new StringBuilder();
            sb.Append($"DECLARE @typeName varchar(50) = '{typeName}';");
            if (flightId == null || flightId == "")
            {
                str1 = $" AND AIRLINE_NAME LIKE '{company}%' AND c.COUNTRY_NAME LIKE '{country}%' END";
                str2 = $" AND AIRLINE_NAME LIKE '{company}%' AND c.COUNTRY_NAME LIKE '{country}%') as ss END";
                str3 = $" AND AIRLINE_NAME LIKE '{company}%' AND c.COUNTRY_NAME LIKE '{country}%'";
            }
            else
            {
                str1 = $" AND AIRLINE_NAME LIKE '{company}%' AND c.COUNTRY_NAME LIKE '{country}%' AND f.ID = '{flightId}' END";
                str2 = $" AND AIRLINE_NAME LIKE '{company}%' AND c.COUNTRY_NAME LIKE '{country}%' AND f.ID = '{flightId}') as ss END";
                str3 = $" AND AIRLINE_NAME LIKE '{company}%' AND c.COUNTRY_NAME LIKE '{country}%' AND f.ID = '{flightId}'";
            }
            sb.Append($" IF (@typeName ='Arrivals') BEGIN");
            sb.Append($" SELECT f.ID, a.AIRLINE_NAME, c.COUNTRY_NAME as 'Coming from', co.COUNTRY_NAME as Destination, f.DEPARTURE_TIME as 'Departure time', REAL_DEPARTURE_TIME, f.LANDING_TIME as 'Landing time', REAL_LANDING_TIME");
            sb.Append($" FROM Flights as f");
            sb.Append($" INNER JOIN AirlineCompanies as a on f.AIRLINECOMPANY_ID = a.ID");
            sb.Append($" INNER JOIN Countries as c on f.ORIGIN_COUNTRY_CODE = c.ID");
            sb.Append($" INNER JOIN Countries as co on f.DESTINATION_COUNTRY_CODE = co.ID");
            sb.Append($" WHERE (REAL_LANDING_TIME BETWEEN GETDATE() AND DATEADD(hour, 12, GETDATE()) OR (REAL_LANDING_TIME BETWEEN DATEADD(hour, -4, GETDATE()) AND GETDATE()))");
            sb.Append(str1);
            sb.Append($" ELSE IF (@typeName ='Departures') BEGIN");
            sb.Append($" SELECT f.ID, a.AIRLINE_NAME, c.COUNTRY_NAME as 'Coming from', co.COUNTRY_NAME as Destination, f.DEPARTURE_TIME as 'Departure time', REAL_DEPARTURE_TIME, f.LANDING_TIME as 'Landing time', REAL_LANDING_TIME");
            sb.Append($" FROM Flights as f");
            sb.Append($" INNER JOIN AirlineCompanies as a on f.AIRLINECOMPANY_ID = a.ID");
            sb.Append($" INNER JOIN Countries as c on f.ORIGIN_COUNTRY_CODE = c.ID");
            sb.Append($" INNER JOIN Countries as co on f.DESTINATION_COUNTRY_CODE = co.ID");
            sb.Append($" WHERE (REAL_DEPARTURE_TIME BETWEEN GETDATE() AND DATEADD(hour, 12, GETDATE()))");
            sb.Append(str1);
            sb.Append($" ELSE IF (@typeName ='') BEGIN");
            sb.Append($" select distinct ID, AIRLINE_NAME, ComingFrom as 'Coming from', Destination, DepartureTime as 'Departure time', REAL_DEPARTURE_TIME, LANDING_TIME as 'Landing time', REAL_LANDING_TIME");
            sb.Append($" FROM (SELECT f.ID, a.AIRLINE_NAME, c.COUNTRY_NAME as ComingFrom, co.COUNTRY_NAME as Destination, f.DEPARTURE_TIME as DepartureTime, REAL_DEPARTURE_TIME, f.LANDING_TIME, REAL_LANDING_TIME");
            sb.Append($" FROM Flights as f");
            sb.Append($" INNER JOIN AirlineCompanies as a on f.AIRLINECOMPANY_ID = a.ID INNER JOIN Countries as c on f.ORIGIN_COUNTRY_CODE = c.ID");
            sb.Append($" INNER JOIN Countries as co on f.DESTINATION_COUNTRY_CODE = co.ID");
            sb.Append($" WHERE (REAL_LANDING_TIME BETWEEN GETDATE() AND DATEADD(hour, 12, GETDATE()) OR (REAL_LANDING_TIME BETWEEN DATEADD(hour, -4, GETDATE()) AND GETDATE()))");
            sb.Append(str3);
            sb.Append($" UNION");
            sb.Append($" SELECT f.ID, a.AIRLINE_NAME, c.COUNTRY_NAME as ComingFrom, co.COUNTRY_NAME as Destination, f.DEPARTURE_TIME as DepartureTime, REAL_DEPARTURE_TIME, f.LANDING_TIME, REAL_LANDING_TIME");
            sb.Append($" FROM Flights as f");
            sb.Append($" INNER JOIN AirlineCompanies as a on f.AIRLINECOMPANY_ID = a.ID INNER JOIN Countries as c on f.ORIGIN_COUNTRY_CODE = c.ID");
            sb.Append($" INNER JOIN Countries as co on f.DESTINATION_COUNTRY_CODE = co.ID WHERE(REAL_DEPARTURE_TIME BETWEEN GETDATE() AND DATEADD(hour, 12, GETDATE()))");
            sb.Append(str2);

            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Flights");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                FlightView flight = new FlightView();
                flight.ID = (long)dr["ID"];
                flight.AIRLINE_NAME = (string)dr["AIRLINE_NAME"];
                flight.O_COUNTRY_NAME = (string)dr["Coming from"];
                flight.D_COUNTRY_NAME = (string)dr["Destination"];
                flight.DEPARTURE_TIME = (DateTime)dr["Departure time"];
                flight.REAL_DEPARTURE_TIME = (DateTime)dr["REAL_DEPARTURE_TIME"];
                flight.LANDING_TIME = (DateTime)dr["Landing time"];
                flight.REAL_LANDING_TIME = (DateTime)dr["REAL_LANDING_TIME"];
                flights.Add(flight);
            }
            return flights;
        }
        public FlightView GetFlightById(long flightId)
        {
            FlightView flight = null;
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT f.ID, a.AIRLINE_NAME, c.COUNTRY_NAME as 'Coming from', co.COUNTRY_NAME as Destination, f.DEPARTURE_TIME as 'Departure time', f.LANDING_TIME as 'Landing time'");
            sb.Append($" FROM Flights as f");
            sb.Append($" INNER JOIN AirlineCompanies as a on f.AIRLINECOMPANY_ID = a.ID");
            sb.Append($" INNER JOIN Countries as c on f.ORIGIN_COUNTRY_CODE = c.ID");
            sb.Append($" INNER JOIN Countries as co on f.DESTINATION_COUNTRY_CODE = co.ID");
            sb.Append($" WHERE f.ID = {flightId}");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Flights");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                flight = new FlightView();
                flight.ID = (long)dr["ID"];
                flight.AIRLINE_NAME = (string)dr["AIRLINE_NAME"];
                flight.O_COUNTRY_NAME = (string)dr["Coming from"];
                flight.D_COUNTRY_NAME = (string)dr["Destination"];
                flight.DEPARTURE_TIME = (DateTime)dr["Departure time"];
                flight.LANDING_TIME = (DateTime)dr["Landing time"];
            }
            if (flight != null)
            {
                return flight;
            }
            return null;
        }
        public IList<long> GetFlightIds()
        {
            IList<long> flightsIds = new List<long>();
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT ID FROM Flights");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Flights");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                Flight flight = new Flight();
                flight.ID = (long)dr["ID"];
                flightsIds.Add(flight.ID);
            }
            return flightsIds;
        }
        public IList<long> GetFlightIdsByScheduledTime(string typeName)
        {
            IList<long> flightsIds = new List<long>();
            StringBuilder sb = new StringBuilder();
            //string str1 = "";

            if(typeName == "Arrivals")
            {
                sb.Append($" SELECT f.ID FROM Flights as f");
                sb.Append($" WHERE f.ID IN (SELECT f.ID FROM Flights as f");
                sb.Append($" INNER JOIN AirlineCompanies as a on f.AIRLINECOMPANY_ID = a.ID");
                sb.Append($" INNER JOIN Countries as c on f.ORIGIN_COUNTRY_CODE = c.ID");
                sb.Append($" INNER JOIN Countries as co on f.DESTINATION_COUNTRY_CODE = co.ID");
                sb.Append($" WHERE(REAL_LANDING_TIME BETWEEN GETDATE() AND DATEADD(hour, 12, GETDATE()) OR(REAL_LANDING_TIME BETWEEN DATEADD(hour, -4, GETDATE()) AND GETDATE())))");
                //str1 = $" WHERE(REAL_LANDING_TIME BETWEEN GETDATE() AND DATEADD(hour, 12, GETDATE()) OR (REAL_LANDING_TIME BETWEEN DATEADD(hour, -4, GETDATE()) AND GETDATE())))";
            }
            else if (typeName == "Departures")
            {
                sb.Append($" SELECT f.ID FROM Flights as f");
                sb.Append($" WHERE f.ID IN (SELECT f.ID FROM Flights as f");
                sb.Append($" INNER JOIN AirlineCompanies as a on f.AIRLINECOMPANY_ID = a.ID");
                sb.Append($" INNER JOIN Countries as c on f.ORIGIN_COUNTRY_CODE = c.ID");
                sb.Append($" INNER JOIN Countries as co on f.DESTINATION_COUNTRY_CODE = co.ID");
                sb.Append($" WHERE (REAL_DEPARTURE_TIME BETWEEN GETDATE() AND DATEADD(hour, 12, GETDATE())))");
                //str1 = $" WHERE (REAL_DEPARTURE_TIME BETWEEN GETDATE() AND DATEADD(hour, 12, GETDATE())))";
            }
            else if (typeName == "" || typeName == null)
            {
                sb.Append($" select distinct ID");
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
                //str1 = $" WHERE(REAL_DEPARTURE_TIME BETWEEN GETDATE() AND DATEADD(hour, 12, GETDATE())) AND (REAL_LANDING_TIME BETWEEN GETDATE() AND DATEADD(hour, 12, GETDATE()) OR (LANDING_TIME BETWEEN DATEADD(hour, -4, GETDATE()) AND GETDATE())))";
            }
            //sb.Append($" SELECT f.ID FROM Flights as f");
            //sb.Append($" WHERE f.ID IN (SELECT f.ID FROM Flights as f");
            //sb.Append($" INNER JOIN AirlineCompanies as a on f.AIRLINECOMPANY_ID = a.ID");
            //sb.Append($" INNER JOIN Countries as c on f.ORIGIN_COUNTRY_CODE = c.ID");
            //sb.Append($" INNER JOIN Countries as co on f.DESTINATION_COUNTRY_CODE = co.ID");
            //sb.Append(str1);

            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Flights");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                Flight flight = new Flight();
                flight.ID = (long)dr["ID"];
                flightsIds.Add(flight.ID);
            }
            return flightsIds;
        }
        public IList<Flight> GetFlightsToFillCalendar(string o_countryName, string d_countryName, string  companyName, string destinationDate, int monthsToAdd, int hoursToAdd)
        {
            IList<Flight> flights = new List<Flight>();
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT distinct CONVERT(DATE, REAL_DEPARTURE_TIME) as realDepartureDate");
            sb.Append($" FROM Flights as f");
            sb.Append($" INNER JOIN AirlineCompanies as a on f.AIRLINECOMPANY_ID = a.ID");
            sb.Append($" INNER JOIN Countries as c on f.ORIGIN_COUNTRY_CODE = c.ID");
            sb.Append($" INNER JOIN Countries as co on f.DESTINATION_COUNTRY_CODE = co.ID");
            sb.Append($" WHERE MONTH(REAL_DEPARTURE_TIME) BETWEEN(SELECT MONTH(GETDATE())) AND (SELECT MONTH(DATEADD(month, {monthsToAdd}, '{destinationDate}')))");
            sb.Append($" AND REAL_DEPARTURE_TIME > (SELECT DATEADD(hour, {hoursToAdd}, GETDATE())) AND a.AIRLINE_NAME = '{companyName}' AND co.COUNTRY_NAME = '{d_countryName}' AND c.COUNTRY_NAME = '{o_countryName}'");
            string SQL = sb.ToString();
            
            AddToLogFile("SQL: " + SQL );
            DataSet DS = DL.GetSqlQueryDS(SQL, "Flights");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                FlightView flight = new FlightView();
                AddToLogFile("dr[realDepartureDate]: " + dr["realDepartureDate"].ToString());
                flight.REAL_DEPARTURE_TIME = Convert.ToDateTime(dr["realDepartureDate"]);
      
                flights.Add(flight);
            }
            return flights;
        }
        private void AddToLogFile(string str)
        {
            //DateTime dt = DateTime.Now;
            //string ll = dt.Day.ToString() + dt.Month.ToString() + dt.Year.ToString();
            //string path = @"C:\Projects\AirlineManagement\";
            //path = path + "Log" + ll + ".txt";
            //TextWriter writer = new StreamWriter(path, true);
            //writer.WriteLine(str);
            //writer.Close();
        }
    }
}
