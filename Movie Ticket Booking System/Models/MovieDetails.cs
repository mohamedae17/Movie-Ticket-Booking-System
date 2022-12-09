using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Movie_Ticket_Booking_System.Models
{
    public class MovieDetails
    {
        public int Id { get; set; }
        [Required]
        public string MovieName { get; set; }
        public string MovieDescription { get; set; }
        public int durationMinutes { get; set; }
        public string Language { get; set; }
        public DateTime DateAndTime { get; set; }
        public string Country { get; set; }
        public string genre { get; set; }
        public string MoviePicture { get; set; }
        public ICollection<Show> shows { get; set; }
    }
}