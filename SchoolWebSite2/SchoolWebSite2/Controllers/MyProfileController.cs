using Microsoft.AspNet.Identity.EntityFramework;
using SchoolWebSite2.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SchoolWebSite2.Controllers
{
    public class MyProfileController : Controller
    {
        public ActionResult Index()
        {
            var db = new SchoolWebsiteDatabaseEntities();
            var user = db.User.First(u => u.Login == User.Identity.Name);
            var person = user.Person;
            return View(person);
        }

        [HttpGet]
        public ActionResult CreateUser()
        {
            var authDB = new ApplicationDbContext();
            ViewBag.Roles = new SelectList(authDB.Roles, "Name", "Name");
            return View(new Person());
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(Person person, HttpPostedFileBase upload)
        {
            var authDB = new ApplicationDbContext();
            var db = new SchoolWebsiteDatabaseEntities();
            ViewBag.Roles = new SelectList(authDB.Roles, "Name", "Name");

            if (!db.IsLoginUnique(person.Login))
            {
                ModelState.AddModelError(string.Empty, "Пользователь с таким именем уже существует!");
            }

            if (upload != null && upload.ContentLength > 0)
            {
                person.Photo = @"~/Images/" + upload.FileName;
            }

            if (ModelState.IsValid)
            {
                var store = new UserStore<ApplicationUser>(authDB);
                var manager = new ApplicationUserManager(store);
                var authUser = new ApplicationUser() { UserName = person.Login, Email = person.Email };
                await manager.CreateAsync(authUser, person.Password);
                var role = authDB.Roles.First(r => r.Name == person.Role);
                var savedAuthUser = authDB.Users.First(u => u.UserName == person.Login);
                authUser.Roles.Add(new IdentityUserRole() { RoleId = role.Id, UserId = savedAuthUser.Id});
                authDB.SaveChanges();

                var savedPerson = db.Person.Add(person);
                db.User.Add(new User() { Login = person.Login, PersonId = savedPerson.Id, RegistrationDate = DateTime.Now });
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(person);
        }

        [HttpGet]
        public ActionResult EditProfile()
        {
            var db = new SchoolWebsiteDatabaseEntities();
            var user = db.User.First(u => u.Login == User.Identity.Name);
            var person = user.Person;
            return View(person);
        }

        [HttpPost]
        public ActionResult EditProfile( Person person, HttpPostedFileBase upload)
        {
            var db = new SchoolWebsiteDatabaseEntities();

            if (upload != null && upload.ContentLength > 0)
            {
                person.Photo = @"~/Images/" + upload.FileName;
            }

            ModelState.Remove("Role");
            ModelState.Remove("Password");
            ModelState.Remove("Login");
            if (ModelState.IsValid)
            {
                var dbPerson = db.Person.First(p => p.Id == person.Id);
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

                return RedirectToAction("Index");
            }

            return View(person);
        }
    }
}