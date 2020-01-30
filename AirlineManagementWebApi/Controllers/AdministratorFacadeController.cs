using AirlineManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ServiceStack.Redis;

namespace AirlineManagementWebApi.Controllers
{
    //[Authorize]
    [BasicAuthentication]
    public class AdministratorFacadeController : ApiController
    {
        private FlyingCenterSystem FCS;
        private LoginToken<Administrator> adminLoginToken;
    
        private void GetLoginToken()
        {
            Request.Properties.TryGetValue("token", out object loginToken);

            adminLoginToken = loginToken as LoginToken<Administrator>;
        }
        /// <summary>
        /// Create new airline company and return it's id
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(AirlineCompany))]
        [Route("api/AdministratorFacade/createairlinecompany", Name = "createairlinecompany")]
        [HttpPost]
        public IHttpActionResult CreateNewAirline([FromBody] AirlineCompany airlineCompany)
        {
            GetLoginToken();
            if (adminLoginToken == null)
            {
                return Unauthorized();
            }
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInAdministratorFacade administratorFacade = FCS.GetFacade(adminLoginToken) as ILoggedInAdministratorFacade;
            long airlineCompanyId = administratorFacade.CreateNewAirline(adminLoginToken, airlineCompany);
            airlineCompany =  administratorFacade.GetAirlineCompanyById(adminLoginToken, airlineCompanyId);
            return CreatedAtRoute("createairlinecompany", new { id = airlineCompanyId}, airlineCompany);
        }
        /// <summary>
        /// Update airline company
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(string))]
        [Route("api/AdministratorFacade/updateairline/{id}")]
        [HttpPut]
        public IHttpActionResult UpdateAirlineDetails(int id, [FromBody] AirlineCompany airlineCompany)
        {
            GetLoginToken();
            if (adminLoginToken == null)
            {
                return Unauthorized();
            }
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInAdministratorFacade administratorFacade = FCS.GetFacade(adminLoginToken) as ILoggedInAdministratorFacade;
            airlineCompany = administratorFacade.GetAirlineCompanyById(adminLoginToken, id);
            if(airlineCompany == null)
            {
                return BadRequest("Id not found");
            }
            else
            {
                administratorFacade.UpdateAirlineDetails(adminLoginToken, airlineCompany);
                return Ok($"Airline company with ID = {id} updated");
            }
        }
        /// <summary>
        /// Remove airline company by id
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(string))]
        [Route("api/AdministratorFacade/deleteairline/{id}")]
        [HttpDelete]
        public IHttpActionResult RemoveAirlineById([FromUri]long id)
        {
            AirlineCompany airlineCompany = null;
            IHttpActionResult res = null;
            GetLoginToken();
            if (adminLoginToken == null)
            {
                return Unauthorized();
            }
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInAdministratorFacade administratorFacade = FCS.GetFacade(adminLoginToken) as ILoggedInAdministratorFacade;
            try
            {
                airlineCompany = administratorFacade.GetAirlineCompanyById(adminLoginToken, id);
                if (airlineCompany != null)
                {
                    administratorFacade.RemoveAirline(adminLoginToken, airlineCompany);
                    res = Ok($"Airline company with ID = {id} not found");
                }
            }
            catch (Exception e1)
            {
                res = BadRequest("Airline company hasn't been deleted " + e1.Message);
            }
            return res;
        }
        /// <summary>
        /// Create new customer and return it's id
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(Customer))]
        [Route("api/AdministratorFacade/createcustomer", Name = "createcustomer")]
        [HttpPost]
        public IHttpActionResult CreateNewCustomer([FromBody] Customer customer)
        {
            GetLoginToken();
            if (adminLoginToken == null)
            {
                return Unauthorized();
            }
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInAdministratorFacade administratorFacade = FCS.GetFacade(adminLoginToken) as ILoggedInAdministratorFacade;
            long customerId = administratorFacade.CreateNewCustomer(adminLoginToken, customer);
            customer = administratorFacade.GetCustomerById(adminLoginToken, customerId);
            return CreatedAtRoute("createcustomer", new { id = customerId }, customer);
        }
        /// <summary>
        /// Remove customer
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(string))]
        [Route("api/AdministratorFacade/deletecustomer/{id}")]
        [HttpDelete]
        public IHttpActionResult RemoveCustomer([FromUri] long id)
        {
            IHttpActionResult res = null;
            Customer customer = null;
            GetLoginToken();
            if (adminLoginToken == null)
            {
                return Unauthorized();
            }
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInAdministratorFacade administratorFacade = FCS.GetFacade(adminLoginToken) as ILoggedInAdministratorFacade;
            try
            {
                customer = administratorFacade.GetCustomerById(adminLoginToken, id);
                if (customer != null)
                {
                    administratorFacade.RemoveCustomer(adminLoginToken, customer);
                    res = Ok($"Customer with ID = {id} not found");
                }
            }
            catch (Exception e1)
            {
                res = BadRequest("Airline company hasn't been deleted " + e1.Message);
            }
            return res;
        }
        /// <summary>
        /// Get all flights
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(Flight))]
        [Route("api/AdministratorFacade/allflights")]
        [HttpGet]
        public IHttpActionResult GetAllFlights()
        {
            GetLoginToken();
            if(adminLoginToken == null)
            {
                return Unauthorized();
            }
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInAdministratorFacade administratorFacade = FCS.GetFacade(adminLoginToken) as ILoggedInAdministratorFacade;
            IList<Flight> flights = administratorFacade.GetAllFlights(adminLoginToken);

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
        [ResponseType(typeof(Flight))]
        [Route("api/AdministratorFacade/allairlinecompanies", Name = "getAllAirlineCompanies")]
        [HttpGet]
        public IHttpActionResult GetAllAirlineCompanies()
        {
            GetLoginToken();
            if (adminLoginToken == null)
            {
                return Unauthorized();
            }
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInAdministratorFacade administratorFacade = FCS.GetFacade(adminLoginToken) as ILoggedInAdministratorFacade;
            IList<AirlineCompany> airlineCompanies = administratorFacade.GetAllAirlineCompanies(adminLoginToken);

            if (airlineCompanies.Count == 0)
            {
                return NotFound();
            }
            return Ok(airlineCompanies);
        }
        /// <summary>
        /// Get Customer by user name (Query parameters)
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(Customer))]
        [Route("api/AdministratorFacade/customerbyusername/search")]
        [HttpGet]
        public IHttpActionResult GetCustomerByUserName(string username = "")
        {
            IHttpActionResult res = null;
            Customer customer = null;
            GetLoginToken();
            if (adminLoginToken == null)
            {
                return Unauthorized();
            }
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInAdministratorFacade administratorFacade = FCS.GetFacade(adminLoginToken) as ILoggedInAdministratorFacade;
            customer = administratorFacade.GetCustomerByUserName(adminLoginToken, username);
            if (username != "")
            {
                customer = administratorFacade.GetCustomerByUserName(adminLoginToken, username);
                res = Ok(customer);
            }
            else if ((username != "" && customer == null) || username == "")
            {
                res = NotFound();
            }
            return res;
        }
    }
}
