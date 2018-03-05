using Instagram.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Instagram.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ApplicationContext db;

        public UserController(ApplicationContext context)
        {
            db = context;
        }



        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Profile()
        {
            string username = User.Identity.Name;
            var user = await db.Users.FirstOrDefaultAsync(u=>u.UserName == User.Identity.Name);
            if(user != null)
            {
                return View(user);
            }
            return View();
        }
        
    }
}