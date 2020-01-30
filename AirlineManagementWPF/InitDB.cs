using AirlineManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementWPF
{
    static public class InitDB
    {
        public static void InitDataBase()
        {
            SqlDAO DL = new SqlDAO(FlightCenterConfig.strConn);
            string SQL = "DELETE FROM Tickets";
            DL.ExecuteSqlNonQuery(SQL);
            SQL = "DELETE FROM Customers";
            DL.ExecuteSqlNonQuery(SQL);
            SQL = "DELETE FROM Flights";
            DL.ExecuteSqlNonQuery(SQL);
            SQL = "DELETE FROM AirlineCompanies";
            DL.ExecuteSqlNonQuery(SQL);
            SQL = "DELETE FROM Countries";
            DL.ExecuteSqlNonQuery(SQL);
            SQL = "DBCC CHECKIDENT ('Customers', RESEED, 0)";
            DL.ExecuteSqlNonQuery(SQL);
            SQL = "DBCC CHECKIDENT ('Countries', RESEED, 0)";
            DL.ExecuteSqlNonQuery(SQL);
            SQL = "DBCC CHECKIDENT ('AirlineCompanies', RESEED, 0)";
            DL.ExecuteSqlNonQuery(SQL);
            SQL = "DBCC CHECKIDENT ('Flights', RESEED, 0)";
            DL.ExecuteSqlNonQuery(SQL);
            SQL = "DBCC CHECKIDENT ('Tickets', RESEED, 0)";
            DL.ExecuteSqlNonQuery(SQL);
        }
    }
}
