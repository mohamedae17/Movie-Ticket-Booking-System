using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Movie_Ticket_Booking_System.Models
{
    public class CinemaHallSeat
    {
        [Key]
        public int Id { get; set; }
        public int seatRow { get; set; }
        public int seatColum { get; set; }
        [ForeignKey("Halls")]
        public int HallId { get; set; }
        public Halls Halls { get; set; }
    }
}