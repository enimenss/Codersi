using Coders;
using Coders.Graph;
using Coders.Models;
using System;
using System.Collections.Generic;
using System.IO;
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


        public JsonResult Log(string Username ,string Pass)
        {
            User newUser;
            using (var data = new CodersEntities())
            {
                newUser = data.Users.Where(x => x.Username ==Username && x.Pass==Pass).FirstOrDefault();
            }
            if (newUser == null)
            {
                return Json(false);
            }

            Session["User"] = newUser.Guid.ToString();

            return Json(true);
        }


        [HttpPost]
        public JsonResult MakeAGraph(string city)
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
        public JsonResult GetGraph(List<Point> graph,bool? saveToDatabase,int City,int CompatitionId)
        {
          if(graph==null)  return Json(false);
            List<Solution> Edges=new List<Solution>();
            foreach (var ele in graph)
            {
                Edges.Add( new Solution { Fulfilled = 1, LandmarkChild_Id = ele.id2, LandmarkParent_Id = ele.id1 });
            }
            GraphAsLists Graph = new GraphAsLists();
            Graph.MakeAGraph(City,CompatitionId,Edges);
            List<BadEdge> bads = Graph.topologicalOrderTrav();
            bads = bads.Concat(Graph.ConnectEdge(Graph.AllPathInRim())).ToList();
            if (bads.Count>0)
            {
               
                return Json(bads);
            }
            




            foreach (var ele in graph)
            {
                Solution s = new Solution { Fulfilled = 1, LandmarkChild_Id = ele.id2, LandmarkParent_Id = ele.id1 };
                db.Solutions.Add(s);
                db.SaveChanges();
            }

            //if (bads != null || bads.Count > 0)
            //{
            //    using (var db = new CodersEntities())
            //    {
            //        foreach (var ele in bads)
            //        {
            //            Solution edg = new Solution { Fulfilled = 1, LandmarkParent_Id = ele.ParentNode, LandmarkChild_Id = ele.ChildNode };
            //            db.Solutions.Add(edg);
            //            db.SaveChanges();
            //        }
            //    }
            //}//ako pristane na modifikaciju stavi negde drugde ne mora pera ce

            return Json(true);
        }


        [HttpPost]
        public JsonResult GetRouts(List<Point> graph, int numBersOfTeamMates, bool? fromDatabase,int City,int CompatitionId)
        {
            GraphAsLists Graph = new GraphAsLists();
            if (fromDatabase == false)
            {
                List<Solution> Edges = new List<Solution>();
                foreach (var ele in graph)
                {
                    Edges.Add(new Solution { Fulfilled = 1, LandmarkChild_Id = ele.id2, LandmarkParent_Id = ele.id1 });
                }
                Graph.MakeAGraph(City, CompatitionId, Edges);
            }
            else
            {
                Graph.MakeAGraph(City, CompatitionId);
            }
            var x= Graph.GenareRouts(numBersOfTeamMates);
            return Json(x);
        }

        [HttpPost]
        public JsonResult Register(User landmarkModel)
        {
           int count = (from u in db.Users where u.Username == landmarkModel.Username || u.Email == landmarkModel.Email select u).Count();
            if(count > 0)
            {
                return Json(false);
            }
            User user = new User { Id=7,Email = landmarkModel.Email, Username = landmarkModel.Username, Pass = landmarkModel.Pass, Named = landmarkModel.Named, Surname = landmarkModel.Surname, Guid = Guid.NewGuid() };
            if (Request.Files.Count > 0)
            {
                HttpFileCollectionBase files = Request.Files;
                string fname;
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];

                    fname = file.FileName;
                    string[] nameAndExtension = fname.Split('.');
                    if (nameAndExtension[1].ToLower() == "pdf" || nameAndExtension[1].ToLower() == "jpg" || nameAndExtension[1].ToLower() == "png")
                    {
                        fname = nameAndExtension[0] + "." + nameAndExtension[1];
                        fname = Path.Combine(Server.MapPath("~/Images"), fname);

                        file.SaveAs(fname);
                    }
                }
            }
                        db.Users.Add(user);
            db.SaveChanges();
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