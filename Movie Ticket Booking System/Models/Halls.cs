using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Movie_Ticket_Booking_System.Models
{
    public class Halls
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int totalSeats { get; set; }
        [Display(Name = "Cinema Name")]
        [ForeignKey("Cinema")]
        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; }
        public virtual ICollection<Show> shows { get; set; }
    }
}