using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HunterMVC
{
    public class MyAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var key = filterContext.HttpContext.Request.QueryString["param_name"];
            if (!IsValid(key))
            {
                // Unauthorized!
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        private bool IsValid(string key)
        {
            // You know what to do here => go hit your RavenDb
            // and perform the necessary checks
            throw new NotImplementedException();
        }
    }
}