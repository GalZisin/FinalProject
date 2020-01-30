using AirlineManagement;
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
    [BasicAuthentication]
    public class CustomerFacadeController : ApiController
    {
        private FlyingCenterSystem FCS;
        private LoginToken<Customer> customerLoginToken;
       
        private void GetLoginToken()
        {
            Request.Properties.TryGetValue("token", out object loginToken);

            customerLoginToken = loginToken as LoginToken<Customer>;
        }
        /// <summary>
        /// Get all  customer flights (Query parameters)
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(Flight))]
        [Route("api/CustomerFacade/allmyflights/search")]
        [HttpGet]
        public IHttpActionResult GetAllMyFlights(string username ="")
        {
            IList<Flight> flights = null;
            IHttpActionResult result = null;
            GetLoginToken();
            if (customerLoginToken == null)
            {
                return Unauthorized();
            }
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInCustomerFacade customerFacade = FCS.GetFacade(customerLoginToken) as ILoggedInCustomerFacade;
            if(username != "")
            {
                flights = customerFacade.GetAllMyFlights(customerLoginToken, username);
                result = Ok(flights);
            }
            else if((username != "" && flights.Count == 0) || username == "")
            {
                result = NotFound();
            }

            return result;
        
        }
        [ResponseType(typeof(Ticket))]
        [Route("api/CustomerFacade/purchaseticket", Name = "purchaseticket")]
        [HttpPost]
        public IHttpActionResult PurchaseTicket([FromBody] Flight flight)
        {
            GetLoginToken();
            if (customerLoginToken == null)
            {
                return Unauthorized();
            }
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInCustomerFacade customerFacade = FCS.GetFacade(customerLoginToken) as ILoggedInCustomerFacade;
            try
            {
                Ticket ticket = customerFacade.PurchaseTicket(customerLoginToken, flight);

                return CreatedAtRoute("purchaseticket", new { id = ticket.ID }, ticket);
            }
            catch (Exception e1)
            {
                return BadRequest(e1.Message);
            }
        }
        [ResponseType(typeof(string))]
        [Route("api/CustomerFacade/deleteticket/{customerId}")]
        [HttpDelete]
        public IHttpActionResult RemoveCustomer([FromUri] long customerId)
        {
            IHttpActionResult res = null;
            Ticket ticket = null;
            GetLoginToken();
            if (customerLoginToken == null)
            {
                return Unauthorized();
            }
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInCustomerFacade customerFacade = FCS.GetFacade(customerLoginToken) as ILoggedInCustomerFacade;
            try
            {
                ticket = customerFacade.GetTicketByCustomerId(customerLoginToken, customerId);
                if (ticket != null)
                {
                    customerFacade.CancelTicket(customerLoginToken, ticket);
                    res = Ok($"Ticket with customer Id = {customerId} not found");
                }
            }
            catch (Exception e1)
            {
                res = BadRequest("Ticket hadn't been deleted " + e1.Message);
            }
            return res;
        }
    }
}
