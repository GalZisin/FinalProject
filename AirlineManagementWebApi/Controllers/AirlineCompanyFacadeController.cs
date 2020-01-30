using AirlineManagement;
using AirlineManagementWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace AirlineManagementWebApi.Controllers
{
    //[Authorize]
    [BasicAuthenticationAttribute]
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
        /// Get all flights
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(Flight))]
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
            IList<Flight> flights = airlineCompanyFacade.GetAllFlights(airlineCompanyLoginToken);

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
            Flight flight = null;
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
                    res = Ok($"Flight with ID = {flightId} not found");
                }
            }
            catch (Exception e1)
            {
                res = BadRequest("Flight hasn't been deleted " + e1.Message);
            }
            return res;
        }
        /// <summary>
        /// Create new flight and return it's id
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(Flight))]
        [Route("api/AirlineCompanyFacade/createflight", Name = "createflight")]
        [HttpPost]
        public IHttpActionResult CreateNewFlight([FromBody] Flight flight)
        {
            GetLoginToken();
            if (airlineCompanyLoginToken == null)
            {
                return Unauthorized();
            }
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInAirlineFacade airlineCompanyFacade = FCS.GetFacade(airlineCompanyLoginToken) as ILoggedInAirlineFacade;
            long flightId = airlineCompanyFacade.CreateFlight(airlineCompanyLoginToken, flight);
            flight = airlineCompanyFacade.GetFlightByFlightId(airlineCompanyLoginToken, flightId);
            return CreatedAtRoute("createflight", new { id = flightId }, flight);
        }
        /// <summary>
        /// Update flight
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(string))]
        [Route("api/AirlineCompanyFacade/updateflight/{id}")]
        [HttpPut]
        public IHttpActionResult UpdateFlight(int id, [FromBody] Flight flight)
        {
            GetLoginToken();
            if (airlineCompanyLoginToken == null)
            {
                return Unauthorized();
            }
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInAirlineFacade airlineCompanyFacade = FCS.GetFacade(airlineCompanyLoginToken) as ILoggedInAirlineFacade;
            flight = airlineCompanyFacade.GetFlightByFlightId(airlineCompanyLoginToken, id);
            if (flight == null)
            {
                return BadRequest("Id not found");
            }
            else
            {
                airlineCompanyFacade.UpdateFlight(airlineCompanyLoginToken, flight);
                return Ok($"Flight ID = {id} updated");
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
        /// Modify airline company details
        /// </summary>
        /// <param name="airlineCompany"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        [Route("api/AirlineCompanyFacade/modifayairline")]
        [HttpPut]
        public IHttpActionResult ModifyAirlineDetails([FromBody] AirlineCompany airlineCompany)
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
                airlineCompanyFacade.ModifyAirlineDetails(airlineCompanyLoginToken, airlineCompany);
                return Ok("Airline company details had been modified");
            }
            catch (Exception e1)
            {
                return BadRequest(e1.Message);
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
            IList<Flight> flights = airlineCompanyFacade.GetAllFlights(airlineCompanyLoginToken);
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
    }
}
