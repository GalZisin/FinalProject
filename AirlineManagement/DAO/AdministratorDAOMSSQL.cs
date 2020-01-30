using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement
{
    public class AdministratorDAOMSSQL : IAdministratorDAO
    {
        SqlDAO DL; // A central class of database connections
        public AdministratorDAOMSSQL()
        {
            DL = new SqlDAO(FlightCenterConfig.strConn);
        }
        public string CheckIfAdministratorExist(Administrator t)
        {
            StringBuilder sb = new StringBuilder();
            string SQL1 = $"SELECT COUNT(*) FROM Administrators WHERE USER_NAME = '{t.USER_NAME}'";
            string res = DL.ExecuteSqlScalarStatement(SQL1);
            return res;
        }
        public long Add(Administrator t)
        {
            StringBuilder sb = new StringBuilder();
            sb = new StringBuilder();
            sb.Append($"INSERT INTO Administrators (FIRST_NAME, LAST_NAME, USER_NAME, PASSWORD)");
            sb.Append($" values('{ t.FIRST_NAME}', '{ t.LAST_NAME}', '{ t.USER_NAME}', '{ t.PASSWORD}')");
            string SQL = sb.ToString();
            DL.ExecuteSqlNonQuery(SQL);

            SQL = $"SELECT ID FROM Administrators WHERE USER_NAME ='{t.USER_NAME}'";
            return Int64.Parse(DL.ExecuteSqlScalarStatement(SQL));
        }

        public Administrator Get(long id)
        {
            Administrator admin = null;
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM Administrators WHERE ID = {id}");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Administrators");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                admin = new Administrator();
                admin.ID = (long)dr["ID"];
                admin.FIRST_NAME = (string)dr["FIRST_NAME"];
                admin.LAST_NAME = (string)dr["LAST_NAME"];
                admin.USER_NAME = (string)dr["USER_NAME"];
                admin.PASSWORD = (string)dr["PASSWORD"];
            }
            if (admin != null)
            {
                return admin;
            }
            return null;
        }

        public IList<Administrator> GetAll()
        {
            IList<Administrator> administrators = new List<Administrator>();
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM Administrators");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Administrators");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                Administrator admin = new Administrator();
                admin.ID = (long)dr["ID"];
                admin.FIRST_NAME = (string)dr["FIRST_NAME"];
                admin.LAST_NAME = (string)dr["LAST_NAME"];
                admin.USER_NAME = (string)dr["USER_NAME"];
                admin.PASSWORD = (string)dr["PASSWORD"];
                administrators.Add(admin);
            }
            return administrators;
        }
        public Administrator GetAdministratorByUserName(string userName)
        {
            Administrator a = null;
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM Administrators WHERE USER_NAME = '{userName}'");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Customers");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                a = new Administrator();
                a.ID = (long)dr["ID"];
                a.FIRST_NAME = (string)dr["FIRST_NAME"];
                a.LAST_NAME = (string)dr["LAST_NAME"];
                a.USER_NAME = (string)dr["USER_NAME"];
                a.PASSWORD = (string)dr["PASSWORD"];
            }
            if (a != null)
            {
                return a;
            }
            return null;
        }
        public void Remove(Administrator t)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"DELETE FROM Administrators WHERE ID = {t.ID}");
            string SQL = sb.ToString();
            string res = DL.ExecuteSqlNonQuery(SQL);
            if (res == "")
            {
                throw new AdministratorDeleteErrorException("Administrator delete error: " + DL.ErrorMessage);
            }
        }

        public void Update(Administrator t)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"UPDATE Administrators SET FIRST_NAME = '{t.FIRST_NAME}', LAST_NAME = '{t.LAST_NAME}', USER_NAME = '{t.USER_NAME}', PASSWORD = '{t.PASSWORD}'");
            sb.Append($" WHERE ID = { t.ID}");
            string SQL = sb.ToString();
            string res = DL.ExecuteSqlNonQuery(SQL);
            if (res == "")
            {
                throw new AdministratorUpdateErrorException("Administrator update error: " + DL.ErrorMessage);
            }
        }
    }
}
