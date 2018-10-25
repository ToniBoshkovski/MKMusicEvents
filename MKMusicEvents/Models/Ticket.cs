using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MKMusicEvents.Models
{
    public class Ticket
    {
        
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Card Name")]
        public string CardName { get; set; }

        [Required]
        [Display(Name = "Card Number")]
        public int CardNumber { get; set; }

        [Required]
        [Display(Name = "Expiry Date")]
        public int ExpiryDateMonth { get; set; }

        [Required]
        public int ExpiryDateYear { get; set; }

        [Required]
        [Display(Name = "Security Code")]
        public int SecurityCode { get; set; }

        [Required]
        [Display(Name = "Name on Card")]
        public string CardholderName { get; set; }

        public bool SaveCard { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}