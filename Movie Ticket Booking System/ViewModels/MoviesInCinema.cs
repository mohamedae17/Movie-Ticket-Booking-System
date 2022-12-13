using Movie_Ticket_Booking_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movie_Ticket_Booking_System.ViewModels
{
    public class MoviesInCinema
    {
        public string cinema { get; set; }
        public List<MovieDetails> MovieDetails { get; set; }
    }
}