using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MKMusicEvents.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int EventId { get; set; }
        public int Quantity { get; set; }
        public string Time { get; set; }
    }
}