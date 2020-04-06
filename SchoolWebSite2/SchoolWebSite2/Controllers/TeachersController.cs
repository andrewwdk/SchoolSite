using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolWebSite2.Models;
using PagedList;
using PagedList.Mvc;

namespace SchoolWebSite2.Controllers
{
    public class TeachersController : Controller
    {
        public ActionResult Index(string search, int? page)
        {
            var db = new SchoolWebsiteDatabaseEntities();
            var teachers = db.Person.Where(p => p.Role == "Учитель");
            if (!string.IsNullOrEmpty(search))
            {
                teachers = teachers.Where(p => p.Name.Contains(search) || p.Surname.Contains(search) || p.Patronimic.Contains(search));
            }
            var result = teachers.OrderBy(p => p.Surname).ToList().ToPagedList(page ?? 1, 3);
            return View(result);
        }
    }
}