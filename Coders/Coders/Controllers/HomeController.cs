using Coders.ModelViews;
using PagedList;
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

        public ActionResult NewLandmarks(int? page)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "RealTime");

            }
     
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            var NewLandmarks = (from l in db.NewLandmarks select new NewLandmarksView {
                Id = l.Id,
                Picture = l.Picture,
                Rate=(db.UserRatings.Where(x => x.NewLandmarksId == l.Id).Count()== 0)?0:(int)l.Rate / db.UserRatings.Where(x => x.NewLandmarksId == l.Id).Count(),
                Date =(DateTime)l.Date,
                Description=l.Description,
                Author=l.User.Named
            });
            NewLandmarks =NewLandmarks.OrderBy(x => x.Date);
            return View(NewLandmarks.ToPagedList(pageNumber, pageSize));
        }


        public JsonResult Rate(int Id,int Star)
        {
            string guid = Session["User"].ToString();
            int UserId = (from t in db.Users where t.Guid.ToString() == guid select t.Id).FirstOrDefault();
            UserRating rate = (from r in db.UserRatings where r.UserId == UserId && r.NewLandmarksId == Id select r).FirstOrDefault();
            int Rating;
            if (rate != null)
            {
                int OldRate = (int)rate.Rate;
                Rating = Star - OldRate;
                rate.Rate = Star;
                db.SaveChanges();
            }
            else
            {
                Rating = Star;
                UserRating uRate = new UserRating { NewLandmarksId = Id, Rate = Star, UserId = UserId };
                db.UserRatings.Add(uRate);
                db.SaveChanges();
            }
            NewLandmark land = db.NewLandmarks.Find(Id);
            land.Rate = land.Rate + Rating;
            db.SaveChanges();
            int FullStar = (int)land.Rate / (db.UserRatings.Where(x => x.NewLandmarksId == Id)).Count();
            return Json(FullStar);
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