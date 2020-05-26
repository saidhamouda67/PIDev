using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Domain.Entities
{
   public class Event
    {
        public int Id { get; set; }
        public string ratingUsers { get; set; }
        public string Name { get; set; }

        public string  Description { get; set; }

        public string Category { get; set; }
        public string Participants { get; set; }

        public DateTime heurD { get; set; }
        public DateTime heurF { get; set; }

        public string ImageUrl { get; set; }

        public float Rating { get; set; }

        public int RatingQuantity { get; set; }

    }
}
