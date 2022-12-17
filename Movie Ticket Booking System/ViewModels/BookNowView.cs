using Movie_Ticket_Booking_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movie_Ticket_Booking_System.ViewModels
{
    public class BookNowView
    {
        public Show show { get; set; }
        public int number { get; set; }
        public List<ShowSeat> showSeatofMovie { get; set; }
    }
}