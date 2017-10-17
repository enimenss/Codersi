using Coders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Coders.Controllers
{
    public class RealTimeController : Controller
    {
        // GET: SignalR
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login([Bind(Include = "Username")] User u)
        {
            User newUser;
            using (var data = new CodersEntities())
            {
                newUser = data.Users.Where(x => x.Username == u.Username).FirstOrDefault();
            }
            if (newUser == null)
            {
                return View(u);
            }

            Session["User"] = newUser.Guid.ToString();

            return RedirectToAction("Index", "RealTime");
        }
    }
}