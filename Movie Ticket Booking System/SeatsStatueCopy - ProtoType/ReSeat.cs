using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movie_Ticket_Booking_System.SeatsStatueCopy___ProtoType
{
    public class ReSeat : Seat
    {
        public override Seat ShallowCopy()
        {
            return (ReSeat)this.MemberwiseClone();
        }
    }
}