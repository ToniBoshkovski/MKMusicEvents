using MKMusicEvents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MKMusicEvents.ViewModels
{
    public class EventRatingViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public double EventRatingGrade { get; set; }
        public int EventRating { get; set; }
        public bool Favorite { get; set; }
    }
}