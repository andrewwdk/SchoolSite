using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolWebSite2.Models;

namespace SchoolWebSite2.Controllers
{
    public class TeachersController : Controller
    {
        public ActionResult Index()
        {
            var db = new SchoolWebsiteDatabaseEntities();
            var teachers = db.Person.Where(p => p.Role == "Учитель");
            return View(teachers);
        }
    }
}