using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MKMusicEvents.Models;

namespace MKMusicEvents.ViewModels
{
    public class MyTicketsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public int Quantity { get; set; }
        public string Price { get; set; }
    }
}