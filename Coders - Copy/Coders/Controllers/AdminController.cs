using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Coders.Data_Layer;
using Coders.Models;
using static Coders.Enums;
using System.IO;
using Coders;
using Coders.Graph;

namespace Coders.Controllers
{
    public class AdminController : Controller
    {
        static int userFilter = -1;
        static int competitionFilter = -1;
        static int landmarkFilter = -1;
        static Guid competitionGuid = new Guid();

        // GET: Admin
        public ActionResult Index()
        {
            Session["User"] = Guid.Parse("3995CBCC-E1DC-4B30-B924-C6C3010164EA");
            var newLandmarks = DLLandmarks.Data.ReturnNewLandmarks();
            return View(newLandmarks);
        }

        public ActionResult Competitions()
        {
            var competitions = DLCompetition.Data.ReturnStartingCompetitions();
            return View(competitions);
        }

        public ActionResult Groups(Guid guid)
        {
            competitionGuid = guid;
            var groups = DLCompetition.Data.ReturnGroups(guid);
            return View(groups);
        }

        public JsonResult NewGroup(string id)
        {
            Group group = new Group();
            group.Name = id;
            group.CountOfFounds = 1;
            var guid = competitionGuid;
            DLCompetition.Data.SaveGroup(group, guid, (Guid)Session["User"]);
            return Json(true);
        }

        public ActionResult Join(Guid guid)
        {
            DLCompetition.Data.JoinGroup(guid, (Guid)Session["User"]);
            return RedirectToAction("Index", "Admin");
        }

        public JsonResult LoadUsers()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var search = Request.Form.GetValues("search[value]").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("order[0][column]").FirstOrDefault();
            var sortDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = DLUser.Data.ReturnUserCount();

            List<UserView> usersView = new List<UserView>();

            if (!string.IsNullOrEmpty(search))
            {
                usersView = DLUser.Data.ReturnUsers();

                if (userFilter != -1)
                {
                    switch (userFilter)
                    {
                        case 1: usersView = usersView.Where(x => x.Named.ToLower().Contains(search.ToLower())).ToList(); break;
                        case 2: usersView = usersView.Where(x => x.Surname.ToLower().ToString().Contains(search.ToLower())).ToList(); break;
                        case 3: usersView = usersView.Where(x => x.Username.ToLower().Contains(search.ToLower())).ToList(); break;
                        case 4: usersView = usersView.Where(x => x.Email.ToLower().ToString().Contains(search.ToLower())).ToList(); break;
                        default: break;
                    }
                }
                else
                {
                    usersView.Skip(skip).Take(pageSize).ToList();
                }
                
            }
            else
            {
                usersView = DLUser.Data.ReturnUsers(skip, pageSize);
            }

            if (!string.IsNullOrEmpty(sortDir))
            {
                if (sortDir.CompareTo("desc") == 0)
                {
                    switch (int.Parse(sortColumn))
                    {
                        case 1: usersView = usersView.OrderByDescending(x => x.Named).ToList(); break;
                        case 2: usersView = usersView.OrderByDescending(x => x.Surname).ToList(); break;
                        case 3: usersView = usersView.OrderByDescending(x => x.Username).ToList(); break;
                        case 4: usersView = usersView.OrderByDescending(x => x.Email).ToList(); break;
                        default: break;
                    }
                }
                else
                {
                    switch (int.Parse(sortColumn))
                    {
                        case 1: usersView = usersView.OrderBy(x => x.Named).ToList(); break;
                        case 2: usersView = usersView.OrderBy(x => x.Surname).ToList(); break;
                        case 3: usersView = usersView.OrderBy(x => x.Username).ToList(); break;
                        case 4: usersView = usersView.OrderBy(x => x.Email).ToList(); break;
                        default: break;
                    }
                }
            }

            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = usersView }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoadCompetitions()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var search = Request.Form.GetValues("search[value]").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("order[0][column]").FirstOrDefault();
            var sortDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = DLCompetition.Data.ReturnCompetitionCount();

            List<CompetitionView> competitionsView = new List<CompetitionView>();

            if (!string.IsNullOrEmpty(search))
            {
                competitionsView = DLCompetition.Data.ReturnCompetitions();

                if (competitionFilter != -1)
                {
                    switch (competitionFilter)
                    {
                        case 1: competitionsView = competitionsView.Where(x => x.Id.ToString().Contains(search.ToLower())).ToList(); break;
                        case 2: competitionsView = competitionsView.Where(x => x.Name.ToLower().Contains(search.ToLower())).ToList(); break;
                        case 3: competitionsView = competitionsView.Where(x => x.StartingDate.ToString().Contains(search.ToLower())).ToList(); break;
                        case 4: competitionsView = competitionsView.Where(x => x.Type.ToString().Contains(search.ToLower())).ToList(); break;
                        case 5: competitionsView = competitionsView.Where(x => x.EndingDate.ToString().Contains(search.ToLower())).ToList(); break;
                        //case 6: competitionsView = competitionsView.Where(x => x.City.Contains(search.ToUpper())).ToList(); break;
                        case 7: competitionsView = competitionsView.Where(x => x.LandmarkCount.ToString().Contains(search.ToLower())).ToList(); break;
                        default: break;
                    }
                }
                else
                {
                    competitionsView.Skip(skip).Take(pageSize).ToList();
                }

            }
            else
            {
                competitionsView = DLCompetition.Data.ReturnCompetitions(skip, pageSize);
            }

            if (!string.IsNullOrEmpty(sortDir))
            {
                if (sortDir.CompareTo("desc") == 0)
                {
                    switch (int.Parse(sortColumn))
                    {
                        case 1: competitionsView = competitionsView.OrderByDescending(x => x.Id).ToList(); break;
                        case 2: competitionsView = competitionsView.OrderByDescending(x => x.Name).ToList(); break;
                        case 3: competitionsView = competitionsView.OrderByDescending(x => x.StartingDate).ToList(); break;
                        case 4: competitionsView = competitionsView.OrderByDescending(x => x.EndingDate).ToList(); break;
                        case 5: competitionsView = competitionsView.OrderByDescending(x => x.Type).ToList(); break;
                        case 6: competitionsView = competitionsView.OrderByDescending(x => x.City).ToList(); break;
                        case 7: competitionsView = competitionsView.OrderByDescending(x => x.LandmarkCount).ToList(); break;
                        default: break;
                    }
                }
                else
                {
                    switch (int.Parse(sortColumn))
                    {
                        case 1: competitionsView = competitionsView.OrderBy(x => x.Id).ToList(); break;
                        case 2: competitionsView = competitionsView.OrderBy(x => x.Name).ToList(); break;
                        case 3: competitionsView = competitionsView.OrderBy(x => x.StartingDate).ToList(); break;
                        case 4: competitionsView = competitionsView.OrderBy(x => x.EndingDate).ToList(); break;
                        case 5: competitionsView = competitionsView.OrderBy(x => x.Type).ToList(); break;
                        case 6: competitionsView = competitionsView.OrderBy(x => x.City).ToList(); break;
                        case 7: competitionsView = competitionsView.OrderBy(x => x.LandmarkCount).ToList(); break;

                        default: break;
                    }
                }
            }

            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = competitionsView }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoadLandmarks()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var search = Request.Form.GetValues("search[value]").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("order[0][column]").FirstOrDefault();
            var sortDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = DLLandmarks.Data.ReturnLandmarkCount();

            List<LandmarkView> landmarkViews = new List<LandmarkView>();

            if (!string.IsNullOrEmpty(search))
            {
                landmarkViews = DLLandmarks.Data.ReturnLandmarks();

                if (landmarkFilter != -1)
                {
                    switch (landmarkFilter)
                    {
                        case 1: landmarkViews = landmarkViews.Where(x => x.Id.ToString().Contains(search.ToLower())).ToList(); break;
                        case 2: landmarkViews = landmarkViews.Where(x => x.Name.ToLower().Contains(search.ToLower())).ToList(); break;
                        case 3: landmarkViews = landmarkViews.Where(x => x.City.ToString().Contains(search.ToLower())).ToList(); break;
                        default: break;
                    }
                }
                else
                {
                    landmarkViews.Skip(skip).Take(pageSize).ToList();
                }
                
            }
            else
            {
                landmarkViews = DLLandmarks.Data.ReturnLandmarks(skip, pageSize);
            }

            if (!string.IsNullOrEmpty(sortDir))
            {
                if (sortDir.CompareTo("desc") == 0)
                {
                    switch (int.Parse(sortColumn))
                    {
                        case 1: landmarkViews = landmarkViews.OrderByDescending(x => x.Id).ToList(); break;
                        case 2: landmarkViews = landmarkViews.OrderByDescending(x => x.Name).ToList(); break;
                        case 3: landmarkViews = landmarkViews.OrderByDescending(x => x.City).ToList(); break;
                        default: break;
                    }
                }
                else
                {
                    switch (int.Parse(sortColumn))
                    {
                        case 1: landmarkViews = landmarkViews.OrderBy(x => x.Id).ToList(); break;
                        case 2: landmarkViews = landmarkViews.OrderBy(x => x.Name).ToList(); break;
                        case 3: landmarkViews = landmarkViews.OrderBy(x => x.City).ToList(); break;

                        default: break;
                    }
                }
            }

            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = landmarkViews }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoadHints(int id)
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var search = Request.Form.GetValues("search[value]").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("order[0][column]").FirstOrDefault();
            var sortDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = DLHint.Data.ReturnHintsCount(id);

            List<HintView> hintsView = new List<HintView>();

            if (!string.IsNullOrEmpty(search))
            {
                hintsView = DLHint.Data.ReturnHints(id);

                if (landmarkFilter != -1)
                {
                    switch (landmarkFilter)
                    {
                        case 1: hintsView = hintsView.Where(x => x.Id.ToString().Contains(search.ToLower())).ToList(); break;
                        case 2: hintsView = hintsView.Where(x => x.Description.ToLower().Contains(search.ToLower())).ToList(); break;
                        case 3: hintsView = hintsView.Where(x => x.Answer.ToString().Contains(search.ToLower())).ToList(); break;
                        default: break;
                    }
                }
                else
                {
                    hintsView.Skip(skip).Take(pageSize).ToList();
                }

            }
            else
            {
                hintsView = DLHint.Data.ReturnHints(id, skip, pageSize);
            }

            if (!string.IsNullOrEmpty(sortDir))
            {
                if (sortDir.CompareTo("desc") == 0)
                {
                    switch (int.Parse(sortColumn))
                    {
                        case 1: hintsView = hintsView.OrderByDescending(x => x.Id).ToList(); break;
                        case 2: hintsView = hintsView.OrderByDescending(x => x.Description).ToList(); break;
                        case 3: hintsView = hintsView.OrderByDescending(x => x.Answer).ToList(); break;
                        default: break;
                    }
                }
                else
                {
                    switch (int.Parse(sortColumn))
                    {
                        case 1: hintsView = hintsView.OrderBy(x => x.Id).ToList(); break;
                        case 2: hintsView = hintsView.OrderBy(x => x.Description).ToList(); break;
                        case 3: hintsView = hintsView.OrderBy(x => x.Answer).ToList(); break;

                        default: break;
                    }
                }
            }

            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = hintsView }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeHint(int id, string description, string answer)
        {
            HintView hint = new HintView();
            hint.Id = id;
            hint.Description = description;
            hint.Answer = answer;
            DLHint.Data.ChangeHint(hint);
            return Json(true);
        }

        public JsonResult DeleteHint(int id)
        {
            DLHint.Data.DeleteHint(id);
            return Json(true);
        }

        public JsonResult FilterUsers(int filter)
        {
            userFilter = filter;
            return Json(new { data = true });
        }

        public JsonResult FilterCompetitions(int filter)
        {
            competitionFilter = filter;
            return Json(new { data = true });
        }

        public JsonResult FilterLandmarks(int filter)
        {
            landmarkFilter = filter;
            return Json(new { data = true });
        }

        public JsonResult DeleteUser(int id)
        {
            DLUser.Data.DeleteUser(id);
            return Json(true);
        }

        public JsonResult DeleteCompetition(int id)
        {
            DLCompetition.Data.DeleteCompetition(id);
            return Json(true);
        }

        public JsonResult DeleteLandmark(int id)
        {
            DLLandmarks.Data.DeleteLandmark(id);
            return Json(true);
        }

        public JsonResult ChangeCompetition(int id, string name, string startdate, string enddate, Cities city, int landmarkcount, Types type)
        {
            CompetitionView competitionView = new CompetitionView();
            competitionView.Id = id;
            competitionView.Name = name;
            competitionView.StartingDate = startdate;
            competitionView.EndingDate = enddate;
            competitionView.City = city;
            competitionView.LandmarkCount = landmarkcount;
            competitionView.Type = type;
            DLCompetition.Data.ChangeCompetition(competitionView);
            return Json(true);
        }

        public JsonResult ChangeLandmark(int id, Cities city, string name)
        {
            LandmarkView landmarkView = new LandmarkView();
            landmarkView.Id = id;
            landmarkView.City = city;
            landmarkView.Name = name;
            DLLandmarks.Data.ChangeLandmark(landmarkView);
            return Json(true);
        }

        public JsonResult AddCompetition(string name, string startdate, string enddate, Cities city, string landmarkcount, Types type)
        {
            CompetitionView competitionView = new CompetitionView();
            competitionView.Name = name;
            competitionView.StartingDate = startdate;
            competitionView.EndingDate = enddate;
            competitionView.City = city;
            competitionView.LandmarkCount = Int32.Parse(landmarkcount);
            competitionView.Type = type;
            DLCompetition.Data.AddCompetition(competitionView);
            return Json(true);
        }

        public JsonResult AddLandmark(LandmarkView landmark)
        {
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
                        DLLandmarks.Data.AddLandmark(landmark);
                        return Json(true);
                    }
                    else
                    {
                        return Json(false);
                    }
                }
            }
            return Json(false);
        }

        public JsonResult UserAddLandmark(NewLandmarkView landmark)
        {
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
                        DLLandmarks.Data.UserAddLandmark(landmark);
                    }
                    else
                    {
                        return Json(false);
                    }
                }
            }

            return Json(true);
        }

        public JsonResult AddHint(string description, string answer, int landmarkId)
        {
            HintView hint = new HintView();
            hint.Description = description;
            hint.Answer = answer;
            hint.Landmark_Id = landmarkId;
            DLHint.Data.AddHint(hint);
            return Json(true);
        }

        public JsonResult DeleteNewLandmark(int id)
        {
            DLLandmarks.Data.DeleteNewLandmark(id);
            return Json(true);
        }

        [HttpPost]
        public JsonResult MakeAGraph(string city)
        {
            CodersEntities db = new CodersEntities();
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
        public JsonResult GetGraph(List<Point> graph, bool? saveToDatabase, int City, int CompatitionId)
        {
            CodersEntities db = new CodersEntities();
            if (graph == null) return Json(false);
            List<Solution> Edges = new List<Solution>();
            foreach (var ele in graph)
            {
                Edges.Add(new Solution { Fulfilled = 1, LandmarkChild_Id = ele.id2, LandmarkParent_Id = ele.id1 });
            }
            GraphAsLists Graph = new GraphAsLists();
            Graph.MakeAGraph(City, CompatitionId, Edges);
            List<BadEdge> bads = Graph.topologicalOrderTrav();
            bads = bads.Concat(Graph.ConnectEdge(Graph.AllPathInRim())).ToList();
            if (bads.Count > 0)
            {

                return Json(bads);
            }

            foreach (var ele in graph)
            {
                Solution s = new Solution { Fulfilled = 1, LandmarkChild_Id = ele.id2, LandmarkParent_Id = ele.id1 };
                db.Solutions.Add(s);
                db.SaveChanges();
            }
            return Json(true);
        }

        [HttpPost]
        public JsonResult GetRouts(List<Point> graph, int numBersOfTeamMates, bool? fromDatabase, int City, int CompatitionId)
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
            var x = Graph.GenareRouts(numBersOfTeamMates);
            return Json(x);
        }
    }
}