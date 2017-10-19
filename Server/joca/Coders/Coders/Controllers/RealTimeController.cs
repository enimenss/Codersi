using Coders;
using Coders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Coders.Controllers
{
    public class RealTimeController : Controller
    {
        private CodersEntities db = new CodersEntities();
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

            return RedirectToAction("Chat", "Home");
        }


        [HttpPost]
        public JsonResult MakeAGraph()
        {
            var Data = (from l in db.Landmarks
                        select new
                        {
                            Id = l.Id,
                            Lat = l.Location_X,
                            Lon = l.Location_Y,
                            Name = l.Name
                        }).ToList();
            return Json(Data);
        }

        [HttpPost]
        public JsonResult GetGraph(List<Point> graph)
        {
          if(graph==null)  return Json(false);

          foreach(var ele in graph)
            {
                Solution s = new Solution { Groups_Id = 2, Fulfilled = 1, LandmarkChild_Id = ele.id2, LandmarkParent_Id = ele.id1 };
                db.Solutions.Add(s);
                db.SaveChanges();
            }
            return Json(true);
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