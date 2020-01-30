using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace AirlineManagementWebApi.Controllers
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        //private readonly PermissionAction[] permissionActions;

        public CustomAuthorize()
        {
            
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            string header=actionContext.Request.Headers.ToString();
            // step 1 : retrieve user object

            // step 2 : retrieve user permissions

            // step 3 : match user permission(s) agains class/method's required premissions

            // step 4 : continue/redirect to access denied page
        }
    }
    
}