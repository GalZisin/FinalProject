using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement
{
    public class LoginService : ILoginService
    {
        private IAirlineDAO _arilineDAO;
        private ICustomerDAO _customerDAO;
        private IAdministratorDAO _administrator;

        public bool TryAdminLogin(string userName, string password, out LoginToken<Administrator> token)
        {
            _administrator = new AdministratorDAOMSSQL();
            Administrator administrator = _administrator.GetAdministratorByUserName(userName);

            if (administrator != null)
            {
                if (administrator.PASSWORD == password)
                {
                    token = new LoginToken<Administrator>() { User = administrator };
                    return true;
                }
                else
                {
                    throw new WrongPasswordException("Wrong Password Exception");
                }
            }
            token = null;
            return false;
        }

        public bool TryAirlineLogin(string userName, string password, out LoginToken<AirlineCompany> token)
        {
            _arilineDAO = new AirlineDAOMSSQL();
            AirlineCompany airline = _arilineDAO.GetAirlineByUsername(userName);

            if (airline != null)
            {
                if (airline.PASSWORD == password)
                {
                    token = new LoginToken<AirlineCompany>() { User = airline };
                    return true;
                }
                else
                {
                    throw new WrongPasswordException("Wrong password exception");
                }
            }
            token = null;
            return false;
        }

        public bool TryCustomerLogin(string userName, string password, out LoginToken<Customer> token)
        {
            _customerDAO = new CustomerDAOMSSQL();
            Customer customer = _customerDAO.GetCustomerByUsername(userName);

            if (customer != null)
            {
                if (customer.PASSWORD == password)
                {
                    token = new LoginToken<Customer>() { User = customer };
                    return true;
                }
                else
                {
                    throw new WrongPasswordException("Wrong Password Exception");
                }
            }
            token = null;
            return false;
        }
    }
}
