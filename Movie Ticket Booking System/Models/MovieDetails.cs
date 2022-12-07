using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Movie_Ticket_Booking_System.Models
{
    public class MovieDetails
    {
        public int Id { get; set; }
        public string MovieName { get; set; }
        public string MovieDescription { get; set; }
        public string Language { get; set; }
        public DateTime DateAndTime { get; set; }
        public string MoviePicture { get; set; }
        [Display(Name ="Cinema Name")]
        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; }
    }
}