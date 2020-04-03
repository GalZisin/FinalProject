using AirlineManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ServiceStack.Redis;
using AirlineManagementWebApi.Models;

namespace AirlineManagementWebApi.Controllers
{
    //[Authorize]
    [JwtAuthentication]
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
        /// Create new admin company and return it's id
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(AirlineCompany))]
        [Route("api/AdministratorFacade/createadmin", Name = "createadmin")]
        [HttpPost]
        public IHttpActionResult CreateNewAdministrator([FromBody] Administrator admin)
        {
            GetLoginToken();
            if (adminLoginToken == null)
            {
                return Unauthorized();
            }
            //IHttpActionResult res = null;
            long adminId = 0;
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInAdministratorFacade administratorFacade = FCS.GetFacade(adminLoginToken) as ILoggedInAdministratorFacade;
            try
            {
                adminId = administratorFacade.CreateNewAdministrator(adminLoginToken, admin);
                admin = administratorFacade.GetAdministratorById(adminLoginToken, adminId);
                return Ok("Administrator account created succsesfully ");
                //return CreatedAtRoute("createairlinecompany", new { id = airlineCompanyId }, airlineCompany);
            }
            catch (InvalidTokenException ex)
            {
                return Ok($"Administrator not created, authorization error");
            }
            catch (AdministratorAlreadyExistException ex)
            {
                return Ok($"Administrator already exist");
            }
            catch (Exception e1)
            {
                return Ok($"Administrator not created, {e1.Message}");
            }


        }
        [ResponseType(typeof(string))]
        [Route("api/AdministratorFacade/updateadmin")]
        [HttpPut]
        public IHttpActionResult UpdateAdministratorDetails([FromBody] Administrator updatedAdministrator)
        {
            GetLoginToken();
            if (adminLoginToken == null)
            {
                return Unauthorized();
            }
            Administrator administrator = null;
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInAdministratorFacade administratorFacade = FCS.GetFacade(adminLoginToken) as ILoggedInAdministratorFacade;
            administrator = administratorFacade.GetAdministratorById(adminLoginToken, updatedAdministrator.ID);
            if (administrator == null)
            {
                return BadRequest("Id not found");
            }
            else
            {
                try
                {
                    administratorFacade.UpdateAdministratorDetails(adminLoginToken, updatedAdministrator);
                    return Ok($"Administrator with ID = {updatedAdministrator.ID} updated succsesfully");
                }
                catch (InvalidTokenException ex)
                {
                    return Ok($"Administrator with ID = {updatedAdministrator.ID} not updated, authorization error");
                }
                catch (AdministratorUpdateErrorException ex)
                {
                    return Ok($"Administrator with ID = {updatedAdministrator.ID} not updated, { ex.Message}");
                }
                catch (AdministratorAlreadyExistException ex)
                {
                    return Ok($"Administrator with ID = {updatedAdministrator.ID} not updated, administrator already exist");
                }
                catch (Exception e1)
                {
                    return Ok($"Administrator with ID = {updatedAdministrator.ID} not updated, {e1.Message}");
                }
            }
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
            //IHttpActionResult res = null;
            long airlineCompanyId = 0;
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInAdministratorFacade administratorFacade = FCS.GetFacade(adminLoginToken) as ILoggedInAdministratorFacade;
            try
            {
                airlineCompanyId = administratorFacade.CreateNewAirline(adminLoginToken, airlineCompany);
                airlineCompany = administratorFacade.GetAirlineCompanyById(adminLoginToken, airlineCompanyId);
               return Ok("AirlineCompany account created succsesfully ");
                //return CreatedAtRoute("createairlinecompany", new { id = airlineCompanyId }, airlineCompany);
            }
            catch (InvalidTokenException ex)
            {
                return Ok($"AirlineCompany not created, authorization error");
            }
            catch (AirlineCompanyAlreadyExistException ex)
            {
                return Ok($"AirlineCompany already exist");
            }
            catch (Exception e1)
            {
                return Ok($"AirlineCompany not created, {e1.Message}");
            }
  

        }
        /// <summary>
        /// Update airline company
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(string))]
        [Route("api/AdministratorFacade/updateairlinecompany")]
        [HttpPut]
        public IHttpActionResult UpdateAirlineDetails([FromBody] AirlineCompany updatedAirlineCompany)
        {
            GetLoginToken();
            if (adminLoginToken == null)
            {
                return Unauthorized();
            }
            AirlineCompany airlineCompany = null;
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInAdministratorFacade administratorFacade = FCS.GetFacade(adminLoginToken) as ILoggedInAdministratorFacade;
            airlineCompany = administratorFacade.GetAirlineCompanyById(adminLoginToken, updatedAirlineCompany.ID);
            if (airlineCompany == null)
            {
                return BadRequest("Id not found");
            }
            else
            {
                try
                {
                    administratorFacade.UpdateAirlineDetails(adminLoginToken, updatedAirlineCompany);
                    return Ok($"Airline Company with ID = {updatedAirlineCompany.ID} updated succsesfully");
                }

                catch (InvalidTokenException ex)
                {
                    return Ok($"Airline Company with ID = {updatedAirlineCompany.ID} not updated, authorization error");
                }
                catch (AirlineCompanyUpdateErrorException ex)
                {
                    return Ok($"Airline Company with ID = {updatedAirlineCompany.ID} not updated, {ex.Message}");
                }
                catch (AirlineCompanyAlreadyExistException ex)
                {
                    return Ok($"Airline Company with ID = {updatedAirlineCompany.ID} not updated, Airline Company already exist");
                }
                catch (Exception e1)
                {
                    return Ok($"Airline Company with ID = {updatedAirlineCompany.ID} not updated, {e1.Message}");
                }
            }
        }

        [ResponseType(typeof(string))]
        [Route("api/AdministratorFacade/updatecustomer")]
        [HttpPut]
        public IHttpActionResult UpdateCustomerDetails([FromBody] Customer updatedCustomer)
        {
            GetLoginToken();
            if (adminLoginToken == null)
            {
                return Unauthorized();
            }
            Customer customer = null;
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInAdministratorFacade administratorFacade = FCS.GetFacade(adminLoginToken) as ILoggedInAdministratorFacade;
            customer = administratorFacade.GetCustomerById(adminLoginToken, updatedCustomer.ID);
            if (customer == null)
            {
                return BadRequest("Id not found");
            }
            else
            {
                try
                {
                    administratorFacade.UpdateCustomerDetails(adminLoginToken, updatedCustomer);
                    return Ok($"Customer with ID = {updatedCustomer.ID} updated succsesfully");
                }
                catch (InvalidTokenException ex)
                {
                    return Ok($"Customer with ID = {updatedCustomer.ID} not updated, authorization error");
                }
                catch (CustomerUpdateErrorException ex)
                {
                    return Ok($"Customer with ID = {updatedCustomer.ID} not updated, {ex.Message}");
                }
                catch (CustomerAlreadyExistException ex)
                {
                    return Ok($"Customer with ID = {updatedCustomer.ID} not updated, customer already exist");
                }
                catch (Exception e1)
                {
                    return Ok($"Customer with ID = {updatedCustomer.ID} not updated, {e1.Message}");
                }

            }
        }
        /// <summary>
        /// Remove airline company by id
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(string))]
        [Route("api/AdministratorFacade/deletecompany/{id}")]
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
                    res = Ok($"Airline company with ID = {id} deleted succsesfully");
                }
            }
            catch (InvalidTokenException ex)
            {
                return Ok($"Airline company not deleted, authorization error");
            }
            catch (AirlineCompanyDeleteErrorException e1)
            {
                return Ok($"Airline company not deleted, airline company with ID = {id} not found");
            }
            catch (Exception e1)
            {
                res = BadRequest("Airline company hasn't been deleted " + e1.Message);
            }
            return res;
        }
        /// <summary>
        /// Remove administrator company by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(string))]
        [Route("api/AdministratorFacade/deleteadmin/{id}")]
        [HttpDelete]
        public IHttpActionResult RemoveAdministrator([FromUri]long id)
        {
            Administrator administrator = null;
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
                administrator = administratorFacade.GetAdministratorById(adminLoginToken, id);
                if (administrator != null)
                {
                    administratorFacade.RemoveAdministrator(adminLoginToken, administrator);
                    res = Ok($"Administrator with ID = {id} deleted succsesfully");
                }
            }
            catch (InvalidTokenException ex)
            {
                return Ok($"Administrator not deleted, authorization error");
            }
            catch (AirlineCompanyDeleteErrorException e1)
            {
                return Ok($"Administrator not deleted, Administrator with ID = {id} not found");
            }
            catch (Exception e1)
            {
                res = BadRequest("Administrator hasn't been deleted " + e1.Message);
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
            long customerId = 0;
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInAdministratorFacade administratorFacade = FCS.GetFacade(adminLoginToken) as ILoggedInAdministratorFacade;
            try
            {
                customerId = administratorFacade.CreateNewCustomer(adminLoginToken, customer);
                customer = administratorFacade.GetCustomerById(adminLoginToken, customerId);
                return Ok("Customer account created succsesfully ");
                //return CreatedAtRoute("createcustomer", new { id = customerId }, customer);
            }
            catch (InvalidTokenException ex)
            {
                return Ok($"Customer not created, authorization error");
            }
            catch (CustomerAlreadyExistException ex)
            {
                return Ok($"Customer already exist");
            }
            catch (Exception e1)
            {
                return Ok($"Customer not created, {e1.Message}");
            }

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
                    res = Ok($"Customer with ID = {id} deleted succsesfully");
                }
            }
            catch (InvalidTokenException ex)
            {
                return Ok($"Customer not deleted, authorization error");
            }
            catch (CustomerDeleteErrorException e1)
            {
                return Ok($"Customer not deleted, customer with ID = {id} not found");
            }
            catch (Exception e1)
            {
                return BadRequest("Customer hasn't been deleted " + e1.Message);
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
            if (adminLoginToken == null)
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
        [ResponseType(typeof(AirlineCompany))]
        [Route("api/AdministratorFacade/getallairlinecompanies")]
        [HttpPost]
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
        /// Get all customers
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(Customer))]
        [Route("api/AdministratorFacade/getAllCustomers")]
        [HttpPost]
        public IHttpActionResult GetAllCustomers()
        {
            GetLoginToken();
            if (adminLoginToken == null)
            {
                return Unauthorized();
            }
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInAdministratorFacade administratorFacade = FCS.GetFacade(adminLoginToken) as ILoggedInAdministratorFacade;
            IList<Customer> customers = administratorFacade.GetAllCustomers(adminLoginToken);

            if (customers.Count == 0)
            {
                return NotFound();
            }
            return Ok(customers);
        }
        /// <summary>
        /// Get all Administrators
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(Administrator))]
        [Route("api/AdministratorFacade/getAllAdministrators")]
        [HttpPost]
        public IHttpActionResult GetAllAdministrators()
        {
            GetLoginToken();
            if (adminLoginToken == null)
            {
                return Unauthorized();
            }
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInAdministratorFacade administratorFacade = FCS.GetFacade(adminLoginToken) as ILoggedInAdministratorFacade;
            IList<Administrator> administrators = administratorFacade.GetAllAdministrators(adminLoginToken);

            if (administrators.Count == 0)
            {
                return NotFound();
            }
            return Ok(administrators);
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
        [ResponseType(typeof(Customer))]
        [Route("api/AdministratorFacade/getCustomerById")]
        [HttpPost]
        public IHttpActionResult GetCustomerById([FromBody] CustomerDetailsAdmin customerDetails_Id)
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
            CustomerDetailsAdmin customerId = new CustomerDetailsAdmin { };
            customerId.id = customerDetails_Id.id;
            customer = administratorFacade.GetCustomerById(adminLoginToken, customerId.id);
            if (customer != null)
            {
                res = Ok(customer);
            }
            else
            {
                res = NotFound();
            }
            return res;
        }
        [ResponseType(typeof(AirlineCompany))]
        [Route("api/AdministratorFacade/getAirlineCompanyById")]
        [HttpPost]
        public IHttpActionResult GetAirlineCompanyById([FromBody] AirlineCompanyDetailsAdmin airlineCompanyDetails_Id)
        {
            IHttpActionResult res = null;
            AirlineCompany airlineCompany = null;
            GetLoginToken();
            if (adminLoginToken == null)
            {
                return Unauthorized();
            }
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInAdministratorFacade administratorFacade = FCS.GetFacade(adminLoginToken) as ILoggedInAdministratorFacade;
            AirlineCompanyDetailsAdmin airlineCompanyId = new AirlineCompanyDetailsAdmin { };
            airlineCompanyId.id = airlineCompanyDetails_Id.id;
            airlineCompany = administratorFacade.GetAirlineCompanyById(adminLoginToken, airlineCompanyId.id);
            if (airlineCompany != null)
            {
                res = Ok(airlineCompany);
            }
            else
            {
                res = NotFound();
            }
            return res;
        }
        [ResponseType(typeof(Administrator))]
        [Route("api/AdministratorFacade/getAdministratorById")]
        [HttpPost]
        public IHttpActionResult GetAdministratorById([FromBody] AdministratorDetailsAdmin administratorDetails_Id)
        {
            IHttpActionResult res = null;
            Administrator administrator = null;
            GetLoginToken();
            if (adminLoginToken == null)
            {
                return Unauthorized();
            }
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInAdministratorFacade administratorFacade = FCS.GetFacade(adminLoginToken) as ILoggedInAdministratorFacade;
            AdministratorDetailsAdmin administratorId = new AdministratorDetailsAdmin { };
            administratorId.id = administratorDetails_Id.id;
            administrator = administratorFacade.GetAdministratorById(adminLoginToken, administratorId.id);
            if (administrator != null)
            {
                res = Ok(administrator);
            }
            else
            {
                res = NotFound();
            }
            return res;
        }
        [ResponseType(typeof(string))]
        [Route("api/AdministratorFacade/getAdministratorFirstName")]
        [HttpPost]
        public IHttpActionResult ShowFirstNameOnLogin()
        {
            GetLoginToken();
            if (adminLoginToken == null)
            {
                return Unauthorized();
            }
            return Ok(adminLoginToken.User.FIRST_NAME); ;
        }
    }
}
