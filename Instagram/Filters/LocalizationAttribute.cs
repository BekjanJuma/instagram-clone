using Instagram.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Instagram.Filters
{
   public class LocalizationAttribute : ActionFilterAttribute
   {

      public override void OnActionExecuted(ActionExecutedContext filterContext) {
         string cultureName = null;

         // Attempt to read the culture cookie from Request
         if (filterContext.HttpContext.Request != null) {
            HttpCookie cultureCookie = filterContext.HttpContext.Request.Cookies["_culture"];
            if (cultureCookie != null)
               cultureName = cultureCookie.Value;
            else
               cultureName = filterContext.HttpContext.Request.UserLanguages != null && filterContext.HttpContext.Request.UserLanguages.Length > 0 ?
                       filterContext.HttpContext.Request.UserLanguages[0] :  // obtain it from HTTP header AcceptLanguages
                       null;
            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe
                                                                            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
         }

         string lang = (string)filterContext.RouteData.Values["lang"];
         if (lang != null) {
            try {
               Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);
            } catch (Exception e) {
               throw new NotSupportedException($"Invalid language code '{lang}'.");
            }
         }

         base.OnActionExecuted(filterContext);
      }


   }
}