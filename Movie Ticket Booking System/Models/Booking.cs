using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Movie_Ticket_Booking_System.Models
{
    public class Booking
    {
        [Key]
        public int numberOfSeats { get; set; }
        public string BookingNumber { get; set; }
        public DateTime createdOn { get; set; }
        public bool BookingStatus { get; set; }
        public ICollection<ShowSeat> showSeats { get; set; }
    }
}