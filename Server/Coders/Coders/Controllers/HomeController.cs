using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Coders.Controllers
{
    public class HomeController : Controller
    {
        private CodersEntities db = new CodersEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Chat()
        {
            string guid = Session["User"].ToString();
            int? Group = (from u in db.Users where u.Guid.ToString() == guid select u.Members.FirstOrDefault().Groups_Id).FirstOrDefault();
            var poruke = (from m in db.Messages where m.Groups_Id==Group select m);
            poruke = poruke.OrderBy(x => x.Date);
            return View(poruke);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}