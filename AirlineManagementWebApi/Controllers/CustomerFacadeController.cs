using AirlineManagement;
using AirlineManagement.POCO.Views;
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
    [JwtAuthentication]
    public class CustomerFacadeController : ApiController
    {
        private FlyingCenterSystem FCS;
        private LoginToken<Customer> customerLoginToken;
        static readonly object key = new object();
        private void GetLoginToken()
        {
            Request.Properties.TryGetValue("token", out object loginToken);

            customerLoginToken = loginToken as LoginToken<Customer>;
        }
        /// <summary>
        /// Get all  customer flights (Query parameters)
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(FlightView))]
        [Route("api/CustomerFacade/allmyflights/search")]
        [HttpGet]
        public IHttpActionResult GetAllMyFlights(string username = "")
        {
            IList<FlightView> flights = null;
            IHttpActionResult result = null;
            GetLoginToken();
            if (customerLoginToken == null)
            {
                return Unauthorized();
            }
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInCustomerFacade customerFacade = FCS.GetFacade(customerLoginToken) as ILoggedInCustomerFacade;
            if (username != "")
            {
                flights = customerFacade.GetAllMyFlights(customerLoginToken, username);
                result = Ok(flights);
            }
            else if ((username != "" && flights.Count == 0) || username == "")
            {
                result = NotFound();
            }

            return result;

        }
        /// <summary>
        /// Get all tickets by customer id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [ResponseType(typeof(TicketView))]
        [Route("api/CustomerFacade/getAllTicketsByCustomerId/{customerId}")]
        [HttpGet]
        public IHttpActionResult GetAllTicketsByCustomerId([FromUri] long customerId)
        {
            IList<TicketView> tickets = null;
            GetLoginToken();
            if (customerLoginToken == null)
            {
                return Unauthorized();
            }
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInCustomerFacade customerFacade = FCS.GetFacade(customerLoginToken) as ILoggedInCustomerFacade;
            try
            {
                tickets = customerFacade.GetAllTicketsByCustomerId(customerLoginToken, customerId);
                if (tickets != null)
                {

                    return Ok(tickets);
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
        //[ResponseType(typeof(Ticket))]
        //[Route("api/CustomerFacade/purchaseticket", Name = "purchaseticket")]
        //[HttpPost]
        //public IHttpActionResult PurchaseTicket([FromBody] TicketView ticket)
        //{
        //    GetLoginToken();
        //    if (customerLoginToken == null)
        //    {
        //        return Unauthorized();
        //    }
        //    FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
        //    ILoggedInCustomerFacade customerFacade = FCS.GetFacade(customerLoginToken) as ILoggedInCustomerFacade;
        //    try
        //    {
        //        Ticket ticket1 = customerFacade.PurchaseTicket(customerLoginToken, ticket);

        //        return CreatedAtRoute("purchaseticket", new { id = ticket1.ID }, ticket1);
        //    }
        //    catch (Exception e1)
        //    {
        //        return BadRequest(e1.Message);
        //    }
        //}
        [ResponseType(typeof(void))]
        [Route("api/CustomerFacade/purchaseTickets", Name = "purchaseticket")]
        [HttpPost]
        public IHttpActionResult PurchaseTicketsForPassengers([FromBody] PassengersTickets orderedTickets)
        {
            GetLoginToken();
            if (customerLoginToken == null)
            {
                return Unauthorized();
            }
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInCustomerFacade customerFacade = FCS.GetFacade(customerLoginToken) as ILoggedInCustomerFacade;
            int length = 2 * orderedTickets.Tickets.Length;
            Ticket[] tickets = new Ticket[length];
            DateTime departureDate1 = DateTime.Parse(orderedTickets.Flights[0].DepartureTime);
            DateTime departureDate2 = DateTime.Parse(orderedTickets.Flights[1].DepartureTime);

            if (departureDate1.AddHours(3) > DateTime.Now && departureDate2.AddHours(3) > DateTime.Now)
            {
                Ticket ticket = null;
                int index = 0;
                for (int j = 0; j < orderedTickets.Flights.Length; j++)
                {
                    foreach (MyTicket t in orderedTickets.Tickets)
                    {
                        ticket = new Ticket();
                        ticket.FLIGHT_ID = orderedTickets.Flights[j].Id;
                        ticket.CUSTOMER_ID = customerLoginToken.User.ID;
                        ticket.FIRST_NAME = t.FirstName;
                        ticket.LAST_NAME = t.LastName;
                        tickets[index] = ticket;
                        index++;
                    }

                }
            }
            else
            {
                return Ok("Problem to purchase tickets, invalid dates.");
            }
            string result = "";
            bool isBadRequest = false;
            lock (key)
            {
                try
                {
                    customerFacade.PurchaseTickets(customerLoginToken, tickets);
                    result = "Tickets ordered succsesfully";
                }
                catch (NoFlightException e1)
                {
                    result = "flights does not exist";
                }

                catch (TicketAlreadyExistException e1)
                {
                    result = "Tickets already exist";
                }
                catch (NoTicketsException e1)
                {
                    result = "No tickets left";
                }
                catch (Exception e1)
                {
                    result = e1.Message;
                }
           
            }
            if (isBadRequest)
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
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
                //ticket = customerFacade.GetTicketByCustomerId(customerLoginToken, customerId);
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
        /// <summary>
        /// Update customer details
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        [Route("api/CustomerFacade/updateCustomer")]
        [HttpPut]
        public IHttpActionResult UpdateCustomerDetails([FromBody] Customer updatedCustomer)
        {
            GetLoginToken();
            if (customerLoginToken == null)
            {
                return Unauthorized();
            }
            Customer customer = null;
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInCustomerFacade customerFacade = FCS.GetFacade(customerLoginToken) as ILoggedInCustomerFacade;
            customer = customerFacade.GetCustomerByCustomerId(customerLoginToken, updatedCustomer.ID);
            if (customer == null)
            {
                return BadRequest("Id not found");
            }
            else
            {
                try
                {
                    customerFacade.UpdateCustomerDetails(customerLoginToken, updatedCustomer);
                    string newToken = TokenManager.GenerateToken(updatedCustomer.USER_NAME + ":" + updatedCustomer.PASSWORD);

                    return Ok(newToken);
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
                    return Ok($"Customer with ID = {updatedCustomer.ID} not updated, Customer already exist");
                }
                catch (Exception e1)
                {
                    return Ok($"Customer with ID = {updatedCustomer.ID} not updated, {e1.Message}");
                }
            }

        }
        [ResponseType(typeof(string))]
        [Route("api/CustomerFacade/deletecustomer/{customerId}")]
        [HttpDelete]
        public IHttpActionResult DeleteCustomer([FromUri] long customerId)
        {
            IHttpActionResult res = null;
            Customer customer = null;
            GetLoginToken();
            if (customerLoginToken == null)
            {
                return Unauthorized();
            }
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInCustomerFacade customerFacade = FCS.GetFacade(customerLoginToken) as ILoggedInCustomerFacade;
            try
            {
                customer = customerFacade.GetCustomerByCustomerId(customerLoginToken, customerId);
                if (customer != null)
                {
                    customerFacade.DeleteCustomer(customerLoginToken, customer);
                    res = Ok($"Customer with ID = {customerId} deleted succsesfully");
                }
            }
            catch (InvalidTokenException ex)
            {
                return Ok($"Customer not deleted, authorization error");
            }
            catch (CustomerDeleteErrorException e1)
            {
                return Ok($"Customer with ID = {customerId} not deleted");
            }
            catch (Exception e1)
            {
                res = BadRequest("Customer hasn't been deleted " + e1.Message);
            }
            return res;
        }
        /// <summary>
        /// Get customer by customer id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [ResponseType(typeof(Customer))]
        [Route("api/CustomerFacade/getCustomerByCustomerId/{customerId}")]
        [HttpGet]
        public IHttpActionResult GetCustomerByCustomerId([FromUri] long customerId)
        {
            Customer customer = null;
            GetLoginToken();
            if (customerLoginToken == null)
            {
                return Unauthorized();
            }
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInCustomerFacade customerFacade = FCS.GetFacade(customerLoginToken) as ILoggedInCustomerFacade;
            try
            {
                customer = customerFacade.GetCustomerByCustomerId(customerLoginToken, customerId);
                if (customer != null)
                {

                    return Ok(customer);
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
        [ResponseType(typeof(TicketView))]
        [Route("api/CustomerFacade/getTicketByTicketId/{ticketId}")]
        [HttpGet]
        public IHttpActionResult GetTicketByTicketId([FromUri] long ticketId)
        {
            TicketView ticket = null;
            GetLoginToken();
            if (customerLoginToken == null)
            {
                return Unauthorized();
            }
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInCustomerFacade customerFacade = FCS.GetFacade(customerLoginToken) as ILoggedInCustomerFacade;
            try
            {
                //ticket = customerFacade.GetTicketByTicketId(customerLoginToken, ticketId);
                if (ticket != null)
                {

                    return Ok(ticket);
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
        /// Get customer Id
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(long))]
        [Route("api/CustomerFacade/getCustomerId")]
        [HttpGet]
        public IHttpActionResult GetCustomerId()
        {
            GetLoginToken();
            if (customerLoginToken == null)
            {
                return Unauthorized();
            }
            try
            {
                return Ok(customerLoginToken.User.ID);
            }
            catch (Exception e1)
            {
                return Ok(e1.Message);
            }

        }
        /// <summary>
        /// Get customer data
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(Customer))]
        [Route("api/CustomerFacade/getCustomer")]
        [HttpGet]
        public IHttpActionResult GetCustomer()
        {
            GetLoginToken();
            if (customerLoginToken == null)
            {
                return Unauthorized();
            }
            Customer customer = null;
            customer = customerLoginToken.User;
            return Ok(customer); ;
        }
        /// <summary>
        /// Get customer first name
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(string))]
        [Route("api/CustomerFacade/getCustomerFirstName")]
        [HttpPost]
        public IHttpActionResult ShowFirstNameOnLogin()
        {

            GetLoginToken();
            if (customerLoginToken == null)
            {
                return Unauthorized();
            }
            return Ok(customerLoginToken.User.FIRST_NAME); ;
        }
    }
}
