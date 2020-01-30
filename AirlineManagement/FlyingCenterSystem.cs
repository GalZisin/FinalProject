using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AirlineManagement
{
    public class FlyingCenterSystem
    {
        private SqlDAO DL;
        private static FlyingCenterSystem _MyFlyingCenterInstance;
        private static object key = new object();
        int time = 20; // Constant time for transference 20:00
        protected FlyingCenterSystem()
        {
            DL = new SqlDAO(FlightCenterConfig.strConn);
          
            new Task(() =>
            {
                bool flag = true;
                while (true)
                {
                    if (DateTime.Now.Hour < time) // Checks every 10 minutes if the time for transference has passed or not
                    {
                        flag = true;
                    }
                    else
                    {
                        if (flag)
                        {
                            DL.MoveTicketsExpired3HoursAgo();
                            DL.MoveFlightsExpired3HoursAgo();
                        }
                        flag = false;
                    }
                    Thread.Sleep(600000); //10 minutes interval
                }
            });
        }
        /// <summary>
        /// Get Flying center System instance
        /// </summary>
        /// <returns></returns>
        public static FlyingCenterSystem GetFlyingCenterSystemInstance()
        {
            if (_MyFlyingCenterInstance == null)
            {
                lock (key)
                {
                    if (_MyFlyingCenterInstance == null)
                    {
                        _MyFlyingCenterInstance = new FlyingCenterSystem();
                    }
                }
            }
            return _MyFlyingCenterInstance;
        }
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public ILoginToken Login(string userName, string Password)
        {
            LoginService loginService = new LoginService();

            if (loginService.TryAdminLogin(userName, Password, out LoginToken<Administrator> AdminToken))
            {
                return AdminToken;
            }
            else if (loginService.TryAirlineLogin(userName, Password, out LoginToken<AirlineCompany> AirlineCompanyToken))
            {
                return AirlineCompanyToken;
            }
            else if (loginService.TryCustomerLogin(userName, Password, out LoginToken<Customer> CustomerToken))
            {
                return CustomerToken;
            }
            else
            return null;
        }
        /// <summary>
        /// Get Facade
        /// </summary>
        /// <param name="loginToken"></param>
        /// <returns></returns>
        public IFacade GetFacade(ILoginToken loginToken)
        {
            string a = "";
            if (loginToken != null)
            {
                a = loginToken.GetType().GenericTypeArguments[0].Name;
            }
            if (a == "Administrator")
            {
                return new LoggedInAdministratorFacade();
            }
            else if (a == "AirlineCompany")
            {
                return new LoggedInAirlineFacade();
            }
            else if (a == "Customer")
            {
                return new LoggedInCustomerFacade();
            }
            else // IloginToken is null - > user is Anonymous
                return new AnonymousUserFacade();
        }
    }
}
