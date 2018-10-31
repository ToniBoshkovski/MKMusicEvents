using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MKMusicEvents.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Date { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        public string Image { get; set; }

        public double  EventRatingGrade { get; set; }

        [Required]
        public int Quantity { get; set; }

        public int Price { get; set; }
    }
    
}