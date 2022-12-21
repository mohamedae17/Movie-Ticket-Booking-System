using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Movie_Ticket_Booking_System.Models
{
    public class Cinema
    {
        public int Id { get; set; }
        [Required]
        public string CinemaName { get; set; }
        [ForeignKey("City")]
        public int CityId { get; set; }
        public virtual City City { get; set; }
        public int totalHalls { get; set; }
        public ICollection<Halls> Halls { get; set; }
    }
}