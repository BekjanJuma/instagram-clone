using Instagram.Helper;
using Instagram.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Instagram.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public FileContentResult UserPhotos()
        {
            if (User.Identity.IsAuthenticated)
            {
                String userId = User.Identity.GetUserId();

                if (userId == null)
                {
                    string fileName = HttpContext.Server.MapPath(@"~/Images/noImg.png");

                    byte[] imageData = null;
                    FileInfo fileInfo = new FileInfo(fileName);
                    long imageFileLength = fileInfo.Length;
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    imageData = br.ReadBytes((int)imageFileLength);

                    return File(imageData, "image/png");

                }
                // to get the user details to load user Image
                var bdUsers = HttpContext.GetOwinContext().Get<ApplicationContext>();
                var userImage = bdUsers.Users.Where(x => x.Id == userId).FirstOrDefault();

                return new FileContentResult(userImage.Avatar, "image/jpeg");
            }
            else
            {
                string fileName = HttpContext.Server.MapPath(@"~/Images/noImg.png");

                byte[] imageData = null;
                FileInfo fileInfo = new FileInfo(fileName);
                long imageFileLength = fileInfo.Length;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                imageData = br.ReadBytes((int)imageFileLength);
                return File(imageData, "image/png");

            }
        }

      [AllowAnonymous]
      [HttpPost]
      public ActionResult SetCulture(string culture) {
         // Validate input
         culture = CultureHelper.GetImplementedCulture(culture);
         // Save culture in a cookie
         HttpCookie cookie = Request.Cookies["_culture"];
         if (cookie != null)
            cookie.Value = culture;   // update cookie value
         else {
            cookie = new HttpCookie("_culture");
            cookie.Value = culture;
            cookie.Expires = DateTime.Now.AddYears(1);
         }
         Response.Cookies.Add(cookie);
         return Redirect(Request.UrlReferrer.PathAndQuery);
      }

      [AllowAnonymous]
      public ActionResult ChangeCurrentCulture(int id) {
         //  
         // Change the current culture for this user.  
         //  
         CultureHelper.CurrentCulture = id;
         //  
         // Cache the new current culture into the user HTTP session.   
         //  
         Session["CurrentCulture"] = id;
         //  
         // Redirect to the same page from where the request was made!   
         //  
         return Redirect(Request.UrlReferrer.ToString());
      }

   }
}