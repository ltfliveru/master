using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace SAProject.Extensions
{ 
    public static class CustomHelpers
    {
        public static IHtmlString PhoneNumber(this HtmlHelper helper, string name, string content)
        {
            string LableStr = $"<input type=\"text\" name=\"{name}\" value=\"{content}\" class=\"form-control\"/>";
            string script =  
                           @"<script> jQuery(document).ready(function ($){ $(""input[name = '"+name+@"']"").mask(""+380(99)999-99-99"");});</script>";

            StringBuilder html = new StringBuilder();
            html.AppendLine(LableStr);
            html.AppendLine(script); 
            return new HtmlString(html.ToString()); 
        }
    }
}