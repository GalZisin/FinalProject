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
    public class TicketDAOMSSQL : ITicketDAO
    {
        private SqlDAO DL;  // A central class of database connections
        public TicketDAOMSSQL()
        {
            DL = new SqlDAO(FlightCenterConfig.strConn);
        }
        public string CheckIfTicketExist(Ticket t)
        {
            StringBuilder sb = new StringBuilder();
            string SQL1 = $"SELECT COUNT(*) FROM Tickets WHERE FLIGHT_ID = {t.FLIGHT_ID} AND CUSTOMER_ID = {t.CUSTOMER_ID} AND FIRST_NAME = '{t.FIRST_NAME}' AND LAST_NAME = '{t.LAST_NAME}'";
            string res = DL.ExecuteSqlScalarStatement(SQL1);
            return res;
        }
        public long Add(Ticket t)
        {
            StringBuilder sb = new StringBuilder();
            sb = new StringBuilder();
            sb.Append($"INSERT INTO Tickets(FLIGHT_ID, CUSTOMER_ID, FIRST_NAME, LAST_NAME)");
            sb.Append($" values({ t.FLIGHT_ID}, { t.CUSTOMER_ID}, '{t.FIRST_NAME}', '{t.LAST_NAME}')");
            string SQL = sb.ToString();
            DL.ExecuteSqlNonQuery(SQL);
            SQL = $"SELECT ID FROM Tickets WHERE FLIGHT_ID = {t.FLIGHT_ID} AND CUSTOMER_ID = {t.CUSTOMER_ID} AND FIRST_NAME = '{t.FIRST_NAME}' AND LAST_NAME = '{t.LAST_NAME}'";
            return Int64.Parse(DL.ExecuteSqlScalarStatement(SQL));
        }
        public Ticket Get(long id)
        {
            TicketView t = null;
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM Tickets WHERE ID = {id}");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Tickets");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                t = new TicketView();
                t.ID = (long)dr["ID"];
                t.FLIGHT_ID = (long)dr["FLIGHT_ID"];
                t.CUSTOMER_ID = (long)dr["CUSTOMER_ID"];
            }
            if (t != null)
            {
                return t;
            }
            return null;
        }
        public TicketView GetTicketByTicketId(long ticketId)
        {
            TicketView t = null;
            StringBuilder sb = new StringBuilder();
            //sb.Append($"SELECT * FROM Tickets WHERE ID = {id}");
            sb.Append($"SELECT t.FIRST_NAME, t.LAST_NAME, (SELECT AIRLINE_NAME FROM AirlineCompanies WHERE ID = f.AIRLINECOMPANY_ID) as airlineName,");
            sb.Append($" (SELECT COUNTRY_NAME FROM Countries WHERE ID = f.ORIGIN_COUNTRY_CODE) as originCountryName,");
            sb.Append($" (SELECT COUNTRY_NAME FROM Countries WHERE ID = f.DESTINATION_COUNTRY_CODE) as destinationCountryName,");
            sb.Append($" f.DEPARTURE_TIME, f.LANDING_TIME, t.ID as ticketId, f.FLIGHT_NUMBER, t.CUSTOMER_ID, f.ID as flightId");
            sb.Append($" FROM Tickets t");
            sb.Append($" INNER JOIN Flights f ON f.ID = t.FLIGHT_ID");
            sb.Append($" WHERE t.ID = {ticketId}");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Tickets");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                t = new TicketView();
                //t.ID = (long)dr["ID"];
                //t.FLIGHT_ID = (long)dr["FLIGHT_ID"];
                //t.CUSTOMER_ID = (long)dr["CUSTOMER_ID"];
                t.ID = (long)dr["ticketId"];
                t.FLIGHT_ID = (long)dr["flightId"];
                t.FIRST_NAME = (string)dr["FIRST_NAME"];
                t.LAST_NAME = (string)dr["LAST_NAME"];
                t.AIRLINE_NAME = (string)dr["airlineName"];
                t.O_COUNTRY_NAME = (string)dr["originCountryName"];
                t.D_COUNTRY_NAME = (string)dr["destinationCountryName"];
                t.DEPARTURE_TIME = (DateTime)dr["DEPARTURE_TIME"];
                t.LANDING_TIME = (DateTime)dr["LANDING_TIME"];
                t.FLIGHT_NUMBER = (string)dr["FLIGHT_NUMBER"];
                t.CUSTOMER_ID = (long)dr["CUSTOMER_ID"];
            }
            if (t != null)
            {
                return t;
            }
            return null;
        }
        public Ticket GetTicketByCustomerId(long customerId)
        {
            Ticket t = null;
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM Tickets WHERE CUSTOMER_ID = {customerId}");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Tickets");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                t = new Ticket();
                t.ID = (long)dr["ID"];
                t.FLIGHT_ID = (long)dr["FLIGHT_ID"];
                t.CUSTOMER_ID = (long)dr["CUSTOMER_ID"];
            }
            if (t != null)
            {
                return t;
            }
            return null;
        }
        public IList<TicketView> GetAllTicketsByCustomerId(long customerId)
        {
            IList<TicketView> ticketView = new List<TicketView>();
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT t.FIRST_NAME, t.LAST_NAME, (SELECT AIRLINE_NAME FROM AirlineCompanies WHERE ID = f.AIRLINECOMPANY_ID) as airlineName,");
            sb.Append($" (SELECT COUNTRY_NAME FROM Countries WHERE ID = f.ORIGIN_COUNTRY_CODE) as originCountryName,");
            sb.Append($" (SELECT COUNTRY_NAME FROM Countries WHERE ID = f.DESTINATION_COUNTRY_CODE) as destinationCountryName,");
            sb.Append($" f.DEPARTURE_TIME, f.LANDING_TIME, t.ID as ticketId, f.FLIGHT_NUMBER, t.CUSTOMER_ID, f.ID as flightId");
            sb.Append($" FROM Tickets t");
            sb.Append($" INNER JOIN Flights f ON f.ID = t.FLIGHT_ID");
            sb.Append($" WHERE t.CUSTOMER_ID = {customerId}");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Tickets");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                TicketView t = new TicketView();
                t.ID = (long)dr["ticketId"];
                t.FLIGHT_ID = (long)dr["flightId"];
                t.FIRST_NAME = (string)dr["FIRST_NAME"];
                t.LAST_NAME = (string)dr["LAST_NAME"];
                t.AIRLINE_NAME = (string)dr["airlineName"];
                t.O_COUNTRY_NAME = (string)dr["originCountryName"];
                t.D_COUNTRY_NAME = (string)dr["destinationCountryName"];
                t.DEPARTURE_TIME = (DateTime)dr["DEPARTURE_TIME"];
                t.LANDING_TIME = (DateTime)dr["LANDING_TIME"];
                t.FLIGHT_NUMBER = (string)dr["FLIGHT_NUMBER"];
                t.CUSTOMER_ID = (long)dr["CUSTOMER_ID"];
                ticketView.Add(t);
            }
            if (ticketView == null)
            {
                return null;
            }
            return ticketView;
        }
        public Ticket GetTicketByCustomerUserName(string customerUserName)
        {
            Ticket t = null;
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM Tickets WHERE CUSTOMER_ID IN (SELECT ID FROM Customers WHERE USER_NAME = '{customerUserName}')");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Tickets");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                t = new Ticket();
                t.ID = (long)dr["ID"];
                t.FLIGHT_ID = (long)dr["FLIGHT_ID"];
                t.CUSTOMER_ID = (long)dr["CUSTOMER_ID"];
            }
            if (t != null)
            {
                return t;
            }
            return null;
        }
        public IList<Ticket> GetAll()
        {
            IList<Ticket> tickets = new List<Ticket>();
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM Tickets");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Tickets");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                Ticket t = new Ticket();
                t.ID = (long)dr["ID"];
                t.FLIGHT_ID = (long)dr["FLIGHT_ID"];
                t.CUSTOMER_ID = (long)dr["CUSTOMER_ID"];
                tickets.Add(t);
            }
            return tickets;
        }
        public IList<Ticket> GetTicketsByAirlineCompanyId(long airlineCompanyId)
        {
            IList<Ticket> tickets = new List<Ticket>();
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM Tickets WHERE FLIGHT_ID IN (SELECT ID FROM Flights WHERE AIRLINECOMPANY_ID = {airlineCompanyId})");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Tickets");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                Ticket t = new Ticket();
                t.ID = (long)dr["ID"];
                t.FLIGHT_ID = (long)dr["FLIGHT_ID"];
                t.CUSTOMER_ID = (long)dr["CUSTOMER_ID"];
                tickets.Add(t);
            }
            return tickets;
        }
        public void Remove(Ticket t)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"DELETE FROM Tickets WHERE ID = {t.ID}");
            string SQL = sb.ToString();
            string res = DL.ExecuteSqlNonQuery(SQL);
            if (res == "")
            {
                throw new TicketDeleteErrorException("Tickets delete error");
            }
        }
        public void RemoveTicketsByCustomerId(long customerId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"DELETE FROM Tickets WHERE CUSTOMER_ID IN (SELECT ID FROM Customers WHERE ID = {customerId});");
            string SQL = sb.ToString();
            string res = DL.ExecuteSqlNonQuery(SQL);
            if (res == "")
            {
                throw new TicketDeleteErrorException("Tickets delete error");
            }
        }
        public void RemoveTicketsByFlightId(long flightId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"DELETE FROM Tickets WHERE FLIGHT_ID IN (SELECT ID FROM Flights WHERE ID = {flightId})");
            string SQL = sb.ToString();
            string res = DL.ExecuteSqlNonQuery(SQL);
            if (res == "")
            {
                throw new TicketDeleteErrorException("Tickets delete error");
            }
        }
        public void RemoveTicketsByAirlineCompanyId(long airlineCompanyId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"DELETE FROM Tickets WHERE FLIGHT_ID IN (SELECT ID FROM Flights WHERE AIRLINECOMPANY_ID = {airlineCompanyId})");
            string SQL = sb.ToString();
            string res = DL.ExecuteSqlNonQuery(SQL);
            if (res == "")
            {
                throw new TicketDeleteErrorException("Tickets delete error");
            }
        }
        public void RemoveTicketsByCountryCode(long countryCode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"DELETE FROM Tickets WHERE FLIGHT_ID IN (SELECT ID FROM Flights WHERE AIRLINECOMPANY_ID IN (SELECT ID FROM AirlineCompanies WHERE COUNTRY_CODE = {countryCode}))");
            string SQL = sb.ToString();
            string res = DL.ExecuteSqlNonQuery(SQL);
            if (res == "")
            {
                throw new TicketDeleteErrorException("Tickets delete error");
            }
        }
        public void Update(Ticket t)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"UPDATE Tickets SET FLIGHT_ID = {t.FLIGHT_ID}, CUSTOMER_ID = {t.CUSTOMER_ID}, FIRST_NAME = '{t.FIRST_NAME}', LAST_NAME = '{t.LAST_NAME}'");
            sb.Append($" WHERE ID = {t.ID}");
            string SQL = sb.ToString();
            string res = DL.ExecuteSqlNonQuery(SQL);
            if (res == "")
            {
                throw new TicketUpdateErrorException("Tickets update error");
            }
        }
    }
}
