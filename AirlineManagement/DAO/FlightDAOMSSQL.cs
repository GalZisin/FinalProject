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
    public class FlightDAOMSSQL : IFlightDAO
    {
        private SqlDAO DL;  // A central class of database connections
        public FlightDAOMSSQL()
        {
            DL = new SqlDAO(FlightCenterConfig.strConn);
        }
        public string CheckIfFlightExist(Flight t)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT COUNT(*) FROM Flights WHERE ID = {t.ID } AND AIRLINECOMPANY_ID = {t.AIRLINECOMPANY_ID}");
            string SQL1 = sb.ToString();
            string res = DL.ExecuteSqlScalarStatement(SQL1);
            return res;
        }
        public long Add(Flight t)
        {
            StringBuilder sb = new StringBuilder();

            sb = new StringBuilder();
            sb.Append($"INSERT INTO Flights(AIRLINECOMPANY_ID, ORIGIN_COUNTRY_CODE, DESTINATION_COUNTRY_CODE, DEPARTURE_TIME,REAL_DEPARTURE_TIME, LANDING_TIME, REAL_LANDING_TIME, REMANING_TICKETS, TOTAL_TICKETS)");
            sb.Append($" values({ t.AIRLINECOMPANY_ID}, { t.ORIGIN_COUNTRY_CODE}, { t.DESTINATION_COUNTRY_CODE}, '{ t.DEPARTURE_TIME.ToString("yyyy-MM-dd HH:mm:ss")}', '{ t.DEPARTURE_TIME.ToString("yyyy-MM-dd HH:mm:ss")}', '{t.LANDING_TIME.ToString("yyyy-MM-dd HH:mm:ss")}', '{t.LANDING_TIME.ToString("yyyy-MM-dd HH:mm:ss")}', {t.REMANING_TICKETS}, {t.TOTAL_TICKETS})");
            string SQL2 = sb.ToString();
            DL.ExecuteSqlNonQuery(SQL2);
            sb = new StringBuilder();
            sb.Append($"SELECT ID FROM Flights WHERE AIRLINECOMPANY_ID = {t.AIRLINECOMPANY_ID}");
            sb.Append($" AND ORIGIN_COUNTRY_CODE = {t.ORIGIN_COUNTRY_CODE}");
            sb.Append($" AND DESTINATION_COUNTRY_CODE = {t.DESTINATION_COUNTRY_CODE}");
            sb.Append($" AND CONVERT(char(16),DEPARTURE_TIME,120) = '{t.DEPARTURE_TIME.ToString("yyyy-MM-dd HH:mm")}'");
            sb.Append($" AND CONVERT(char(16),LANDING_TIME,120) = '{t.LANDING_TIME.ToString("yyyy-MM-dd HH:mm")}'");
            sb.Append($" AND REMANING_TICKETS = {t.REMANING_TICKETS}");
            sb.Append($" AND TOTAL_TICKETS = {t.TOTAL_TICKETS}");
            string SQL3 = sb.ToString();
            return Int64.Parse(DL.ExecuteSqlScalarStatement(SQL3));
        }
        public Flight Get(long flightId)
        {
            Flight flight = null;
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM Flights WHERE ID = {flightId}");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Flights");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                flight = new Flight();
                flight.ID = Convert.ToInt64(dr["ID"]);
                flight.AIRLINECOMPANY_ID = (long)dr["AIRLINECOMPANY_ID"];
                flight.ORIGIN_COUNTRY_CODE = (long)dr["ORIGIN_COUNTRY_CODE"];
                flight.DESTINATION_COUNTRY_CODE = (long)dr["DESTINATION_COUNTRY_CODE"];
                flight.DEPARTURE_TIME = Convert.ToDateTime(dr["DEPARTURE_TIME"].ToString());
                flight.LANDING_TIME = Convert.ToDateTime(dr["LANDING_TIME"].ToString());
                flight.REMANING_TICKETS = (int)dr["REMANING_TICKETS"];
                flight.TOTAL_TICKETS = Convert.ToInt32(dr["TOTAL_TICKETS"]);
            }
            if (flight != null)
            {
                return flight;
            }
            return null;
        }
        public IList<Flight> GetAll()
        {
            IList<Flight> flights = new List<Flight>();
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM Flights");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Flights");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                Flight flight = new Flight();
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
        public IList<Flight> GetFlightsByCustomerId(long CustomerId)
        {
            IList<Flight> flights = new List<Flight>();
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM Flights WHERE ID IN (SELECT FLIGHT_ID FROM Tickets WHERE CUSTOMER_ID = {CustomerId})");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Flights");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                Flight flight = new Flight();
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
        public IList<Flight> GetFlightsByCustomerUserName(string customerUserName)
        {
            IList<Flight> flights = new List<Flight>();
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM Flights WHERE ID IN (SELECT FLIGHT_ID FROM Tickets WHERE CUSTOMER_ID = (SELECT ID FROM Customers WHERE USER_NAME = '{customerUserName}'))");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Flights");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                Flight flight = new Flight();
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
        public Dictionary<Flight, int> GetAllFlightsByVacancy()
        {
            Dictionary<Flight, int> vacancyByFlight = new Dictionary<Flight, int>();
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM Flights");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Flights");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                Flight flight = new Flight();
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
        public IList<Flight> GetAllFlightsByVacancyAndScheduledTime()
        {
            IList<Flight> flights = new List<Flight>();
            StringBuilder sb = new StringBuilder();
            //sb.Append($"SELECT * FROM Flights");
            sb.Append($"SELECT f.ID, a.AIRLINE_NAME, c.COUNTRY_NAME as 'Coming from', co.COUNTRY_NAME as Destination, f.DEPARTURE_TIME as 'Departure time', REAL_DEPARTURE_TIME, f.LANDING_TIME as 'Landing time', REAL_LANDING_TIME, REMANING_TICKETS, TOTAL_TICKETS");
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
                Flight flight = new Flight();
                flight.ID = (long)dr["ID"];
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
        public IList<Flight> GetFlightsByAirlineCompanyId(long airlineCompanyId)
        {
            IList<Flight> flights = new List<Flight>();
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM Flights WHERE AIRLINECOMPANY_ID = {airlineCompanyId}");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Flights");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                Flight flight = new Flight();
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
        public IList<Flight> GetFlightsByDepartureDate(DateTime departureDate)
        {
            IList<Flight> flights = new List<Flight>();
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM Flights WHERE DEPARTURE_TIME = '{departureDate.ToString("yyyy-MM-dd HH:mm:ss")}'");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Flights");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                Flight flight = new Flight();
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
        public IList<Flight> GetFlightsByDestinationCountry(long countryCode)
        {
            IList<Flight> flights = new List<Flight>();
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM Flights WHERE DESTINATION_COUNTRY_CODE = {countryCode}");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Flights");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                Flight flight = new Flight();
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
        public IList<Flight> GetFlightsByLandingDate(DateTime landingDate)
        {
            IList<Flight> flights = new List<Flight>();
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM Flights WHERE LANDING_TIME = '{landingDate.ToString("yyyy-MM-dd HH:mm:ss")}'");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Flights");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                Flight flight = new Flight();
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
        public IList<Flight> GetFlightsByOriginCountry(long countryCode)
        {
            IList<Flight> flights = new List<Flight>();
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM Flights WHERE ORIGIN_COUNTRY_CODE = {countryCode}");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Flights");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                Flight flight = new Flight();
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
        public void Remove(Flight t)
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
        public void Update(Flight t)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"UPDATE Flights SET AIRLINECOMPANY_ID = {t.AIRLINECOMPANY_ID}, ORIGIN_COUNTRY_CODE = {t.ORIGIN_COUNTRY_CODE},");
            sb.Append($" DESTINATION_COUNTRY_CODE = {t.DESTINATION_COUNTRY_CODE}, LANDING_TIME = '{t.LANDING_TIME.ToString("yyyy-MM-dd HH:mm:ss")}', DEPARTURE_TIME = '{t.DEPARTURE_TIME.ToString("yyyy-MM-dd HH:mm:ss")}', REMANING_TICKETS = {t.REMANING_TICKETS}");
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
        public IList<Flight> GetFilteredFlights(string typeName, string flightId, string country, string company)
        {
            string str1 = "", str2 ="", str3="";
            IList<Flight> flights = new List<Flight>();
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
                Flight flight = new Flight();
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
        public Flight GetFlightById(long flightId)
        {
            Flight flight = null;
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
                flight = new Flight();
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
            string str1 = "";
            if(typeName != "" || typeName != null)
            {
                if (typeName == "Arrivals")
                {

                    str1 = $" WHERE(REAL_LANDING_TIME BETWEEN GETDATE() AND DATEADD(hour, 12, GETDATE()) OR (REAL_LANDING_TIME BETWEEN DATEADD(hour, -4, GETDATE()) AND GETDATE())))";
                }
                else if (typeName == "Departures")
                {
                    str1 = $" WHERE (REAL_DEPARTURE_TIME BETWEEN GETDATE() AND DATEADD(hour, 12, GETDATE())))";
                }
                sb.Append($" SELECT f.ID FROM Flights as f");
                sb.Append($" WHERE f.ID IN (SELECT f.ID FROM Flights as f");
                sb.Append($" INNER JOIN AirlineCompanies as a on f.AIRLINECOMPANY_ID = a.ID");
                sb.Append($" INNER JOIN Countries as c on f.ORIGIN_COUNTRY_CODE = c.ID");
                sb.Append($" INNER JOIN Countries as co on f.DESTINATION_COUNTRY_CODE = co.ID");
                sb.Append(str1);
            }
            else
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
                sb.Append($" INNER JOIN Countries as co on f.DESTINATION_COUNTRY_CODE = co.ID WHERE (REAL_DEPARTURE_TIME BETWEEN GETDATE() AND DATEADD (hour, 12, GETDATE())) as ss");
            }
            //else if (typeName == "" || typeName == null)
            //{
            //    str1 = $" WHERE(REAL_DEPARTURE_TIME BETWEEN GETDATE() AND DATEADD(hour, 12, GETDATE())) AND (REAL_LANDING_TIME BETWEEN GETDATE() AND DATEADD(hour, 12, GETDATE()) OR (LANDING_TIME BETWEEN DATEADD(hour, -4, GETDATE()) AND GETDATE())))";
            //}
         
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
    }
}
