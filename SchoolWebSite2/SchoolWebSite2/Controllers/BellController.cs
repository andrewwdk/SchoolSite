using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolWebSite2.Models;

namespace SchoolWebSite2.Controllers
{
    public class BellController : Controller
    {
        public ActionResult Index()
        {
            var db = new SchoolWebsiteDatabaseEntities();
            var result = db.Bell.Select(b => b).ToList();
            return View(new BellsModel() { Bells = result });
        }

        [HttpGet]
        public ActionResult Edit()
        {
            var db = new SchoolWebsiteDatabaseEntities();
            var result = db.Bell.Select(b => b).ToList();
            return View(new BellsModel() { Bells =  result});
        }

        [HttpPost]
        public ActionResult Edit(IEnumerable<Bell> bells)
        {
            if (ModelState.IsValid)
            {
                var db = new SchoolWebsiteDatabaseEntities();
                foreach(var bell in bells)
                {
                    var dbBell = db.Bell.FirstOrDefault(b => b.Id == bell.Id);
                    if(dbBell != null)
                    {
                        dbBell.StartTime = bell.StartTime;
                        dbBell.EndTime = bell.EndTime;
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Edit");
            }

            return View(new BellsModel() { Bells = bells.ToList() });
        }
    }
}