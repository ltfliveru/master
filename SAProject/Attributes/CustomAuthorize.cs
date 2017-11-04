using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SAProject.Attributes
{
    public class CustomAuthorize : AuthorizeAttribute
    {

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool disableAuthentication = false;

#if DEBUG
            disableAuthentication = false; 
            // true отключает необходимость постоянной авторизации при отладке 
#endif

            if (disableAuthentication)
                return true;

            return base.AuthorizeCore(httpContext);
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (this.AuthorizeCore(filterContext.HttpContext))
            {
                base.OnAuthorization(filterContext);
            }
            else
            {
                this.HandleUnauthorizedRequest(filterContext);
            }
        } 

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // Перенаправление неавторизированного запроса на указанный URL
            filterContext.Result = new RedirectResult("~/Home/Error");
        }

     
    }
}