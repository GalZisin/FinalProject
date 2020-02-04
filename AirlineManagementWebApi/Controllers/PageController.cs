using SendGrid;
using SendGrid.Helpers.Mail;
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


        //public static string myGuid = null;
        //static void Execute()
        //{
        //    //string apiKey = System.IO.File.ReadAllText(@"C:\Key\newkey.txt");
        //    var apiKey = Environment.GetEnvironmentVariable("KeyForEmail", EnvironmentVariableTarget.Machine);
        //    var client = new SendGridClient(apiKey);
        //    var from = new EmailAddress("test@example.com", "Example User");
        //    var subject = "Sending with SendGrid is Fun";
        //    var to = new EmailAddress("galzisin86@gmail.com", "Example User");
        //    var plainTextContent = "and easy to do anywhere, even with C#";
        //    myGuid = Guid.NewGuid().ToString();
        //    var htmlContent = "Click here to confirm your email<br>http://localhost:57588/Page/ConfirmEmail?guid=" + myGuid;  //"<strong>and easy to do anywhere, even with C#</strong>";
        //    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        //    var response = client.SendEmailAsync(msg).Result;
        //}
        //public ActionResult Email()
        //{
        //    Execute();
        //    return Content($"Email sent");
        //}

        public ActionResult ConfirmEmail()
        {
            var q = Request.QueryString;
            //Execute();

            string guid = q.Get("guid");

            if (guid == AnonymousFacadeController.myGuid)
                return Content("Email confirmed");
            return Content("Email NOT confirmed! watch out1!!!");
        }
    }
}