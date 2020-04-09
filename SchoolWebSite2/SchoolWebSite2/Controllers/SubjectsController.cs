using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolWebSite2.Models;

namespace SchoolWebSite2.Controllers
{
    public class SubjectsController : Controller
    {
        public ActionResult Index()
        {
            var db = new SchoolWebsiteDatabaseEntities();
            var result = db.Subject.Select(s => s).OrderBy(s => s.FullName).ToList();
            return View(new CreateAndListSubjectModel() { NewSubject = new Subject(), Subjects = result});
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var db = new SchoolWebsiteDatabaseEntities();
            var subject = db.Subject.FirstOrDefault(s => s.Id == id);
            db.Subject.Remove(subject);
            db.SaveChanges();
            var result = db.Subject.Select(s => s).OrderBy(s => s.FullName).ToList();
            return View("Index", new CreateAndListSubjectModel() { NewSubject = new Subject(), Subjects = result });
        }

        [HttpPost]
        public ActionResult Create(CreateAndListSubjectModel model)
        {
            var db = new SchoolWebsiteDatabaseEntities();
            if (ModelState.IsValid)
            {
                db.Subject.Add(model.NewSubject);
                db.SaveChanges();
                var result = db.Subject.Select(s => s).OrderBy(s => s.FullName).ToList();
                return View("Index", new CreateAndListSubjectModel() { NewSubject = new Subject(), Subjects = result });
            }

            var list = db.Subject.Select(s => s).OrderBy(s => s.FullName).ToList();
            return View("Index", new CreateAndListSubjectModel() { NewSubject = model.NewSubject, Subjects = list });
        }
    }

}