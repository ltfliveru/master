using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAProject.Attributes
{
    public class CustomAuthorize : AuthorizeAttribute
    { 
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = true;
          
            return authorize;
        } 
    }
}