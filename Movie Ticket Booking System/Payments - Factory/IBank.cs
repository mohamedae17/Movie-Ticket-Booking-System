using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movie_Ticket_Booking_System.Payments
{
    public interface IBank
    {
        void withDraw();
    }
}