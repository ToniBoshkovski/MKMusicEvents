using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MKMusicEvents.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}