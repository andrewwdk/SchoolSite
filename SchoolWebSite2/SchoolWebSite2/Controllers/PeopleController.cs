using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolWebSite2.Models;

namespace SchoolWebSite2.Controllers
{
    public class PeopleController : Controller
    {
        public ActionResult Index(int id)
        {
            var db = new SchoolWebsiteDatabaseEntities();
            var person = db.Person.FirstOrDefault(p => p.Id == id);
            if(person == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(person);
        }

        public ActionResult Edit(int id)
        {
            var db = new SchoolWebsiteDatabaseEntities();
            var person = db.Person.FirstOrDefault(p => p.Id == id);
            if (person == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(person);
        }

        [HttpPost]
        public ActionResult Edit(Person person, HttpPostedFileBase upload)
        {
            var db = new SchoolWebsiteDatabaseEntities();

            if (upload != null && upload.ContentLength > 0)
            {
                if (string.IsNullOrEmpty(person.Photo))
                {
                    person.Photo = @"~/Images/" + upload.FileName;
                }
            }

            ModelState.Remove("Role");
            ModelState.Remove("Password");
            ModelState.Remove("Login");
            if (ModelState.IsValid)
            {
                var dbPerson = db.Person.FirstOrDefault(p => p.Id == person.Id);
                if (dbPerson != null)
                {
                    dbPerson.Address = person.Address;
                    dbPerson.BirthDate = person.BirthDate;
                    dbPerson.Email = person.Email;
                    dbPerson.HomeNumber = person.HomeNumber;
                    dbPerson.Info = person.Info;
                    dbPerson.MobileNumber = person.MobileNumber;
                    dbPerson.Name = person.Name;
                    dbPerson.Patronimic = person.Patronimic;
                    dbPerson.Photo = person.Photo;
                    dbPerson.Surname = person.Surname;
                    dbPerson.Login = "111111111";
                    dbPerson.Password = "111111111";
                    db.SaveChanges();
                }

                if(dbPerson.Role == "Учитель")
                {
                    return RedirectToAction("Index", "Teachers");
                }
                else
                {
                    return RedirectToAction("Index", "Students");
                }
                
            }

            return View(person);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            bool isTeacher = false;
            var authDB = new ApplicationDbContext();
            var db = new SchoolWebsiteDatabaseEntities();
            var dbPerson = db.Person.FirstOrDefault(p => p.Id == id);
            if (dbPerson != null)
            {
                isTeacher = dbPerson.Role == "Учитель";
                var userName = db.User.FirstOrDefault(u => u.PersonId == id).Login;
                db.Person.Remove(dbPerson);
                db.SaveChanges();

                var authUser = authDB.Users.FirstOrDefault(u => u.UserName == userName);
                authDB.Users.Remove(authUser);
                authDB.SaveChanges();
            }
                
            if (isTeacher)
            {
                return RedirectToAction("Index", "Teachers");
            }
            else
            {
                return RedirectToAction("Index", "Students");
            }
        }
    }
}