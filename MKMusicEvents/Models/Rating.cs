using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MKMusicEvents.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int EventId { get; set; }
        public int RatingGrade { get; set; }
    }
}