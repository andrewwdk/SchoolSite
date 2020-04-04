using Microsoft.AspNet.Identity.EntityFramework;
using SchoolWebSite2.Models;
using System;
using System.Collections.Generic;
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
            return View();
        }

        [HttpGet]
        public ActionResult CreateUser()
        {
            var authDB = new ApplicationDbContext();
            ViewBag.Roles = new SelectList(authDB.Roles, "Name", "Name");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(Person person)
        {
            var authDB = new ApplicationDbContext();
            var db = new SchoolWebsiteDatabaseEntities();
            ViewBag.Roles = new SelectList(authDB.Roles, "Name", "Name");

            if (!db.IsLoginUnique(person.Login))
            {
                ModelState.AddModelError(string.Empty, "Пользователь с таким именем уже существует!");
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
    }
}