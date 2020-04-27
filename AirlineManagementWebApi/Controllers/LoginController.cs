using AirlineManagement;
using AirlineManagementWebApi.Models;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Web;
using System.Web.Http;


namespace AirlineManagementWebApi.Controllers
{
    public class LoginController : ApiController
    {
        private FlyingCenterSystem FCS;
        private ILoginToken loginToken = null;

        [Route("api/Login/UserLogin")]
        [HttpPost]
        public IHttpActionResult Authenticate([FromBody] LoginRequest login)
        {
            var loginResponse = new LoginResponse { };
            LoginRequest loginrequest = new LoginRequest { };
            loginrequest.Username = login.Username.ToLower();
            loginrequest.Password = login.Password;

            IHttpActionResult response;
            // HttpResponseMessage response=null;
            HttpResponseMessage responseMsg = new HttpResponseMessage();
            //bool isUsernamePasswordValid = false;

            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            try
            {
                loginToken = FCS.Login(loginrequest.Username, loginrequest.Password);
            }
            catch(WrongPasswordException e1)
            {
                // if credentials are not valid send unauthorized status code in response
                loginResponse.responseMsg.StatusCode = HttpStatusCode.Unauthorized;
                //return Request.CreateResponse(HttpStatusCode.Unauthorized);
                response = ResponseMessage(loginResponse.responseMsg);
                return response;

                //return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "I am unauthorized IHttpActionResult custom message!"));
            }
            catch (Exception e)
            {
                // if credentials are not valid send unauthorized status code in response
                loginResponse.responseMsg.StatusCode = HttpStatusCode.InternalServerError;
                //return Request.CreateResponse(HttpStatusCode.Unauthorized);
                response = ResponseMessage(loginResponse.responseMsg);
                return response;
            }
            
            
            string tokenType = "";
            string name = "";
            string ltType = "";
            if(loginToken == null)
            {
                // if credentials are not valid send unauthorized status code in response
                loginResponse.responseMsg.StatusCode = HttpStatusCode.Unauthorized;
                //return Request.CreateResponse(HttpStatusCode.Unauthorized);
                response = ResponseMessage(loginResponse.responseMsg);
                return response;
            }
            ltType = loginToken.GetType().FullName;
        
            if (ltType.IndexOf("AirlineManagement.Administrator") >= 0)
            {
                LoginToken<Administrator> tname = loginToken as LoginToken<Administrator>;
                tokenType = "Administrator";
               name = tname.User.FIRST_NAME;
            }
            else if (ltType.IndexOf("AirlineManagement.AirlineCompany") >= 0)
            {
                LoginToken<AirlineCompany> tname = loginToken as LoginToken<AirlineCompany>;
                tokenType = "AirlineCompany";
                name = tname.User.AIRLINE_NAME;
            }
            else if (ltType.IndexOf("AirlineManagement.Customer") >= 0)
            {
                LoginToken<Customer> tname = loginToken as LoginToken<Customer>;
                tokenType = "Customer";
                name = tname.User.FIRST_NAME;
            }
    

            if (loginToken != null)
                //isUsernamePasswordValid = true;
            // if credentials are valid
           // if (isUsernamePasswordValid)
            {
                var token = TokenManager.GenerateToken(loginrequest.Username+":"+ loginrequest.Password);
                //return the token
                TypeAndToken typetoken = new TypeAndToken { type=tokenType,token=token, name= name};
                    //return Request.CreateResponse(HttpStatusCode.Created, token);
                    return Ok<TypeAndToken>(typetoken);

            }
            else
            {
                // if credentials are not valid send unauthorized status code in response
                loginResponse.responseMsg.StatusCode = HttpStatusCode.Unauthorized;
                //return Request.CreateResponse(HttpStatusCode.Unauthorized);
                response = ResponseMessage(loginResponse.responseMsg);
                return response;
            }
        }

    }
    public class TypeAndToken
    {
        public string type { get; set; }
        public string token { get; set; }
        public string name { get; set; }
    }

}
