using Movie_Ticket_Booking_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movie_Ticket_Booking_System.ViewModels
{
    public class CinemasInCities
    {
        public string city { get; set; }
        public Cinema cinema { get; set; }
        public List<MovieDetails> movieDetails { get; set; }
    }
}