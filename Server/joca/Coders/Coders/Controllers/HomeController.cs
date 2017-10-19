using Coders.ModelViews;
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

        private readonly static ConnectionMapping<string> Connections = ConnectionMapping<string>.GetConection;

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetGroup()
        {
            string name = Session["User"].ToString();
            int? Group = (from u in db.Users where u.Guid.ToString() == name select u.Members.FirstOrDefault().Groups_Id).FirstOrDefault();
            var Members = (from m in db.Members
                           where m.Groups_Id == Group
                           select new GroupView
                           {
                               Guidd = m.User.Guid.ToString(),
                               Username = m.User.Named,
                               PosX = m.User.Location_X,
                               PosY = m.User.Location_Y,
                               Picture = m.User.Picture

                           }).ToList();
            return Json(Members);
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
            var friends = db.Members.Where(s => s.Groups_Id == Group).ToList().Select(x => new OnlineFriendsView
            {
                Name=x.User.Named,
                Guid=x.User.Guid.ToString(),
                Picture=x.User.Picture,
                isOnline = (bool)(Connections.GetConnections(x.User.Guid.ToString()).Count() > 0) ? true : false
            }).ToList();
          
                TempData["friends"] = friends;
            
            
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