using Movie_Ticket_Booking_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movie_Ticket_Booking_System.ViewModels
{
    public class Movieslist
    {
        public string CinemaName { get; set; }
        public List<MovieDetails> movieDetails { get; set; }
    }
}