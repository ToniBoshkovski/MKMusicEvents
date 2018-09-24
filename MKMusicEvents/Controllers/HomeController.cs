using Microsoft.AspNet.Identity;
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
            if(User.Identity.Name != null)
            {
                ViewBag.IsAdmin = db.Users.Where(x => x.UserName == System.Web.HttpContext.Current.User.Identity.Name).Select(x => x.IsAdmin).FirstOrDefault();
            }
            var model = db.Events.Where(m => m.Date.Contains(search) || m.Name.Contains(search) || search == null);
            return View(model);
        }

        public ActionResult Add()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Add(Event model)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(model);
                db.SaveChanges();
            }
            return RedirectToAction("Index", db.Events.ToList());
        }

        public ActionResult Edit(int id)
        {
            return PartialView(db.Events.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(Event model, int id)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Delete(int id)
        {
            db.Events.Remove(db.Events.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index", db.Events.ToList());
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

        [HttpPost]
        public JsonResult AddToFavorites(int id)
        {
            try
            {
                JsonResponse result = new JsonResponse();
                Favorites model = new Favorites();
                if (User.Identity.Name != "")
                {
                    result.ErrorCode = 0;
                    result.Message = "Successufully added event to favorites.";
                    model.UserId = User.Identity.GetUserId();
                    model.EventId = id;
                    if (!db.Favorites.Any(m => m.UserId == model.UserId && m.EventId == id))
                    {
                        db.Favorites.Add(model);
                        db.SaveChanges();
                    }
                    else
                    {
                        int favoriteId = db.Favorites.Where(m => m.UserId == model.UserId && m.EventId == id).Select(m => m.Id).FirstOrDefault();
                        db.Favorites.Remove(db.Favorites.Find(favoriteId));
                        db.SaveChanges();
                        result.ErrorCode = 100;
                    }
                }
                else
                {
                    result.ErrorCode = 100;
                    result.Message = "You must log in first";
                }

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        public class JsonResponse
        {
            public int ErrorCode { get; set; }
            public string Message { get; set; }
        }

        public ActionResult Favorites()
        {
            return View();
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