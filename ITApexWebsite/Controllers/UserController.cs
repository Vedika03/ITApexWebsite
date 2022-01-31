using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITApexWebsite.Models;

namespace ITApexWebsite.Controllers
{
    public class UserController : Controller
    {
        DbITApexEntities db = new DbITApexEntities();
        // GET: User
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(tblUser user)
        {
            var verify = db.tblUsers.Where(u => u.u_email.Equals(user.u_email) && u.u_pass.Equals(user.u_pass)).SingleOrDefault();
            if (verify == null)
            {
                ViewBag.error = "Email or password is incorrect";
                return View();

            }
            else
            {
                Session["userId"] = verify.u_Id;
                return RedirectToAction("../Home/Index");
            }
        }

            public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(tblUser r)
        {
            db.tblUsers.Add(r);
            db.SaveChanges();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("../Home/Index");
        }


        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }
    }
}