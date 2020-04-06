using PagedList;
using SchoolWebSite2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolWebSite2.Controllers
{
    public class StudentsController : Controller
    {
        public ActionResult Index(string search, int? page)
        {
            var db = new SchoolWebsiteDatabaseEntities();
            var students = db.Person.Where(p => p.Role == "Ученик");
            if (!string.IsNullOrEmpty(search))
            {
                students = students.Where(p => p.Name.Contains(search) || p.Surname.Contains(search) || p.Patronimic.Contains(search));
            }
            var result = students.OrderBy(p => p.Surname).ToList().ToPagedList(page ?? 1, 3);
            return View(result);
        }
    }
}