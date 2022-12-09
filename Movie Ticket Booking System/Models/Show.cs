using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Movie_Ticket_Booking_System.Models
{
    public class Show
    {
        [Key]
        public int ID { get; set; }
        public DateTime createOn { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endDate { get; set; }
        [ForeignKey("Halls")]
        public int HallId { get; set; }
        public Halls Halls { get; set; }
        [ForeignKey("movieDetails")]
        public int MovieId { get; set; }
        public MovieDetails movieDetails { get; set; }
        public ICollection<ShowSeat> showSeats { get; set; }

    }
}