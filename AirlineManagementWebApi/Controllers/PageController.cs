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
        public ActionResult FlightHome()
        {
            return new FilePathResult("~/index.html", "text/html");
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

            if (jsonStringAccount == "")
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
            try
            {
                anonymousFacade.CreateNewCustomerFromRedis(customer);

            }
            catch { return Content("Error in create customer"); }

   
            string htmlString = "<!DOCTYPE html><html><head><style>" +
          "div.card {" +
          " width: 500px; box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);  text-align: center; margin: auto" +
          "}" +
          "div.header {" +
          " background-color: #4CAF50; color: white; padding: 10px; font-size: 36px; font-family: Book Antiqua;" +
          "}" +
          "</style>" +
          "<body>" +
          "<div class=\"card\">" +
          "<div class=\"header\">" +
          "<h5>Congratulations!</h5>" +
          "<h5>You have successfully registered</h5>" +
          "</div>" +
          "</div>" +
          "</body>" +
          "</head>" +
          "</html>";
            return Content(htmlString, "text/html");
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