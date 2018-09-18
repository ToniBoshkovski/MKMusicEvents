using Microsoft.AspNet.Identity.EntityFramework;
using MKMusicEvents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MKMusicEvents.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Index(string search)
        {
            ViewBag.IsAdmin = db.Users.Where(x => x.UserName == System.Web.HttpContext.Current.User.Identity.Name).Select(x => x.IsAdmin).FirstOrDefault();
            var model = db.Events.Where(m => m.Date.Contains(search) || m.Name.Contains(search) || search == null );
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(Event model)
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult EventInfo(int id)
        {
            Event model = db.Events.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (db != null)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}