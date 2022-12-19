using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movie_Ticket_Booking_System.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Cinema> cinemas { get; set; }
    }
}