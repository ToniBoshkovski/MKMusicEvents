﻿using Microsoft.AspNet.Identity.EntityFramework;
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
            return View("Index", db.Events.ToList());
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
            return View("Index", db.Events.ToList());
        }

        public ActionResult Delete(int id)
        {

            db.Events.Remove(db.Events.Find(id));
            db.SaveChanges();
            return View("Index", db.Events.ToList());
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