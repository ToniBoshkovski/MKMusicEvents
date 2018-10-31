using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MKMusicEvents.Models;
using MKMusicEvents.ViewModels;
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
            ViewBag.IsAdmin = 2;
            var userId = User.Identity.GetUserId();
            if(User.Identity.Name != "")
            {
                ViewBag.IsAdmin = db.Users.Where(x => x.UserName == System.Web.HttpContext.Current.User.Identity.Name).Select(x => x.IsAdmin).FirstOrDefault();
            }

            List<EventRatingViewModel> ListViewModel = new List<EventRatingViewModel>();
            var events = db.Events.Where(/*m => m.Date.Contains(search) ||*/ m => m.Name.Contains(search) || search == null).ToList();
            var ratings = db.Ratings.Where(r => r.UserId == userId).ToList();

            foreach (var e in events)
            {
                EventRatingViewModel viewmodel = new EventRatingViewModel();
                if (!db.Favorites.Any(f => f.UserId == userId && f.EventId == e.Id))
                {
                    viewmodel.Favorite = false;
                }
                else
                {
                    viewmodel.Favorite = true;
                }

                if (!db.Ratings.Any(r => r.UserId == userId && r.EventId == e.Id))
                {
                    viewmodel.Id = e.Id;
                    viewmodel.Name = e.Name;
                    viewmodel.Date = e.Date;
                    viewmodel.Description = e.Description;
                    viewmodel.Image = e.Image;
                    viewmodel.EventRatingGrade = e.EventRatingGrade;
                    viewmodel.EventRating = 0;
                    ListViewModel.Add(viewmodel);
                }
                else
                {
                    viewmodel.Id = e.Id;
                    viewmodel.Name = e.Name;
                    viewmodel.Date = e.Date;
                    viewmodel.Description = e.Description;
                    viewmodel.Image = e.Image;
                    viewmodel.EventRatingGrade = e.EventRatingGrade;
                    viewmodel.EventRating = ratings.Where(r => r.EventId == e.Id).Select(r => r.RatingGrade).FirstOrDefault();
                    ListViewModel.Add(viewmodel);
                }
            }
            return View(ListViewModel);
        }

        public ActionResult _Add()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult _Add(Event model)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(model);
                db.SaveChanges();
            }
            return RedirectToAction("Index", db.Events.ToList());
        }

        public ActionResult _Edit(int id)
        {
            return PartialView(db.Events.Find(id));
        }

        [HttpPost]
        public ActionResult _Edit(Event model, int id)
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
                    model.UserId = User.Identity.GetUserId();
                    model.EventId = id;
                    if (!db.Favorites.Any(m => m.UserId == model.UserId && m.EventId == id))
                    {
                        db.Favorites.Add(model);
                        db.SaveChanges();
                        result.ErrorCode = 0;
                        result.Message = "Successufully added event to favorites.";
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
                    result.ErrorCode = 200;
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
            var userId = User.Identity.GetUserId();
            var model = from e in db.Events.ToList()
                        join f in db.Favorites.Where(f => f.UserId == userId).ToList() on e.Id equals f.EventId
                        select e;
            return View(model);
        }

        [HttpPost]
        public JsonResult AddToRating(int id, int rating)
        {
            try
            {
                JsonRatingResponse jsonResponse = new JsonRatingResponse();
                Rating model = new Rating();
                var userId = User.Identity.GetUserId();

                if (User.Identity.Name != "")
                {
                    var exist = db.Ratings.Where(r => r.UserId == userId && r.EventId == id).FirstOrDefault();
                    if (exist != null)
                    {
                        exist.RatingGrade = rating;
                        db.SaveChanges();
                        jsonResponse.ResponseCode = 100;
                    }
                    else
                    {
                        model.UserId = userId;
                        model.EventId = id;
                        model.RatingGrade = rating;
                        db.Ratings.Add(model);
                        db.SaveChanges();
                        jsonResponse.ResponseCode = 200;
                    }

                    var ratingsByUser = db.Ratings.Where(r => r.EventId == id);
                    var sumRating = 0;
                    var counter = 0;
                    foreach (var item in ratingsByUser)
                    {
                        sumRating += item.RatingGrade;
                        counter++;
                    }
                    jsonResponse.ResponseRatingGrade = System.Math.Round((double)sumRating / counter, 2);
                    var thisEvent = db.Events.Where(e => e.Id == id).FirstOrDefault();
                    thisEvent.EventRatingGrade = System.Math.Round(jsonResponse.ResponseRatingGrade, 2);
                    db.SaveChanges();
                }
                else
                {
                    jsonResponse.ResponseCode = 400;
                    jsonResponse.ResponseMessage = "You must log in first!";
                    jsonResponse.ResponseRatingGrade = 0;
                }

                return Json(jsonResponse, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(ex.Message);
            }
        }

        public class JsonRatingResponse
        {
            public int ResponseCode { get; set; }
            public string ResponseMessage { get; set; }
            public double ResponseRatingGrade { get; set; }
        }

        public ActionResult _Buy()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult _Buy(TicketInformation ticketInformation, int id)
        {
            if (ModelState.IsValid)
            {
                var ticketsAvailable = db.Events.Where(e => e.Id == id).Select(e => e.Quantity).FirstOrDefault();
                if (ticketsAvailable > 0 && ticketsAvailable >= ticketInformation.Quantity)
                {
                    Ticket tickets = new Ticket();
                    tickets.UserId = User.Identity.GetUserId();
                    tickets.EventId = id;
                    tickets.Quantity = ticketInformation.Quantity;
                    tickets.Time = DateTime.Now.ToString();
                    var model = db.Events.Find(id);
                    model.Quantity = ticketsAvailable - ticketInformation.Quantity;
                    db.Tickets.Add(tickets);
                    db.SaveChanges();
                }
                else
                {
                    Console.Write("Not enough tickets available");
                }
                db.TicketsInformation.Add(ticketInformation);
                db.SaveChanges();
            }

            return RedirectToAction("Index", db.TicketsInformation.ToList());
        }

        public ActionResult MyTickets()
        {
            var userId = User.Identity.GetUserId();
            var model = from e in db.Events.ToList()
                        join t in db.Tickets.Where(t => t.UserId == userId).ToList() on e.Id equals t.EventId
                        select new { e.Id, e.Name, e.Price, t.Quantity, t.Time, };

            List<MyTicketsViewModel> MyTicketsViewModel = new List<MyTicketsViewModel>();
            foreach (var item in model)
            {
                MyTicketsViewModel viewModel = new MyTicketsViewModel();
                viewModel.Id = item.Id;
                viewModel.Name = item.Name;
                var price = item.Price * item.Quantity;
                viewModel.Price = price + " ден.";
                viewModel.Quantity = item.Quantity;
                viewModel.Date = item.Time;
                MyTicketsViewModel.Add(viewModel);
            }
            return View(MyTicketsViewModel);
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