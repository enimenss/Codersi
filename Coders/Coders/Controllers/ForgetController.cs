using Coders;
using Coders.SendMail;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;


namespace Coders.Controllers
{
    public class ForgetController : Controller
    {
        private CodersEntities db = new CodersEntities();

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection param)
        {
            string email = param["email"];
            if (String.IsNullOrEmpty(email))
            {
                ViewBag.errorMassage = "Morate uneti email";
                return View();
            }

           User user = db.Users.Where(s => s.Email == email).FirstOrDefault();
           if (user == null)
            {
                ViewBag.errorMassage = "nepostojeci email";
                return View();
            }
            Session["UserId"] = user;
            Session["forget"] = Environment.TickCount;

            Mail.SendMail(user.Email, "<h1>" + Session["forget"].ToString() + "</h1>");

             return RedirectToAction("kod","Forget");

            }


        public ActionResult kod()
        {
            if (Session["UserId"] == null || Session["forget"] == null)
            {
                return RedirectToAction("Index", "Forget");
            }
            ViewBag.Message = "poslali smo kod na Mail " + ((User)Session["UserId"]).Email;
            return View();

        }
        [HttpPost]
        public ActionResult kod(FormCollection param)
        {
            if (Session["UserId"] == null || Session["forget"] == null)
            {
                return RedirectToAction("Index", "Forget");
            }

            if (param["kod"] == Session["forget"].ToString())
            {
                Session["forget"] = null;
                return RedirectToAction("Pass");
            }
            else
                return View(); ;

        }


        public ActionResult Pass()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Forget");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Pass(FormCollection param)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Forget");
            }

            if (param["pass"] != "" && param["pass"] == param["potpass"])
            {

                string pass = param["pass"];
                string guid = ((User)Session["UserId"]).Guid.ToString();
                var user = db.Users.Where(x=>x.Guid.ToString()==guid).SingleOrDefault();
                db.Entry(user).Property(x => x.Pass).CurrentValue = pass;
                db.SaveChanges();
                Session["userId"] = null;
                return RedirectToAction("Login", "RealTime");
            }
            else
            {

                ViewBag.errorMassage = "Sifre se ne poklapaju";
                return View();
            }
        }


    }
}