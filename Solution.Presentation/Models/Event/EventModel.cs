using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Solution.Presentation.Models.Event
{
    public class EventModel
    {

        public int EventId { get;  set; }
        public DateTime DateEvent { get;  set; }
        public string Name { get;  set; }
        public string Description { get;  set; }
        public string Participants { get;  set; }
        public DateTime HeureF { get;  set; }
        public string Category { get;  set; }
        public string ImageEvent { get; set; }
    }
}