using MKMusicEvents.ApiClasses;
using MKMusicEvents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MKMusicEvents.Controllers.Api
{
    public class EventsController : ApiController
    {
        private ApplicationDbContext db;

        public EventsController()
        {
            db = new ApplicationDbContext();
        }

        // GET /Api/Events
        [HttpGet]
        public IHttpActionResult ListAllEvents()
        {
            try
            {
                ListEvents response = new ListEvents();
                response.events = db.Events.ToList();

                if (response.events == null || (response.events != null && response.events.Count() == 0))
                {
                    return Content(HttpStatusCode.NotFound, "The events are not Found");
                }

                return Content(HttpStatusCode.OK, response);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "Iternal server error");
                
            }
        }

        // GET /Api/Events/1
        [HttpGet]
        public IHttpActionResult Event(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return Content(HttpStatusCode.BadRequest, "Not a valid id");
                }

                ListEvents response = new ListEvents();
                response.events = db.Events.Where(e => e.Id == id).ToList();

                if (response.events == null || (response.events != null && response.events.Count() == 0))
                {
                    return Content(HttpStatusCode.NotFound, "The event is not Found");
                }

                return Content(HttpStatusCode.OK, response);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "Iternal server error");
            }
        }

        // POST /Api/Events
        [HttpPost]
        public IHttpActionResult CreateEvent(Event eventInDb)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Content(HttpStatusCode.BadRequest, "Model isn't valid");
                }

                db.Events.Add(eventInDb);
                db.SaveChanges();

                return Content(HttpStatusCode.OK, eventInDb);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "Iternal server error");
            }
        }

        // DELETE /Api/Events/1
        [HttpDelete]
        public IHttpActionResult DeleteEvent(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return Content(HttpStatusCode.BadRequest, "Not a valid id");
                }

                var eventInDb = db.Events.SingleOrDefault(e => e.Id == id);

                if (eventInDb == null)
                {
                    return Content(HttpStatusCode.NotFound, "The event is not Found");
                }

                db.Events.Remove(eventInDb);
                db.SaveChanges();

                return Content(HttpStatusCode.OK, eventInDb);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "Iternal server error");
            }
        }

        [HttpGet]
        public IHttpActionResult EventsByYear(DateTime beginDate, DateTime endDate)
        {
            try
            {
                if (beginDate == null || endDate == null)
                {
                    return Content(HttpStatusCode.BadRequest, "Not a valid date");
                }

                var events = db.Events.Where(e => e.Date >= beginDate && e.Date <= endDate).ToList();

                if (events == null)
                {
                    return Content(HttpStatusCode.NotFound, "The events are not Found");
                }

                return Content(HttpStatusCode.OK, events);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "Iternal server error");
            }
        }

    }
}
