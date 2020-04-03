using AirlineManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
//using System.Web.Http.Filters;
using System.Web.Http;
using ServiceStack.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace AirlineManagementWebApi.Controllers
{
     public class JwtAuthenticationAttribute : AuthorizeAttribute
    //public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        private FlyingCenterSystem FCS;
        private ILoginToken loginToken = null;
      
        //[ThreadStatic]
        //public static Airline CurrentAirline = null;


        public override void OnAuthorization(HttpActionContext actionContext)
        {

            // got user name + password here in server
            // How to get username and password?
            // does the request have username +psw?
            if (actionContext.Request.Headers.Authorization == null)
            {
                //stops the request -will not arrive to web api controller
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized,
                    "you must send name +pwd in basic authentication");
                return;
            }



            string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
    

            //string decodedAuthenticationToken = Encoding.UTF8.GetString(
            //    Convert.FromBase64String(authenticationToken));
            //'basic admin:9999'
            //string[] usernamePasswordArray = decodedAuthenticationToken.Split(':');
            //string username = usernamePasswordArray[0];
            //string password = usernamePasswordArray[1];
            string tokenUsername = TokenManager.ValidateToken(authenticationToken);
            string[] usernamePasswordArray= tokenUsername.Split(':');
            string username = usernamePasswordArray[0];
            string password = usernamePasswordArray[1];
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            loginToken =  FCS.Login(username, password);

            if (loginToken != null)
            {
                actionContext.Request.Properties["token"] = loginToken;
                return;
            }
            //    if (username == "admin" && password == "9999")
            //{
            //    //1 by thread 
            //    Thread.CurrentPrincipal = new GenericPrincipal(
            //        new GenericIdentity(username), null);

            //    //2 by request
            //    //actionContext.Request.GetRequestContext().Principal = new GenericPrincipal(   
            //    //    new GenericIdentity(username), null);

            //    // 3 by request got the user data from DB
            //    AirlineCompany CurrentAirline = new AirlineCompany { Name = "El Al", Password = "1234", OriginCountry = "Israel" };
            //    actionContext.Request.Properties["air-line"] = CurrentAirline;

            //    //actionContext.RequestContext.Principal
            //    //Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity("itay"), null);


            //    return;
            //}
            //else
            //{
            //    //actionContext.Response = actionContext.Request
            //    //    .CreateResponse(HttpStatusCode.Unauthorized);

            //}

            //stops the request -will not arrive to web api controller
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "You are not allowed!");
        }
    
    }
}