using Coders.Graph;
using Coders.Models;
using Coders.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Coders.Controllers
{
    public class CompetitionController : Controller
    {
        private CodersEntities db = new CodersEntities();
        // GET: Competition
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult NextHint(int landmarkId, int hintId)
        {
           // HintView hint = DLHint.Data.ReturnNextHint(landmarkId, hintId);
            return Json(1);
        }
        public JsonResult getNewLandMark(Rout CurentNode)
        {
            //compStart(1, 2);


            //string guid = Session["User"].ToString();
            //int UserId = (from t in db.Users where t.Guid.ToString() == guid select t.Id).FirstOrDefault();
            //LandMarkView node;
            //Point nextRout;
            //if (CurentNode == null)
            //{
            //    UserRout u = (from ur in db.UserRouts where ur.UserId == UserId && ur.Fulfilled == 0 select ur).ToList().OrderBy(x => x.Id).FirstOrDefault();
            //    Rout r = u.Rout;
            //    node = (from l in db.Landmarks
            //            where l.Id == r.LandmarkChild_Id
            //            select new LandMarkView
            //            {
            //                City = l.City,
            //                FilePath = l.FilePath,
            //                FilePathLandmark = l.FilePathLandmark,
            //                Id = l.Id,
            //                Name = l.Name
            //            }).FirstOrDefault();
            //    nextRout = (from n in db.Routs
            //                      where n.LandmarkParent_Id == r.LandmarkChild_Id
            //                      select new Point
            //                      {
            //                          id1 = (int)n.LandmarkParent_Id,
            //                          id2 = (int)n.LandmarkChild_Id
            //                      }).FirstOrDefault();
            //}
            //else
            //{
            //    node=(from l in db.Landmarks where l.Id==CurentNode.LandmarkChild_Id select new LandMarkView
            //    {
            //        City = l.City,
            //        FilePath = l.FilePath,
            //        FilePathLandmark = l.FilePathLandmark,
            //        Id = l.Id,
            //        Name = l.Name
            //    }).FirstOrDefault();

            //    nextRout = (from n in db.Routs
            //                where n.LandmarkParent_Id == CurentNode.LandmarkChild_Id
            //                select new Point
            //                {
            //                    id1 = (int)n.LandmarkParent_Id,
            //                    id2 = (int)n.LandmarkChild_Id
            //                }).FirstOrDefault();

            //    //next edge for him
            //    //   
            //}
            if(CurentNode==null)
            {
                LandMarkView node = new LandMarkView
                {
                    City = 2,
                    FilePath = "",
                    FilePathLandmark = "/image/LandMark1.png",
                    Lat = 43.32986040795889,
                    Lon = 21.893906654386228,
                    Id = 5,
                    Name = "Elektronski fakultet"
                };
                return Json(new { MyLandMark = node, MyCurrentNode = "ss"  });
            }
            else
            {
                LandMarkView node = new LandMarkView
                {
                    City = 2,
                    FilePath = "",
                    FilePathLandmark = "/image/LandMark2.png",
                    Lat= 43.323070270605612,
                    Lon= 21.8971253070049,
                    Id = 6,
                    Name = "Key"
                };
                return Json(new { MyLandMark = node, MyCurrentNode = "ss" });
            }

        }
        public JsonResult getHintsForLandMaks(int IDMyLandMark)
        {
            var HintsList = (from l in db.Hints where l.Landmark_Id == IDMyLandMark select new HintsView {
                Id = l.Id,
                Answer = l.Answer,
                Description = l.Description,
                Radius = l.Radius
            }).ToList();

            return Json(HintsList);
        }
        
        public void HintCorect(int id)
        {
            //upis u bazu;
        }
        public JsonResult GetGroup()
        {
            string name = Session["User"].ToString();
            int? Group = (from u in db.Users where u.Guid.ToString() == name select u.Members.FirstOrDefault().Groups_Id).FirstOrDefault();
            var Members = (from m in db.Members
                           where m.Groups_Id == Group
                           select new //GroupView
                           {
                               Guidd = m.User.Guid.ToString(),
                               Username = m.User.Named,
                               PosX = m.User.Location_X,
                               PosY = m.User.Location_Y,
                               Picture = m.User.Picture

                           }).ToList();
            return Json(Members);
        }

        [NonAction]
       public void compStart(int compId,int city)
       {
            var GroupsId = (from g in db.Participants where g.Competition_Id == compId select g.Groups_Id).ToList();
            var Graph = new GraphAsLists();
            Graph.MakeAGraph(city, compId);
            foreach (var id in GroupsId) {
                var Users = (from m in db.Members where m.Groups_Id == id select m.User).ToList();
                var edge = Graph.GenareRouts(Users.Count);
                foreach (var e in edge) {
                    Rout r = new Rout { Competition_Id = compId, LandmarkParent_Id = e.ParentNode, LandmarkChild_Id = e.ChildNode };
                    var test = (from d in db.Routs where d.LandmarkChild_Id == e.ChildNode && d.LandmarkParent_Id == e.ParentNode select d).FirstOrDefault();
                    int rId;
                    if (test == null)
                    {
                        db.Routs.Add(r);
                        db.SaveChanges();
                        rId = r.Id;
                    }
                    else
                    {
                        rId = test.Id;
                    }
                    int num = e.path.Count;
                    for(int i = 0; i < num; i++)
                    {
                        UserRout u = new UserRout { Fulfilled = 0, Routs_Id = rId, UserId = Users[i].Id };
                        db.UserRouts.Add(u);
                    }
                }
            }
       }
    }
}