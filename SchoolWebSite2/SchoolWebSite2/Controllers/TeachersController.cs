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

        public ActionResult IndexForAdmin()
        {
            var db = new SchoolWebsiteDatabaseEntities();
            var teachers = db.Teacher.OrderBy(t => t.Person.Surname).ToList();
            return View(teachers);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new SchoolWebsiteDatabaseEntities();
            var teacherToEdit = db.Teacher.First(t => t.PersonId == id);
            var subjects = db.Subject.Select(s => s).ToList();
            RemoveUnneccessarySubject(subjects, teacherToEdit);
            ViewBag.Subjects = new SelectList(subjects, "Id", "FullName");
            var teacherSubjects = teacherToEdit.Subject.OrderBy(s => s.FullName).ToList();
            return View(new TeacherWithSubjectsModel() { Subjects = teacherSubjects, Teacher = teacherToEdit});
        }

        [HttpPost]
        public ActionResult Edit(TeacherWithSubjectsModel model)
        {
            var db = new SchoolWebsiteDatabaseEntities();

            if (ModelState.IsValid)
            {
                var teacherToEdit = db.Teacher.First(t => t.PersonId == model.Teacher.PersonId);
                if (teacherToEdit != null)
                {
                    teacherToEdit.TeacherLoad = model.Teacher.TeacherLoad;
                }
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = model.Teacher.PersonId });
            }
            var teacher = db.Teacher.First(t => t.PersonId == model.Teacher.PersonId);
            var subjects = db.Subject.Select(s => s).ToList();
            RemoveUnneccessarySubject(subjects, teacher);
            ViewBag.Subjects = new SelectList(subjects, "Id", "FullName");
            model.Teacher = teacher;
            model.Subjects = teacher.Subject.OrderBy(s => s.FullName).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddSubject(TeacherWithSubjectsModel model)
        {
            var db = new SchoolWebsiteDatabaseEntities();

            if (model.NewSubjectId == null)
            {
                ModelState.AddModelError(string.Empty, "Выберите предмет из списка!");
            }

            ModelState.Remove("Teacher.TeacherLoad");
            if (ModelState.IsValid)
            {
                var teacher = db.Teacher.First(t => t.PersonId == model.Teacher.PersonId);
                var subject = db.Subject.First(s => s.Id == model.NewSubjectId);
                if (teacher != null)
                {
                    teacher.Subject.Add(subject);
                }
                db.SaveChanges();
            }

            return RedirectToAction("Edit", new { id = model.Teacher.PersonId });
        }

        [HttpPost]
        public ActionResult RemoveSubject(int subjectId, int teacherId)
        {
            var db = new SchoolWebsiteDatabaseEntities();
            var teacher = db.Teacher.First(t => t.PersonId == teacherId);
            var subject = db.Subject.First(s => s.Id == subjectId);
            if (teacher != null)
            {
                teacher.Subject.Remove(subject);
            }
            db.SaveChanges();
            return RedirectToAction("Edit", new { id = teacherId });
        }

        private void RemoveUnneccessarySubject(List<Subject> list, Teacher teacher)
        {
            foreach(var subject in teacher.Subject)
            {
                list.Remove(subject);
            }
        }
    }
}