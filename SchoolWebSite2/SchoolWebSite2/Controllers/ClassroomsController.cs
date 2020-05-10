using SchoolWebSite2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolWebSite2.Controllers
{
    public class ClassroomsController : Controller
    {
        public ActionResult Index()
        {
            var db = new SchoolWebsiteDatabaseEntities();
            var list = db.Classroom.Select(c => c).OrderBy(c => c.Number).ToList();
            return View(new CreateAndListClassroomModel() { Classrooms = list, NewClassroom = new Classroom() });
        }

        [HttpPost]
        public ActionResult Create(CreateAndListClassroomModel model)
        {
            var db = new SchoolWebsiteDatabaseEntities();

            if (!db.IsClassroomUnique(model.NewClassroom))
            {
                ModelState.AddModelError(string.Empty, "Кабинет с таким номером уже существует!");
            }

            if (ModelState.IsValid)
            {
                db.Classroom.Add(model.NewClassroom);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var list = db.Classroom.Select(c => c).OrderBy(c => c.Number).ToList();
            return View("Index", new CreateAndListClassroomModel() { Classrooms = list, NewClassroom = model.NewClassroom });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var db = new SchoolWebsiteDatabaseEntities();
            var classroom = db.Classroom.FirstOrDefault(s => s.Id == id);
            db.Classroom.Remove(classroom);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var db = new SchoolWebsiteDatabaseEntities();
            var classroom = db.Classroom.First(c => c.Id == id);
            return View(classroom);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new SchoolWebsiteDatabaseEntities();
            var classroom = db.Classroom.First(c => c.Id == id);
            return View(classroom);
        }

        [HttpPost]
        public ActionResult Edit(Classroom model)
        {
            var db = new SchoolWebsiteDatabaseEntities();
            if (ModelState.IsValid)
            {
                var classroom = db.Classroom.First(c => c.Id == model.Id);
                if (classroom != null)
                {
                    classroom.SitsCount = model.SitsCount;
                    classroom.HasComputers = model.HasComputers;
                    classroom.HasDigitalEquipment = model.HasDigitalEquipment;
                }
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = model.Id });
            }
            return View(model);
        }
    }
}