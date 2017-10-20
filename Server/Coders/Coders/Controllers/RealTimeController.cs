using Coders;
using Coders.Graph;
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
            List<Solution> Edges=new List<Solution>();
            foreach (var ele in graph)
            {
                Edges.Add( new Solution { Fulfilled = 1, LandmarkChild_Id = ele.id2, LandmarkParent_Id = ele.id1 });
            }
            GraphAsLists Graph = new GraphAsLists();
            Graph.MakeAGraph(1, 2,Edges);
            List<BadEdge> bads = Graph.topologicalOrderTrav();
            if (bads != null)
            {
                return Json(bads);
            }
            bads = Graph.ConnectEdge(Graph.AllPathInRim());
            if (bads != null)
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
        public JsonResult GetRouts()
        {
            GraphAsLists Graph = new GraphAsLists();
            Graph.MakeAGraph(1, 2);
            var x= Graph.GenareRouts(3);
            return Json(x);


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