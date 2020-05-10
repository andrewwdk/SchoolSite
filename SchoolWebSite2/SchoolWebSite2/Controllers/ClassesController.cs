using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolWebSite2.Models;

namespace SchoolWebSite2.Controllers
{
    public class ClassesController : Controller
    {
        public ActionResult Index()
        {
            var db = new SchoolWebsiteDatabaseEntities();
            ViewBag.Teachers = new SelectList(db.Teacher, "PersonId", "FullName");
            var list = db.Class.Select(c => c).OrderBy(c => c.Number).ThenBy(c => c.Code).ToList();
            return View(new CreateAndListClassModel() { Classes = list, NewClass = new Class() });
        }

        [HttpPost]
        public ActionResult Create(CreateAndListClassModel model)
        {
            var db = new SchoolWebsiteDatabaseEntities();

            model.NewClass.Code = (model.NewClass.Code != null) ? model.NewClass.Code.Trim() : null;

            if (!db.IsClassUnique(model.NewClass))
            {
                ModelState.AddModelError(string.Empty, "Класс с таким номером и кодом уже существует!");
            }

            if (ModelState.IsValid)
            {
                db.Class.Add(model.NewClass);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var list = db.Class.Select(c => c).OrderBy(c => c.Number).ThenBy(c => c.Code).ToList();
            ViewBag.Teachers = new SelectList(db.Teacher, "PersonId", "FullName");
            return View("Index", new CreateAndListClassModel() { Classes = list, NewClass = model.NewClass });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var db = new SchoolWebsiteDatabaseEntities();
            var classToRemove = db.Class.FirstOrDefault(s => s.Id == id);
            db.Class.Remove(classToRemove);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new SchoolWebsiteDatabaseEntities();
            var classToEdit = db.Class.First(c => c.Id == id);
            ViewBag.Teachers = new SelectList(db.Teacher, "PersonId", "FullName", classToEdit.ClassTeacher);
            ViewBag.Students = new SelectList(db.Student.Where(s => s.Class == null), "PersonId", "FullName");
            var studentsInClass = db.Student.Where(s => s.Class.HasValue && s.Class == id).ToList();
            return View(new ClassWithStudentsModel() { Class = classToEdit, Students = studentsInClass});
        }

        [HttpPost]
        public ActionResult Edit(ClassWithStudentsModel model)
        {
            var db = new SchoolWebsiteDatabaseEntities();

            model.Class.Code = (model.Class.Code != null) ? model.Class.Code.Trim() : null;

            if (!db.IsClassUnique(model.Class))
            {
                ModelState.AddModelError(string.Empty, "Класс с таким номером и кодом уже существует!");
            }

            if (ModelState.IsValid)
            {
                var classToEdit = db.Class.First(c => c.Id == model.Class.Id);
                if(classToEdit != null)
                {
                    classToEdit.Code = model.Class.Code;
                    classToEdit.Info = model.Class.Info;
                    classToEdit.ClassTeacher = model.Class.ClassTeacher;
                }
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = model.Class.Id });
            }
            var cl = db.Class.First(c => c.Id == model.Class.Id);
            ViewBag.Teachers = new SelectList(db.Teacher, "PersonId", "FullName", cl.ClassTeacher);
            ViewBag.Students = new SelectList(db.Student.Where(s => s.Class == null), "PersonId", "FullName");
            if(model.Students == null)
            {
                model.Students = new List<Student>();
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult AddStudent(ClassWithStudentsModel model)
        {
            var db = new SchoolWebsiteDatabaseEntities();

            if (model.NewStudentId == null)
            {
                ModelState.AddModelError(string.Empty, "Выберите ученика из списка!");
            }

            ModelState.Remove("Class.Code");
            ModelState.Remove("Class.Number");
            if (ModelState.IsValid)
            {
                var studentToEdit = db.Student.First(s => s.PersonId == model.NewStudentId);
                if (studentToEdit != null)
                {
                    studentToEdit.Class = model.Class.Id;
                }
                db.SaveChanges();
            }

            return RedirectToAction("Edit", new { id = model.Class.Id });
        }

        [HttpPost]
        public ActionResult RemoveStudent(int studentId, int classId)
        {
            var db = new SchoolWebsiteDatabaseEntities();
            var studentToRemove = db.Student.First(s => s.PersonId == studentId);
            if(studentToRemove != null)
            {
                studentToRemove.Class = null;
            }
            db.SaveChanges();
            return RedirectToAction("Edit", new { id = classId });
        }

        [HttpPost]
        public ActionResult IncrementYear()
        {
            var db = new SchoolWebsiteDatabaseEntities();
            var classList = db.Class.OrderByDescending(c => c.Number).ToList();
            for(int i = 0; i < classList.Count(); i++)
            {
                if(classList[i].Number == 11)
                {
                    db.Class.Remove(classList[i]);
                }
                else
                {
                    classList[i].Number++;
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var db = new SchoolWebsiteDatabaseEntities();
            var students = db.Student.Where(s => s.Class.HasValue && s.Class == id).ToList();
            var classToShow = db.Class.First(c => c.Id == id);
            if (classToShow.ClassTeacher == null)
            {
                classToShow.ClassTeacherName = "Не выбран";
            }
            else
            {
                var teacher = db.Teacher.First(t => t.PersonId == classToShow.ClassTeacher);
                classToShow.ClassTeacherName = teacher.FullName;
            } 

            return View(new ClassWithStudentsModel() { Class = classToShow, Students = students });
        }

        [HttpGet]
        public ActionResult List()
        {
            var db = new SchoolWebsiteDatabaseEntities();
            var classes = db.Class.Select(c => c).OrderBy(c => c.Number).ThenBy(c => c.Code).ToList();
            return View(classes);
        }
    }
}