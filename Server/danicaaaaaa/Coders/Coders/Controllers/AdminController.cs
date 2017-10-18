using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Coders.Data_Layer;
using Coders.Models;

namespace Coders.Controllers
{
    public class AdminController : Controller
    {
        static int userFilter = -1;
        static int competitionFilter = -1;
        static int landmarkFilter = -1;

        // GET: Admin
        public ActionResult Index()
        {
            return View();
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

        public JsonResult ChangeCompetition(CompetitionView competitionModal)
        {
            DLCompetition.Data.ChangeCompetition(competitionModal);
            return Json(true);
        }

        public JsonResult ChangeLandmark(LandmarkView landmarknModal)
        {
            DLLandmarks.Data.ChangeLandmark(landmarknModal);
            return Json(true);
        }
    }
}