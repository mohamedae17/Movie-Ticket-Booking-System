using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Movie_Ticket_Booking_System.Models
{
    public class ShowSeat : CinemaHallSeat
    {
        [Key]
        public int seatNumber { get; set; }
        public bool isReserved { get; set; }
        public double price { get; set; }
        [ForeignKey("booking")]
        public int BookId { get; set; }
        public Booking booking { get; set; }
    }
}