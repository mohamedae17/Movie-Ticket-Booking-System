using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Movie_Ticket_Booking_System.Models
{
    public class FrontEndOfficer
    {
        [Key]
        public int Id { get; set; }
        public int seatRow { get; set; }
        public int seatColum { get; set; }
        public bool isReserved { get; set; }
        public string BookingNumber { get; set; }
        public DateTime? createdOn { get; set; }
        public int ShowId { get; set; }
        public Show Show { get; set; }
    }
}