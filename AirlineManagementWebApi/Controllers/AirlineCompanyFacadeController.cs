using AirlineManagement;
using AirlineManagement.POCO.Views;
using AirlineManagementWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Description;

namespace AirlineManagementWebApi.Controllers
{
    //[Authorize]
    [JwtAuthenticationAttribute]
    //[CustomAuthorize]
    public class AirlineCompanyFacadeController : ApiController
    {
        private FlyingCenterSystem FCS;
        private LoginToken<AirlineCompany> airlineCompanyLoginToken;
        private void GetLoginToken()
        {
            Request.Properties.TryGetValue("token", out object loginToken);

            airlineCompanyLoginToken = loginToken as LoginToken<AirlineCompany>;
        }
        /// <summary>
        /// Get all tickets
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(Ticket))]
        [Route("api/AirlineCompanyFacade/alltickets")]
        [HttpGet]
        public IHttpActionResult GetAllTickets()
        {
            GetLoginToken();
            if (airlineCompanyLoginToken == null)
            {
                return Unauthorized();
            }
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInAirlineFacade airlineCompanyFacade = FCS.GetFacade(airlineCompanyLoginToken) as ILoggedInAirlineFacade;
            IList<Ticket> Tickets = airlineCompanyFacade.GetAllTickets(airlineCompanyLoginToken);

            if (Tickets.Count == 0)
            {
                return NotFound();
            }
            return Ok(Tickets);
        }
        /// <summary>
        /// Get all countries
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(Country))]
        [Route("api/AirlineCompanyFacade/getAllCountries")]
        [HttpGet]
        public IHttpActionResult GetAllCountries()
       {
            GetLoginToken();
            if (airlineCompanyLoginToken == null)
            {
                return Unauthorized();
            }
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInAirlineFacade airlineCompanyFacade = FCS.GetFacade(airlineCompanyLoginToken) as ILoggedInAirlineFacade;
            IList<Country> countries = airlineCompanyFacade.GetAllCountries(airlineCompanyLoginToken);

            if (countries.Count == 0)
            {
                return NotFound();
            }
            return Ok(countries);
        }
        /// <summary>
        /// Get all flights
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(FlightView))]
        [Route("api/AirlineCompanyFacade/allflights")]
        [HttpGet]
        public IHttpActionResult GetAllFlights()
        {
            GetLoginToken();
            if (airlineCompanyLoginToken == null)
            {
                return Unauthorized();
            }
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInAirlineFacade airlineCompanyFacade = FCS.GetFacade(airlineCompanyLoginToken) as ILoggedInAirlineFacade;
            IList<FlightView> flights = airlineCompanyFacade.GetAllFlights(airlineCompanyLoginToken);

            if (flights.Count == 0)
            {
                return NotFound();
            }
            return Ok(flights);
        }
        /// <summary>
        /// Remove Flight
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(string))]
        [Route("api/AirlineCompanyFacade/deleteflight/{flightId}")]
        [HttpDelete]
        public IHttpActionResult RemoveFlight([FromUri] long flightId)
        {
            IHttpActionResult res = null;
            FlightView flight = null;
            GetLoginToken();
            if (airlineCompanyLoginToken == null)
            {
                return Unauthorized();
            }
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInAirlineFacade airlineCompanyFacade = FCS.GetFacade(airlineCompanyLoginToken) as ILoggedInAirlineFacade;
            try
            {
                flight = airlineCompanyFacade.GetFlightByFlightId(airlineCompanyLoginToken, flightId);
                if (flight != null)
                {
                    airlineCompanyFacade.CancelFlight(airlineCompanyLoginToken, flight);
                    res = Ok($"Flight with ID = {flightId} deleted succsesfully");
                }
            }
            catch (InvalidTokenException ex)
            {
                return Ok($"Flight not deleted, authorization error");
            }
            catch (FlightDeleteErrorException e1)
            {
                return Ok($"Flight not deleted, flight with ID = {flightId} not found");
            }
            catch (Exception e1)
            {
                res = BadRequest("Flight hasn't been deleted " + e1.Message);
            }
            return res;
        }
        [ResponseType(typeof(string))]
        [Route("api/AirlineCompanyFacade/deletecompany/{companyId}")]
        [HttpDelete]
        public IHttpActionResult DeleteAirlineCompany([FromUri] long companyId)
        {
            IHttpActionResult res = null;
            AirlineCompany airlineCompany = null;
            GetLoginToken();
            if (airlineCompanyLoginToken == null)
            {
                return Unauthorized();
            }
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInAirlineFacade airlineCompanyFacade = FCS.GetFacade(airlineCompanyLoginToken) as ILoggedInAirlineFacade;
            try
            {
                airlineCompany = airlineCompanyFacade.GetAirlineCompanyByAirlineCompanyId(airlineCompanyLoginToken, companyId);
                if (airlineCompany != null)
                {
                    airlineCompanyFacade.DeleteAirlineCompany(airlineCompanyLoginToken, airlineCompany);
                    res = Ok($"Airline Company with ID = {companyId} deleted succsesfully");
                }
            }
            catch (InvalidTokenException ex)
            {
                return Ok($"Airline Company not deleted, authorization error");
            }
            catch (AirlineCompanyDeleteErrorException e1)
            {
                return Ok($"Airline Company with ID = {companyId} not deleted");
            }
            catch (Exception e1)
            {
                res = BadRequest("Airline company hasn't been deleted " + e1.Message);
            }
            return res;
        }
        /// <summary>
        /// Create new flight and return it's id
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(FlightView))]
        [Route("api/AirlineCompanyFacade/createflight", Name = "createflight")]
        [HttpPost]
        public IHttpActionResult CreateNewFlight([FromBody] FlightView flight)
        {
            GetLoginToken();
            if (airlineCompanyLoginToken == null)
            {
                return Unauthorized();
            }
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInAirlineFacade airlineCompanyFacade = FCS.GetFacade(airlineCompanyLoginToken) as ILoggedInAirlineFacade;
            try
            {
                long flightId = airlineCompanyFacade.CreateFlight(airlineCompanyLoginToken, flight);
                flight = airlineCompanyFacade.GetFlightByFlightId(airlineCompanyLoginToken, flightId);
                //return CreatedAtRoute("createflight", new { id = flightId }, flight);
                return Ok("Flight created succsesfully");
            }
            catch (InvalidTokenException ex)
            {
                return Ok($"Flight not created, authorization error");

            }
            catch (FlightAlreadyExistException ex)
            {
                return Ok($"Flight already exist");
            }
            catch (Exception e1)
            {
                return Ok($"Flight not created, {e1.Message}");
            }
        }
        /// <summary>
        /// Get flight by flight ID
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        [ResponseType(typeof(Flight))]
        [Route("api/AirlineCompanyFacade/getFlightByFlightId/{flightId}")]
        [HttpGet]
        public IHttpActionResult GetFlightByFlightId([FromUri] long flightId)
        {
            GetLoginToken();
            if (airlineCompanyLoginToken == null)
            {
                return Unauthorized();
            }
            Flight flight = null;
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInAirlineFacade airlineCompanyFacade = FCS.GetFacade(airlineCompanyLoginToken) as ILoggedInAirlineFacade;
            flight = airlineCompanyFacade.GetFlightByFlightId(airlineCompanyLoginToken, flightId);
            if (flight == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(flight);
            }
            //return CreatedAtRoute("createflight", new { id = flightId }, flight);
        }
        /// <summary>
        /// Update flight
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(string))]
        [Route("api/AirlineCompanyFacade/updateflight")]
        [HttpPut]
        public IHttpActionResult UpdateFlight([FromBody] FlightView updatedFlight)
        {
            GetLoginToken();
            if (airlineCompanyLoginToken == null)
            {
                return Unauthorized();
            }
            FlightView flight = null;
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInAirlineFacade airlineCompanyFacade = FCS.GetFacade(airlineCompanyLoginToken) as ILoggedInAirlineFacade;
            flight = airlineCompanyFacade.GetFlightByFlightId(airlineCompanyLoginToken, updatedFlight.ID);
            if (flight == null)
            {
                return BadRequest("Id not found");
            }
            else
            {
                try
                {
                    airlineCompanyFacade.UpdateFlight(airlineCompanyLoginToken, updatedFlight);
                    return Ok($"Flight with ID = {updatedFlight.ID} updated succsesfully");
                }
                catch (InvalidTokenException ex)
                {
                    return Ok($"Flight with ID = {updatedFlight.ID} not updated, authorization error");
                }
                catch (FlightUpdateErrorException ex)
                {
                    return Ok($"Flight with ID = {updatedFlight.ID} not updated, { ex.Message}");
                }
                catch (FlightAlreadyExistException ex)
                {
                    return Ok($"Flight with ID = {updatedFlight.ID} not updated, Flight already exist");
                }
                catch (Exception e1)
                {
                    return Ok($"Flight with ID = {updatedFlight.ID} not updated, {e1.Message}");
                }
            }

        }

        /// <summary>
        /// Change old password
        /// </summary>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(string))]
        [Route("api/AirlineCompanyFacade/changepassword")]
        [HttpPut]
        public IHttpActionResult ChangeMyPassword([FromBody] ChangePassword changePass)
        {
            GetLoginToken();
            if (airlineCompanyLoginToken == null)
            {
                return Unauthorized();
            }
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInAirlineFacade airlineCompanyFacade = FCS.GetFacade(airlineCompanyLoginToken) as ILoggedInAirlineFacade;
            try
            {
                airlineCompanyFacade.ChangeMyPassword(airlineCompanyLoginToken, changePass.oldPassword, changePass.newPassword);
                return Ok("Password changed");
            }
            catch (Exception e1)
            {
                return BadRequest(e1.Message);
            }

        }
        /// <summary>
        /// Update airline company details
        /// </summary>
        /// <param name="airlineCompany"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        [Route("api/AirlineCompanyFacade/updateairlinecompany")]
        [HttpPut]
        public IHttpActionResult UpdateAirlineDetails([FromBody] AirlineCompany updatedAirlineCompany)
        {
            GetLoginToken();
            if (airlineCompanyLoginToken == null)
            {
                return Unauthorized();
            }
            AirlineCompany airlineCompany = null;
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInAirlineFacade airlineCompanyFacade = FCS.GetFacade(airlineCompanyLoginToken) as ILoggedInAirlineFacade;
            airlineCompany = airlineCompanyFacade.GetAirlineCompanyByAirlineCompanyId(airlineCompanyLoginToken, updatedAirlineCompany.ID);
            if (airlineCompany == null)
            {
                return BadRequest("Id not found");
            }
            else
            {
                try
                {
                    airlineCompanyFacade.UpdateAirlineDetails(airlineCompanyLoginToken, updatedAirlineCompany);
                    string newToken = TokenManager.GenerateToken(updatedAirlineCompany.USER_NAME + ":" + updatedAirlineCompany.PASSWORD);
               
              
                    return Ok(newToken);
                    //return Ok($"Airline Company with ID = {updatedAirlineCompany.ID} updated succsesfully");
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
        /// <summary>
        /// Get customer by username (Query parameters)
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(Customer))]
        [Route("api/AirlineCompanyFacade/customerbyusername/search")]
        [HttpGet]
        public IHttpActionResult GetCustomerByUserName(string username = "")
        {
            IHttpActionResult res = null;
            Customer customer = null;
            GetLoginToken();
            if (airlineCompanyLoginToken == null)
            {
                return Unauthorized();
            }
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInAirlineFacade airlineCompanyFacade = FCS.GetFacade(airlineCompanyLoginToken) as ILoggedInAirlineFacade;
            if (username != "")
            {
                customer = airlineCompanyFacade.GetCustomerByUserName(airlineCompanyLoginToken, username);
                res = Ok(customer);
            }
            else if ((username != "" && customer == null) || username == "")
            {
                res = NotFound();
            }

            return res;
        }
        [ResponseType(typeof(FlightView))]
        [Route("api/AirlineCompanyFacade/GetAllFlightsByAirlineCompanyId/{companyId}")]
        [HttpGet]
        public IHttpActionResult GetAllFlightsByCompanyId([FromUri] long companyId)
        {
            IList<FlightView> flights = null;
            GetLoginToken();
            if (airlineCompanyLoginToken == null)
            {
                return Unauthorized();
            }
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInAirlineFacade airlineCompanyFacade = FCS.GetFacade(airlineCompanyLoginToken) as ILoggedInAirlineFacade;
            try
            {
                flights = airlineCompanyFacade.GetAllFlightsByCompanyId(airlineCompanyLoginToken, companyId);
                if (flights != null)
                {

                    return Ok(flights);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e1)
            {
                return NotFound();
            }

        }
        /// <summary>
        /// Get Airline company by company name
        /// </summary>
        /// <param name="companyName"></param>
        /// <returns></returns>
        [ResponseType(typeof(AirlineCompany))]
        [Route("api/AirlineCompanyFacade/GetAirlineCompanyByAirlineName/{companyName}")]
        [HttpGet]
        public IHttpActionResult GetAirlineCompanyByAirlineName([FromUri] string companyName)
        {
            AirlineCompany airlineCompany = null;
            GetLoginToken();
            if (airlineCompanyLoginToken == null)
            {
                return Unauthorized();
            }
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInAirlineFacade airlineCompanyFacade = FCS.GetFacade(airlineCompanyLoginToken) as ILoggedInAirlineFacade;
            try
            {
                airlineCompany = airlineCompanyFacade.GetAirlineCompanyByAirlineName(airlineCompanyLoginToken, companyName);
                if (airlineCompany != null)
                {

                    return Ok(airlineCompany);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e1)
            {
                return NotFound();
            }

        }
        [ResponseType(typeof(AirlineCompany))]
        [Route("api/AirlineCompanyFacade/getAirlineCompanyByAirlineId/{companyId}")]
        [HttpGet]
        public IHttpActionResult GetAirlineCompanyByAirlineCompanyId([FromUri] long companyId)
        {
            AirlineCompany airlineCompany = null;
            GetLoginToken();
            if (airlineCompanyLoginToken == null)
            {
                return Unauthorized();
            }
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInAirlineFacade airlineCompanyFacade = FCS.GetFacade(airlineCompanyLoginToken) as ILoggedInAirlineFacade;
            try
            {
                airlineCompany = airlineCompanyFacade.GetAirlineCompanyByAirlineCompanyId(airlineCompanyLoginToken, companyId);
                if (airlineCompany != null)
                {

                    return Ok(airlineCompany);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e1)
            {
                return NotFound();
            }

        }
        [ResponseType(typeof(AirlineCompany))]
        [Route("api/AirlineCompanyFacade/getAirlineCompany")]
        [HttpGet]
        public IHttpActionResult GetAirlineCompany()
        {
            GetLoginToken();
            if (airlineCompanyLoginToken == null)
            {
                return Unauthorized();
            }
            AirlineCompany airlineCompany = null;
            airlineCompany = airlineCompanyLoginToken.User;
            return Ok(airlineCompany); ;
        }
        /// <summary>
        /// Get Airline Company ID
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(string))]
        [Route("api/AirlineCompanyFacade/getAirlineCompanyId")]
        [HttpGet]
        public IHttpActionResult GetAirlineCompanyId()
        {
            GetLoginToken();
            if (airlineCompanyLoginToken == null)
            {
                return Unauthorized();
            }
            return Ok(airlineCompanyLoginToken.User.ID); ;
        }
        /// <summary>
        /// Get airline company name
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(string))]
        [Route("api/AirlineCompanyFacade/getAirlineCompanyName")]
        [HttpPost]
        public IHttpActionResult ShowFirstNameOnLogin()
        {
            GetLoginToken();
            if (airlineCompanyLoginToken == null)
            {
                return Unauthorized();
            }
            return Ok(airlineCompanyLoginToken.User.AIRLINE_NAME); ;
        }
    }
}
