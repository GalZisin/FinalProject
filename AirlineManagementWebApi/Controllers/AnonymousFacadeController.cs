using AirlineManagement;
using AirlineManagementWebApi.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace AirlineManagementWebApi.Controllers
{


    public class AnonymousFacadeController : ApiController
    {
        private FlyingCenterSystem fcs;
        /// <summary>
        /// Get all flights
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(Flight))]
        [Route("api/AnonymousFacade/allflights")]
        [HttpGet]
        public IHttpActionResult GetAllFlights()
        {
            fcs = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            IAnonymousUserFacade anonymousFacade = fcs.GetFacade(null) as IAnonymousUserFacade;
            IList<Flight> flights = anonymousFacade.GetAllFlights();
            if (flights.Count == 0)
            {
                return NotFound();
            }
            return Ok(flights);
        }
        /// <summary>
        /// Get all airline companies
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(AirlineCompany))]
        [Route("api/AnonymousFacade/allairlinecompanies")]
        [HttpGet]
        public IHttpActionResult GetAllAirlineCompanies()
        {
            fcs = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            IAnonymousUserFacade anonymousFacade = fcs.GetFacade(null) as IAnonymousUserFacade;
            IList<AirlineCompany> airlineCompanies = anonymousFacade.GetAllAirlineCompanies();
            if (airlineCompanies.Count == 0)
            {
                return NotFound();
            }
            return Ok(airlineCompanies);
        }
        /// <summary>
        ///  Get all airline companies by scheduled Time
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        [ResponseType(typeof(AirlineCompany))]
        [Route("api/AnonymousFacade/allairlinecompanies/search")]
        [HttpGet]
        public IHttpActionResult GetAllAirlineCompaniesByScheduledTime(string typeName)
        {
            fcs = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            IAnonymousUserFacade anonymousFacade = fcs.GetFacade(null) as IAnonymousUserFacade;
            IList<AirlineCompany> airlineCompanies = anonymousFacade.GetAllAirlineCompaniesByScheduledTime(typeName);
            if (airlineCompanies.Count == 0)
            {
                return NotFound();
            }
            return Ok(airlineCompanies);
        }
        /// <summary>
        /// Get flights by vacancy
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(Flight))]
        [Route("api/AnonymousFacade/gebyvacancy/{id}")]
        [HttpGet]
        public IHttpActionResult GetAllFlightsVacancy()
        {
            fcs = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            IAnonymousUserFacade anonymousFacade = fcs.GetFacade(null) as IAnonymousUserFacade;
            Dictionary<Flight, int> flights = anonymousFacade.GetAllFlightsByVacancy();

            if (flights.Count == 0)
            {
                return NotFound();
            }
            return Ok(flights);
        }
        /// <summary>
        /// Get flights by vacancy and scheduled time
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(Flight))]
        [Route("api/AnonymousFacade/getflightsbyvacancy")]
        [HttpGet]
        public IHttpActionResult GetAllFlightsByVacancyAndScheduledTime()
        {
            fcs = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            IAnonymousUserFacade anonymousFacade = fcs.GetFacade(null) as IAnonymousUserFacade;
            IList<Flight> flights = anonymousFacade.GetAllFlightsByVacancyAndScheduledTime();
            if (flights.Count == 0)
            {
              
                return NotFound();
            }
            return Ok(flights);
        }
        /// <summary>
        /// Get grouped by flights by vacancy and scheduled time
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(Flight))]
        [Route("api/AnonymousFacade/getgrupedbyflightsbyvacancy")]
        [HttpGet]
        public IHttpActionResult GetAllGroupedByFlightsByVacancyAndScheduledTime()
        {
            fcs = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            IAnonymousUserFacade anonymousFacade = fcs.GetFacade(null) as IAnonymousUserFacade;
            IList<Flight> flights = anonymousFacade.GetAllFlightsByVacancyAndScheduledTime();
            FlightLists flightLists = new FlightLists();
            List<KMessage> ids = flights.GroupBy(x => new { x.ID }).Select(g => new KMessage() { Keyst = g.Key.ID.ToString() }).ToList<KMessage>();
            List<KMessage> companies = flights.GroupBy(x => new { x.AIRLINE_NAME }).Select(g => new KMessage() { Keyst = g.Key.AIRLINE_NAME }).ToList<KMessage>();
            List<KMessage> originCountries = flights.GroupBy(x => new { x.O_COUNTRY_NAME }).Select(g => new KMessage() { Keyst = g.Key.O_COUNTRY_NAME }).ToList<KMessage>();
            List<KMessage> destinationCountries = flights.GroupBy(x => new { x.D_COUNTRY_NAME }).Select(g => new KMessage() { Keyst = g.Key.D_COUNTRY_NAME }).ToList<KMessage>();

            flightLists.ids = new List<string>();
            flightLists.companies = new List<string>();
            flightLists.originCountries = new List<string>();
            flightLists.destinationCountries = new List<string>();

            foreach (KMessage item in ids)
            {
                flightLists.ids.Add(item.Keyst);
            }
            foreach (KMessage item in companies)
            {
                flightLists.companies.Add(item.Keyst);
            }
            foreach (KMessage item in originCountries)
            {
                flightLists.originCountries.Add(item.Keyst);
            }
            foreach (KMessage item in destinationCountries)
            {
                flightLists.destinationCountries.Add(item.Keyst);
            }

            if (flights.Count == 0)
            {
                return NotFound();
            }
            return Ok(flightLists);
        }
        /// <summary>
        /// Get flight by id
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(Flight))]
        [Route("api/AnonymousFacade/getbyid/{flightId}")]
        [HttpGet]
        public IHttpActionResult GetFlightById(int flightId)
        {
            fcs = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            IAnonymousUserFacade anonymousFacade = fcs.GetFacade(null) as IAnonymousUserFacade;
            Flight flight = anonymousFacade.GetFlightById(flightId);

            if (flight == null)
            {
                return NotFound();
            }
            return Ok(flight);
        }
        /// <summary>
        /// Search with parameters (GET)
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(Flight))]
        [Route("api/AnonymousFacade/SearchFlightByConditions/search")]
        [HttpGet]
        public IHttpActionResult SearchFlightByConditions(string typeName, string flightId, string country, string company)
        {

            IList<Flight> flights = null;
            fcs = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            IAnonymousUserFacade anonymousFacade = fcs.GetFacade(null) as IAnonymousUserFacade;

            flights = anonymousFacade.GetAllFilteredFlights(typeName, flightId, country, company);
            if (flights == null)
            {
                return NotFound();
            }
            SetRandomDepartureDelayedStatus(flights);
            SetRandomArrivalDelayedStatus(flights);
            return Ok(flights);
        }
        [ResponseType(typeof(Flight))]
        [Route("api/AnonymousFacade/getflightsbyvacancy/search")]
        [HttpGet]
        public IHttpActionResult SearchAllFlightsByVacancyAndScheduledTime(string flightId, string originCountry, string destinationCountry, string company, string departureDate, string returnDate)
        {
            string departureFormatedDate = departureDate.Substring(0, 15);
            DateTime ddt = DateTime.ParseExact(departureFormatedDate, "ddd MMM dd yyyy", null);
            departureFormatedDate = ddt.ToString("yyyy-MM-dd");

            string returnFormatedDate = returnDate.Substring(0, 15);
            DateTime rdt = DateTime.ParseExact(returnFormatedDate, "ddd MMM dd yyyy", null);
            returnFormatedDate = rdt.ToString("yyyy-MM-dd");

            IList <RoundTripFlights> roundTripFlights = new List<RoundTripFlights>();
            IList<Flight> flights1 = null;
            IList<Flight> flights2 = null;
            fcs = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            IAnonymousUserFacade anonymousFacade = fcs.GetFacade(null) as IAnonymousUserFacade;

            flights1 = anonymousFacade.GetAllGoingFlightsByVacancyAndScheduledTime(flightId, originCountry, destinationCountry, company, departureFormatedDate);

         

            foreach(Flight flight1 in flights1)
            {
                flights2 = anonymousFacade.GetAllReturnFlightsByVacancyAndScheduledTime(flight1.O_COUNTRY_NAME, flight1.D_COUNTRY_NAME, company, returnFormatedDate);
                foreach (Flight flight2 in flights2)
                {
                    
                    //flight1.TIME_DIFF = flight1.REAL_LANDING_TIME - flight1.REAL_DEPARTURE_TIME;
                    //flight2.TIME_DIFF = flight2.REAL_LANDING_TIME - flight2.REAL_DEPARTURE_TIME;
                    roundTripFlights.Add(new RoundTripFlights { f1 = flight1, f2 = flight2 });
                }
            }

            if (flights1 == null && flights2 ==null)
            {
                return NotFound();
            }
            return Ok(roundTripFlights);
        }
        /// <summary>
        /// Get flights by origin country code
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(Flight))]
        [Route("api/AnonymousFacade/getbyorigincountrycode/{countryCode}")]
        [HttpGet]
        public IHttpActionResult GetFlightsByOriginCountry(int countryCode)
        {
            fcs = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            IAnonymousUserFacade anonymousFacade = fcs.GetFacade(null) as IAnonymousUserFacade;
            IList<Flight> flights = anonymousFacade.GetFlightsByOriginCountry(countryCode);

            if (flights.Count == 0)
            {
                return NotFound();
            }
            return Ok(flights);
        }
        /// <summary>
        /// Get flights by destination country code (Query parameters)
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(Flight))]
        [Route("api/AnonymousFacade/getbydestinationcountrycode/search")]
        [HttpGet]
        public IHttpActionResult GetFlightsByDestinationCountry(int countryCode = 0)
        {
            IHttpActionResult res = null;
            IList<Flight> flights = null;
            fcs = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            IAnonymousUserFacade anonymousFacade = fcs.GetFacade(null) as IAnonymousUserFacade;
            if (countryCode != 0)
            {
                flights = anonymousFacade.GetFlightsByDestinationCountry(countryCode);
                res = Ok(flights);
            }
            else if ((countryCode != 0 && flights.Count == 0) || countryCode == 0)
            {
                res = NotFound();
            }

            return res;
        }
        /// <summary>
        /// Get flights by departure date
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(Flight))]
        [Route("api/AnonymousFacade/getbydeparturedate/{departureDate}")]
        [HttpGet]
        public IHttpActionResult GetFlightsByDepatrureDate(DateTime departureDate)
        {
            fcs = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            IAnonymousUserFacade anonymousFacade = fcs.GetFacade(null) as IAnonymousUserFacade;
            IList<Flight> flights = anonymousFacade.GetFlightsByDepatrureDate(departureDate);

            if (flights.Count == 0)
            {
                return NotFound();
            }
            return Ok(flights);
        }
        /// <summary>
        /// Get flights by landinge date
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(Flight))]
        [Route("api/AnonymousFacade/getbylandingdate/{landingeDate}")]
        [HttpGet]
        public IHttpActionResult GetFlightsByLandingDate(DateTime landingeDate)
        {
            fcs = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            IAnonymousUserFacade anonymousFacade = fcs.GetFacade(null) as IAnonymousUserFacade;
            IList<Flight> flights = anonymousFacade.GetFlightsByLandingDate(landingeDate);

            if (flights.Count == 0)
            {
                return NotFound();
            }
            return Ok(flights);
        }
        /// <summary>
        /// Get random order list
        /// </summary>
        /// <param name="from"></param>
        /// <param name="rangeEx"></param>
        /// <returns></returns>
        private int[] RandomNumber(int from, int rangeEx)
        {
            var orderedList = Enumerable.Range(from, rangeEx);
            var rng = new Random();
            return orderedList.OrderBy(c => rng.Next()).ToArray();
        }
        /// <summary>
        /// Set random departure delayes status
        /// </summary>
        /// <param name="flights"></param>
        public void SetRandomDepartureDelayedStatus(IList<Flight> flights)
        {
            Random random = new Random();
            int perpercentage;
            int randomMinutes;
            int numOfFlights;
            fcs = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            IAnonymousUserFacade anonymousFacade = fcs.GetFacade(null) as IAnonymousUserFacade;
            Flight[] flightsArray = new Flight[flights.Count];
            int[] flightsRandomIndex = new int[flights.Count];
            perpercentage = random.Next(10, 21);

            int m = 0;
            foreach (Flight f in flights)
            {
                if (f.REAL_DEPARTURE_TIME == f.DEPARTURE_TIME)
                {
                    flightsArray[m] = f;
                    m++;
                }
            }
            if (flights.Count >= 5)
            {
                numOfFlights = (flightsArray.Length * perpercentage) / 100;

                if (numOfFlights == 0)
                {
                    numOfFlights = 1;
                }
                numOfFlights = numOfFlights - flights.Count + m;
                if (numOfFlights < 0)
                {
                    numOfFlights = 0;
                }
                flightsRandomIndex = RandomNumber(0, flightsArray.Length - 1);
            }
            else
            {
                numOfFlights = 0;
            }
            if (numOfFlights > 0)
            {
                for (int i = 0; i < numOfFlights; i++)
                {
                    int index = flightsRandomIndex[i];

                    randomMinutes = random.Next(30, 241);
                    flightsArray[index].REAL_DEPARTURE_TIME = flightsArray[index].DEPARTURE_TIME.AddMinutes(randomMinutes);
                    flightsArray[index].TIME_DIFF = flightsArray[index].DEPARTURE_TIME.Subtract(flightsArray[index].REAL_DEPARTURE_TIME);
                    anonymousFacade.UpdateRealDepartureTime(flightsArray[index].ID, flightsArray[index].REAL_DEPARTURE_TIME);
                }
            }
        }
        /// <summary>
        /// Set random arrivals delayes status
        /// </summary>
        /// <param name="arrivalFlights"></param>
        public void SetRandomArrivalDelayedStatus(IList<Flight> arrivalFlights)
        {
            Random random = new Random();
            int perpercentage;
            int randomMinutes;
            int numOfFlights;
            fcs = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            IAnonymousUserFacade anonymousFacade = fcs.GetFacade(null) as IAnonymousUserFacade;
            Flight[] flightsArray = new Flight[arrivalFlights.Count];
            int[] flightsRandomIndex = new int[arrivalFlights.Count];
            DateTime localDate = DateTime.Now;
            perpercentage = random.Next(10, 21);

            int m = 0;
            int nf = 0;
            foreach (Flight f in arrivalFlights)
            {
                if (localDate < f.REAL_LANDING_TIME.AddHours(-2))
                {
                    nf++;
                    if (f.REAL_LANDING_TIME == f.LANDING_TIME)
                    {


                        flightsArray[m] = f;
                        m++;

                    }
                }
            }
            if (m > 1)
            {
                if (flightsArray.Length >= 1)
                {

                    numOfFlights = (nf * perpercentage) / 100;

                    if (numOfFlights == 0)
                    {
                        numOfFlights = 1;
                    }
                    numOfFlights = numOfFlights - (nf - m);

                    if (numOfFlights < 0)
                    {
                        numOfFlights = 0;
                    }
                    flightsRandomIndex = RandomNumber(0, m - 1);
                }
                else
                {
                    numOfFlights = 0;
                }
                if (numOfFlights > 0)
                {
                    for (int i = 0; i < numOfFlights; i++)
                    {
                        int index = flightsRandomIndex[i];

                        randomMinutes = random.Next(30, 241);
                        flightsArray[index].REAL_LANDING_TIME = flightsArray[index].LANDING_TIME.AddMinutes(randomMinutes);
                        flightsArray[index].TIME_DIFF = flightsArray[index].LANDING_TIME.Subtract(flightsArray[index].REAL_LANDING_TIME);
                        anonymousFacade.UpdateRealArrivalTime(flightsArray[index].ID, flightsArray[index].REAL_LANDING_TIME);
                    }
                }
            }

        }
        /// <summary>
        /// Get all coutries
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(Country))]
        [Route("api/AnonymousFacade/allCountries")]
        [HttpGet]
        public IHttpActionResult GetAllCountries()
        {
            fcs = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            IAnonymousUserFacade anonymousFacade = fcs.GetFacade(null) as IAnonymousUserFacade;
            IList<Country> countries = anonymousFacade.GetAllCountries();
            if (countries.Count == 0)
            {
                return NotFound();
            }
            return Ok(countries);
        }
        /// <summary>
        /// Get all coutries by scheduled time
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(Country))]
        [Route("api/AnonymousFacade/allCountriesByScheduledTime/search")]
        [HttpGet]
        public IHttpActionResult GetAllCountriesByScheduledTime(string typeName)
        {
            fcs = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            IAnonymousUserFacade anonymousFacade = fcs.GetFacade(null) as IAnonymousUserFacade;
            IList<Country> countries = anonymousFacade.GetAllCountriesByScheduledTime(typeName);
            if (countries.Count == 0)
            {
                return NotFound();
            }
            return Ok(countries);
        }
        /// <summary>
        /// Get all flight Ids
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(long))]
        [Route("api/AnonymousFacade/allFlightIds")]
        [HttpGet]
        public IHttpActionResult GetAllFlightIds()
        {
            fcs = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            IAnonymousUserFacade anonymousFacade = fcs.GetFacade(null) as IAnonymousUserFacade;
            IList<long> fligthIds = anonymousFacade.GetAllFlightsIds();
            if (fligthIds.Count == 0)
            {
                return NotFound();
            }
            return Ok(fligthIds);
        }
        /// <summary>
        /// Get all flight Ids by scheduled time
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(long))]
        [Route("api/AnonymousFacade/allFlightIdsByScheduledTime/search")]
        [HttpGet]
        public IHttpActionResult GetFlightIdsByScheduledTime(string typeName)
        {
            fcs = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            IAnonymousUserFacade anonymousFacade = fcs.GetFacade(null) as IAnonymousUserFacade;
            IList<long> fligthIds = anonymousFacade.GetFlightIdsByScheduledTime(typeName);
            if (fligthIds.Count == 0)
            {
                return NotFound();
            }
            return Ok(fligthIds);
        }
        /// <summary>
        /// Search with parameters (POST)
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(Flight))]
        [Route("api/AnonymousFacade/searchbydata", Name = "searchbydata")]
        [HttpPost]
        public IHttpActionResult SearchWithParametars([FromBody] SearchParameters searchData)
        {
            IList<Flight> flights = null;
            fcs = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            IAnonymousUserFacade anonymousFacade = fcs.GetFacade(null) as IAnonymousUserFacade;
            SearchParameters searchParameters = new SearchParameters();
            searchParameters.airlineCompany = searchData.airlineCompany;
            searchParameters.originCountry = searchData.originCountry;
            searchParameters.flightId = searchData.flightId;
            searchParameters.arrivalsDepartures = searchData.arrivalsDepartures;
            //string typeName, string flightId, string country, string company
            flights = anonymousFacade.GetAllFilteredFlights(searchParameters.arrivalsDepartures, searchParameters.flightId, searchParameters.originCountry, searchParameters.airlineCompany);

            if (flights == null)
            {
                return NotFound();
            }
            SetRandomDepartureDelayedStatus(flights);
            SetRandomArrivalDelayedStatus(flights);
            return Ok(flights);
        }




        [ResponseType(typeof(string))]
        [Route("api/AnonymousFacade/register", Name = "register")]
        [HttpPost]
        public IHttpActionResult SendEmailToConfirm([FromBody] AccountParameters newAccount)
        {
            fcs = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            IAnonymousUserFacade anonymousFacade = fcs.GetFacade(null) as IAnonymousUserFacade;
            AccountParameters account = new AccountParameters();
            account.email = newAccount.email;
            account.firstName = newAccount.firstName;
            account.lastName = newAccount.lastName;
            account.userName = newAccount.userName;
            account.phoneNumber = newAccount.phoneNumber;
            account.password = newAccount.password;

            Execute(account.email, account.firstName, account.lastName, account.userName);

            var jsonStringAccount = Newtonsoft.Json.JsonConvert.SerializeObject(account);
            string host = "localhost";

            string key = account.userName;

            // Store data in the cache

            bool success = Save(host, key, jsonStringAccount);


            return Ok($"Email sent");
        }

        //public static string myGuid = null;
        private static void Execute(string email, string firstName, string lastName, string userName)
        {
            var apiKey = Environment.GetEnvironmentVariable("KeyForEmail", EnvironmentVariableTarget.Machine);
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("test@example.com", "Airline management");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress(email, $"Example User ");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var token = TokenManager.GenerateToken(userName);
            //myGuid = Guid.NewGuid().ToString();
            var htmlContent = "Hello"+" "+firstName+" "+lastName+"<br>Click here to confirm your email<br>http://localhost:57588/Page/ConfirmEmail?guid=" + token;  //"<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg).Result;
        }

        private static bool Save(string host, string key, string value)
        {
            bool isSuccess = false;
            using (RedisClient redisClient = new RedisClient(host))
            {
                if (redisClient.Get<string>(key) == null)
                {
                    isSuccess = redisClient.Set(key, value);
                    //redisClient.Remove(key);
                }
            }
            return isSuccess;
        }
        //private static string Get(string host, string key)
        //{
        //    using (RedisClient redisClient = new RedisClient(host))
        //    {
        //        return redisClient.Get<string>(key);
        //    }
        //}
    }

}
