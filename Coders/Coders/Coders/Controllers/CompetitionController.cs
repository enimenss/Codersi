using Coders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Coders.Controllers
{
    public class CompetitionController : Controller
    {
        // GET: Competition
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult NextHint(int landmarkId, int hintId)
        {
            HintView hint = DLHint.Data.ReturnNextHint(landmarkId, hintId);
            return Json(hint);
        }

        public void HintCorect(int id)
        {
            //upis u bazu;
        }
    }
}