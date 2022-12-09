using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Movie_Ticket_Booking_System.Models
{
    public class Cinema
    {
        public int Id { get; set; }
        [Required]
        public string CinemaName { get; set; }
        [Required]
        public string CinemaCity { get; set; }
        public int totalHalls { get; set; }
        public ICollection<Halls> Halls { get; set; }
    }
}