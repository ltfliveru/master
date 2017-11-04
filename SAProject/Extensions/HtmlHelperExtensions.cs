using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace SAProject.Extensions
{ 
    public static class CustomHelpers
    {
        public static IHtmlString PhoneNumber(this HtmlHelper helper, string name, string content)
        {
            string LableStr = $"<input type=\"text\" name=\"{name}\" value=\"{content}\" class=\"form-control\"/>";
            string script =  
                           @"<script> jQuery(document).ready(function ($){ $(""input[name = '"+name+@"']"").mask(""380999999999"");});</script>";

            StringBuilder html = new StringBuilder();
            html.AppendLine(LableStr);
            html.AppendLine(script); 
            return new HtmlString(html.ToString()); 
        }

        public static MvcHtmlString PhoneNumberFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, 
            object htmlAttributes)
        {
            var fullHtmlFieldName = html
                .ViewContext
                .ViewData
                .TemplateInfo
                .GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression));
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);            
            var inputElement = new TagBuilder("input");
            if (metadata.Model != null)
            {
                var valueInput = metadata.Model != null ?
                    (metadata.Model.ToString().Contains("380") ? metadata.Model.ToString().Replace("380", string.Empty) : metadata.Model.ToString())
                        : string.Empty;
                inputElement.Attributes.Add("value", string.Format("380{0}", valueInput));
            }
            inputElement.Attributes.Add("name", fullHtmlFieldName);
            var attrs = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            if (attrs.ContainsKey("class"))
            { 
                inputElement.Attributes.Add("class", attrs["class"].ToString());
            }            
            var validationAttributes = html.GetUnobtrusiveValidationAttributes(fullHtmlFieldName, metadata);
            foreach (var key in validationAttributes.Keys)
            {
                inputElement.Attributes.Add(key, validationAttributes[key].ToString());
            }            
            var script = string.Empty;
            if (attrs.ContainsKey("id"))
            {
                var idAttrib = attrs["id"].ToString();
                inputElement.Attributes.Add("id", idAttrib);
                script = @"<script> window.onload = function () { MaskedInput({
                                    elm: document.getElementById('"+ idAttrib + @"'),
                                    format: '380_________',
                                    separator: '380 '});}; </script>";
            }
            var result = inputElement.ToString();
            StringBuilder htmlString = new StringBuilder();
            htmlString.AppendLine(result);
            htmlString.AppendLine(script);
            result = htmlString.ToString();
            return new MvcHtmlString(result);
        }
    }
}