using AirlineManagement;
using AirlineManagementWebApi.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AirlineManagementWebApi.Controllers
{
    public class PageController : Controller
    {
        // GET: Page
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetDepartureFlights()
        {
            return new FilePathResult("~/Views/Page/departures.html", "text/html");
        }
        public ActionResult GetLandingFlights()
        {
            return new FilePathResult("~/Views/Page/landing.html", "text/html");
        }
        public ActionResult SearchFlights()
        {
            return new FilePathResult("~/Views/Page/search.html", "text/html");
        }
        public ActionResult FlightDeals()
        {
            return new FilePathResult("~/Views/Page/deals.html", "text/html");
        }

        public ActionResult ConfirmEmail()
        {
            string host = "localhost";
            var q = Request.QueryString;
            //Execute();

            string guid = q.Get("guid");

            string tokenUsername = TokenManager.ValidateToken(guid);
            string jsonStringAccount = "";
            try
            {
                jsonStringAccount = Get(host, tokenUsername);
                Remove(host, tokenUsername);
            }
            catch { }

            if(jsonStringAccount == "")
                return Content("Email NOT confirmed! watch out1!!!");
            AccountParameters objectAccount = Newtonsoft.Json.JsonConvert.DeserializeObject<AccountParameters>(jsonStringAccount);
              FlyingCenterSystem fcs;
      

            fcs = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            IAnonymousUserFacade anonymousFacade = fcs.GetFacade(null) as IAnonymousUserFacade;
            Customer customer = new Customer();
            customer.FIRST_NAME = objectAccount.firstName;
            customer.LAST_NAME = objectAccount.lastName;
            customer.USER_NAME = objectAccount.userName;
            customer.PASSWORD = objectAccount.password;
            customer.PHONE_NO = objectAccount.phoneNumber;
            try
            {
                anonymousFacade.CreateNewCustomerFromRedis(customer);

            }
            catch { return Content("Error in create customer"); }

     
            //if (guid == AnonymousFacadeController.myGuid)
            return Content("Email confirmed");
        
        }


        private string Get(string host, string key)
        {
            using (RedisClient redisClient = new RedisClient(host))
            {
                return redisClient.Get<string>(key);
            }
        }
        private bool Remove(string host, string key)
        {
            using (RedisClient redisClient = new RedisClient(host))
            {
                return redisClient.Remove(key);
            }
           
        }
    }
}