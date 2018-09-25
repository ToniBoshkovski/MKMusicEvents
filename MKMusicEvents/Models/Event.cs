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
        [RegularExpression("(\\d{1,2}).(\\d{1,2}).(\\d{4})", ErrorMessage = "Invalid format")]
        public string Date { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }
    }
}