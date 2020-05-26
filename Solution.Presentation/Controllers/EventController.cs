using System;
using System.Collections.Generic;
using System.Linq;
using Solution.Data;
using Solution.Domain.Entities;
using Solution.Service;

using System.Web.Http;
using Solution.Presentation.Models.Event;

namespace Solution.Presentation.Controllers
{

    public class EventController : ApiController
    {


        //IEventService MyService = null;
        MyContext ctx = new MyContext();
        List<EventModel> events = new List<EventModel>();
        public EventController()
        {
            //MyService = new EventService();
          
            Index();
            events = Index().ToList();
        }
        public List<EventModel> Index()
        {
            List<Event> mandates = ctx.Events.ToList();
            List<EventModel> mandatesXml = new List<EventModel>();
            foreach (Event i in mandates)
            {
                mandatesXml.Add(new EventModel
                {
                    EventId = i.Id,
                    DateEvent = i.heurD,
                    Name = i.Name,
                    Description = i.Description,
                    Participants = i.Participants,
                    Category = i.Category,
                    HeureF = i.heurF,
                    ImageEvent=i.ImageUrl
                });
            }
            return mandatesXml;
        }
        // GET api/EventWebApi
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Json(events);
        }
        //GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            Event ev = ctx.Events.ToList().Where(s => s.Id == id).FirstOrDefault();
            return Json(ev);
        }
        //// POST: api/EventWebApi
        [Route("api/EventPost")]
        public IHttpActionResult PostNewFeed(EventModel postt)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            using (var ctx = new MyContext())
            {
                ctx.Events.Add(new Event()
                {
                    heurD = DateTime.Today,
                    Name = postt.Name,
                    ImageUrl = postt.ImageEvent,
                    Description = postt.Description,
                    Participants = postt.Participants,
                    Category = postt.Category,
                    heurF = DateTime.Today
                });
                ctx.SaveChanges();
            }
            return Ok();
        }
        //// PUT: api/EventWebApi/5
        public IHttpActionResult Put(int id,EventModel student)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");
            using (var ctx = new MyContext())
            {
                var existingStudent = ctx.Events.Where(s => s.Id == id)
                                                        .FirstOrDefault<Event>();
                if (existingStudent != null)
                {
                    existingStudent.Name = student.Name;
                    existingStudent.Category = student.Category;
                    existingStudent.Description = student.Description;
                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok();
        }
        // DELETE: api/EventWebApi/5
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid user id");
            using (var ctx = new MyContext())
            {
                var student = ctx.Events
                    .Where(s => s.Id == id)
                    .FirstOrDefault();
                ctx.Entry(student).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }
            return Ok();

        }

        //// PUT: api/Participer/5
        [Route("api/event/{id}/participer/{userId}")]

        public IHttpActionResult Participer(int id, int userId)
        {
            
            if (id <= 0)
                return BadRequest("Not a valid user id");
            using (var ctx = new MyContext())
            {
                var theEvent = ctx.Events
                    .Where(s => s.Id == id)
                    .FirstOrDefault<Event>();
                    if(theEvent==null) return BadRequest("there is no event with this id");


                if (theEvent != null)
                {
                    string dotVergule = "";
                    string currentParticipants = theEvent.Participants;
                    if (currentParticipants != "")
                        dotVergule = ",";
                    string newParticipants = currentParticipants + dotVergule+userId.ToString();
                    theEvent.Participants = newParticipants;
                    ctx.SaveChanges();
                }
            }
            return Ok();

        }


        [Route("api/event/{id}/add-rating/{userId}")]
        public IHttpActionResult Put(int id, int userId, Event ev)
        {
            if (id <= 0)
                return BadRequest("Not a valid user id" +id.ToString()+userId.ToString());



            using (var ctx = new MyContext())
            {
                var theEvent = ctx.Events
                    .Where(s => s.Id == id)
                    .FirstOrDefault<Event>();

                if (theEvent != null)
                {
                    if (theEvent.Participants.Contains(userId.ToString()))
                    {
                        string currentRatingUsers = theEvent.ratingUsers;
                        if (currentRatingUsers == null) currentRatingUsers = "";
                        if (currentRatingUsers.Contains(userId.ToString()))
                        {
                            return BadRequest("the current user already have a rating");
                        }
                        else
                        {
                            string dotVergule = "";
                            if (currentRatingUsers != "")
                                dotVergule = ",";
                            string newRatingUsers = currentRatingUsers + dotVergule + userId.ToString();
                            theEvent.ratingUsers = newRatingUsers;
                            float currentRating = theEvent.Rating;
                            int currentRatingQuantity = theEvent.RatingQuantity;

                            float finalRating = (currentRating + ev.Rating) / (currentRatingQuantity + 1);
                            theEvent.Rating = finalRating;
                            theEvent.RatingQuantity = currentRatingQuantity + 1;
                            ctx.SaveChanges();
                        }
                    }
                    else
                    {
                        return BadRequest("this user didnt already participate");
                    }

                }
                else
                {
                    return BadRequest("the event does not exist");
                }
            }
            return Ok();

        }

    }
}