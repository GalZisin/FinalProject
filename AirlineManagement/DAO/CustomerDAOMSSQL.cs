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
    public class CustomerDAOMSSQL : ICustomerDAO
    {
        private SqlDAO DL;  // A central class of database connections
        public CustomerDAOMSSQL()
        {
            DL = new SqlDAO(FlightCenterConfig.strConn);
        }
        public string CheckIfCustomerExist(Customer t)
        {
            StringBuilder sb = new StringBuilder();
            string SQL1 = $"SELECT COUNT(*) FROM Customers WHERE USER_NAME = '{t.USER_NAME}' OR EXISTS (SELECT USER_NAME FROM AirlineCompanies WHERE USER_NAME = '{t.USER_NAME}')";
            string res = DL.ExecuteSqlScalarStatement(SQL1);
            return res;
        }
        public long Add(Customer t)
        {
            StringBuilder sb = new StringBuilder();
            sb = new StringBuilder();
            sb.Append($"INSERT INTO Customers(USER_NAME, PASSWORD, FIRST_NAME, LAST_NAME, ADDRESS, PHONE_NO, CREDIT_CARD_NUMBER)");
            sb.Append($" values('{ t.USER_NAME}', '{ t.PASSWORD}', '{ t.FIRST_NAME}', '{ t.LAST_NAME}','{ t.ADDRESS}', '{ t.PHONE_NO}', '{ t.CREDIT_CARD_NUMBER}')");
            string SQL = sb.ToString();
            DL.ExecuteSqlNonQuery(SQL);
            SQL = $"SELECT ID FROM Customers WHERE USER_NAME = '{t.USER_NAME}'";

            return Int64.Parse(DL.ExecuteSqlScalarStatement(SQL));
        }

        public Customer Get(long id)
        {
            Customer c = null;
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM customers WHERE ID = {id}");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Customers");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                c = new Customer();
                c.ID = (long)dr["ID"];
                c.USER_NAME = (string)dr["USER_NAME"];
                c.PASSWORD = (string)dr["PASSWORD"];
                c.FIRST_NAME = (string)dr["FIRST_NAME"];
                c.LAST_NAME = (string)dr["LAST_NAME"];
                c.ADDRESS = (string)dr["ADDRESS"];
                c.PHONE_NO = (string)dr["PHONE_NO"];
                c.CREDIT_CARD_NUMBER = (string)dr["CREDIT_CARD_NUMBER"];
            }
            if (c != null)
            {
                return c;
            }
            else
                return null;
        }

        public IList<Customer> GetAll()
        {
            IList<Customer> customers = new List<Customer>();
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM customers");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Customers");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                Customer c = new Customer();
                c.ID = (long)dr["ID"];
                c.USER_NAME = (string)dr["USER_NAME"];
                c.PASSWORD = (string)dr["PASSWORD"];
                c.FIRST_NAME = (string)dr["FIRST_NAME"];
                c.LAST_NAME = (string)dr["LAST_NAME"];
                c.ADDRESS = (string)dr["ADDRESS"];
                c.PHONE_NO = (string)dr["PHONE_NO"];
                c.CREDIT_CARD_NUMBER = (string)dr["CREDIT_CARD_NUMBER"];
                customers.Add(c);
            }
                return customers;
        }

        public Customer GetCustomerByUsername(string userName)
        {
            Customer c = null;
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM customers WHERE USER_NAME = '{userName}'");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Customers");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                c = new Customer();
                c.ID = (long)dr["ID"];
                c.USER_NAME = (string)dr["USER_NAME"];
                c.PASSWORD = (string)dr["PASSWORD"];
                c.FIRST_NAME = (string)dr["FIRST_NAME"];
                c.LAST_NAME = (string)dr["LAST_NAME"];
                c.ADDRESS = (string)dr["ADDRESS"];
                c.PHONE_NO = (string)dr["PHONE_NO"];
                c.CREDIT_CARD_NUMBER = (string)dr["CREDIT_CARD_NUMBER"];
            }
            if (c != null)
            {
                return c;
            }
            return null;
        }
        public Customer GetCustomerByName(string customerName)
        {
            Customer c = null;
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM customers WHERE FIRST_NAME = '{customerName}'");
            string SQL = sb.ToString();
            DataSet DS = DL.GetSqlQueryDS(SQL, "Customers");
            DataTable dt = DS.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                c = new Customer();
                c.ID = (long)dr["ID"];
                c.USER_NAME = (string)dr["USER_NAME"];
                c.PASSWORD = (string)dr["PASSWORD"];
                c.FIRST_NAME = (string)dr["FIRST_NAME"];
                c.LAST_NAME = (string)dr["LAST_NAME"];
                c.ADDRESS = (string)dr["ADDRESS"];
                c.PHONE_NO = (string)dr["PHONE_NO"];
                c.CREDIT_CARD_NUMBER = (string)dr["CREDIT_CARD_NUMBER"];
            }
            if (c != null)
            {
                return c;
            }
            return null;
        }

        public void Remove(Customer t)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"DELETE FROM Customers WHERE ID = {t.ID}");
            string SQL = sb.ToString();
            string res = DL.ExecuteSqlNonQuery(SQL);
            if (res == "")
            {
                throw new CustomerDeleteErrorException("Customer delete error");
            }
        }

        public void Update(Customer t)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"UPDATE Customers SET USER_NAME = '{t.USER_NAME}', PASSWORD = '{t.PASSWORD}', FIRST_NAME = '{t.FIRST_NAME}',");
            sb.Append($" LAST_NAME = '{t.LAST_NAME}', ADDRESS = '{t.ADDRESS}', PHONE_NO = '{t.PHONE_NO}', CREDIT_CARD_NUMBER = '{t.CREDIT_CARD_NUMBER}'");
            sb.Append($" WHERE ID = { t.ID}");
            string SQL = sb.ToString();
            string res = DL.ExecuteSqlNonQuery(SQL);
            if(res == "")
            {
                throw new CustomerUpdateErrorException("Customer update error");
            }
        }
    }
}
