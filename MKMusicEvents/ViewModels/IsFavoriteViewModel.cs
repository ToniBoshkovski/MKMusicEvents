using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MKMusicEvents.Models;

namespace MKMusicEvents.ViewModels
{
    public class IsFavoriteViewModel
    {
        public List<Event> Event { get; set; }
        public bool IsFavorite { get; set; }
    }
}